using gk3.Properties;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml.Linq;

namespace gk3
{
    public partial class gk3 : Form
    {
        private Bitmap bitmap;
        private Bitmap imageBitmap;
        private Color[,] imageColors = null;
        private Quantizer quantizer;
        private int colorCount;
        private int initColorCount = 500;
        private Bitmap play;
        private Bitmap pause;
        bool animation = false;
        int animationCount = 10;
        private System.Windows.Forms.Timer animationTimer;
        bool first = true;
        int maxLevel = 8;
        private Bitmap image;
        private Bitmap gen1Image = null;
        private Bitmap gen2Image = null;
        bool drawEdges = false;
        public gk3()
        {
            InitializeComponent();

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 1000;
            animationTimer.Tick += Timer_Tick;

            quantizer = new Quantizer();

            colorCount = initColorCount;
            color.Text = "Color count: " + colorCount;

            pictureBox.Width = (ClientSize.Width - groupBox.Width) / 2;
            pictureBox.Height = pictureBox.Width;
            newImageBox.Width = (ClientSize.Width - groupBox.Width) / 2;
            newImageBox.Height = newImageBox.Width;

            bitmap = new Bitmap(pictureBox.Width / 2, pictureBox.Height);
            pictureBox.Image = bitmap;

            imageBitmap = new Bitmap(newImageBox.Width / 2, newImageBox.Height);
            newImageBox.Image = imageBitmap;

            Bitmap miniPlay = Resources.play;
            play = new Bitmap(animationButton.Width, animationButton.Height);
            using (Graphics g = Graphics.FromImage(play))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(miniPlay, 0, 0, play.Width, play.Height);
            }

            Bitmap miniPause = Resources.pause;
            pause = new Bitmap(animationButton.Width, animationButton.Height);
            using (Graphics g = Graphics.FromImage(pause))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(miniPause, 0, 0, pause.Width, pause.Height);
            }

            animationButton.Image = play;
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    colorCount = initColorCount;
                    color.Text = "Color count: " + colorCount.ToString();
                    try
                    {
                        image = new Bitmap(openFileDialog.FileName);
                        Bitmap resizedImage = new Bitmap(bitmap.Width, bitmap.Height);

                        using (Graphics g = Graphics.FromImage(resizedImage))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                        }
                        loadColors(resizedImage);
                        image = resizedImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("B³¹d podczas wczytywania obrazu: " + ex.Message);
                    }
                    drawColors(colorCount);
                }
            }
        }
        private void loadColors(Bitmap image)
        {
            quantizer = new Quantizer();
            first = true;
            maxLevel = 8;
            imageColors = new Color[image.Width, image.Height];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = image.GetPixel(i, j);
                    quantizer.addColor(new MyColor(color.R, color.G, color.B));
                    imageColors[i, j] = color;
                }
            }
            this.image = image;
        }
        private void drawColors(int colorCount)
        {
            if (colorCount <= 0) return;

            pictureBox.Invalidate();
            newImageBox.Invalidate();

            List<MyColor> palette = quantizer.makePalette(colorCount);

            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            imageBitmap = new Bitmap(newImageBox.Width, newImageBox.Height);

            Graphics g = Graphics.FromImage(bitmap);
            Graphics gImage = Graphics.FromImage(imageBitmap);

            drawPaletteTree(palette, g);
            drawNewImage(palette, gImage);

            pictureBox.Image = bitmap;
            newImageBox.Image = imageBitmap;
        }
        private void drawNewImage(List<MyColor> palette, Graphics g)
        {
            for (int i = 0; i < imageColors.GetLength(0); i++)
            {
                for (int j = 0; j < imageColors.GetLength(1); j++)
                {
                    Color imageColor = imageColors[i, j];
                    int index = quantizer.getPaletteIndex(new MyColor(imageColor.R, imageColor.G, imageColor.B));
                    if (index > palette.Count) continue;
                    MyColor color = palette[index];
                    imageBitmap.SetPixel(i, j, Color.FromArgb(color.red, color.green, color.blue));
                }
            }
        }
        private void drawPaletteTree(List<MyColor> palette, Graphics g)
        {
            double centerX = bitmap.Width / 2.0;
            double centerY = bitmap.Height / 2.0;
            List<Node> leaves = quantizer.getLeafNodes();
            quantizer.root.x = (int)centerX;
            quantizer.root.y = (int)centerY;
            int ile = 0;

            if (first)
            {
                first = false;
                for (int i = maxLevel-1; i >= 0; i--)
                {
                    if (quantizer.levels[i].Count == 0)
                        maxLevel--;
                    else break;
                }
            }

            double maxRadius = Math.Min(centerX, centerY) * (maxLevel - 1) / maxLevel - 10;

           
            double[] offsets = new double[8];
            List<Node>[] levelNodes = new List<Node>[quantizer.maxLevel];
            for (int i = 0; i < quantizer.maxLevel; i++)
                levelNodes[i] = new List<Node>();

            foreach (var node in leaves)
            {
                levelNodes[node.level].Add(node);
            }

            int[] levelRadius = new int[maxLevel + 1];


            for (int level = maxLevel; level > -1; level--)
            {
                //levelNodes[level].OrderBy(leaf => leaf.getColor().red)
                //         .ThenBy(leaf => leaf.getColor().green)
                //         .ThenBy(leaf => leaf.getColor().blue)
                //         .ToList();
                levelNodes[level].OrderBy(node => node.index);

                foreach (var node in levelNodes[level])
                {
                    int nodesInLevel = levelNodes[level].Count;
                    if (nodesInLevel <= 0)
                        continue;

                    double angleIncrement = 2 * Math.PI / nodesInLevel;

                    double angle = offsets[level];

                    offsets[level] += angleIncrement;

                    double distanceFromCenter = (level + 1) * maxRadius / maxLevel;

                    double x = centerX + distanceFromCenter * Math.Cos(angle);
                    double y = centerY + distanceFromCenter * Math.Sin(angle);

                    int r = (int)(2 * 3.14 * distanceFromCenter / nodesInLevel);

                    if (r * maxLevel > maxRadius / 2)
                    {
                        r = (int)((double)maxRadius / maxLevel / 2);
                        angleIncrement = r / distanceFromCenter;
                    }
                    if (r == 0) r = 2;

                    levelRadius[level] = r;

                    MyColor color = node.getColor();

                    // edges to children
                    if (drawEdges && !node.isLeaf())
                    {
                        foreach (var child in node.children)
                        {
                            if (child == null) continue;
                            if (child.active) g.DrawLine(new Pen(Color.FromArgb(color.red, color.green, color.blue)), (float)x, (float)y, child.x, child.y);
                        }
                    }

                    // color node
                    SolidBrush brush = new SolidBrush(Color.FromArgb(color.red, color.green, color.blue));
                    g.FillEllipse(brush, (float)x - r / 2, (float)y - r / 2, r, r);
                    if (node.isLeaf())
                    {
                        //g.DrawEllipse(Pens.Black, (float)x - r / 2, (float)y - r / 2, r, r);

                        int x1 = (int)(centerX + (distanceFromCenter + r) * Math.Cos(angle));
                        int y1 = (int)(centerY + (distanceFromCenter + r) * Math.Sin(angle));
                        Point p1 = new Point(x1, y1);

                        int x2 = (int)(centerX + (distanceFromCenter) * Math.Cos(angle + angleIncrement / 2));
                        int y2 = (int)(centerY + (distanceFromCenter) * Math.Sin(angle + angleIncrement / 2));
                        Point p2 = new Point(x2, y2);


                        int x3 = (int)(centerX + (distanceFromCenter) * Math.Cos(angle - angleIncrement / 2));
                        int y3 = (int)(centerY + (distanceFromCenter) * Math.Sin(angle - angleIncrement / 2));
                        Point p3 = new Point(x3, y3);

                        PointF[] points = { p1, p2, p3 };

                        g.FillPolygon(brush, points);
                    }


                    node.x = (int)x;
                    node.y = (int)y;

                    if (level > 0)
                    {
                        if (!levelNodes[level - 1].Contains(node.parentNode))
                        {
                            levelNodes[level - 1].Add(node.parentNode);
                        }
                    }
                }
            }
            MyColor rootColor = quantizer.root.getColor();
            g.FillEllipse(new SolidBrush(Color.FromArgb(rootColor.red, rootColor.green, rootColor.blue)), (float)centerX - 5, (float)centerY - 5, 10, 10);

        }

        private void next10Button_Click(object sender, EventArgs e)
        {
            colorCount -= 10;
            colorCount = colorCount < 0 ? 0 : colorCount;
            color.Text = "Color count: " + colorCount.ToString();
            drawColors(colorCount);
        }

        private void next20Button_Click(object sender, EventArgs e)
        {
            colorCount -= 20;
            colorCount = colorCount < 0 ? 0 : colorCount;
            color.Text = "Color count: " + colorCount.ToString();
            drawColors(colorCount);
        }

        private void next50Button_Click(object sender, EventArgs e)
        {
            colorCount -= 50;
            colorCount = colorCount < 0 ? 0 : colorCount;
            color.Text = "Color count: " + colorCount.ToString();
            drawColors(colorCount);
        }

        private void gk3_SizeChanged(object sender, EventArgs e)
        {

            pictureBox.Invalidate();
            newImageBox.Invalidate();

            if (ClientSize.Width - 160 < 2 * ClientSize.Height)
            {
                ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Width / 2 + 160);
            }
            pictureBox.Width = (ClientSize.Width - groupBox.Width) / 2;
            pictureBox.Height = pictureBox.Width;

            newImageBox.Width = (ClientSize.Width - groupBox.Width) / 2;
            newImageBox.Height = newImageBox.Width;

            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            imageBitmap = new Bitmap(newImageBox.Width, newImageBox.Height);


            if (imageColors != null)
            {
              //  drawColors(colorCount);
            }

            pictureBox.Image = bitmap;
            newImageBox.Image = imageBitmap;

            this.MinimumSize = this.Size;
        }
        private void radio10_CheckedChanged(object sender, EventArgs e)
        {
            animationCount = 10;
        }
        private void radio20_CheckedChanged(object sender, EventArgs e)
        {
            animationCount = 20;
        }
        private void radio50_CheckedChanged(object sender, EventArgs e)
        {
            animationCount = 50;
        }
        private void animationButton_Click(object sender, EventArgs e)
        {
            if (!animation)
            {
                animationButton.Image = pause;
                animationTimer.Start();
            }
            else
            {
                animationButton.Image = play;
                animationTimer.Stop();
            }
            animation = !animation;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            colorCount -= animationCount;
            colorCount = colorCount < 0 ? 0 : colorCount;
            color.Text = "Color count: " + colorCount.ToString();
            drawColors(colorCount);
        }
        private void edgesButton_CheckedChanged(object sender, EventArgs e)
        {
            if (edgesButton.Checked)
                drawEdges = true;
            else drawEdges = false;

            drawColors(colorCount);
        }

        private void gen1Button_Click(object sender, EventArgs e)
        {
            if (gen1Image == null)
                generate1();
            colorCount = initColorCount;
            loadColors(gen1Image);
            drawColors(colorCount);
        }

        private void gen2Button_Click(object sender, EventArgs e)
        {
            if (gen2Image == null)
                generate2();
            colorCount = initColorCount;
            loadColors(gen2Image);
            drawColors(colorCount);
        }
        private void generate1()
        {
            Bitmap resizedImage = new Bitmap(imageBitmap.Width, imageBitmap.Height);

            Color[] colors = new Color[8];
            colors[0] = Color.Black;
            colors[1] = Color.White;
            colors[2] = Color.FromArgb(255, 0, 0);
            colors[3] = Color.FromArgb(0, 255, 255);
            colors[4] = Color.FromArgb(0, 255, 0);
            colors[5] = Color.FromArgb(255, 0, 255); ;
            colors[6] = Color.FromArgb(0, 0, 255);
            colors[7] = Color.FromArgb(255, 255, 0);
            for (int i = 0; i < imageBitmap.Width; i++)
            {
                for (int j = 0; j < imageBitmap.Height; j++)
                {
                    int x = i / (imageBitmap.Width / 4);
                    int y = j / (imageBitmap.Height / 2);
                    int index = x * 2 + y;
                    resizedImage.SetPixel(i, j, colors[index]);
                }
            }
            gen1Image = resizedImage;
            image = resizedImage;
        }
        private void generate2()
        {
            Bitmap resizedImage = new Bitmap(imageBitmap.Width, imageBitmap.Height);
            double brightness = 1.0;

            for (int i = 0; i < imageBitmap.Width; i++)
            {
                for (int j = 0; j < imageBitmap.Height; j++)
                {
                    double hue = (double)i / imageBitmap.Width * 360.0; // Barwa zale¿na od wspó³rzêdnej x
                    double saturation = 1 - (double)j / imageBitmap.Height; // Nasycenie zale¿ne od wspó³rzêdnej y

                    Color color = ColorFromHSV(hue, saturation, brightness);

                    resizedImage.SetPixel(i, j, color);
                }
            }
            gen2Image = resizedImage;
            image = resizedImage;
        }
        private Color ColorFromHSV(double hue, double saturation, double brightness)
        {
            double hh, p, q, t, ff;

            if (saturation <= 0.0)
            {
                int value = (int)(brightness * 255.0);
                return Color.FromArgb(255, value, value, value);
            }

            hh = hue;
            if (hh >= 360.0) hh = 0.0;
            hh /= 60.0;
            int i = (int)hh;
            ff = hh - i;
            p = brightness * (1.0 - saturation);
            q = brightness * (1.0 - (saturation * ff));
            t = brightness * (1.0 - (saturation * (1.0 - ff)));

            int red, green, blue;
            switch (i)
            {
                case 0:
                    red = (int)(brightness * 255.0);
                    green = (int)(t * 255.0);
                    blue = (int)(p * 255.0);
                    break;
                case 1:
                    red = (int)(q * 255.0);
                    green = (int)(brightness * 255.0);
                    blue = (int)(p * 255.0);
                    break;
                case 2:
                    red = (int)(p * 255.0);
                    green = (int)(brightness * 255.0);
                    blue = (int)(t * 255.0);
                    break;
                case 3:
                    red = (int)(p * 255.0);
                    green = (int)(q * 255.0);
                    blue = (int)(brightness * 255.0);
                    break;
                case 4:
                    red = (int)(t * 255.0);
                    green = (int)(p * 255.0);
                    blue = (int)(brightness * 255.0);
                    break;
                default:
                    red = (int)(brightness * 255.0);
                    green = (int)(p * 255.0);
                    blue = (int)(q * 255.0);
                    break;
            }
            return Color.FromArgb(255, red, green, blue);
        }
    }
}