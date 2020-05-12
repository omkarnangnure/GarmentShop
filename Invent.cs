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

namespace WindowsFormsApp4
{
    public partial class Invent : Form
    {
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommandBuilder scb;

        public Invent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30");                sda = new SqlDataAdapter(@"Select * from INVENTORY", con);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                scb = new SqlCommandBuilder(sda);
                sda.Update(dt);
            }
            catch { }
        }
    }
}
