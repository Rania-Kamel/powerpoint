using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_1
{
    public partial class Form1 : Form
    {


        public Form1()
        {

            InitializeComponent();
        }

        private void uploadImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog image = new OpenFileDialog();
            image.Multiselect = true;
            image.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            DialogResult dr = image.ShowDialog();


            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                String[] files = image.FileNames;

                foreach (string img in files)
                {

                    this.listBox1.Items.Add(img);


                }


            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void slideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panelimages.Controls.Clear();
            statusStrip1.Visible = true;
            toolStripStatusLabel1.Text = "";
            listBox1.SelectionMode = SelectionMode.None;
            timer1.Interval = 100;
            timer1.Start();
            counter = 0;
        }
        int counter;
        PictureBox picture = new PictureBox();
        private void timer1_Tick(object sender, EventArgs e)
        {

            
                if (counter == listBox1.Items.Count)
                {
                    timer1.Stop();
                    return;
                }
                string temp = listBox1.Items[counter].ToString();
                picture.Image = Image.FromFile(temp);
                picture = ImageResizing(picture);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                panelimages.Controls.Add(picture);
                toolStripStatusLabel1.Text = temp;
                counter++;
           

        }

        private void showOneImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            statusStrip1.Visible = true;
            toolStripStatusLabel1.Text = "";
            this.panelimages.Controls.Clear();
            listBox1.SelectionMode = SelectionMode.None;
            listBox1.SelectionMode = SelectionMode.One;

        }

        private void selectImagesToolStripMenuItem_Click(object sender, EventArgs e)

        {
            timer1.Stop();
            statusStrip1.Visible = false;
            this.panelimages.Controls.Clear();
            listBox1.SelectionMode = SelectionMode.None;
            listBox1.SelectionMode = SelectionMode.MultiSimple;


        }
        int maxWidth, maxHeight;
        int viewHeight;
        int viewWidth;

        public PictureBox ImageResizing(PictureBox image)
        {
            int imageWidth = image.Image.Width;
            int imageHeight = image.Image.Height;

            if (listBox1.SelectionMode == SelectionMode.MultiSimple)
            {
                maxWidth = 90;
                maxHeight = 120;

            }
            else
            {
                maxWidth = 530;
                maxHeight = 400;
            }

                if (imageHeight > imageWidth)
                {
                    viewHeight = maxHeight;
                    viewWidth = (maxHeight * imageWidth) / imageHeight;
                }
                else
                {
                    viewWidth = maxWidth;
                    viewHeight = (maxWidth * imageHeight) / imageWidth;
                }

                image.Size = new Size(viewWidth, viewHeight);
                int xAxis = (maxWidth - viewWidth) / 2;
                int yAxis = (maxHeight - viewHeight) / 2;

                image.Location = new Point(xAxis, yAxis);
            

            return image;

        }

        private PictureBox DisplayImages(object item)
        {
            
                PictureBox picture = new PictureBox();
                string temp = listBox1.GetItemText(item);
                picture.Image = Image.FromFile(temp);
                picture = ImageResizing(picture);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;

                if (listBox1.SelectionMode == SelectionMode.One)

                    toolStripStatusLabel1.Text = temp;
                panelimages.Controls.Add(picture);
          

            return picture;
        }

private void panelimages_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            int xAxis;
            int yAxis;


            panelimages.Controls.Clear();

            if (listBox1.SelectionMode == SelectionMode.One)
            {
                DisplayImages(listBox1.SelectedItem);
            }
            else
            {

                    foreach (object item in listBox1.SelectedItems)
                    {
                         
                        PictureBox picture = DisplayImages(item);

                        if (x + maxWidth + 10 >= 530)
                        {
                            x = 0;
                            xAxis = (maxWidth - viewWidth) / 2;
                            yAxis = (maxHeight - viewHeight) / 2;
                            picture.Location = new Point(x+ xAxis, y + maxHeight + 30 + yAxis);
                            y += maxHeight + 30;
                        }
                        else
                        {
                            xAxis = (maxWidth - viewWidth) / 2;
                            yAxis = (maxHeight - viewHeight) / 2;
                            picture.Location = new Point(x+ xAxis, y + yAxis);
                        }

                        this.panelimages.Controls.Add(picture);
                        x += maxWidth + 20;

                    }
            
        }
        }
    }
}

