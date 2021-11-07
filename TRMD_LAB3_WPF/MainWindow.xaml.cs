using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TRMD_LAB3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetRegisterElementVisisble(Visibility.Hidden);
            SetEntryElementVisisble(Visibility.Hidden);
            SetFilterElementVisisble(Visibility.Hidden);
            SetMsgLabel("", Visibility.Hidden, Brushes.Red);
            CurrentUser = null;
        }


        public User CurrentUser { get; set; }
        public List<User> AllUsers { get; set; } = new List<User>();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void реєстраціяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRegisterElementVisisble(Visibility.Visible);
            SetEntryElementVisisble(Visibility.Hidden);
            SetFilterElementVisisble(Visibility.Hidden);
            SetMsgLabel("", Visibility.Hidden, Brushes.Red);
        }

        private void вхідToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CurrentUser != null)
            {
                SetMsgLabel($"Current user is {CurrentUser.Login}", Visibility.Visible, Brushes.Green);
                return;
            }
            SetRegisterElementVisisble(Visibility.Hidden);
            SetEntryElementVisisble(Visibility.Visible);
            SetFilterElementVisisble(Visibility.Hidden);
            SetMsgLabel("", Visibility.Hidden, Brushes.Red);

        }

        private void отриманняДанихToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRegisterElementVisisble(Visibility.Hidden);
            SetEntryElementVisisble(Visibility.Hidden);
            SetFilterElementVisisble(Visibility.Visible);
            SetMsgLabel("", Visibility.Hidden, Brushes.Red);
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SetRegisterElementVisisble(Visibility.Hidden);
            SetEntryElementVisisble(Visibility.Hidden);
            SetFilterElementVisisble(Visibility.Hidden);
            SetMsgLabel("Exit successfuly", Visibility.Visible, Brushes.Green);
            CurrentUser = null;
        }

        private void SetRegisterElementVisisble(Visibility visible)
        {
            Register_textBox1.Visibility = visible;
            Register_textBox2.Visibility = visible;
            Register_dateTimePicker1.Visibility = visible;
            Register_textBox4.Visibility = visible;

            Register_label1.Visibility = visible;
            Register_label2.Visibility = visible;
            Register_label3.Visibility = visible;
            Register_label4.Visibility = visible;

            Register_button1.Visibility = visible;
        }
        private void SetEntryElementVisisble(Visibility visible)
        {
            Login_textBox1.Visibility = visible;
            Login_textBox2.Visibility = visible;

            Login_Label1.Visibility = visible;
            Login_label2.Visibility = visible;

            Login_button1.Visibility = visible;
        }
        private void SetFilterElementVisisble(Visibility visible)
        {
            Filter_dateTimePicker1.Visibility = visible;
            Filter_dateTimePicker2.Visibility = visible;
            Filter_textBox5.Visibility = visible;
            Filter_textBox6.Visibility = visible;

            Filter_label1.Visibility = visible;
            Filter_label2.Visibility= visible;
            Filter_label3.Visibility= visible;
            Filter_label4.Visibility= visible;

            Filter_checkBox1.Visibility = visible;
            Filter_checkBox2.Visibility = visible;
            Filter_checkBox3.Visibility = visible;
            Filter_checkBox4.Visibility = visible;


            Filter_button1.Visibility = visible;
        }

        private void Register_textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void SetMsgLabel(string text, Visibility visible, SolidColorBrush forecolor)
        {
            Message_label1.Content = text;
            Message_label1.Visibility = visible;
            Message_label1.Foreground = forecolor;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Login_button1_Click_1(object sender, RoutedEventArgs e)
        {
            var login = Login_textBox1.Text;
            var pass = Login_textBox2.Text;
            if (string.IsNullOrEmpty(login))
            {
                SetMsgLabel("Incorrect login", Visibility.Visible, Brushes.Red);
                return;
            }
            if (string.IsNullOrEmpty(pass))
            {
                SetMsgLabel("Incorrect password", Visibility.Visible, Brushes.Red);
                return;
            }
            var user = AllUsers.FirstOrDefault(x => x.Login == login && x.Password == pass);
            if (user == null)
            {
                SetMsgLabel("Incorrect login or password", Visibility.Visible, Brushes.Red);
                return;
            }
            CurrentUser = user;

            SetMsgLabel($"Welcome, {login}!", Visibility.Visible, Brushes.Green);
            return;
        }

        private void Filter_button1_Click_1(object sender, RoutedEventArgs e)
        {
            var login = Filter_textBox5.Text;
            var datecreated = Filter_dateTimePicker1.SelectedDate.HasValue ? Filter_dateTimePicker1.SelectedDate.Value : new DateTime();
            var age = Filter_dateTimePicker2.SelectedDate.HasValue ?  DateTime.Now.Year - Filter_dateTimePicker2.SelectedDate.Value.Year : -1;
            var gender = Filter_textBox6.Text;
            var useLogin = Filter_checkBox1.IsChecked.Value;
            var useDatecreated = Filter_checkBox2.IsChecked.Value;
            var useAge = Filter_checkBox3.IsChecked.Value;
            var useGender = Filter_checkBox4.IsChecked.Value;
            var usercount = 0;
            var successMsg = "Users ";
            var errorMsg = "User not found";

            if (useLogin)
            {
                usercount = AllUsers.Where(x => x.Login == login).ToList().Count;
                successMsg = $"{successMsg} with login {login},";
            }
            if (useDatecreated)
            {
                usercount = AllUsers.Where(x => x.DateCreated.Date >= datecreated.Date).ToList().Count;
                successMsg = $"{successMsg}  datecreated after {datecreated.Date.Date},";
            }
            if (useAge)
            {
                usercount = AllUsers.Where(x => x.Age == age).ToList().Count;
                successMsg = $"{successMsg} with age {age},";
            }
            if (useGender)
            {
                usercount = AllUsers.Where(x => x.Gender == gender).ToList().Count;
                successMsg = $"{successMsg} with gender {gender}";
            }

            if (usercount == 0)
            {
                SetMsgLabel($"{errorMsg}", Visibility.Visible, Brushes.Red);
                return;
            }

            SetMsgLabel($"{successMsg} count = {usercount}", Visibility.Visible, Brushes.Green);
        }

        private void Register_button1_Click_1(object sender, RoutedEventArgs e)
        {
            var login = Register_textBox1.Text;
            var pass = Register_textBox2.Text;
            var age = Register_dateTimePicker1.SelectedDate.HasValue? DateTime.Now.Year - Register_dateTimePicker1.SelectedDate.Value.Year : 0;
            var gender = Register_textBox4.Text;


            if (age < 15)
            {
                SetMsgLabel("You must be more 15 age old", Visibility.Visible, Brushes.Red);
                return;
            }
            if (string.IsNullOrEmpty(login))
            {
                SetMsgLabel("Incorrect login", Visibility.Visible, Brushes.Red);
                return;
            }
            if (string.IsNullOrEmpty(pass))
            {
                SetMsgLabel("Incorrect password", Visibility.Visible, Brushes.Red);
                return;
            }
            if (string.IsNullOrEmpty(gender))
            {
                SetMsgLabel("Incorrect gender", Visibility.Visible, Brushes.Red);
                return;
            }
            var userWithLogin = AllUsers.Any(x => x.Login == login);
            if (userWithLogin)
            {
                SetMsgLabel($"User with login {login} already exist", Visibility.Visible, Brushes.Red);
                return;
            }



            var newUser = new User()
            {
                Login = login,
                Password = pass,
                Age = age,
                Gender = gender,
                DateCreated = DateTime.Now
            };
            AllUsers.Add(newUser);

            SetMsgLabel($"User with nickname {login} has registered successfully", Visibility.Visible, Brushes.Green);
            SetRegisterElementVisisble(Visibility.Hidden);
            return;
        }
    }
}

