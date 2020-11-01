//using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Stattic default login (doesn't based on db)
        string defaultEmployeeUsername = "admin";
        string defaultEmployeePassword = "admin";

        //SQL Connection
        const string connectionString = @"server=localhost; user id=root;password=; database=db_payrollsystem;";
        MySqlCommand cm = new MySqlCommand();
        MySqlConnection cn = new MySqlConnection();
        MySqlDataReader dr;

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
      
        public MainWindow()
        {
            InitializeComponent();
            cn = new MySqlConnection(connectionString);
            //opens database
            cn.Open();
            btn_Login_Admin.Click += Login_Admin;
            btn_signUp.Click += signup_Click;

        }

        //buttons for main function
        private void signup_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (tb_Username.IsEnabled == false)
                {

                    tb_Username.Focus();
                }
                else if (tb_Username.Text.Equals("") || pb_Password.Password.Equals(""))
                {
                    MessageBox.Show("Please complete all the necessary fields!");
                }

                else
                {
                    string hashedPassword = SHA512(pb_Password.Password);
                    string sql = " select * from tbl_personalinformation where Username like ('" + tb_Username.Text + "')";
                    cm = new MySqlCommand(sql, cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {     
                        if (tb_Username.Text == dr[1].ToString())
                        {
                            MessageBox.Show("Username was already taken, try other username");
                            dr.Close();
                        }
                    }
                    else
                    {
                        string query = "INSERT INTO tbl_personalinformation (`Username`, `Password`, `Account_Type_ID`, `PositionID`, `Surname`, `GivenName`, `MiddleName`, `Birthdate`,`Email`,`Address`,`TIN`,`remarks_id`)" +
                " VALUES (@Username, @Password, @Account_Type_ID, @PositionID, @Surname, @GivenName, @MiddleName, @Birthdate, @Email, @Address, @TIN, @remarks_id)";
                        MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                        MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                        commandDatabase.Parameters.AddWithValue("@Username", tb_Username.Text);
                        commandDatabase.Parameters.AddWithValue("@Password", hashedPassword);
                        commandDatabase.Parameters.AddWithValue("@Account_Type_ID", tb_AccountypeID.Text);
                        commandDatabase.Parameters.AddWithValue("@PositionID", tb_PositionID.Text);
                        commandDatabase.Parameters.AddWithValue("@Surname", tb_Surname.Text);
                        commandDatabase.Parameters.AddWithValue("@GivenName", tb_GivenName.Text);
                        commandDatabase.Parameters.AddWithValue("@MiddleName", tb_MiddleName.Text);
                        commandDatabase.Parameters.AddWithValue("@Birthdate", Convert.ToDateTime(tb_Birthdate.Text));
                        commandDatabase.Parameters.AddWithValue("@Email", tb_Email.Text);
                        commandDatabase.Parameters.AddWithValue("@Address", tb_Address.Text);
                        commandDatabase.Parameters.AddWithValue("@TIN", tb_TIN.Text);
                        commandDatabase.Parameters.AddWithValue("@remarks_id", Convert.ToInt32(tb_remarks_id.Text));
                        databaseConnection.Open();
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                        MessageBox.Show("User Successfully Created");
                        databaseConnection.Close();

                    }
                }
            }     catch (Exception)
                    {
                        MessageBox.Show("Invalid");
                    }

                
            
        }
        private void btn_EmployeeLogin_Click(object sender, RoutedEventArgs e)
        {
            String username = Employee_UserName.Text;
            Employee_Window employeewindow = new Employee_Window(username);
            try
            {
               
                string hashedPassword = SHA512(Employee_Password.Password);
                string sql = " select * from tbl_personalinformation where Username like ('" + Employee_UserName.Text + "') AND (Account_Type_ID = 1 or Account_Type_ID = 2)";
                cm = new MySqlCommand(sql, cn);
                dr = cm.ExecuteReader();
                dr.Read();

                if (Employee_UserName.Text == defaultEmployeeUsername && Employee_Password.Password == defaultEmployeePassword)
                {
                    employeewindow.Show();
                    //this.Hide();
                    dr.Close();
                }
                else if (Employee_UserName.Text == "" || Employee_Password.Password == "")
                {
                    MessageBox.Show("Please input username or password");
                }
                else if (dr.HasRows)
                {

                    if (Employee_UserName.Text == dr[1].ToString() && hashedPassword == dr[2].ToString())
                    {
                        employeewindow.Show();
                        this.Hide();
                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password");
                    }
                    }

                }
            


            catch (Exception)
            {
                MessageBox.Show("Incorrect Username or Password");
                dr.Close();
            }


          

        }
        private void Login_Admin(object sender, RoutedEventArgs e)
        {
            try
            {
                String username = Admin_UserName.Text;
                Admin_Window adminwindow = new Admin_Window(username);
               
                string hashedPassword = SHA512(Admin_Password.Password);
            string sql = " select * from tbl_personalinformation where Username like ('" + Admin_UserName.Text + "') AND Account_Type_ID = 1";
            cm = new MySqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            dr.Read();

            if (Admin_UserName.Text == defaultEmployeeUsername && Admin_Password.Password == defaultEmployeePassword)
            {
                adminwindow.Show();
                this.Hide();
                dr.Close();
            }
            else if (Admin_UserName.Text == "" || Admin_Password.Password == "")
            {
                MessageBox.Show("Please input username or password");
            }
            else if (dr.HasRows)
            {
                
                    if (Admin_UserName.Text == dr[1].ToString() && hashedPassword == dr[2].ToString())
                    {
                        adminwindow.Show();
                        this.Hide();
                        dr.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password");
                        dr.Close();
                    }
                }
               
            }
             catch (Exception)
            {
                MessageBox.Show("Incorrect Username or Password");
                dr.Close();
            }


        }
        private void btn_Login1_Click(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Visible;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void btn_SignUp_Click_1(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Visible;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void btn_Adminlogin_Click(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Visible;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void btn_Employeelogin_Click_1(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Visible;
        }
        private void btn_signup_Click1(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Visible;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (confirmation_pass.Password == "admin")
            {
                Grid_Login.Visibility = Visibility.Hidden;
                Grid_Signup.Visibility = Visibility.Visible;
                Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
                Grid_Adminlogin.Visibility = Visibility.Hidden;
                Grid_Employeelogin.Visibility = Visibility.Hidden;
            }
        }

        //Home button click events
        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void Home_Button_Click_1(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void Home_Button2_Click(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Hidden;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void Home_Button3_Click(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Visible;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }
        private void Home_Button4_Click(object sender, RoutedEventArgs e)
        {
            Grid_Login.Visibility = Visibility.Visible;
            Grid_Signup.Visibility = Visibility.Hidden;
            Grid_Dialogconfirmation.Visibility = Visibility.Hidden;
            Grid_Adminlogin.Visibility = Visibility.Hidden;
            Grid_Employeelogin.Visibility = Visibility.Hidden;
        }

        //Gotfocus click events
        private void tb_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_Username.Text = "";

        }
        private void pb_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            pb_Password.Password = "";

        }
        private void confirmation_pass_GotFocus(object sender, RoutedEventArgs e)
        {
            confirmation_pass.Password = "";

        }
        private void Employee_UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            Employee_UserName.Text = "";

        }
        private void Employee_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            Employee_Password.Password = "";

        }
        private void Admin_UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            Admin_UserName.Text = "";

        }
        private void Admin_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            Admin_Password.Password = "";

        }

        
    }
}
