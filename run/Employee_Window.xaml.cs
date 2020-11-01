using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace run
{
    /// <summary>
    /// Interaction logic for Employee_Window.xaml
    /// </summary>
    public partial class Employee_Window : Window
    {

        //SQL connection
        const string connectionString = @"server=localhost; user id=root;password=; database=db_payrollsystem;";
        
        public Employee_Window(String user)
        {
            InitializeComponent();
            label_Username.Content = user;
            adminprofilepic_changed();      
            listSalary();
            listDeductions();
            listJournal();


        }
        //for pic in sidebar
        private void adminprofilepic_changed()
        {
            try
            {
                string query = "SELECT Profile_Pic FROM tbl_personalinformation WHERE Username=@Username";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                commandDatabase.Parameters.AddWithValue("@Username", Convert.ToString(label_Username.Content));
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        byte[] blob = (byte[])reader["Profile_Pic"];
                        MemoryStream stream = new MemoryStream();
                        stream.Write(blob, 0, blob.Length);
                        stream.Position = 0;

                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();

                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        ms.Seek(0, SeekOrigin.Begin);
                        bi.StreamSource = ms;
                        bi.EndInit();
                        adminprofilepic.Source = bi;
                    }


                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //list for tables
        private void listSalary()
        {
            try
            {
                string query = "SELECT a.Username, b.Basic_Salary_Per_Month, b.Total_Transportation_Allowance, b.Overtime_Salary, b.Total_leave_salary, b.Total_Salary, " +
                    "b.Date_Salary FROM tbl_personalinformation a join tbl_salary b on b.IDNumber = a.IDNumber where a.Username like ('" + Convert.ToString(label_Username.Content) + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_salary.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void listDeductions()
        {
            try
            {
                string query = "SELECT  b.Username, a.Witholding_Tax_Amount, a.SSS_Contribution_Amount, a.SSS_Loan_Deduction, a.HDMF_Contribution_Amount," +
                    " a.HDMF_Loan_Deduction, a.Philhealth_Insurance_Corporation_Contribution_Cost, a.Remaining_Debt, a.Vale, a.Other, a.Late_Deduction_Cost, a.Total_Deductions, " +
                    "a.Date_Deduction FROM tbl_deductions a join tbl_personalinformation b on b.IDNumber = a.IDNumber where b.Username like ('" + Convert.ToString(label_Username.Content) + "')";
                 
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Deuction.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void listJournal()
        {
            try
            {
                string query = "SELECT d.ParyrollJournalID, a.Username, b.Total_Salary, c.Total_Deductions, d.Net_Salary, d.Pay_Date FROM tbl_payrolljournal d JOIN tbl_deductions c ON c.DeductionID = d.DeductionID JOIN tbl_salary b ON b.SalaryID = d.SalaryID JOIN" +
                    " tbl_personalinformation a ON a.IDNumber = d.IDNumber where a.Username like ('" + Convert.ToString(label_Username.Content) + "')";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Journal.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //button for sidebar
   
        private void btn_Salary_Click(object sender, RoutedEventArgs e)
        {
           
            Grid_Salary.Visibility = Visibility.Visible;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
        }
        private void btn_Deduction_Click(object sender, RoutedEventArgs e)
        {
          
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Visible;
            Grid_Journal.Visibility = Visibility.Hidden;
        }
        private void btn_Journal_Click(object sender, RoutedEventArgs e)
        {
           
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Visible;
        }

        //for sidebar
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Logout = new MainWindow();
            this.Close();
            Logout.Show();

        }

        //search function
        //search function
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
             if (Grid_Salary.Visibility == Visibility.Visible)
            {
                searchsalary();
                if (tb_search.Text == "")
                {
                    listSalary();
                }

            }
            else if (Grid_Deductions.Visibility == Visibility.Visible)
            {
                searchdeduction();
                if (tb_search.Text == "")
                {
                    listDeductions();
                }

            }
            else if (Grid_Journal.Visibility == Visibility.Visible)
            {
                searchJournal();
                if (tb_search.Text == "")
                {
                    listJournal();
                }

            }



        }
        private void searchsalary()
        {

            try
            {

                string query = "SELECT a.Username, " +
                "b.Basic_Salary_Per_Month, " +
                "b.Total_Transportation_Allowance, " +
                "b.Overtime_Salary, b.Total_leave_salary, " +
                "b.Total_Salary, " +
                  "b.Date_Salary FROM tbl_personalinformation a join tbl_salary b on b.IDNumber = a.IDNumber where " +
                  "(a.Username like ('" + Convert.ToString(label_Username.Content) +"') and a.Username like ('" + tb_search.Text + "%'))  " +
                  "or (b.Basic_Salary_Per_Month like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (b.Total_Transportation_Allowance like ('" + tb_search.Text + "%')  and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (b.Overtime_Salary like ('" + tb_search.Text + "%')  and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (b.Total_leave_salary like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (b.Total_Salary like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (MONTH(b.Date_Salary) like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (DAY(b.Date_Salary) like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                  "or (YEAR(b.Date_Salary) like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))";



            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
           

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_salary.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void searchdeduction()
        {
            try
            {


                string query = "SELECT b.Username, a.Witholding_Tax_Amount, a.SSS_Contribution_Amount, a.SSS_Loan_Deduction, a.HDMF_Contribution_Amount, a.HDMF_Loan_Deduction, a.Philhealth_Insurance_Corporation_Contribution_Cost, a.Remaining_Debt, a.Vale, a.Other, a.Late_Deduction_Cost, " +
                    "a.Total_Deductions, a.Date_Deduction FROM tbl_deductions a join tbl_personalinformation b on b.IDNumber = a.IDNumber where " +
                    "(b.Username like ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "'))  " +
                    "or (a.Witholding_Tax_Amount like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.SSS_Contribution_Amount like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.SSS_Loan_Deduction like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.HDMF_Contribution_Amount like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.HDMF_Loan_Deduction like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Philhealth_Insurance_Corporation_Contribution_Cost like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Remaining_Debt like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Vale like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Other like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Late_Deduction_Cost like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Total_Deductions like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) " +
                    "or (a.Date_Deduction like  ('" + tb_search.Text + "%') and b.Username like ('" + Convert.ToString(label_Username.Content) + "')) ";
                    
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Deuction.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void searchJournal()
        {
            try
            {
                string query = "SELECT a.Username, " +
                "b.Total_Deductions, " +
                "c.Net_Salary, " +
                "c.Pay_Date FROM tbl_payrolljournal c JOIN tbl_deductions b ON b.DeductionID = c.DeductionID JOIN" +
                    " tbl_personalinformation a ON a.IDNumber = c.IDNumber " +
                    "where (a.Username like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                    "or (b.Total_Deductions like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                     "or (c.Net_Salary like ('" + tb_search.Text + "%') and a.Username like ('" + Convert.ToString(label_Username.Content) + "'))" +
                     "or (MONTH(c.Pay_Date) like('" + tb_search.Text + "%') and a.Username like('" + Convert.ToString(label_Username.Content) + "'))" +
                     "or(DAY(c.Pay_Date) like('" + tb_search.Text + "%') and a.Username like('" + Convert.ToString(label_Username.Content) + "'))" +
                     "or(YEAR(c.Pay_Date) like('" + tb_search.Text + "%') and a.Username like('" + Convert.ToString(label_Username.Content) + "'))";


            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
           

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Journal.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void dataGrid_Journal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_Journal.SelectedIndex > -1)
                {

                    tb_payslipid.Text = selected["ParyrollJournalID"].ToString();
                   
                }
                else
                {

                    tb_payslipid.Text = "0";
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_printpayslip_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int payslipid = Convert.ToInt32(tb_payslipid.Text);
                Payslip employeewindow = new Payslip(payslipid);
                if (tb_payslipid.Text == "")
                {
                    MessageBox.Show("Please select items you want to print");
                }
                    else
                    {
                    employeewindow.Show();
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        
    }
}

