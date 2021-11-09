using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace dz_02._11._2021
{

    public partial class Form1 : Form
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        SoundPlayer soundPlayer;
        System.IO.Stream sstream;

        Point p1 = new Point(0, 0),
              p2 = new Point(0, 0);

        public void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                p1.X = e.X;
                p1.Y = e.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
          
            if (e.Button == MouseButtons.Left)
            {
                p2.X = e.X;
                p2.Y = e.Y;
            }

            int a = p1.X, b = p2.X;
            if (a > b) swap(ref a, ref b);
            p1.X = a; p2.X = b;

            a = p1.Y; b = p2.Y;
            if (a > b)
                swap(ref a, ref b);
            p1.Y = a; p2.Y = b;

            if ((p1.X < 0 || p1.Y < 0) || (p2.X > this.Width-15 || p2.Y > this.Height-40))
            {
                MessageBox.Show("Выход за пределы окна");
            }
            else
            if(p2.X-p1.X<10 || p2.Y - p1.Y < 10)
            {
                MessageBox.Show("Минимальный размер обьекта 10;10");
            }
            else
            {
                Label label = new Label();
                label.Location = p1;
                Random r = new Random();
                label.BackColor = Color.FromArgb(r.Next(155, 255) - r.Next(0, 155), r.Next(155, 255) - r.Next(0, 155), r.Next(155, 255) - r.Next(0, 155));
                label.Width = p2.X - p1.X;
                label.Height = p2.Y - p1.Y;
                label.Text = label.Name;
                label.MouseClick += new MouseEventHandler(click);
                label.MouseDoubleClick += new MouseEventHandler(dclick);
                this.Controls.Add(label);
                sstream = assembly.GetManifestResourceStream(@"dz_02._11._2021.2.wav");
                soundPlayer = new SoundPlayer(sstream);
                soundPlayer.Play();
            }
        }

        private void click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Text = $"X = {((Label)sender).Location.X} , Y = {((Label)sender).Location.Y} , P = {(p2.X - p1.X) * (p2.Y - p1.Y)}";
            }
        }

        private void dclick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sstream = assembly.GetManifestResourceStream(@"dz_02._11._2021.1.wav");
                soundPlayer = new SoundPlayer(sstream);
                soundPlayer.Play();
                this.Controls.Remove((Control)sender);
            }
        }
    }
}

