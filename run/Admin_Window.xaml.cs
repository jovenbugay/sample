using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data;

namespace run
{
  
    public partial class Admin_Window : Window
    {
        //holds data for opening an image
        string strName, imageName = "";
        //database location
        const string connectionString = @"server=localhost; user id=root;password=; database=db_payrollsystem;";
       

        public Admin_Window(String user)
        {
            //call of list's method
            InitializeComponent();
            label_Username.Content = user;
            listPersonalInformation();
            listSalary();
            listDeductions();
            listJournal();
            listWithholdingtax();
            listSSSContribution();
            listHDMFContribution();
            listSSSLoan();
            listHDMFLoan();
            listAccountType();
            listJobPosition();
            listLeaveReport();
            listRemarks();
            listSummary();

            //button events
            btn_addPersonalInfo.Click += addPersonalInfotosummaryrecord;
            btn_addPersonalInfo.Click += addPersonalInfo;
            btn_editPersonalInfo.Click += editPersonalInfotosummaryrecord;
            btn_editPersonalInfo.Click += editPersonalInfo;
            btn_deletePersonalInfo.Click += deletePersonalInfotosummaryrecord;
            btn_deletePersonalInfo.Click += deletePersonalInfo;
            btn_calculatetotalsalary.Click += getinitialsalary;
            btn_calculatetotalsalary.Click += getsickorleavetotalsalary;
            btn_calculatetotalsalary.Click += gettotalsalary;
            btn_calculatetotalsalary.Click += gettotalsalary;
            btn_addSalary.Click += addSalarytosummaryrecord;
            btn_addSalary.Click += addSalary;
            btn_editSalary.Click += editSalarytosummaryrecord;
            btn_editSalary.Click += editSalary;
            btn_deleteSalary.Click += deleteSalarytosummaryrecord;
            btn_deleteSalary.Click += deleteSalary;
            btn_addDeduction.Click += addDeductionstosummaryrecord;
            btn_addDeduction.Click += addDeductions;
            btn_editDeduction.Click += editDeductionstosummaryrecord;
            btn_editDeduction.Click += editDeductions;
            btn_deleteDeduction.Click += deleteDeductionstosummaryrecord;
            btn_deleteDeduction.Click += deleteDeductions;
            btn_addJournal.Click += addJournaltosummaryrecord;
            btn_addJournal.Click += addJournal;
            btn_editJournal.Click += editJournaltosummaryrecord;
            btn_editJournal.Click += editJournal;
            btn_deleteJournal.Click += deleteJournaltosummaryrecord;
            btn_deleteJournal.Click += deleteJournal;
            btn_addBIR.Click += addWithholdingtaxtosummaryrecord;
            btn_addBIR.Click += addBIR;
            btn_editBIR.Click += editWithholdingtaxtosummaryrecord;
            btn_editBIR.Click += editBIR;
            btn_deleteBIR.Click += deleteWithholdingtaxtosummaryrecord;
            btn_deleteBIR.Click += deleteBIR;
            btn_addSSSContribution.Click += addSSSContributiontosummaryrecord;
            btn_addSSSContribution.Click += addSSSContribution;
            btn_editSSSContribution.Click += editSSSContributiontosummaryrecord;
            btn_editSSSContribution.Click += editSSSContribution;
            btn_deleteSSSContribution.Click += deleteSSSContributiontosummaryrecord;
            btn_deleteSSSContribution.Click += deleteSSSContribution;
            btn_addHDMFcontribution.Click += addHDMFContributiontosummaryrecord;
            btn_addHDMFcontribution.Click += addHDMFcontribution;
            btn_editHDMFcontribution.Click += editHDMFContributiontosummaryrecord;
            btn_editHDMFcontribution.Click += editHDMFcontribution;
            btn_deleteHDMFcontribution.Click += deleteHDMFContributiontosummaryrecord;
            btn_deleteHDMFcontribution.Click += deleteHDMFcontribution;
            btn_addSSSLoan.Click += addSSSLoanntosummaryrecord;
            btn_addSSSLoan.Click += addSSSLoan;
            btn_editSSSLoan.Click += editSSSLoanntosummaryrecord;
            btn_editSSSLoan.Click += editSSSLoan;
            btn_deleteSSSLoan.Click += deleteSSSLoanntosummaryrecord;
            btn_deleteSSSLoan.Click += deleteSSSLoan;
            btn_addHDMFLoan.Click += addHDMFLoanntosummaryrecord;
            btn_addHDMFLoan.Click += addHDMFLoan;
            btn_editHDMFLoan.Click += editHDMFLoanntosummaryrecord;
            btn_editHDMFLoan.Click += editHDMFLoan;
            btn_deleteHDMFLoan.Click += deleteHDMFLoanntosummaryrecord;
            btn_deleteHDMFLoan.Click += deleteHDMFLoan;
            btn_addAccountType.Click += addAccountTypetosummaryrecord;
            btn_addAccountType.Click += addAccountType;
            btn_editAccountType.Click += editAccountTypetosummaryrecord;
            btn_editAccountType.Click += editAccountType;
            btn_deleteAccountType.Click += deleteAccountTypetosummaryrecord;
            btn_deleteAccountType.Click += deleteAccountType;
            btn_addJobposition.Click += addJobpositiontosummaryrecord;
            btn_addJobposition.Click += addJobposition;
            btn_editJobposition.Click += editJobpositiontosummaryrecord;
            btn_editJobposition.Click += editJobposition;
            btn_deleteJobposition.Click += deleteJobpositiontosummaryrecord;
            btn_deleteJobposition.Click += deleteJobposition;
            btn_addLeaveReport.Click += addLeaveReporttosummaryrecord;
            btn_addLeaveReport.Click += addLeaveReport;
            btn_editLeaveReport.Click += editLeaveReporttosummaryrecord;
            btn_editLeaveReport.Click += editLeaveReport;
            btn_deleteLeaveReport.Click += deleteLeaveReporttosummaryrecord;
            btn_deleteLeaveReport.Click += deleteLeaveReport;
            btn_addRemarks.Click += addRemarkstosummaryrecord;
            btn_addRemarks.Click += addRemarks;
            btn_editRemarks.Click += editRemarkstosummaryrecord;
            btn_editRemarks.Click += editRemarks;
            btn_deleteRemarks.Click += deleteRemarkstosummaryrecord;
            btn_deleteRemarks.Click += deleteRemarks;

            //calculation buttoons
            btn_calculatetotaldeductions.Click += getsalaryid;
            btn_calculatetotaldeductions.Click += getbasicsalaryperdayandmonth;
            btn_calculatetotaldeductions.Click += gettaxnumber;
            btn_calculatetotaldeductions.Click += getsssnumber;
            btn_calculatetotaldeductions.Click += getsssloanid;
            btn_calculatetotaldeductions.Click += gethdmfcontributionid;
            btn_calculatetotaldeductions.Click += gethdmfloanid;
            btn_calculatetotaldeductions.Click += getphilhealthcontributioncost;
            btn_calculatetotaldeductions.Click += getlatedeductioncost;
            btn_calculatetotaldeductions.Click += gettaxrate;
            btn_calculatetotaldeductions.Click += getwithholdingtaxamount;
            btn_calculatetotaldeductions.Click += getcontributionamount;
            btn_calculatetotaldeductions.Click += getannualinterestrate;
            btn_calculatetotaldeductions.Click += getsssloandeduction;
            btn_calculatetotaldeductions.Click += getemployeeshare;
            btn_calculatetotaldeductions.Click += gethdmfcontributionamount;
            btn_calculatetotaldeductions.Click += gethdmfannualinterestrate;
            btn_calculatetotaldeductions.Click += gethdmfloandeduction;
            btn_calculatetotaldeductions.Click += gettotaldeduction;


            btn_calculatejournal.Click += getsalaryid1;
            btn_calculatejournal.Click += getdeductionid;
            btn_calculatejournal.Click += gettotalsalary1;
            btn_calculatejournal.Click += gettotaldeduction1;
            btn_calculatejournal.Click += getnetsalary;
            adminprofilepic_changed();

        }
        //method for lists
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
                else
                {
                    btn_editPersonalInfo.IsEnabled = false;
                    btn_deletePersonalInfo.IsEnabled = false;
                    tb_IDNumber.Text = "";
                    tb_Username.Text = "";
                    tb_Password.Text = "";
                    tb_AccountTypedID.Text = "";
                    tb_PositionID.Text = "";
                    tb_Surname.Text = "";
                    tb_GivenName.Text = "";
                    tb_MiddleName.Text = "";
                    tb_Birthdate.Text = "";
                    tb_Email.Text = "";
                    tb_Address.Text = "";
                    tb_TIN.Text = "";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
        private void listPersonalInformation()
        {
            string query = "SELECT * FROM `tbl_personalinformation`";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
               
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_PersonalInfo.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void listSalary()
        {
            try
            {
            string query = "SELECT * FROM `tbl_salary`";

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
            string query = "SELECT * FROM `tbl_deductions`";

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
            try {
                string query = "SELECT * FROM `tbl_payrolljournal`";
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
        private void listWithholdingtax()
        {
            try
            {
             

            string query = "SELECT * FROM `tbl_withholdingtax`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Withholding.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listSSSContribution()
        {
            try { 
      

            string query = "SELECT * FROM `tbl_ssscontribution`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_SSSContribution.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void listHDMFContribution()
        {
            try { 
            string query = "SELECT * FROM `tbl_hdmfcontribution`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_HDMFContribution.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listSSSLoan()
        {
            try { 
            string query = "SELECT * FROM `tbl_sssloaninterest`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_SSSLoan.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listHDMFLoan()
        {
            try { 
      


            string query = "SELECT * FROM `tbl_hdmfloaninterest`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_HDMFLoan.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listAccountType()
        {
            try { 
            string query = "SELECT * FROM `tbl_accounttype`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_AccountType.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listJobPosition()
        {
            try { 
            string query = "SELECT * FROM `tbl_jobposition`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Jobposition.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listLeaveReport()
        {
            try { 
   


            string query = "SELECT * FROM `tbl_leavereport`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_LeaveReport.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listRemarks()
        {
            try { 

            string query = "SELECT * FROM `tbl_remarks`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Remarks.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listSummary()
        {
           try { 
            string query = "SELECT * FROM `tbl_summaryrecord`";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_Summary.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //button for sidebar
        private void btn_Personalinfo_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Visible;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Salary_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Visible;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Deduction_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Visible;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Journal_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Visible;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Contribution_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Visible;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Loan_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Visible;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_AccountType_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Visible;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_JobPosition_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Visible;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_LeaveReport_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Visible;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Remarks_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Visible;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_BIR_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Visible;
            Grid_Summary.Visibility = Visibility.Hidden;
        }
        private void btn_Summary_Click(object sender, RoutedEventArgs e)
        {
            Grid_PersonalInfo.Visibility = Visibility.Hidden;
            Grid_Salary.Visibility = Visibility.Hidden;
            Grid_Deductions.Visibility = Visibility.Hidden;
            Grid_Journal.Visibility = Visibility.Hidden;
            Grid_Contribution.Visibility = Visibility.Hidden;
            Grid_Loan.Visibility = Visibility.Hidden;
            Grid_AccountType.Visibility = Visibility.Hidden;
            Grid_JobPosition.Visibility = Visibility.Hidden;
            Grid_LeaveReport.Visibility = Visibility.Hidden;
            Grid_Remarks.Visibility = Visibility.Hidden;
            Grid_BIR.Visibility = Visibility.Hidden;
            Grid_Summary.Visibility = Visibility.Visible;
        }
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Logout = new MainWindow();
            this.Close();
            Logout.Show();

        }
        private void btn_SSS_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_SSSContribution.Visibility = Visibility.Visible;
            SSSModifier.Visibility = Visibility.Visible;
            dataGrid_HDMFContribution.Visibility = Visibility.Hidden;
            HDMFModifier.Visibility = Visibility.Hidden;
            label_SSS.Visibility = Visibility.Visible;
            label_HDMF.Visibility = Visibility.Hidden;


        }
        private void btn_HDMF_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_SSSContribution.Visibility = Visibility.Hidden;
            SSSModifier.Visibility = Visibility.Hidden;
            dataGrid_HDMFContribution.Visibility = Visibility.Visible;
            HDMFModifier.Visibility = Visibility.Visible;
            label_SSS.Visibility = Visibility.Hidden;
            label_HDMF.Visibility = Visibility.Visible;
        }
        private void btn_SSSLoan_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_SSSLoan.Visibility = Visibility.Visible;
            dataGrid_HDMFLoan.Visibility = Visibility.Hidden;
            label_SSSLoan.Visibility = Visibility.Visible;
            label_HDMFLoan.Visibility = Visibility.Hidden;
            SSSLoan.Visibility = Visibility.Visible;
            HDMFLoan.Visibility = Visibility.Hidden;

        }
        private void btn_HDMFLoan_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_SSSLoan.Visibility = Visibility.Hidden;
            dataGrid_HDMFLoan.Visibility = Visibility.Visible;
            label_SSSLoan.Visibility = Visibility.Hidden;
            label_HDMFLoan.Visibility = Visibility.Visible;
            SSSLoan.Visibility = Visibility.Hidden;
            HDMFLoan.Visibility = Visibility.Visible;
        }

        //selection change
        private void dataGrid_PersonalInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;

                if (dataGrid_PersonalInfo.SelectedIndex > -1)
                {
                    btn_editPersonalInfo.IsEnabled = true;
                    btn_deletePersonalInfo.IsEnabled = true;
                    tb_IDNumber.Text = selected["IDNumber"].ToString();
                    tb_Username.Text = selected["Username"].ToString();
                    tb_Password.Text = selected["Password"].ToString();
                    tb_AccountTypedID.Text = selected["Account_Type_ID"].ToString();
                    tb_PositionID.Text = selected["PositionID"].ToString();
                    tb_Surname.Text = selected["Surname"].ToString();
                    tb_GivenName.Text = selected["GivenName"].ToString();
                    tb_MiddleName.Text = selected["MiddleName"].ToString();
                    tb_Birthdate.Text = selected["Birthdate"].ToString();
                    tb_Email.Text = selected["Email"].ToString();
                    tb_Address.Text = selected["Address"].ToString();
                    tb_TIN.Text = selected["TIN"].ToString();
                    tb_remarks_id.Text = selected["remarks_id"].ToString();

                    string query = "SELECT Profile_Pic FROM tbl_personalinformation WHERE IDNumber=@IDNumber";
                    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.CommandTimeout = 60;
                    commandDatabase.Parameters.AddWithValue("@IDNumber", tb_IDNumber.Text);
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
                            profilepic.Source = bi;
                        }
                    }

                }
                else
                {
                    btn_editPersonalInfo.IsEnabled = false;
                    btn_deletePersonalInfo.IsEnabled = false;
                    tb_IDNumber.Text = "";
                    tb_Username.Text = "";
                    tb_Password.Text = "";
                    tb_AccountTypedID.Text = "";
                    tb_PositionID.Text = "";
                    tb_Surname.Text = "";
                    tb_GivenName.Text = "";
                    tb_MiddleName.Text = "";
                    tb_Birthdate.Text = "";
                    tb_Email.Text = "";
                    tb_Address.Text = "";
                    tb_TIN.Text = "";
                    tb_remarks_id.Text = "";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_salary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_salary.SelectedIndex > -1)
            {
                
                btn_editSalary.IsEnabled = true;
                btn_deleteSalary.IsEnabled = true;
                tb_Salaryid.Text = selected["SalaryID"].ToString();
                tb_IDNumber1.Text = selected["IDNumber"].ToString();
                tb_Basicsalaryperday.Text = selected["Basic_Salary_Per_Day"].ToString();
                tb_Attendanceindays.Text = selected["AttendanceInDays"].ToString();
                tb_Basicsalarypermonth.Text = selected["Basic_Salary_Per_Month"].ToString();
                tb_Transpoallowanceperday.Text = selected["Transportation_Allowance_Per_Day"].ToString();
                tb_Totaltranspoallowance.Text = selected["Total_Transportation_Allowance"].ToString();
                tb_Totalovertime.Text = selected["Total_Overtime"].ToString();
                tb_Overtimesalary.Text = selected["Overtime_Salary"].ToString();
                tb_Sickorleaveindays.Text = selected["Leave_in_days"].ToString();
                tb_Totalsickorleavesalary.Text = selected["Total_leave_salary"].ToString();
                tb_Totalsalary.Text = selected["Total_Salary"].ToString();
            }
            else
            {
                btn_editSalary.IsEnabled = false;
                btn_deleteSalary.IsEnabled = false;
                tb_Salaryid.Text = "0";
                tb_IDNumber1.Text = "0";
                tb_Basicsalaryperday.Text = "0";
                tb_Attendanceindays.Text = "0";
                tb_Basicsalarypermonth.Text = "0";
                tb_Transpoallowanceperday.Text = "0";
                tb_Totaltranspoallowance.Text = "0";
                tb_Totalovertime.Text = "0";
                tb_Overtimesalary.Text = "0";
                tb_Totalsalary.Text = "0";
                tb_Sickorleaveindays.Text = "0";
                tb_Totalsickorleavesalary.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_Deuction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_Deuction.SelectedIndex > -1)
            {
                btn_editDeduction.IsEnabled = true;
                btn_deleteDeduction.IsEnabled = true;
                tb_Deductionid.Text = selected["DeductionID"].ToString();
                tb_Salaryid1.Text = selected["SalaryID"].ToString(); 
                tb_IDNumber2.Text = selected["IDNumber"].ToString();
                tb_Monthlywithholdingtaxnumber.Text = selected["Monthly_Withholding_Tax_Number"].ToString();
                tb_Withholdingtaxamount.Text = selected["Witholding_Tax_Amount"].ToString();
                tb_SSSnumber.Text = selected["SSSNumber"].ToString();
                tb_SSSContributionAmount.Text = selected["SSS_Contribution_Amount"].ToString();
                tb_SSSloanid.Text = selected["SSSLoanID"].ToString();
                tb_SSSinitialloanamount.Text = selected["SSS_Initial_Loan_Amount"].ToString();
                tb_SSSloandeduction.Text = selected["SSS_Loan_Deduction"].ToString();
                tb_Remainingmonthsforsssloan.Text = selected["Remaining_Months_For_SSS_Loan"].ToString();
                tb_HDMFcontributionid.Text = selected["HDMFContributionID"].ToString();
                tb_HDMFcontributionamount.Text = selected["HDMF_Contribution_Amount"].ToString();
                tb_HDMFloanid.Text = selected["HDMFLoanID"].ToString();
                tb_HDMFinitialloanamount.Text = selected["HDMF_Initial_Loan_Amount"].ToString();
                tb_HDMFloandeduction.Text = selected["HDMF_Loan_Deduction"].ToString();
                tb_RemainingmonthsforHDMFloan.Text = selected["Remaining_Months_For_HDMF_Loan"].ToString();
                tb_Philhealthcontributioncost.Text = selected["Philhealth_Insurance_Corporation_Contribution_Cost"].ToString();
                tb_Remainingdebt.Text = selected["Remaining_Debt"].ToString();
                tb_Vale.Text = selected["Vale"].ToString();
                tb_Other.Text = selected["Other"].ToString();
                tb_Latedeductioninminutes.Text = selected["Total_Lates_In_Minutes"].ToString();
                tb_Latedeductioncost.Text = selected["Late_Deduction_Cost"].ToString();
                tb_Totaldeductions.Text = selected["Total_Deductions"].ToString();
                
            }
            else
            {   btn_editDeduction.IsEnabled = false;
                btn_deleteDeduction.IsEnabled = false;
                tb_Deductionid.Text = "0";
                tb_IDNumber2.Text = "0";
                tb_Salaryid1.Text = "0";
                tb_Monthlywithholdingtaxnumber.Text = "0";
                tb_Withholdingtaxamount.Text = "0";
                tb_SSSnumber.Text = "0";
                tb_SSSContributionAmount.Text = "0";
                tb_SSSloanid.Text = "0";
                tb_SSSinitialloanamount.Text = "0";
                tb_SSSloandeduction.Text = "0";
                tb_Remainingmonthsforsssloan.Text = "0";
                tb_HDMFcontributionid.Text = "0";
                tb_HDMFcontributionamount.Text = "0";
                tb_Deductionid.Text = "0";
                tb_HDMFloanid.Text = "0";
                tb_HDMFinitialloanamount.Text = "0";
                tb_HDMFloandeduction.Text = "0";
                tb_RemainingmonthsforHDMFloan.Text = "0";
                tb_Philhealthcontributioncost.Text = "0";
                tb_Remainingdebt.Text = "0";
                tb_Vale.Text = "0";
                tb_Other.Text = "0";
                tb_Latedeductioninminutes.Text = "0";
                tb_Latedeductioncost.Text = "0";
                tb_Totaldeductions.Text = "0";

                }
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
            {   btn_editJournal.IsEnabled = true;
                btn_deleteJournal.IsEnabled = true;
                tb_Journalid.Text = selected["ParyrollJournalID"].ToString();
                tb_IDnumber3.Text = selected["IDNumber"].ToString();
                tb_Salaryid2.Text = selected["SalaryID"].ToString();
                tb_Deductionid1.Text = selected["DeductionID"].ToString();
                tb_Netsalary.Text = selected["Net_Salary"].ToString();
                tb_payslipid.Text = selected["ParyrollJournalID"].ToString();

                }
            else
            {
                btn_editJournal.IsEnabled = false;
                btn_deleteJournal.IsEnabled = false;
                tb_Journalid.Text = "0";
                tb_IDnumber3.Text = "0";
                tb_Salaryid2.Text = "0";
                tb_Deductionid1.Text = "0";
                tb_Netsalary.Text = "0";
                tb_totalsalary.Text = "0";
                tb_totaldeductions.Text = "0";
                tb_payslipid.Text = "0";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_Withholding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_Withholding.SelectedIndex > -1)
            {
                btn_editBIR.IsEnabled = true;
                btn_deleteBIR.IsEnabled = true;
                tb_Monthlywithholdingtaxnumber1.Text = selected["Monthly_Withholding_Tax_Number"].ToString();
                tb_Taxrate.Text = selected["Tax_Rate"].ToString();
                    tb_minrangeoftaxableincome.Text = selected["minrangeoftaxableincome"].ToString();
                tb_maxrangeoftaxableincome.Text = selected["maxrangeoftaxableincome"].ToString();
                }
            else
            {   
                btn_editBIR.IsEnabled = false;
                btn_deleteBIR.IsEnabled = false;
                tb_Monthlywithholdingtaxnumber1.Text = "";
                tb_Taxrate.Text = "";
                    tb_minrangeoftaxableincome.Text = "";
                    tb_maxrangeoftaxableincome.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_SSSContribution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_SSSContribution.SelectedIndex > -1)
            {
                btn_editSSSContribution.IsEnabled = true;
                btn_deleteSSSContribution.IsEnabled = true;
                tb_SSSnumber1.Text = selected["SSSNumber"].ToString();
                    tb_minrangeofcompensation.Text = selected["minrangeofcompensation"].ToString();
                    tb_maxrangeofcompensation.Text = selected["maxrangeofcompensation"].ToString();
                tb_Monthlysalarycredit.Text = selected["Monthly_Salary_Credit"].ToString();
                tb_Employeecontribution.Text = selected["Employee_Contribution"].ToString();
            }
            else
            {  
                btn_editSSSContribution.IsEnabled = false;
                btn_deleteSSSContribution.IsEnabled = false;
                tb_SSSnumber1.Text = "";
                    tb_minrangeofcompensation.Text = "";
                    tb_maxrangeofcompensation.Text = "";
                tb_Monthlysalarycredit.Text = "";
                tb_Employeecontribution.Text = "";

            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_HDMFContribution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_HDMFContribution.SelectedIndex > -1)
            {
                btn_editHDMFcontribution.IsEnabled = true;
                btn_deleteHDMFcontribution.IsEnabled = true;
                tb_HDMFcontributionid1.Text = selected["HDMFContributionID"].ToString();
                tb_Employeeshare.Text = selected["Employee_Share"].ToString();
                    tb_minrangeofmonthlycompensation.Text = selected["minrangeofmonthlycompensation"].ToString();
                    tb_maxrangeofmonthlycompensation.Text = selected["maxrangeofmonthlycompensation"].ToString();
                }
            else
            {   
                btn_editHDMFcontribution.IsEnabled = false;
                btn_deleteHDMFcontribution.IsEnabled = false;
                tb_HDMFcontributionid1.Text = "";
                tb_Employeeshare.Text = "";
                    tb_minrangeofmonthlycompensation.Text = "";
                    tb_maxrangeofmonthlycompensation.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_SSSLoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_SSSLoan.SelectedIndex > -1)
            {
                btn_editSSSLoan.IsEnabled = true;
                btn_deleteSSSLoan.IsEnabled = true;
                tb_SSSloanid1.Text = selected["SSSLoanID"].ToString();
                tb_minloanamount.Text = selected["minloanamount"].ToString();
                    tb_maxloanamount.Text = selected["maxloanamount"].ToString();
                    tb_Annualinterestrate.Text = selected["SSS_Annual_Interest_Rate"].ToString();
            }
            else
            {
                btn_editSSSLoan.IsEnabled = false;
                btn_deleteSSSLoan.IsEnabled = false;
                tb_SSSloanid1.Text = "";
                    tb_minloanamount.Text = "";
                    tb_maxloanamount.Text = "";
                    tb_Annualinterestrate.Text = "";
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_HDMFLoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_HDMFLoan.SelectedIndex > -1)
            {
               
                btn_editHDMFLoan.IsEnabled = true;
                btn_deleteHDMFLoan.IsEnabled = true;
                tb_HDMFloanid1.Text = selected["HDMFLoanID"].ToString();
                tb_HDMFannualinterestrate.Text = selected["HDMF_Annual_Interest_Rate"].ToString();
                    tb_minfixpricingperiodinyears.Text = selected["minfixpricingperiodinyears"].ToString();
                    tb_maxfixpricingperiodinyears.Text = selected["maxfixpricingperiodinyears"].ToString();
                }
            else
            {
                btn_editHDMFLoan.IsEnabled = false;
                btn_deleteHDMFLoan.IsEnabled = false;
                tb_HDMFloanid1.Text = "";
                tb_HDMFannualinterestrate.Text = "";
                    tb_minfixpricingperiodinyears.Text = "";
                    tb_maxfixpricingperiodinyears.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_AccountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_AccountType.SelectedIndex > -1)
            {
                btn_editAccountType.IsEnabled = true;
                btn_deleteAccountType.IsEnabled = true;
                tb_Accounttypeid.Text = selected["Account_Type_ID"].ToString();
                tb_Accounttype.Text = selected["Account_Type"].ToString();
            }
            else
            {
                btn_editAccountType.IsEnabled = false;
                btn_deleteAccountType.IsEnabled = false;
                tb_Accounttypeid.Text = "";
                tb_Accounttype.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_Jobposition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_Jobposition.SelectedIndex > -1)
            {
                btn_editJobposition.IsEnabled = true;
                btn_deleteJobposition.IsEnabled = true;
                tb_Positionid.Text = selected["PositionID"].ToString();
                tb_Positionname.Text = selected["PositionName"].ToString();
            }
            else
            {
                btn_editJobposition.IsEnabled = false;
                btn_deleteJobposition.IsEnabled = false;
                tb_Positionid.Text = "";
                tb_Positionname.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_LeaveReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_LeaveReport.SelectedIndex > -1)
                {
                    btn_editLeaveReport.IsEnabled = true;
                    btn_deleteLeaveReport.IsEnabled = true;
                    tb_Leavereportid.Text = selected["LeaveReportID"].ToString();
                    tb_Idnumber.Text = selected["IDNumber"].ToString();
                    tb_Typeofleave.Text = selected["Type_of_Leave"].ToString();
                }
                else
                {
                    btn_editLeaveReport.IsEnabled = false;
                    btn_deleteLeaveReport.IsEnabled = false;
                    tb_Leavereportid.Text = "";
                    tb_Idnumber.Text = "";
                    tb_Typeofleave.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_Remarks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_Remarks.SelectedIndex > -1)
                {

                    btn_editRemarks.IsEnabled = true;
                    btn_deleteRemarks.IsEnabled = true;
                    tb_Remarksid.Text = selected["remarks_id"].ToString();
                    tb_Description.Text = selected["description"].ToString();

                }
                else
                {

                    btn_editRemarks.IsEnabled = false;
                    btn_deleteRemarks.IsEnabled = false;
                    tb_Remarksid.Text = "";
                    tb_Description.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dataGrid_Summary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView selected = gd.SelectedItem as DataRowView;
                if (dataGrid_Summary.SelectedIndex > -1)
                {
                    btn_editSummary.IsEnabled = true;
                    btn_deleteSummary.IsEnabled = true;
                    tb_Recordid.Text = selected["summaryrecord_id"].ToString();
                    tb_actionperofmedby.Text = selected["action_performed_by"].ToString();
                    tb_action_performed.Text = selected["action_performed"].ToString();
                }
                else
                {
                    btn_editSummary.IsEnabled = false;
                    btn_deleteSummary.IsEnabled = false;
                    tb_Recordid.Text = "";
                    tb_actionperofmedby.Text = "";
                    tb_action_performed.Text = "";

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        //crud 
        private void addPersonalInfo(object sender, RoutedEventArgs e)
        {
           
            if (imageName == "")
            {
                try
                {
                    string query = "INSERT INTO tbl_personalinformation (`Username`, `Password`, `Account_Type_ID`, `PositionID`, `Surname`, `GivenName`, `MiddleName`, `Birthdate`,`Email`,`Address`,`TIN`, `Profile_Pic`, `remarks_id`)" +
                        " VALUES (@Username, @Password, @Account_Type_ID, @PositionID, @Surname, @GivenName, @MiddleName, @Birthdate, @Email, @Address, @TIN, @Profile_Pic, @remarks_id)";


                    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    commandDatabase.Parameters.AddWithValue("@Username", tb_Username.Text);
                    commandDatabase.Parameters.AddWithValue("@Password", SHA512(tb_Password.Text));
                    commandDatabase.Parameters.AddWithValue("@Account_Type_ID", tb_AccountTypedID.Text);
                    commandDatabase.Parameters.AddWithValue("@PositionID", tb_PositionID.Text);
                    commandDatabase.Parameters.AddWithValue("@Surname", tb_Surname.Text);
                    commandDatabase.Parameters.AddWithValue("@GivenName", tb_GivenName.Text);
                    commandDatabase.Parameters.AddWithValue("@MiddleName", tb_MiddleName.Text);
                    commandDatabase.Parameters.AddWithValue("@Birthdate", Convert.ToDateTime(tb_Birthdate.Text));
                    commandDatabase.Parameters.AddWithValue("@Email", tb_Email.Text);
                    commandDatabase.Parameters.AddWithValue("@Address", tb_Address.Text);
                    commandDatabase.Parameters.AddWithValue("@TIN", tb_TIN.Text);
                    commandDatabase.Parameters.AddWithValue("@remarks_id", Convert.ToInt32(tb_remarks_id.Text));
                    FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                    byte[] imgByteArr = new byte[fileStream.Length];
                    fileStream.Read(imgByteArr, 0, Convert.ToInt32(fileStream.Length));
                    fileStream.Close();
                    commandDatabase.Parameters.Add(new MySqlParameter("@Profile_Pic", imgByteArr));
                    commandDatabase.CommandTimeout = 60;

                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();

                    MessageBox.Show("Personal Information successfuly added");

                    databaseConnection.Close();

                }
                catch (Exception)
                {
                    //// Show any error message.
                    MessageBox.Show("No picture selected");
                }
                finally
                {
                    listPersonalInformation();
                }
            }
            else
            {
                try
                {
                    string query2 = "INSERT INTO tbl_personalinformation (`Username`, `Password`, `Account_Type_ID`, `PositionID`, `Surname`, `GivenName`, `MiddleName`, `Birthdate`,`Email`,`Address`,`TIN`, `Profile_Pic`, `remarks_id`)" +
                        " VALUES (@Username, @Password, @Account_Type_ID, @PositionID, @Surname, @GivenName, @MiddleName, @Birthdate, @Email, @Address, @TIN, @Profile_Pic, @remarks_id)";


                    MySqlConnection databaseConnection2 = new MySqlConnection(connectionString);
                    MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection2);
                    commandDatabase2.Parameters.AddWithValue("@Username", tb_Username.Text);
                    commandDatabase2.Parameters.AddWithValue("@Password", SHA512(tb_Password.Text));
                    commandDatabase2.Parameters.AddWithValue("@Account_Type_ID", tb_AccountTypedID.Text);
                    commandDatabase2.Parameters.AddWithValue("@PositionID", tb_PositionID.Text);
                    commandDatabase2.Parameters.AddWithValue("@Surname", tb_Surname.Text);
                    commandDatabase2.Parameters.AddWithValue("@GivenName", tb_GivenName.Text);
                    commandDatabase2.Parameters.AddWithValue("@MiddleName", tb_MiddleName.Text);
                    commandDatabase2.Parameters.AddWithValue("@Birthdate", Convert.ToDateTime(tb_Birthdate.Text));
                    commandDatabase2.Parameters.AddWithValue("@Email", tb_Email.Text);
                    commandDatabase2.Parameters.AddWithValue("@Address", tb_Address.Text);
                    commandDatabase2.Parameters.AddWithValue("@TIN", tb_TIN.Text);
                    commandDatabase2.Parameters.AddWithValue("@remarks_id", Convert.ToInt32(tb_remarks_id.Text));
                    FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                    byte[] imgByteArr = new byte[fileStream.Length];
                    fileStream.Read(imgByteArr, 0, Convert.ToInt32(fileStream.Length));
                    fileStream.Close();
                    commandDatabase2.Parameters.Add(new MySqlParameter("@Profile_Pic", imgByteArr));
                    commandDatabase2.CommandTimeout = 60;

                    databaseConnection2.Open();
                    MySqlDataReader myReader = commandDatabase2.ExecuteReader();

                    MessageBox.Show("Personal Information successfuly added");

                    databaseConnection2.Close();

                }
                catch (Exception ex)
                {
                    //// Show any error message.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    listPersonalInformation();
                }
            }
            
          
            
        }
        private void addPersonalInfotosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "Employee " +tb_GivenName.Text+ " "+tb_MiddleName.Text +" " + tb_Surname.Text +" has been added to personal information table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editPersonalInfo(object sender, RoutedEventArgs e)
        {
            try {

                string query = "UPDATE `tbl_personalinformation` SET `Username`=@Username, `Password`=@Password, `Account_Type_ID`=@Account_Type_ID, `PositionID`=@PositionID, " +
                "`Surname`=@Surname, `GivenName`=@GivenName, `MiddleName`=@MiddleName, `Birthdate`=@Birthdate,`Email`=@Email,`Address`=@Address,`TIN`=@TIN, `Profile_Pic`=@Profile_Pic, `remarks_id`=@remarks_id where `IDNumber`=@IDNumber";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDNumber.Text));
                commandDatabase.Parameters.AddWithValue("@Username", tb_Username.Text);
                commandDatabase.Parameters.AddWithValue("@Password", SHA512(tb_Password.Text));
                commandDatabase.Parameters.AddWithValue("@Account_Type_ID", Convert.ToInt32(tb_AccountTypedID.Text));
                commandDatabase.Parameters.AddWithValue("@PositionID", Convert.ToInt32(tb_PositionID.Text));
                commandDatabase.Parameters.AddWithValue("@Surname", tb_Surname.Text);
                commandDatabase.Parameters.AddWithValue("@GivenName", tb_GivenName.Text);
                commandDatabase.Parameters.AddWithValue("@MiddleName", tb_MiddleName.Text);
                commandDatabase.Parameters.AddWithValue("@Birthdate", Convert.ToDateTime(tb_Birthdate.Text));
                commandDatabase.Parameters.AddWithValue("@Email", tb_Email.Text);
                commandDatabase.Parameters.AddWithValue("@Address", tb_Address.Text);
                commandDatabase.Parameters.AddWithValue("@TIN", tb_TIN.Text);
                commandDatabase.Parameters.AddWithValue("@remarks_id", Convert.ToInt32(tb_remarks_id.Text));
                FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                byte[] imgByteArr = new byte[fileStream.Length];
                fileStream.Read(imgByteArr, 0, Convert.ToInt32(fileStream.Length));
                fileStream.Close();
                commandDatabase.Parameters.Add(new MySqlParameter("@Profile_Pic", imgByteArr));
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Account succesfully edited");
                databaseConnection.Close();
            }

            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listPersonalInformation();
            }
        }
        private void editPersonalInfotosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with id number " + tb_IDNumber.Text + " has been edited from personal information table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deletePersonalInfo(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_personalinformation` WHERE`IDNumber`=@IDNumber";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDNumber.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Personal Information successfuly deleted");


                // Succesfully deleted

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listPersonalInformation();
            }
        }
        private void deletePersonalInfotosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with id number " + tb_IDNumber.Text + " has been deleted from personal information table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addSalary(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_salary (`IDNumber`, `Basic_Salary_Per_Day`, `AttendanceInDays`, `Basic_Salary_Per_Month`, `Transportation_Allowance_Per_Day`, " +
                "`Total_Transportation_Allowance`, `Total_Overtime`, `Overtime_Salary`, `Leave_in_days`, `Total_leave_salary`,`Total_Salary`,`Date_Salary`) VALUES (@IDNumber, @Basic_Salary_Per_Day, " +
                "@AttendanceInDays, @Basic_Salary_Per_Month, @Transportation_Allowance_Per_Day, @Total_Transportation_Allowance, @Total_Overtime, @Overtime_Salary, @Leave_in_days, @Total_leave_salary, " +
                "@Total_Salary, @Date_Salary)";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDNumber1.Text));
                commandDatabase.Parameters.AddWithValue("@Basic_Salary_Per_Day", Convert.ToDouble(tb_Basicsalaryperday.Text));
                commandDatabase.Parameters.AddWithValue("@AttendanceInDays", Convert.ToInt32(tb_Attendanceindays.Text));
                commandDatabase.Parameters.AddWithValue("@Basic_Salary_Per_Month", Convert.ToDouble(tb_Basicsalarypermonth.Text));
                commandDatabase.Parameters.AddWithValue("@Transportation_Allowance_Per_Day", Convert.ToDouble(tb_Transpoallowanceperday.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Transportation_Allowance", Convert.ToDouble(tb_Totaltranspoallowance.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Overtime", Convert.ToDouble(tb_Totalovertime.Text));
                commandDatabase.Parameters.AddWithValue("@Overtime_Salary", Convert.ToDouble(tb_Overtimesalary.Text));
                commandDatabase.Parameters.AddWithValue("@Leave_in_days", Convert.ToInt32(tb_Sickorleaveindays.Text));
                commandDatabase.Parameters.AddWithValue("@Total_leave_salary", Convert.ToDouble(tb_Totalsickorleavesalary.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Salary", Convert.ToDouble(tb_Totalsalary.Text));
                commandDatabase.Parameters.AddWithValue("@Date_Salary", DateTime.Now);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Salary successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSalary();
            }
        }
        private void addSalarytosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with id number of " + tb_IDNumber1.Text + " has been added to salary table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editSalary(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_salary` SET `IDNumber`=@IDNumber, `Basic_Salary_Per_Day`=@Basic_Salary_Per_Day, `AttendanceInDays`=@AttendanceInDays, " +
                "`Basic_Salary_Per_Month`=@Basic_Salary_Per_Month, `Transportation_Allowance_Per_Day`=@Transportation_Allowance_Per_Day, " +
                "`Total_Transportation_Allowance`=@Total_Transportation_Allowance, `Total_Overtime`=@Total_Overtime, " +
                "`Overtime_Salary`=@Overtime_Salary, `Leave_in_days`=@Leave_in_days, `Total_leave_salary`=@Total_leave_salary, `Total_Salary`=@Total_Salary,`Date_Salary`=@Date_Salary where `SalaryID`=@SalaryID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@SalaryID", Convert.ToInt32(tb_Salaryid.Text));
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDNumber1.Text));
                commandDatabase.Parameters.AddWithValue("@Basic_Salary_Per_Day", Convert.ToDouble(tb_Basicsalaryperday.Text));
                commandDatabase.Parameters.AddWithValue("@AttendanceInDays", Convert.ToInt32(tb_Attendanceindays.Text));
                commandDatabase.Parameters.AddWithValue("@Basic_Salary_Per_Month", Convert.ToDouble(tb_Basicsalarypermonth.Text));
                commandDatabase.Parameters.AddWithValue("@Transportation_Allowance_Per_Day", Convert.ToDouble(tb_Transpoallowanceperday.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Transportation_Allowance", Convert.ToDouble(tb_Totaltranspoallowance.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Overtime", Convert.ToDouble(tb_Totalovertime.Text));
                commandDatabase.Parameters.AddWithValue("@Overtime_Salary", Convert.ToDouble(tb_Overtimesalary.Text));
                commandDatabase.Parameters.AddWithValue("@Leave_in_days", Convert.ToInt32(tb_Sickorleaveindays.Text));
                commandDatabase.Parameters.AddWithValue("@Total_leave_salary", Convert.ToDouble(tb_Totalsickorleavesalary.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Salary", Convert.ToDouble(tb_Totalsalary.Text));
                commandDatabase.Parameters.AddWithValue("@Date_Salary", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Salary successfuly edited");
                // Succesfully updated

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSalary();
            }
        }
        private void editSalarytosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with salary id of " + tb_Salaryid.Text + " has been edited from salary table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteSalary(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_salary` where `SalaryID`=@SalaryID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@SalaryID", Convert.ToInt32(tb_Salaryid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Salary successfuly deleted");

                // Succesfully deleted

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSalary();
            }
        }
        private void deleteSalarytosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with salary id of " + tb_Salaryid.Text + " has been deleted from salary table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addDeductions(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_deductions (`IDNumber`, `SalaryID`, `Monthly_Withholding_Tax_Number`, `Witholding_Tax_Amount`, `SSSNumber`, `SSS_Contribution_Amount`, " +
                "`SSSLoanID`, `SSS_Initial_Loan_Amount`, `SSS_Loan_Deduction`, `Remaining_Months_For_SSS_Loan`,`HDMFContributionID`,`HDMF_Contribution_Amount`,`HDMFLoanID`," +
                "`HDMF_Initial_Loan_Amount`,`HDMF_Loan_Deduction`,`Remaining_Months_For_HDMF_Loan`,`Philhealth_Insurance_Corporation_Contribution_Cost`,`Remaining_Debt`,`Vale`," +
                "`Other`,`Total_Lates_In_Minutes`,`Late_Deduction_Cost`,`Total_Deductions`,`Date_Deduction`) VALUES (@IDNumber, @SalaryID, " +
                "@Monthly_Withholding_Tax_Number, @Witholding_Tax_Amount, @SSSNumber, @SSS_Contribution_Amount, " +
                "@SSSLoanID, @SSS_Initial_Loan_Amount, @SSS_Loan_Deduction, @Remaining_Months_For_SSS_Loan,@HDMFContributionID,@HDMF_Contribution_Amount, @HDMFLoanID, " +
                "@HDMF_Initial_Loan_Amount,@HDMF_Loan_Deduction, @Remaining_Months_For_HDMF_Loan, @Philhealth_Insurance_Corporation_Contribution_Cost,@Remaining_Debt," +
                "@Vale,@Other,@Total_Lates_In_Minutes,@Late_Deduction_Cost,@Total_Deductions,@Date_Deduction)";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDNumber2.Text));
                commandDatabase.Parameters.AddWithValue("@SalaryID", Convert.ToInt32(tb_Salaryid1.Text));
                commandDatabase.Parameters.AddWithValue("@Monthly_Withholding_Tax_Number", Convert.ToInt32(tb_Monthlywithholdingtaxnumber.Text));
                commandDatabase.Parameters.AddWithValue("@Witholding_Tax_Amount", Convert.ToDouble(tb_Withholdingtaxamount.Text));
                commandDatabase.Parameters.AddWithValue("@SSSNumber", Convert.ToInt32(tb_SSSnumber.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Contribution_Amount", Convert.ToDouble(tb_SSSContributionAmount.Text));
                commandDatabase.Parameters.AddWithValue("@SSSLoanID", Convert.ToInt32(tb_SSSloanid.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Initial_Loan_Amount", Convert.ToDouble(tb_SSSinitialloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Loan_Deduction", Convert.ToDouble(tb_SSSloandeduction.Text));
                commandDatabase.Parameters.AddWithValue("@Remaining_Months_For_SSS_Loan", Convert.ToInt32(tb_Remainingmonthsforsssloan.Text));
                commandDatabase.Parameters.AddWithValue("@HDMFContributionID", Convert.ToInt32(tb_HDMFcontributionid.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Contribution_Amount", Convert.ToDouble(tb_HDMFcontributionamount.Text));

                commandDatabase.Parameters.AddWithValue("@HDMFLoanID", Convert.ToInt32(tb_HDMFloanid.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Initial_Loan_Amount", Convert.ToDouble(tb_HDMFinitialloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Loan_Deduction", Convert.ToDouble(tb_HDMFloandeduction.Text));
                commandDatabase.Parameters.AddWithValue("@Remaining_Months_For_HDMF_Loan", Convert.ToInt32(tb_RemainingmonthsforHDMFloan.Text));
                commandDatabase.Parameters.AddWithValue("@Philhealth_Insurance_Corporation_Contribution_Cost", Convert.ToDouble(tb_Philhealthcontributioncost.Text));
                commandDatabase.Parameters.AddWithValue("@Remaining_Debt", Convert.ToDouble(tb_Remainingdebt.Text));
                commandDatabase.Parameters.AddWithValue("@Vale", Convert.ToDouble(tb_Vale.Text));
                commandDatabase.Parameters.AddWithValue("@Other", Convert.ToDouble(tb_Other.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Lates_In_Minutes", Convert.ToDouble(tb_Latedeductioninminutes.Text));
                commandDatabase.Parameters.AddWithValue("@Late_Deduction_Cost", Convert.ToDouble(tb_Latedeductioncost.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Deductions", Convert.ToDouble(tb_Totaldeductions.Text));
                commandDatabase.Parameters.AddWithValue("@Date_Deduction", DateTime.Now);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Deduction successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listDeductions();
            }
        }
        private void addDeductionstosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with id number of " + tb_IDNumber2.Text + " has been added to deduction table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editDeductions(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_deductions` SET `IDNumber`=@IDNumber, `SalaryID`=@SalaryID, `Monthly_Withholding_Tax_Number`=@Monthly_Withholding_Tax_Number, `Witholding_Tax_Amount`=@Witholding_Tax_Amount, " +
                "`SSSNumber`=@SSSNumber, `SSS_Contribution_Amount`=@SSS_Contribution_Amount, `SSSLoanID`=@SSSLoanID, `SSS_Initial_Loan_Amount`=@SSS_Initial_Loan_Amount, " +
                "`SSS_Loan_Deduction`=@SSS_Loan_Deduction,`Remaining_Months_For_SSS_Loan`=@Remaining_Months_For_SSS_Loan,`HDMFContributionID`=@HDMFContributionID," +
                "`HDMF_Contribution_Amount`=@HDMF_Contribution_Amount,`HDMFLoanID`=@HDMFLoanID,`HDMF_Initial_Loan_Amount`=@HDMF_Initial_Loan_Amount," +
                "`HDMF_Loan_Deduction`=@HDMF_Loan_Deduction,`Remaining_Months_For_HDMF_Loan`=@Remaining_Months_For_HDMF_Loan," +
                "`Philhealth_Insurance_Corporation_Contribution_Cost`=@Philhealth_Insurance_Corporation_Contribution_Cost," +
                "`Remaining_Debt`=@Remaining_Debt,`Vale`=@Vale,`Other`=@Other,`Total_Lates_In_Minutes`=@Total_Lates_In_Minutes," +
                "`Late_Deduction_Cost`=@Late_Deduction_Cost,`Total_Deductions`=@Total_Deductions,`Date_Deduction`=@Date_Deduction where `DeductionID`=@DeductionID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@DeductionID", Convert.ToInt32(tb_Deductionid.Text));
                commandDatabase.Parameters.AddWithValue("@SalaryID", Convert.ToInt32(tb_Salaryid1.Text));
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDNumber2.Text));
                commandDatabase.Parameters.AddWithValue("@Monthly_Withholding_Tax_Number", Convert.ToInt32(tb_Monthlywithholdingtaxnumber.Text));
                commandDatabase.Parameters.AddWithValue("@Witholding_Tax_Amount", Convert.ToDouble(tb_Withholdingtaxamount.Text));
                commandDatabase.Parameters.AddWithValue("@SSSNumber", Convert.ToInt32(tb_SSSnumber.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Contribution_Amount", Convert.ToDouble(tb_SSSContributionAmount.Text));
                commandDatabase.Parameters.AddWithValue("@SSSLoanID", Convert.ToInt32(tb_SSSloanid.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Initial_Loan_Amount", Convert.ToDouble(tb_SSSinitialloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Loan_Deduction", Convert.ToDouble(tb_SSSloandeduction.Text));
                commandDatabase.Parameters.AddWithValue("@Remaining_Months_For_SSS_Loan", Convert.ToInt32(tb_Remainingmonthsforsssloan.Text));
                commandDatabase.Parameters.AddWithValue("@HDMFContributionID", Convert.ToInt32(tb_HDMFcontributionid.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Contribution_Amount", Convert.ToDouble(tb_HDMFcontributionamount.Text));

                commandDatabase.Parameters.AddWithValue("@HDMFLoanID", Convert.ToInt32(tb_HDMFloanid.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Initial_Loan_Amount", Convert.ToDouble(tb_HDMFinitialloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Loan_Deduction", Convert.ToDouble(tb_HDMFloandeduction.Text));
                commandDatabase.Parameters.AddWithValue("@Remaining_Months_For_HDMF_Loan", Convert.ToInt32(tb_RemainingmonthsforHDMFloan.Text));
                commandDatabase.Parameters.AddWithValue("@Philhealth_Insurance_Corporation_Contribution_Cost", Convert.ToDouble(tb_Philhealthcontributioncost.Text));
                commandDatabase.Parameters.AddWithValue("@Remaining_Debt", Convert.ToDouble(tb_Remainingdebt.Text));
                commandDatabase.Parameters.AddWithValue("@Vale", Convert.ToDouble(tb_Vale.Text));
                commandDatabase.Parameters.AddWithValue("@Other", Convert.ToDouble(tb_Other.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Lates_In_Minutes", Convert.ToDouble(tb_Latedeductioninminutes.Text));
                commandDatabase.Parameters.AddWithValue("@Late_Deduction_Cost", Convert.ToDouble(tb_Latedeductioncost.Text));
                commandDatabase.Parameters.AddWithValue("@Total_Deductions", Convert.ToDouble(tb_Totaldeductions.Text));
                commandDatabase.Parameters.AddWithValue("@Date_Deduction", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Deduction successfuly edited");

                // Succesfully updated

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listDeductions();
            }
        }
        private void editDeductionstosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with deduction id of " + tb_Deductionid.Text + " has been edited from deduction table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteDeductions(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_deductions` where `DeductionID`=@DeductionID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@DeductionID", Convert.ToInt32(tb_Deductionid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Deduction successfuly deleted");

                // Succesfully deleted

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listDeductions();
            }
        }
        private void deleteDeductionstosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with deduction id of " + tb_Deductionid.Text + " has been deleted from deduction table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addJournal(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_payrolljournal (`IDNumber`, `SalaryID`, `DeductionID`,`Net_Salary`, `Pay_Date`) " +
                "VALUES (@IDNumber, @SalaryID, @DeductionID, @Net_Salary, @Pay_Date)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDnumber3.Text));
                commandDatabase.Parameters.AddWithValue("@SalaryID", Convert.ToInt32(tb_Salaryid2.Text));
                commandDatabase.Parameters.AddWithValue("@DeductionID", Convert.ToInt32(tb_Deductionid1.Text));
                commandDatabase.Parameters.AddWithValue("@Net_Salary", Convert.ToDouble(tb_Netsalary.Text));
                commandDatabase.Parameters.AddWithValue("@Pay_Date", DateTime.Now);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Journal successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listJournal();
            }
        }
        private void addJournaltosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with id number of " + tb_IDnumber3.Text + " has been added to journal table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editJournal(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_payrolljournal` SET `IDNumber`=@IDNumber, `SalaryID`=@SalaryID, `DeductionID`=@DeductionID, " +
                "`Net_Salary`=@Net_Salary, `Pay_Date`=@Pay_Date where `ParyrollJournalID`=@ParyrollJournalID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@ParyrollJournalID", Convert.ToInt32(tb_Journalid.Text));
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_IDnumber3.Text));
                commandDatabase.Parameters.AddWithValue("@SalaryID", Convert.ToInt32(tb_Salaryid2.Text));
                commandDatabase.Parameters.AddWithValue("@DeductionID", Convert.ToInt32(tb_Deductionid1.Text));
                commandDatabase.Parameters.AddWithValue("@Net_Salary", Convert.ToDouble(tb_Netsalary.Text));
                commandDatabase.Parameters.AddWithValue("@Pay_Date", DateTime.Now);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Journal successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listJournal();
            }
        }
        private void editJournaltosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with journal id of " + tb_Journalid.Text + " has been edited from journal table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteJournal(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_payrolljournal` where `ParyrollJournalID`=@ParyrollJournalID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@ParyrollJournalID", Convert.ToInt32(tb_Journalid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Journal successfuly deleted");

                // Succesfully deleted

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listJournal();
            }
        }
        private void deleteJournaltosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with journal id of " + tb_Journalid.Text + " has been deleted from journal table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addBIR(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_withholdingtax (`Tax_Rate`, `minrangeoftaxableincome`, `maxrangeoftaxableincome`)" +
              "VALUES (@Tax_Rate, @minrangeoftaxableincome, @maxrangeoftaxableincome)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Tax_Rate", Convert.ToDouble(tb_Taxrate.Text));
                commandDatabase.Parameters.AddWithValue("@minrangeoftaxableincome", tb_minrangeoftaxableincome.Text);
                commandDatabase.Parameters.AddWithValue("@maxrangeoftaxableincome", tb_maxrangeoftaxableincome.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Withholding Tax successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listWithholdingtax();
            }
        }
        private void addWithholdingtaxtosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to withholding tax table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editBIR(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_withholdingtax` SET `Tax_Rate`=@Tax_Rate, `minrangeoftaxableincome`=@minrangeoftaxableincome, `maxrangeoftaxableincome`=@maxrangeoftaxableincome where `Monthly_Withholding_Tax_Number`=@Monthly_Withholding_Tax_Number";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Monthly_Withholding_Tax_Number", Convert.ToInt32(tb_Monthlywithholdingtaxnumber1.Text));
                commandDatabase.Parameters.AddWithValue("@Tax_Rate", Convert.ToDouble(tb_Taxrate.Text));
                commandDatabase.Parameters.AddWithValue("@minrangeoftaxableincome", tb_minrangeoftaxableincome.Text);
                commandDatabase.Parameters.AddWithValue("@maxrangeoftaxableincome", tb_maxrangeoftaxableincome.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Withholding Tax successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listWithholdingtax();
            }
        }
        private void editWithholdingtaxtosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with tax number of " + tb_Monthlywithholdingtaxnumber1.Text + " has been edited from withholding tax table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteBIR(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_withholdingtax` where `Monthly_Withholding_Tax_Number`=@Monthly_Withholding_Tax_Number";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Monthly_Withholding_Tax_Number", Convert.ToInt32(tb_Monthlywithholdingtaxnumber1.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                MessageBox.Show("Withholding Tax successfuly edited");

                // Succesfully deleted

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Ops, maybe the id doesn't exists ?
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listWithholdingtax();
            }
        }
        private void deleteWithholdingtaxtosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with tax number of " + tb_Monthlywithholdingtaxnumber1.Text + " has been deleted from withholding tax table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addSSSContribution(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_ssscontribution (`minrangeofcompensation`, `maxrangeofcompensation`, `Monthly_Salary_Credit`, `Employee_Contribution`)" +
             "VALUES (@minrangeofcompensation,@maxrangeofcompensation, @Monthly_Salary_Credit,  @Employee_Contribution)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@minrangeofcompensation", Convert.ToDouble(tb_minrangeofcompensation.Text));
                commandDatabase.Parameters.AddWithValue("@maxrangeofcompensation", Convert.ToDouble(tb_maxrangeofcompensation.Text));
                commandDatabase.Parameters.AddWithValue("@Monthly_Salary_Credit", Convert.ToDouble(tb_Monthlysalarycredit.Text));
                commandDatabase.Parameters.AddWithValue("@Employee_Contribution", Convert.ToDouble(tb_Employeecontribution.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("SSS contribution successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSSSContribution();
            }
        }
        private void addSSSContributiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to SSS contribution table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editSSSContribution(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_ssscontribution` SET `minrangeofcompensation`=@minrangeofcompensation, `maxrangeofcompensation`=@maxrangeofcompensation, `Monthly_Salary_Credit`=@Monthly_Salary_Credit, `Employee_Contribution`=@Employee_Contribution  " +
                "where `SSSNumber`=@SSSNumber";


                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@SSSNumber", Convert.ToInt32(tb_SSSnumber1.Text));
                commandDatabase.Parameters.AddWithValue("@minrangeofcompensation", Convert.ToDouble(tb_minrangeofcompensation.Text));
                commandDatabase.Parameters.AddWithValue("@maxrangeofcompensation", Convert.ToDouble(tb_maxrangeofcompensation.Text));
                commandDatabase.Parameters.AddWithValue("@Monthly_Salary_Credit", Convert.ToDouble(tb_Monthlysalarycredit.Text));
                commandDatabase.Parameters.AddWithValue("@Employee_Contribution", Convert.ToDouble(tb_Employeecontribution.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("SSS contribution successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSSSContribution();
            }
        }
        private void editSSSContributiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with SSS number of " + tb_SSSnumber1.Text + " has been edited from SSS contribution table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteSSSContribution(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_ssscontribution` where `SSSNumber`=@SSSNumber";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@SSSNumber", Convert.ToInt32(tb_SSSnumber1.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("SSS contribution successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSSSContribution();
            }
        }
        private void deleteSSSContributiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with SSS number of " + tb_SSSnumber1.Text + " has been deleted from SSS contribution table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addHDMFcontribution(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_hdmfcontribution (`Employee_Share`, `minrangeofmonthlycompensation`, `maxrangeofmonthlycompensation`)" +
             "VALUES (@Employee_Share, @minrangeofmonthlycompensation, @maxrangeofmonthlycompensation)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Employee_Share", Convert.ToDouble(tb_Employeeshare.Text));
                commandDatabase.Parameters.AddWithValue("@minrangeofmonthlycompensation", Convert.ToDouble(tb_minrangeofmonthlycompensation.Text));
                commandDatabase.Parameters.AddWithValue("@maxrangeofmonthlycompensation", Convert.ToDouble(tb_maxrangeofmonthlycompensation.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("HDMF contribution successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listHDMFContribution();
            }
        }
        private void addHDMFContributiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to HDMF contribution table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editHDMFcontribution(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_hdmfcontribution` SET `Employee_Share`=@Employee_Share, `minrangeofmonthlycompensation`=@minrangeofmonthlycompensation, `maxrangeofmonthlycompensation`=@maxrangeofmonthlycompensation where `HDMFContributionID`=@HDMFContributionID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@HDMFContributionID", Convert.ToInt32(tb_HDMFcontributionid1.Text));
                commandDatabase.Parameters.AddWithValue("@Employee_Share", Convert.ToDouble(tb_Employeeshare.Text));
                commandDatabase.Parameters.AddWithValue("@minrangeofmonthlycompensation", Convert.ToDouble(tb_minrangeofmonthlycompensation.Text));
                commandDatabase.Parameters.AddWithValue("@maxrangeofmonthlycompensation", Convert.ToDouble(tb_maxrangeofmonthlycompensation.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("HDMF contribution successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listHDMFContribution();
            }
        }
        private void editHDMFContributiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with HDMF contribution of " + tb_HDMFcontributionid1.Text + " has been edited from HDMF contribution table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteHDMFcontribution(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_hdmfcontribution` where `HDMFContributionID`=@HDMFContributionID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@HDMFContributionID", Convert.ToInt32(tb_HDMFcontributionid1.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("HDMF contribution successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listHDMFContribution();
            }
        }
        private void deleteHDMFContributiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with HDMF contribution of " + tb_HDMFcontributionid1.Text + " has been deleted from HDMF contribution table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addSSSLoan(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_sssloaninterest (`minloanamount`, `maxloanamount`, `SSS_Annual_Interest_Rate`)" +
            "VALUES (@minloanamount, @maxloanamount, @SSS_Annual_Interest_Rate)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@minloanamount", Convert.ToDouble(tb_minloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@maxloanamount", Convert.ToDouble(tb_maxloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Annual_Interest_Rate", Convert.ToDouble(tb_Annualinterestrate.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("SSS loan successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSSSLoan();
            }

        }
        private void addSSSLoanntosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to SSS loan table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editSSSLoan(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_sssloaninterest` SET `minloanamount`=@minloanamount, `maxloanamount`=@maxloanamount,`SSS_Annual_Interest_Rate`=@SSS_Annual_Interest_Rate where `SSSLoanID`=@SSSLoanID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@SSSLoanID", Convert.ToInt32(tb_SSSloanid1.Text));
                commandDatabase.Parameters.AddWithValue("@minloanamount", Convert.ToDouble(tb_minloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@maxloanamount", Convert.ToDouble(tb_maxloanamount.Text));
                commandDatabase.Parameters.AddWithValue("@SSS_Annual_Interest_Rate", Convert.ToDouble(tb_Annualinterestrate.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("SSS loan successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSSSLoan();
            }
        }
        private void editSSSLoanntosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with SSS loan id of " + tb_SSSloanid1.Text + " has been edited from SSS loan table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteSSSLoan(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_sssloaninterest` where `SSSLoanID`=@SSSLoanID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@SSSLoanID", Convert.ToInt32(tb_SSSloanid1.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("SSS loan successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSSSLoan();
            }
        }
        private void deleteSSSLoanntosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with SSS loan id of " + tb_SSSloanid1.Text + " has been deleted from SSS loan table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addHDMFLoan(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_hdmfloaninterest (`HDMF_Annual_Interest_Rate`, `minfixpricingperiodinyears`, `maxfixpricingperiodinyears`)" +
             "VALUES (@HDMF_Annual_Interest_Rate, @minfixpricingperiodinyears, @maxfixpricingperiodinyears)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@HDMF_Annual_Interest_Rate", Convert.ToDouble(tb_HDMFannualinterestrate.Text));
                commandDatabase.Parameters.AddWithValue("@minfixpricingperiodinyears", Convert.ToInt32(tb_minfixpricingperiodinyears.Text));
                commandDatabase.Parameters.AddWithValue("@maxfixpricingperiodinyears", Convert.ToInt32(tb_maxfixpricingperiodinyears.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("HDMF loan successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listHDMFLoan();
            }
        }
        private void addHDMFLoanntosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to HDMF loan table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editHDMFLoan(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_hdmfloaninterest` SET `HDMF_Annual_Interest_Rate`=@HDMF_Annual_Interest_Rate, `minfixpricingperiodinyears`=@minfixpricingperiodinyears, `maxfixpricingperiodinyears`=@maxfixpricingperiodinyears where `HDMFLoanID`=@HDMFLoanID";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@HDMFLoanID", Convert.ToInt32(tb_HDMFloanid1.Text));
                commandDatabase.Parameters.AddWithValue("@HDMF_Annual_Interest_Rate", Convert.ToDouble(tb_HDMFannualinterestrate.Text));
                commandDatabase.Parameters.AddWithValue("@minfixpricingperiodinyears", Convert.ToInt32(tb_minfixpricingperiodinyears.Text));
                commandDatabase.Parameters.AddWithValue("@maxfixpricingperiodinyears", Convert.ToInt32(tb_maxfixpricingperiodinyears.Text));
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("HDMF loan successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listHDMFLoan();
            }
        }
        private void editHDMFLoanntosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with HDMF loan id of " + tb_HDMFloanid1.Text + " has been edited from HDMF loan table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteHDMFLoan(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_hdmfloaninterest` where `HDMFLoanID`=@HDMFLoanID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@HDMFLoanID", Convert.ToInt32(tb_HDMFloanid1.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("HDMF loan successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listHDMFLoan();
            }
        }
        private void deleteHDMFLoanntosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with HDMF loan id of " + tb_HDMFloanid1.Text + " has been deleted from HDMF loan table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addAccountType(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_accounttype (`Account_Type`) VALUES (@Account_Type)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Account_Type", tb_Accounttype.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Account type successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listAccountType();
            }
        }
        private void addAccountTypetosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to account type table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editAccountType(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_accounttype` SET `Account_Type`=@Account_Type where `Account_Type_ID`=@Account_Type_ID";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Account_Type_ID", Convert.ToInt32(tb_Accounttypeid.Text));
                commandDatabase.Parameters.AddWithValue("@Account_Type", tb_Accounttype.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Account type successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listAccountType();
            }
        }
        private void editAccountTypetosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with account type id of " + tb_Accounttypeid.Text + " has been edited from account type table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteAccountType(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_accounttype` where `Account_Type_ID`=@Account_Type_ID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Account_Type_ID", Convert.ToInt32(tb_Accounttypeid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("Account type successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listAccountType();
            }
        }
        private void deleteAccountTypetosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with account type id of " + tb_Accounttypeid.Text + " has been deleted from account type table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addJobposition(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_jobposition (`PositionName`) VALUES (@PositionName)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@PositionName", tb_Positionname.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Job position added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listJobPosition();
            }
        }
        private void addJobpositiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to job position table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editJobposition(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_jobposition` SET `PositionName`=@PositionName where `PositionID`=@PositionID";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@PositionID", Convert.ToInt32(tb_Positionid.Text));
                commandDatabase.Parameters.AddWithValue("@PositionName", tb_Positionname.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Job position successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listJobPosition();
            }
        }
        private void editJobpositiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with position id of " + tb_Positionid.Text + " has been edited from job position table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteJobposition(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_jobposition` where `PositionID`=@PositionID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@PositionID", Convert.ToInt32(tb_Positionid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("Job position successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listJobPosition();
            }
        }
        private void deleteJobpositiontosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with position id of " + tb_Positionid.Text + " has been deleted from job position table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addLeaveReport(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_leavereport (`IDNumber`,`Type_of_Leave`) VALUES (@IDNumber, @Type_of_Leave)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_Idnumber.Text));
                commandDatabase.Parameters.AddWithValue("@Type_of_Leave", tb_Typeofleave.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Leave report successfully added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listLeaveReport();
            }
        }
        private void addLeaveReporttosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to leave report table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editLeaveReport(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_leavereport` SET `IDNumber`=@IDNumber,`Type_of_Leave`=@Type_of_Leave where `LeaveReportID`=@LeaveReportID";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@LeaveReportID", Convert.ToInt32(tb_Leavereportid.Text));
                commandDatabase.Parameters.AddWithValue("@IDNumber", Convert.ToInt32(tb_Idnumber.Text));
                commandDatabase.Parameters.AddWithValue("@Type_of_Leave", tb_Typeofleave.Text);
                commandDatabase.CommandTimeout = 60;

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Leave report successfully edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listLeaveReport();
            }
        }
        private void editLeaveReporttosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with leave report id of " + tb_Leavereportid.Text + " has been edited from leave report table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteLeaveReport(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_leavereport` where `LeaveReportID`=@LeaveReportID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@LeaveReportID", Convert.ToInt32(tb_Leavereportid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("Leave report deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listLeaveReport();
            }
        }
        private void deleteLeaveReporttosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with leave report id of " + tb_Leavereportid.Text + " has been deleted from leave report table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void addRemarks(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_remarks (`description`) VALUES (@description)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@description", tb_Description.Text);
               

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Remarks successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listRemarks();
            }
        }
        private void addRemarkstosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row has been added to remarks table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void editRemarks(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_remarks` SET `description`=@description where `remarks_id`=@remarks_id";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@remarks_id", Convert.ToInt32(tb_Remarksid.Text));
                commandDatabase.Parameters.AddWithValue("@description", tb_Description.Text);
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Remarks successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listRemarks();
            }
        }
        private void editRemarkstosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with remarks id of " + tb_Remarksid.Text + " has been edited from remarks table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void deleteRemarks(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_remarks` where `Remarks_ID`=@Remarks_ID";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@Remarks_ID", Convert.ToInt32(tb_Remarksid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("Remarks successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listRemarks();
            }
        }
        private void deleteRemarkstosummaryrecord(object sender, RoutedEventArgs e)
        {
            MySqlCommand cm = new MySqlCommand();
            MySqlConnection cn = new MySqlConnection();
            MySqlDataReader dr;
            cn = new MySqlConnection(connectionString);
            cn.Open();
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`, `action_performed`, `date_stampt`) VALUES (@action_performed_by, @action_performed, @date_stampt)";
                string query2 = "select * from tbl_personalinformation where Username like ('" + label_Username.Content + "')";
                cm = new MySqlCommand(query2, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                tb_profile_id.Text = dr[0].ToString();

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_profile_id.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", "A row with remarks id of " + tb_Remarksid.Text + " has been deleted from remarks table");
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void btn_addSummary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO tbl_summaryrecord (`action_performed_by`,`action_performed`, `date_stampt`) " +
                "VALUES (@action_performed_by, @action_performed, @date_stampt)";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_actionperofmedby.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", tb_action_performed.Text);
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Summary successfuly added");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void btn_editSummary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE `tbl_summaryrecord` SET `action_performed_by`=@action_performed_by, `action_performed`=@action_performed, `date_stampt`=@date_stampt where `summaryrecord_id`=@summaryrecord_id";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@summaryrecord_id", Convert.ToInt32(tb_Recordid.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed_by", Convert.ToInt32(tb_actionperofmedby.Text));
                commandDatabase.Parameters.AddWithValue("@action_performed", tb_action_performed.Text);
                commandDatabase.Parameters.AddWithValue("@date_stampt", DateTime.Now);

                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Summary successfuly edited");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }
        private void btn_deleteSummary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM `tbl_summaryrecord` where `summaryrecord_id`=@summaryrecord_id";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@summaryrecord_id", Convert.ToInt32(tb_Recordid.Text));
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;


                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                MessageBox.Show("Summary successfuly deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listSummary();
            }
        }

        //buttons for calculation
        private void getinitialsalary(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_Attendanceindays.Text);
                double b = Double.Parse(tb_Transpoallowanceperday.Text);
                double c = Double.Parse(tb_Basicsalaryperday.Text);
                double f = (c / 8) * 1.25; //Eto ung over time rate per hour
                double g = Double.Parse(tb_Totalovertime.Text);


                tb_Totaltranspoallowance.Text = (a * b).ToString();
                tb_Basicsalarypermonth.Text = (a *c).ToString();
                tb_Overtimesalary.Text = (f * g).ToString();

            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }
        }
        private void getsickorleavetotalsalary(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_Sickorleaveindays.Text);
                double b = Double.Parse(tb_Basicsalaryperday.Text);

                tb_Totalsickorleavesalary.Text = (a * b).ToString();

            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }
        }
        private void gettotalsalary(object sender, RoutedEventArgs e)
        {
            try
            {
                double h = Double.Parse(tb_Basicsalarypermonth.Text);
                double i = Double.Parse(tb_Totaltranspoallowance.Text);
                double j = Double.Parse(tb_Overtimesalary.Text);
                double k = Double.Parse(tb_Totalsickorleavesalary.Text);
                tb_Totalsalary.Text = (h + i + j + k).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }
        }
        private void getsalaryid(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select SalaryID, max(SalaryID) from tbl_salary WHERE IDNumber like('"+ tb_IDNumber2.Text + "')";



                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_Salaryid1.Text = reader[1].ToString();
                        
                    }
                }
              

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void getbasicsalaryperdayandmonth(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_salary where SalaryID like ('" + tb_Salaryid1.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_Basicsalaryperday1.Text = reader[2].ToString();
                        tb_Basicsalarypermonth1.Text = reader[4].ToString();
                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gettaxnumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from  tbl_withholdingtax where minrangeoftaxableincome <= ('" + Convert.ToDouble(tb_Basicsalarypermonth1.Text) + "') and maxrangeoftaxableincome >= ('" + Convert.ToDouble(tb_Basicsalarypermonth1.Text) + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_Monthlywithholdingtaxnumber.Text = reader[0].ToString();
                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);



            }
        }
        private void getsssnumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from  tbl_ssscontribution where minrangeofcompensation <= ('" + Convert.ToDouble(tb_Basicsalarypermonth1.Text) + "') and maxrangeofcompensation >= ('" + Convert.ToDouble(tb_Basicsalarypermonth1.Text) + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_SSSnumber.Text = reader[0].ToString();
                    }
                }
                

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);



            }

        }
        private void getsssloanid(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from   tbl_sssloaninterest where minloanamount <= ('" + Convert.ToDouble(tb_SSSinitialloanamount.Text) + "') and maxloanamount >= ('" + Convert.ToDouble(tb_SSSinitialloanamount.Text) + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_SSSloanid.Text = reader[0].ToString();
                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void gethdmfcontributionid(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from  tbl_hdmfcontribution where minrangeofmonthlycompensation <= ('" + Convert.ToDouble(tb_Basicsalarypermonth1.Text) + "') and maxrangeofmonthlycompensation >= ('" + Convert.ToDouble(tb_Basicsalarypermonth1.Text) + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_HDMFcontributionid.Text = reader[0].ToString();
                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }
        private void gethdmfloanid(object sender, RoutedEventArgs e)
        {
            try { 

            string query = "select * from tbl_hdmfloaninterest where minfixpricingperiodinyears	 <= ('" + Convert.ToInt32(tb_Fixedpricingperiodinyears1.Text) + "') and maxfixpricingperiodinyears >= ('" + Convert.ToInt32(tb_Fixedpricingperiodinyears1.Text) + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            databaseConnection.Open();
            reader = commandDatabase.ExecuteReader();
            // Success, now list 
            // If there are available rows
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                        tb_HDMFloanid.Text = reader[0].ToString();
                }
            }
            databaseConnection.Close();
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void getphilhealthcontributioncost(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_Basicsalarypermonth1.Text);
                double b = 0.0275;

                tb_Philhealthcontributioncost.Text = (a * b).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void getlatedeductioncost(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_Basicsalaryperday1.Text);
                double b = 8;
                double c = 60;
                double d = Double.Parse(tb_Latedeductioninminutes.Text);

                tb_Latedeductioncost.Text = (((a / b) / c) * d).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void gettaxrate(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_withholdingtax where Monthly_Withholding_Tax_Number like ('" + tb_Monthlywithholdingtaxnumber.Text + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_taxrate.Text = reader[1].ToString();
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void getwithholdingtaxamount(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_Basicsalarypermonth1.Text);
                double b = Double.Parse(tb_taxrate.Text);

                tb_Withholdingtaxamount.Text = (a * b).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void getcontributionamount(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_ssscontribution where SSSNumber like ('" + tb_SSSnumber.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_SSSContributionAmount.Text = reader[4].ToString();
                    }
                }
              

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void getannualinterestrate(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_sssloaninterest where SSSLoanID like ('" + tb_SSSloanid.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_annualinterestrate.Text = reader[3].ToString();
                    }
                }
                

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);



            }
        }
        private void getsssloandeduction(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_SSSinitialloanamount.Text);
                double b = Double.Parse(tb_annualinterestrate.Text);
                double c = 12;
                tb_SSSloandeduction.Text = (((a * b) / c) + (a / c)).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void getemployeeshare(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_hdmfcontribution where HDMFContributionID like ('" + tb_HDMFcontributionid.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_employeeshare.Text = reader[1].ToString();
                    }
                }
              

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gethdmfcontributionamount(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_Basicsalarypermonth1.Text);
                double b = Double.Parse(tb_employeeshare.Text);
                tb_HDMFcontributionamount.Text = (a * b).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void gethdmfannualinterestrate(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_hdmfloaninterest where HDMFLoanID like ('" + tb_HDMFloanid.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_hdmfannualinterestrate.Text = reader[1].ToString();
                    }
                }
                

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gethdmfloandeduction(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_HDMFinitialloanamount.Text);
                double b = Double.Parse(tb_hdmfannualinterestrate.Text);
                double c = 12;
                tb_HDMFloandeduction.Text = (((a * b) / c) + (a / c)).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void gettotaldeduction(object sender, RoutedEventArgs e)
        {
            try
            {

                double b = Double.Parse(tb_Withholdingtaxamount.Text);
                double c = Double.Parse(tb_SSSContributionAmount.Text);
                double d = Double.Parse(tb_SSSloandeduction.Text);
                double f = Double.Parse(tb_HDMFcontributionamount.Text);
                double g = Double.Parse(tb_HDMFloandeduction.Text);
                double h = Double.Parse(tb_Philhealthcontributioncost.Text);
                double i = Double.Parse(tb_Remainingdebt.Text);
                double j = Double.Parse(tb_Vale.Text);
                double k = Double.Parse(tb_Other.Text);
                double l = Double.Parse(tb_Latedeductioncost.Text);
                tb_Totaldeductions.Text = (b + c + d + f + g + h + i + j + k + l).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }
        private void getsalaryid1(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select SalaryID, max(SalaryID) from tbl_salary WHERE IDNumber like('" + tb_IDnumber3.Text + "')";



                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_Salaryid2.Text = reader[1].ToString();

                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void getdeductionid(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select DeductionID, max(DeductionID) from tbl_deductions WHERE IDNumber like('" + tb_IDnumber3.Text + "')";



                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_Deductionid1.Text = reader[1].ToString();

                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void gettotalsalary1(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_salary where SalaryID like ('" + tb_Salaryid2.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_totalsalary.Text = reader[11].ToString();
                        
                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gettotaldeduction1(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from tbl_deductions where DeductionID like ('" + tb_Deductionid1.Text + "')";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 
                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tb_totaldeductions.Text = reader[23].ToString();

                    }
                }


                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void getnetsalary(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = Double.Parse(tb_totalsalary.Text);
                double b = Double.Parse(tb_totaldeductions.Text);

                tb_Netsalary.Text = (a - b).ToString();
            }
            catch (Exception ex)
            {
                //// Show any error message.
                MessageBox.Show(ex.Message);

            }

        }

        //uploading image
        private void upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                profilepic.Source = new BitmapImage(fileUri);
                strName = openFileDialog.SafeFileName;
                imageName = openFileDialog.FileName;
            }
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

        //search function
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Grid_PersonalInfo.Visibility == Visibility.Visible )
            {
                searchpersonalinfo();
                if (tb_search.Text == "")
                {
                    listPersonalInformation();
                }

            }

            else if (Grid_Salary.Visibility == Visibility.Visible)
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
        private void searchpersonalinfo()
        {

            string query = "SELECT * FROM  tbl_personalinformation WHERE IDNumber like ('" + tb_search.Text + "%') or Username like ('" + tb_search.Text + "%') or Password like ('" + tb_search.Text + "%')" +
                "or Account_Type_ID like ('" + tb_search.Text + "%') or PositionID like ('" + tb_search.Text + "%') or Surname like ('" + tb_search.Text + "%')" +
                "or GivenName like ('" + tb_search.Text + "%') or MiddleName like ('" + tb_search.Text + "%') or Birthdate like ('" + tb_search.Text + "%') or Email like ('" + tb_search.Text + "%')" +
                "or Address like ('" + tb_search.Text + "%') or TIN like ('" + tb_search.Text + "%') or remarks_id like ('" + tb_search.Text + "%')";


            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtCashier = new DataTable();
                dtCashier.Load(reader);
                dataGrid_PersonalInfo.ItemsSource = dtCashier.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           


        }
        private void searchsalary()
        {

            string query = "SELECT * FROM  tbl_salary WHERE SalaryID like ('" + tb_search.Text + "%') or IDNumber like ('" + tb_search.Text + "%') or Basic_Salary_Per_Day like ('" + tb_search.Text + "%')" +
                "or AttendanceInDays like ('" + tb_search.Text + "%') or Basic_Salary_Per_Month like ('" + tb_search.Text + "%') or Transportation_Allowance_Per_Day like ('" + tb_search.Text + "%') or Total_Transportation_Allowance like ('" + tb_search.Text + "%') " +
                "or SalaryID like ('" + tb_search.Text + "%') or Overtime_Salary like ('" + tb_search.Text + "%') or Leave_in_days like ('" + tb_search.Text + "%') or Total_leave_salary like ('" + tb_search.Text + "%')" +
                "or Total_Salary like ('" + tb_search.Text + "%') or MONTH(Date_Salary) like ('" + tb_search.Text + "%') or DAY(Date_Salary) like ('" + tb_search.Text + "%') or YEAR(Date_Salary) like ('" + tb_search.Text + "%')";
           

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {

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

            string query = "SELECT * FROM  tbl_deductions WHERE DeductionID like ('" + tb_search.Text + "%') or IDNumber like ('" + tb_search.Text + "%') or SalaryID like ('" + tb_search.Text + "%')" +
                "or Monthly_Withholding_Tax_Number like ('" + tb_search.Text + "%') or Witholding_Tax_Amount like ('" + tb_search.Text + "%') or SSSNumber like ('" + tb_search.Text + "%') or SSS_Contribution_Amount like ('" + tb_search.Text + "%') " +
                "or SSSLoanID like ('" + tb_search.Text + "%') or SSS_Initial_Loan_Amount like ('" + tb_search.Text + "%') or SSS_Loan_Deduction like ('" + tb_search.Text + "%') or Remaining_Months_For_SSS_Loan like ('" + tb_search.Text + "%')" +
                "or HDMFContributionID like ('" + tb_search.Text + "%') or HDMFLoanID like ('" + tb_search.Text + "%') or HDMF_Initial_Loan_Amount like ('" + tb_search.Text + "%') or HDMF_Loan_Deduction like ('" + tb_search.Text + "%') " +
                "or Remaining_Months_For_HDMF_Loan like ('" + tb_search.Text + "%') or Philhealth_Insurance_Corporation_Contribution_Cost like ('" + tb_search.Text + "%') or Remaining_Debt like ('" + tb_search.Text + "%') or Vale like ('" + tb_search.Text + "%')" +
                " or Other like ('" + tb_search.Text + "%')  or Total_Lates_In_Minutes like ('" + tb_search.Text + "%')  or Late_Deduction_Cost like ('" + tb_search.Text + "%') or Total_Deductions like ('" + tb_search.Text + "%') " +
                "or MONTH(Date_Deduction) like ('" + tb_search.Text + "%') or DAY(Date_Deduction) like ('" + tb_search.Text + "%') or YEAR(Date_Deduction) like ('" + tb_search.Text + "%')";


            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {

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

            string query = "SELECT * FROM  tbl_payrolljournal WHERE ParyrollJournalID like ('" + tb_search.Text + "%') or IDNumber like ('" + tb_search.Text + "%') or SalaryID like ('" + tb_search.Text + "%')" +
                "or DeductionID like ('" + tb_search.Text + "%') or Net_Salary like ('" + tb_search.Text + "%') or MONTH(Pay_Date) like ('" + tb_search.Text + "%') or " +
                "DAY(Pay_Date) like ('" + tb_search.Text + "%') or YEAR(Pay_Date) like ('" + tb_search.Text + "%')";


            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {

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
        private void btn_printpayslip_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int payslipid = Convert.ToInt32(tb_payslipid.Text);
                Payslip payslipwindow = new Payslip(payslipid);
                if (tb_payslipid.Text == "")
                {
                    MessageBox.Show("Please select items you want to print");
                }
                else
                {
                    payslipwindow.Show();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }



    }
}





