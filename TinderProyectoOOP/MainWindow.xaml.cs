using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Text.RegularExpressions;

namespace TinderProyectoOOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        List<Persona> users = new List<Persona>();

        Persona userLogIn;
        Dictionary<string,string> usernamesPassword = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
            time.Content = DateTime.Now.ToString("h:mm tt");

            AddCanva.Visibility = Visibility.Hidden;
            loginCanvas.Visibility = Visibility.Hidden;
            page1.Visibility = Visibility.Visible;
            page2.Visibility = Visibility.Hidden;
            page3.Visibility = Visibility.Hidden;
            page4.Visibility = Visibility.Hidden;
            profileMain.Visibility = Visibility.Hidden;
            editInfoCanvas.Visibility = Visibility.Hidden;
            tinderPlaycanvas.Visibility = Visibility.Hidden;
        }

        private void rightbtn_Click(object sender, RoutedEventArgs e)
        {
            if(page1.Visibility == Visibility.Visible)
            {
                page2.Visibility = Visibility.Visible;
                page1.Visibility = Visibility.Hidden;
            }

           else if (page2.Visibility == Visibility.Visible)
            {
                page3.Visibility = Visibility.Visible;
                page2.Visibility = Visibility.Hidden;
            }
            else if (page3.Visibility == Visibility.Visible)
            {
                page4.Visibility = Visibility.Visible;
                page3.Visibility = Visibility.Hidden;
            }
            else if (page4.Visibility == Visibility.Visible)
            {
                page1.Visibility = Visibility.Visible;
                page4.Visibility = Visibility.Hidden;
            }
        }

        private void leftbtn_Click(object sender, RoutedEventArgs e)
        {
            if (page1.Visibility == Visibility.Visible)
            {
                page4.Visibility = Visibility.Visible;
                page1.Visibility = Visibility.Hidden;
            }

            else if (page2.Visibility == Visibility.Visible)
            {
                page1.Visibility = Visibility.Visible;
                page2.Visibility = Visibility.Hidden;
            }
            else if (page3.Visibility == Visibility.Visible)
            {
                page2.Visibility = Visibility.Visible;
                page3.Visibility = Visibility.Hidden;
            }
            else if (page4.Visibility == Visibility.Visible)
            {
                page1.Visibility = Visibility.Visible;
                page4.Visibility = Visibility.Hidden;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
         
            if(loginCanvas.Visibility == Visibility.Hidden)
            {
                page1.Visibility = Visibility.Visible;
                page2.Visibility = Visibility.Hidden;
                page3.Visibility = Visibility.Hidden;
                page4.Visibility = Visibility.Hidden;
           
                loginCanvas.Visibility = Visibility.Visible;
            }

            string Username = userlognametxt.Text;
            string Password = passwordbx.Password.ToString();
            XmlDocument userio = new XmlDocument();
            userio.Load(HttpContext.Current.Server.MapPath("users.xml"));
            foreach(XmlNode nodo in userio.SelectNodes("//user"))
            {
                string nameuser = nodo.SelectSingleNode("username").InnerText;
                string paswuord = nodo.SelectSingleNode("password").InnerText;
                if (nameuser == Username && paswuord ==Password)
                {
                    page1.Visibility = Visibility.Hidden;
                    loginCanvas.Visibility = Visibility.Hidden;
                    editInfoCanvas.Visibility = Visibility.Visible;
                    MessageBox.Show("Your username or password are correct");
                }
                else
                {
                    MessageBox.Show("Your username or pasword are incorrect");
                }

                if (usernamesPassword[Username].Equals(Password))
                {
                    page1.Visibility = Visibility.Hidden;
                    loginCanvas.Visibility = Visibility.Hidden;
                    editInfoCanvas.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Your username or password are incorrect");
                }
            }
        }

        private void createAcc_Click(object sender, RoutedEventArgs e)
        {
            AddCanva.Visibility = Visibility.Visible;

        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            string name = nametxt.Text;
            string date = birthdp.SelectedDate.ToString();
            DateTime birth;
            bool birthdate = DateTime.TryParse(date, out birth);
            string username = usernametxt.Text;
            string password = passwordbx.Password.ToString();
            string email = emailtxt.Text;

            if ((name == "") || (date == "") || (username == "") || (password == "") || (email == ""))
            {

                MessageBox.Show("You must fill in all of the fields.");

            }
            else if (!Regex.IsMatch(emailtxt.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                emailtxt.Select(0, emailtxt.Text.Length);
                emailtxt.Focus();
            }
            else
            {
                Persona user = new Persona(name, birth);
                user.email = email;
                user.Username = username;
                user.Password = password;
                users.Add(user);
                userLogIn = user;
                usernamesPassword.Add(username, password);

                editInfoCanvas.Visibility = Visibility.Visible;
                AddCanva.Visibility = Visibility.Hidden;
            }
        }

        private void Tinder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(users.GetType());
            StreamWriter sw = new StreamWriter("users.xml");
            xs.Serialize(sw, users);
            sw.Close();
        }

        private void Tinder_Loaded(object sender, RoutedEventArgs e)
        {
            FileInfo archivo = new FileInfo("users.xml");
            if (archivo.Exists)
            {
                XmlSerializer xs = new XmlSerializer(users.GetType());
                StreamReader sr = new StreamReader("users.xml");
                users = (List<Persona>)xs.Deserialize(sr);
                sr.Close();

            }

        }

        private void pic1btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfileDialog1 = new OpenFileDialog();
            openfileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (openfileDialog1.ShowDialog() != null)
            {
                

            }
        }

        private void editInfobtn_Click(object sender, RoutedEventArgs e)
        {
            profileMain.Visibility = Visibility.Hidden;
            editInfoCanvas.Visibility = Visibility.Visible;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            string about = abouttxt.Text;
            string work = worktxt.Text;
            string anthem = anthemtxt.Text;
            string gender = genrecombx.SelectedItem.ToString();
            userLogIn.About = about;
            userLogIn.Work = work;
            userLogIn.Anthem = anthem;
            userLogIn.Gender = gender;

            editInfoCanvas.Visibility = Visibility.Hidden;
            profileMain.Visibility = Visibility.Visible;
            nombreedadUSERtxt.Content = userLogIn;
        }

        private void tinderPlay_Click(object sender, RoutedEventArgs e)
        {
            tinderPlaycanvas.Visibility = Visibility.Visible;
        }

        
    }
}
