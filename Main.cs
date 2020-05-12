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
    
    public partial class Main : Form
    {
        public  int kp ;
        public string l;
        public int sz;
        public Main()
        {
            InitializeComponent();
          //  listView1.View = View.Details;
            listView1.FullRowSelect = true;
        }
       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text)|| string.IsNullOrEmpty(comboBox2.Text)|| string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)) 
             return;
            ListViewItem ITEM = new ListViewItem(comboBox1.Text);
            ITEM.SubItems.Add(comboBox3.Text);
            ITEM.SubItems.Add(comboBox2.Text);
            ITEM.SubItems.Add(textBox1.Text);
            ITEM.SubItems.Add(textBox2.Text);
            ITEM.SubItems.Add(textBox3.Text);
            listView1.Items.Add(ITEM);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Focus();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30");
                SqlDataAdapter sda = new SqlDataAdapter(@"select PRICE from STOCK full outer join INVENTORY on STOCK.ITEM_ID=INVENTORY.ITEM_ID where INVENTORY.ITEM_NAME='" + comboBox1.Text + "' and INVENTORY.BRAND='" + comboBox3.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                textBox1.Text = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox8.Clear();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
           
        }

       /* private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = (Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text)).ToString();
            }
            catch (Exception ex)
            { }
        }*/

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count > 0)
                {
                    listView1.Items.Remove(listView1.SelectedItems[0]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
                MessageBox.Show("Select/Add Items");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Inventory s = new Inventory();
            s.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox8.Text = (float.Parse(textBox4.Text) - float.Parse(textBox5.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            double Subtotal = 0;
            foreach (ListViewItem lstItem in listView1.Items) // listView has ListViewItem objects
            {
                Subtotal += double.Parse(lstItem.SubItems[5].Text); // Columns 5
            }
            textBox4.Text = Subtotal.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("check total...");
            }
            else
            {

                kp = Convert.ToInt16(textBox6.Text);
                int lp;

                try
                {
                    String sql = "insert into SALES(SALE_ID,TOTAL)values(@id,@TOTAL)";
                    using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", kp);
                            cmd.Parameters.AddWithValue("@TOTAL", textBox8.Text); // assign value to parameter 
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data saved");
                            button11.Enabled = true;
                            button2.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    string inm, br;
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {

                        int ct;
                        sz = Convert.ToInt32(listView1.Items[i].SubItems[4].Text);
                        l = (listView1.Items[i].SubItems[2].Text).ToString();
                        inm = (listView1.Items[i].SubItems[0].Text).ToString();
                        br = (listView1.Items[i].SubItems[1].Text).ToString();
                        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30");

                        //int k = l - ;
                        SqlDataAdapter sda = new SqlDataAdapter(@"select " + l + " from STOCK where ITEM_ID=(select ITEM_ID from INVENTORY where ITEM_NAME='" + inm + "' and  BRAND='" + br + "')", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ct = Convert.ToInt16(dt.Rows[0][0].ToString());



                        String sql = "update STOCK set " + l + "=" + ct + "-" + sz + " where ITEM_ID=(select ITEM_ID from INVENTORY where ITEM_NAME='" + inm + "' and  BRAND='" + br + "')";
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.ExecuteNonQuery();
            //                MessageBox.Show("Data Updated");

                        }


                    }
                }



                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }




                textBox6.Text = (Convert.ToInt32(textBox6.Text) + 1).ToString();
                        
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30");
                SqlDataAdapter sda = new SqlDataAdapter(@"select PRICE from STOCK full outer join INVENTORY on STOCK.ITEM_ID=INVENTORY.ITEM_ID where INVENTORY.ITEM_NAME='" + comboBox1.Text + "' and INVENTORY.BRAND='" + comboBox3.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                textBox1.Text = dt.Rows[0][0].ToString();
            }
            catch(Exception ex) { MessageBox.Show("Choose correct Combination"); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            SALES s = new SALES();
            s.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Invent i = new Invent();
            i.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataDataSet.INVENTORY' table. You can move, or remove it, as needed.
            this.iNVENTORYTableAdapter.Fill(this.dataDataSet.INVENTORY);
            button7.Enabled = false;
            button11.Enabled = false;
            timer1.Enabled=true;
            string id; 
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GarmentShop.mdf;Integrated Security=True;Connect Timeout=30");
                SqlDataAdapter sda = new SqlDataAdapter(@"select MAX(SALE_ID) from SALES ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                id = dt.Rows[0][0].ToString();
                kp = Convert.ToInt16(id);
                kp = kp + 1;
                textBox6.Text =kp.ToString();

            }
            catch (Exception ex) { MessageBox.Show("Choose correct Combination"); }
                    }

        private void Button11_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox7.AppendText("                 \t OO7 Mens Wear               ");


            textBox7.AppendText(Environment.NewLine + "Receipt ID :" + textBox6.Text + "  \t \t  Date:" + dateTimePicker1.Text + Environment.NewLine);
            textBox7.AppendText("|===========================================|" + Environment.NewLine);
            textBox7.AppendText(Environment.NewLine + "Item_Name\tPrice\tQuantity\t\tTotal" + Environment.NewLine);

            try
            {

                string name, quntity,price,total,str;
                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    
                    quntity = (listView1.Items[i].SubItems[4].Text);
                    name = (listView1.Items[i].SubItems[0].Text).ToString();
                    price = (listView1.Items[i].SubItems[3].Text).ToString();
                    total = (listView1.Items[i].SubItems[5].Text).ToString();
                    str = Environment.NewLine + name + "                 " + price + "               " + quntity + "                          " + total;

                    textBox7.AppendText(str);
                    
                }
                textBox7.AppendText(Environment.NewLine+"|-------------------------------------------------------------------------|"+Environment.NewLine+Environment.NewLine);
                textBox7.AppendText(Environment.NewLine+"\tSubTotal:"+textBox4.Text+"    Discount:"+textBox5.Text+"     Total:"+textBox8.Text+Environment.NewLine);
                textBox7.AppendText(Environment.NewLine+"\t    THANK YOU... VISIT AGAIN...");
                button7.Enabled = true;
            }
        catch(Exception ex)
                {
                MessageBox.Show(ex.Message);
                }
        

        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
         Font font1 = new Font("arial", 16, FontStyle.Regular);
            e.Graphics.DrawString(textBox7.Text, font1, Brushes.Black, 100, 100);

        }

        private void Button7_Click_1(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {

        }

        private void Button12_Click_1(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox8.Clear();
            textBox7.Clear();
            listView1.Items.Clear();
            button2.Enabled = true;
            button7.Enabled = false;
            button11.Enabled = false;
        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                textBox3.Text = (float.Parse(textBox1.Text) * float.Parse(textBox2.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
