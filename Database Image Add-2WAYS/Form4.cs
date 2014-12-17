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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection cn;
        SqlCommand cmd, cmd1;



        private void Form4_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            cmd = new SqlCommand("select * from tblPath", cn);
            cn.Open();

            SqlDataReader dr;
            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lBox.Items.Add(dr["imgName"].ToString());
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


        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void lBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            cmd = new SqlCommand("select * from tblPath where imgName='"+lBox.SelectedItem+"'", cn);
            txtPath.Text = lBox.Text;
            cn.Open();


            try
            {
                {
                    pictureBox1.Image = Image.FromFile(lBox.Text);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            cmd = new SqlCommand("Delete from tblPath where imgName='" + lBox.SelectedItem + "'", cn);
            cn.Open();

            try
            {
                int res1 = cmd.ExecuteNonQuery();
                if (res1 > 0)
                    MessageBox.Show("Image Path Deleted From Database");
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Radmation\Documents\NewImages.mdf;Integrated Security=True;Connect Timeout=30");
            cmd = new SqlCommand("Delete from tblPath where imgName=''", cn);
            cn.Open();


            try
            {
                int res1 = cmd.ExecuteNonQuery();
                if (res1 > 0)
                    MessageBox.Show("Emtpty Rows Deleted From Database");
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
