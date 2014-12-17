using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Database_Image_Add_2WAYS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection cn;
        SqlCommand cmd;

        MemoryStream ms;

        private void Form2_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            cmd = new SqlCommand("select * from tblImg", cn);
            cn.Open();
            SqlDataReader dr;
            try
            {
                dr=cmd.ExecuteReader();
                while (dr.Read())
                {
                    listBox1.Items.Add(dr["imgPath"].ToString());
                }
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            cmd = new SqlCommand("select * from tblImg where imgPath='"+listBox1.SelectedItem+"'", cn);
            cn.Open();
            SqlDataReader dr;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                    pictureBox1.Image = Image.FromStream(ms);
                }
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
