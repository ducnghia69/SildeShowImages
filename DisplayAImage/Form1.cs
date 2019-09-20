using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayAImage
{
    public partial class Form1 : Form
    {
        string openFileName;
        OpenFileDialog folderBrowser = new OpenFileDialog();
        Image img;
        string pathDirectoryImg;
        List<string> imageFiles = new List<string>();
        int cnt = 0;
        public Form1()
        {
            InitializeComponent();
            //var path = Application.StartupPath + "/img";

            //if (Directory.Exists(path))
            //{
            //    var files = Directory.GetFiles(path, ".", SearchOption.AllDirectories);
            //    if(files.Length > 0)
            //    {
            //        foreach (string filename in files)
            //        {
            //            if (Regex.IsMatch(filename, @".jpg|.png|.gif$"))
            //            {
            //                imageFiles.Add(filename);
            //            }
            //        }
            //        pxbImage.Image = Image.FromFile(imageFiles[0]);
            //    }

            //}
            pxbImage.AllowDrop = true;
            pathDirectoryImg = Application.StartupPath + "/img";
            if (Directory.Exists(pathDirectoryImg))
            {
                var files = Directory.GetFiles(pathDirectoryImg, "*.*", SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    foreach (string filename in files)
                    {
                        if (Regex.IsMatch(filename, @".jpg|.png|.gif$"))
                        {
                            imageFiles.Add(filename);
                        }
                    }
                    FileStream fileStream = new FileStream(imageFiles[0], FileMode.Open, FileAccess.Read);
                    pxbImage.Image = Image.FromStream(fileStream);
                    fileStream.Close();
                }

            }

            //List<string> imageFiles = new List<string>();

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {                   
            if(cnt > imageFiles.Count -1 )
            {
                cnt = 0;
            }
            else
            {
                pxbImage.Image = Image.FromFile(imageFiles[cnt]);
                cnt++;
            }
            
        }
        Boolean checkClick = true;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            
            if (checkClick)
            {
                timer1.Enabled = true;
                btnPlay.Text = "Stop";
                checkClick = false;
            }
            else
            {
                timer1.Enabled = false;
                btnPlay.Text = "Play";
                checkClick = true;
            }
           
        }


        private void btnChonFolder_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.Title = "Chọn folder ảnh";
            openfileDialog.Filter = "File anh(*.png; *.jpg)|*.png; *.jpg";
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                openFileName = openfileDialog.FileName;

                try
                {
                    // Output the requested file in richTextBox1.
                    // img = Image.FromFile(openFileName);
                    var files = Directory.GetFiles(folderDialog.SelectedPath, "*.*", SearchOption.AllDirectories);

                    imageFiles = new List<string>();
                    int count = 1;
                    foreach (string filename in files)
                    {
                        if (Regex.IsMatch(filename, @".jpg|.png|.gif$"))
                        {
                            imageFiles.Add(filename);
                            //img = Image.FromFile(filename);

                            //if (img != null)
                            //{
                            //    if (!Directory.Exists(pathDirectoryImg))
                            //    {
                            //        Directory.CreateDirectory(pathDirectoryImg);
                            //    }
                            //    string a = String.Format("/slide_{0}.png", count);
                            //    img.Save(pathDirectoryImg + a);
                            //    count++;

                            //}
                        }
                    }
                    if (imageFiles.Count > 0)
                    {
                       
                        if (Directory.Exists(pathDirectoryImg))
                        {
                            var old_files = Directory.GetFiles(pathDirectoryImg, "*.*", SearchOption.AllDirectories);
                            if (old_files.Length > 0)
                            {
                                foreach (string filename in old_files)
                                {
                                    if (Regex.IsMatch(filename, @".jpg|.png|.gif$"))
                                    {

                                        File.Delete(filename);
                                    }
                                }
                            }
                        }
                        foreach (string filename in imageFiles)
                        {
                            img = Image.FromFile(filename);
                            if (img != null)
                            {
                                if (!Directory.Exists(pathDirectoryImg))
                                {
                                    Directory.CreateDirectory(pathDirectoryImg);
                                }

                                string a = String.Format("/slide_{0}.png", count);
                                img.Save(pathDirectoryImg + a);
                                count++;

                            }
                        }
                        FileStream fileStream = new FileStream(imageFiles[0], FileMode.Open, FileAccess.Read);
                        pxbImage.Image = Image.FromStream(fileStream);
                        fileStream.Close();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("The error is:"
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);

                }
                Invalidate();
                
            }
            btnPlay.Text = "Play";
        }
    }
}
