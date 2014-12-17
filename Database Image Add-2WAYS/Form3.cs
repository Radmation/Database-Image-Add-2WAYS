using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_Image_Add_2WAYS
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void switchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "image files|*.jpg;*.png;*.gif";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.Cancel)
                return;

            pictureBox1.Image = Image.FromFile(ofd.FileName);
            txtPath1.Text = ofd.FileName;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("insert into tblPath(imgName,imgPath) values(@imgName,@imgPath)", cn);
            cmd.Parameters.AddWithValue("@imgName", txtPath1.Text);
            cmd.Parameters.AddWithValue("@imgPath", txtPath1.Text);

            cn.Open();
            try
            {
                int res1 = cmd.ExecuteNonQuery();
                if (res1 > 0)
                    MessageBox.Show("Imaged Path Stored Successfully");
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
    }
}
