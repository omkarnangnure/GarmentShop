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
    public partial class Inventory : Form
    {
        SqlCommandBuilder scb;
        SqlDataAdapter sda;
        DataTable dt;
        public Inventory()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                scb = new SqlCommandBuilder(sda);
                sda.Update(dt);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30");
                //sda = new SqlDataAdapter(@"Select  INVENTORY.ITEM_ID,STOCK.ITEM_ID,INVENTORY.ITEM_NAME,INVENTORY.BRAND,STOCK.S,STOCK.M,STOCK.L,STOCK.XL,STOCK.XXL,STOCK.PRICE  FROM INVENTORY,STOCK where INVENTORY.ITEM_ID=STOCK.ITEM_ID", con);
                sda = new SqlDataAdapter(@"Select * from STOCK", con);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
    
}
