using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Database_Image_Add_2WAYS
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string baseFolder = @"c:\Program Files\MyApp\Images";

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "image files|*.jpg;*.png;*.gif";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.Cancel)
                return;

            pictureBox1.Image = Image.FromFile(ofd.FileName);
            txtPath.Text = ofd.FileName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] Pic_arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(Pic_arr, 0, Pic_arr.Length);

            SqlCommand cmd = new SqlCommand("insert into tblImg(imgPath,imgImage) values(@imgPath,@imgImage)", cn);
            cmd.Parameters.AddWithValue("@imgPath", txtPath.Text);
            cmd.Parameters.AddWithValue("@imgImage", Pic_arr);
            cn.Open();
            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                    MessageBox.Show("Imaged Stored Successfully");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                cn.Close();
            }
        }

        private void btnPhotos_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void switchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }
    }
}
