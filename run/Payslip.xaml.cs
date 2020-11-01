using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Payslip.xaml
    /// </summary>
    public partial class Payslip : Window
    {
        //SQL connection
        const string connectionString = @"server=localhost; user id=root;password=; database=db_payrollsystem;";
        string employeename = "";
        double basicsalarypermonth = 0;
        double totaltranspo = 0;
        double overtimesalary = 0;
        double totalleavesalary = 0;
        double totalsalary = 0;

        double wtax = 0;
        double sss = 0;
        double sssloan = 0;
        double hdmf = 0;
        double hdmfloan = 0;
        double philhealth = 0;
        double remaingdebt = 0;
        double vale = 0;
        double others = 0;
        double latedeductions = 0;
        double totaldeductions = 0;
        double netsalary = 0;
        string journaldate = "";
        string date = "";

        public Payslip(int payslipid)
        {
            InitializeComponent();
            label_Journalid.Content = payslipid;
            initializeData();
            loadDataToPayslip();
        }

        public void initializeData()
        {

            try
            {       
                string query = "SELECT CONCAT (a.Surname,',',' ',a.GivenName,' ',a.MiddleName) as Name, b.Basic_Salary_Per_Month, b.Total_Transportation_Allowance, b.Overtime_Salary, b.Total_leave_salary , b.Total_Salary, c.Witholding_Tax_Amount," +
                    "c.SSS_Contribution_Amount, c.SSS_Loan_Deduction, c.HDMF_Contribution_Amount, c.HDMF_Loan_Deduction, c.Philhealth_Insurance_Corporation_Contribution_Cost," +
                    "c.Remaining_Debt, c.Vale, c.Other, c.Late_Deduction_Cost, c.Total_Deductions, d.Net_Salary, d.Pay_Date  FROM tbl_payrolljournal d JOIN tbl_deductions c ON d.DeductionID = c.DeductionID " +
                    "JOIN tbl_salary b ON c.SalaryID = b.SalaryID JOIN tbl_personalinformation a ON b.IDNumber = a.IDNumber where d.ParyrollJournalID like ('" + Convert.ToString(label_Journalid.Content) + "')";


                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader;
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                DataTable dtPayslip = new DataTable();

          


                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        employeename = reader[0].ToString();
                        basicsalarypermonth = Convert.ToDouble(reader[1]);
                        totaltranspo = Convert.ToDouble(reader[2]);
                        overtimesalary = Convert.ToDouble(reader[3]);
                        totalleavesalary = Convert.ToDouble(reader[4]);
                        totalsalary = Convert.ToDouble(reader[5]);

                        wtax = Convert.ToDouble(reader[6]);
                        sss = Convert.ToDouble(reader[7]);
                        sssloan = Convert.ToDouble(reader[8]);
                        hdmf = Convert.ToDouble(reader[9]);
                        hdmfloan = Convert.ToDouble(reader[10]);
                        philhealth = Convert.ToDouble(reader[11]);
                        remaingdebt = Convert.ToDouble(reader[12]);
                        vale = Convert.ToDouble(reader[13]);
                        others = Convert.ToDouble(reader[14]);
                        latedeductions = Convert.ToDouble(reader[15]);
                        totaldeductions = Convert.ToDouble(reader[16]);
                        netsalary = Convert.ToDouble(reader[17]);
                        journaldate = reader[18].ToString();
                    }
                }

               



                databaseConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        public void loadDataToPayslip()
        {
            for (int i = 0; i < 11; i++)
            {
                int length = journaldate.Length;
                int number = journaldate.Length - 1;
                String store;
                if (length > 0)
                {

                    StringBuilder sb = new StringBuilder(journaldate);
                    sb.Remove(number, 1);
                    store = sb.ToString();
                    journaldate = store;
                    date = journaldate;
                }
            }

            Paragraph para1 = new Paragraph();
            Paragraph para2 = new Paragraph();
            Paragraph para3 = new Paragraph();
            Paragraph para4 = new Paragraph();
            Paragraph para5 = new Paragraph();
            Paragraph para6 = new Paragraph();
            Paragraph para7 = new Paragraph();
            Paragraph para8 = new Paragraph();
            Paragraph para9 = new Paragraph();
            Paragraph para10 = new Paragraph();
            Paragraph para11 = new Paragraph();
            Paragraph para12 = new Paragraph();
            Paragraph para13 = new Paragraph();
            Paragraph para14 = new Paragraph();
            Paragraph para15 = new Paragraph();
            Paragraph para16 = new Paragraph();
            Paragraph para17 = new Paragraph();
            Paragraph para18 = new Paragraph();
            Paragraph paraa = new Paragraph();
            Paragraph parab = new Paragraph();
            Paragraph parac = new Paragraph();
            Paragraph parad = new Paragraph();
            Paragraph parae = new Paragraph();
            Paragraph paraf = new Paragraph();
            Paragraph parag = new Paragraph();
            Paragraph parah = new Paragraph();
            Paragraph parai = new Paragraph();
            Paragraph paraj = new Paragraph();
            Paragraph parak = new Paragraph();

            para1.TextAlignment = TextAlignment.Left;
            para1.FontSize = 12;
            para1.Inlines.Add(new Run("Name: " + employeename));
            para1.Margin = new Thickness(5);


            para2.TextAlignment = TextAlignment.Left;
            para2.FontSize = 12;
            para2.Inlines.Add(new Run("Basic Salary for month: " + basicsalarypermonth.ToString()));
            para2.Margin = new Thickness(5);

            para3.TextAlignment = TextAlignment.Left;
            para3.FontSize = 12;
            para3.Inlines.Add(new Run("Transpo Allowance: " + totaltranspo.ToString()));
            para3.Margin = new Thickness(5);

            para4.TextAlignment = TextAlignment.Left;
            para4.FontSize = 12;
            para4.Inlines.Add(new Run("Overtime Salary: " + overtimesalary.ToString()));
            para4.Margin = new Thickness(5);

            para5.TextAlignment = TextAlignment.Left;
            para5.FontSize = 12;
            para5.Inlines.Add(new Run("Total Leave Salary: " + totalleavesalary.ToString()));
            para5.Margin = new Thickness(5);

            para6.TextAlignment = TextAlignment.Left;
            para6.FontSize = 12;
            para6.Inlines.Add(new Run("Total Salary: " + totalsalary.ToString()));
            para6.Margin = new Thickness(5);

            para7.TextAlignment = TextAlignment.Left;
            para7.FontSize = 12;
            para7.Inlines.Add(new Run("Withholding Tax: " + wtax.ToString()));
            para7.Margin = new Thickness(5);

            para8.TextAlignment = TextAlignment.Left;
            para8.FontSize = 12;
            para8.Inlines.Add(new Run("SSS Contribution: " + sss.ToString()));
            para8.Margin = new Thickness(5);

            para9.TextAlignment = TextAlignment.Left;
            para9.FontSize = 12;
            para9.Inlines.Add(new Run("SSS Loan: " + sssloan.ToString()));
            para9.Margin = new Thickness(5);

            para10.TextAlignment = TextAlignment.Left;
            para10.FontSize = 12;
            para10.Inlines.Add(new Run("HDMF Contribution: " + hdmf.ToString()));
            para10.Margin = new Thickness(5);

            para11.TextAlignment = TextAlignment.Left;
            para11.FontSize = 12;
            para11.Inlines.Add(new Run("HDMF Loan: " + hdmfloan.ToString()));
            para11.Margin = new Thickness(5);

            para12.TextAlignment = TextAlignment.Left;
            para12.FontSize = 12;
            para12.Inlines.Add(new Run("Philhealth: " + philhealth.ToString()));
            para12.Margin = new Thickness(5);

            para13.TextAlignment = TextAlignment.Left;
            para13.FontSize = 12;
            para13.Inlines.Add(new Run("Remaining Debt: " + remaingdebt.ToString()));
            para13.Margin = new Thickness(5);

            para14.TextAlignment = TextAlignment.Left;
            para14.FontSize = 12;
            para14.Inlines.Add(new Run("Vale: " + vale.ToString()));
            para14.Margin = new Thickness(5);

            para15.TextAlignment = TextAlignment.Left;
            para15.FontSize = 12;
            para15.Inlines.Add(new Run("Others: " + others.ToString()));
            para15.Margin = new Thickness(5);

            para16.TextAlignment = TextAlignment.Left;
            para16.FontSize = 12;
            para16.Inlines.Add(new Run("Late Deductions: " + latedeductions.ToString()));
            para16.Margin = new Thickness(5);

            para17.TextAlignment = TextAlignment.Left;
            para17.FontSize = 12;
            para17.Inlines.Add(new Run("Total Deductions: " + totaldeductions.ToString()));
            para17.Margin = new Thickness(5);

            para18.TextAlignment = TextAlignment.Left;
            para18.FontSize = 12;
            para18.Inlines.Add(new Run("Net Salary: " + netsalary.ToString()));
            para18.Margin = new Thickness(5);

            paraa.TextAlignment = TextAlignment.Left;
            paraa.FontSize = 16;
            paraa.Inlines.Add(new Run("EARNINGS"));
            paraa.Margin = new Thickness(5);

            parab.TextAlignment = TextAlignment.Left;
            parab.FontSize = 12;
            parab.Inlines.Add(new Run("Date: " + date.ToString()));
            parab.Margin = new Thickness(5);

            parac.TextAlignment = TextAlignment.Left;
            parac.FontSize = 12;
            parac.Inlines.Add(new Run("-------------------------------------------------------------------"));
            parac.Margin = new Thickness(5);

            parad.TextAlignment = TextAlignment.Left;
            parad.FontSize = 16;
            parad.Inlines.Add(new Run("DEDUCTIONS"));
            parad.Margin = new Thickness(5);

            parae.TextAlignment = TextAlignment.Left;
            parae.FontSize = 12;
            parae.Inlines.Add(new Run("-------------------------------------------------------------------"));
            parae.Margin = new Thickness(5);

            paraf.TextAlignment = TextAlignment.Left;
            paraf.FontSize = 16;
            paraf.Inlines.Add(new Run("NET INCOME"));
            paraf.Margin = new Thickness(5);

            parag.TextAlignment = TextAlignment.Left;
            parag.FontSize = 12;
            parag.Inlines.Add(new Run("-------------------------------------------------------------------"));
            parag.Margin = new Thickness(5);

            parah.TextAlignment = TextAlignment.Left;
            parah.FontSize = 12;
            parah.Inlines.Add(new Run("-------------------------------------------------------------------"));
            parah.Margin = new Thickness(5);

            parai.TextAlignment = TextAlignment.Left;
            parai.FontSize = 14;
            parai.Inlines.Add(new Run("Received By:"));
            parai.Margin = new Thickness(5);

            paraj.TextAlignment = TextAlignment.Left;
            paraj.FontSize = 14;
            paraj.Inlines.Add(new Run("Date Received:"));
            paraj.Margin = new Thickness(5);

            parak.TextAlignment = TextAlignment.Left;
            parak.FontSize = 14;
            parak.Inlines.Add(new Run("Signature Over Printed Name:"));
            parak.Margin = new Thickness(5);


            rtbPayslip.Document.Blocks.Add(parab);
            rtbPayslip.Document.Blocks.Add(para1);
            rtbPayslip.Document.Blocks.Add(parac);
            rtbPayslip.Document.Blocks.Add(paraa);
            rtbPayslip.Document.Blocks.Add(para2);
            rtbPayslip.Document.Blocks.Add(para3);
            rtbPayslip.Document.Blocks.Add(para4);
            rtbPayslip.Document.Blocks.Add(para5);
            rtbPayslip.Document.Blocks.Add(para6);
            rtbPayslip.Document.Blocks.Add(parae);
            rtbPayslip.Document.Blocks.Add(parad);
            rtbPayslip.Document.Blocks.Add(para7);
            rtbPayslip.Document.Blocks.Add(para8);
            rtbPayslip.Document.Blocks.Add(para9);
            rtbPayslip.Document.Blocks.Add(para10);
            rtbPayslip.Document.Blocks.Add(para11);
            rtbPayslip.Document.Blocks.Add(para12);
            rtbPayslip.Document.Blocks.Add(para13);
            rtbPayslip.Document.Blocks.Add(para14);
            rtbPayslip.Document.Blocks.Add(para15);
            rtbPayslip.Document.Blocks.Add(para16);
            rtbPayslip.Document.Blocks.Add(para17);
            rtbPayslip.Document.Blocks.Add(parag);
            rtbPayslip.Document.Blocks.Add(paraf);
            rtbPayslip.Document.Blocks.Add(para18);
            rtbPayslip.Document.Blocks.Add(parah);
            rtbPayslip.Document.Blocks.Add(parai);
            rtbPayslip.Document.Blocks.Add(paraj);
            rtbPayslip.Document.Blocks.Add(parak);
        }
            private void btn_printpayslip_Click(object sender, RoutedEventArgs e)
            {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    btn_printpayslip.Visibility = Visibility.Hidden;
                    btn_cancel.Visibility = Visibility.Hidden;
                    printDialog.PrintVisual(Grid_payslip, "Printing in process");
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {      
            this.Hide();
        }
    }
}
