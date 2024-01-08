using System.Data;
using DGVPrinterHelper;
using Microsoft.Data.SqlClient;

namespace Mini_Market_Management_System
{
    public partial class SellingForm : Form
    {
        DBConnect dBCon = new DBConnect();
        DGVPrinter printer = new DGVPrinter();
        
        public SellingForm()
        {
            InitializeComponent();
        }

        private void GetCategory()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand commmand = new SqlCommand(selectQuery, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(commmand);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox_category.DataSource = table;
            comboBox_category.ValueMember = "CatName";
        }

        private void GetTable()
        {
            string selectQuery = "SELECT ProdName, ProdPrice FROM Product";
            SqlCommand commmand = new SqlCommand(selectQuery, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(commmand);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_product.DataSource = table;
        }

        private void GetSellTable()
        {
            string selectQuery = "SELECT * FROM Bill";
            SqlCommand commmand = new SqlCommand(selectQuery, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(commmand);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_sellList.DataSource = table;
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            label_date.Text = DateTime.Today.ToShortDateString();
            label_seller.Text = LoginForm.sellerName;
            GetTable();
            GetCategory();
            GetSellTable();
        }

        private void DataGridView_product_Click(object sender, EventArgs e)
        {
            TextBox_name.Text = DataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_price.Text = DataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
        }

        int grandTotal = 0, n = 0;

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_price.Text == "" || TextBox_qty.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertQuery = "INSERT INTO Bill VALUES (" + TextBox_id.Text + ",'" + label_seller.Text + "','" + label_date.Text + "'," + grandTotal.ToString() + ")";
                    SqlCommand commmand = new SqlCommand(insertQuery, dBCon.GetCon());
                    dBCon.OpenCon();
                    commmand.ExecuteNonQuery();
                    MessageBox.Show("Order Added Succesfully", "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dBCon.CloseCon();
                    GetSellTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            // Code for print data For PDF file
            printer.Title = "Mdemy MiniMarket Sell Lists";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = System.Drawing.StringFormatFlags.LineLimit | System.Drawing.StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "foxlearn";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_sellList);
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor= Color.Goldenrod;
        }

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            GetTable();
        }

        private void comboBox_category_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuery = "SELECT ProdName, ProdPrice FROM Product WHERE ProdCat ='" + comboBox_category.SelectedValue.ToString() + "'";
            SqlCommand commmand = new SqlCommand(selectQuery, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(commmand);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_product.DataSource = table;
        }

        private void button_addOrder_Click(object sender, EventArgs e)
        {
            if(TextBox_name.Text == "" || TextBox_qty.Text == "")
            {
                MessageBox.Show("Missing Information", "Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int Total = Convert.ToInt32(TextBox_price.Text) * Convert.ToInt32(TextBox_qty.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView_order);
                addRow.Cells[0].Value = ++n;
                addRow.Cells[1].Value = TextBox_name.Text;
                addRow.Cells[2].Value = TextBox_price.Text;
                addRow.Cells[3].Value = TextBox_qty.Text;
                addRow.Cells[4].Value = Total;
                dataGridView_order.Rows.Add(addRow);
                grandTotal += Total;
                label_amount1.Text = grandTotal + " Ks";
            }
        }
    }
}
