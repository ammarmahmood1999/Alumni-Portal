using AlumniDesktopApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlumniDesktopApplication
{
    public partial class Form1 : Form
    {
        private static string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"].ToString();
        public Form1()
        {
            InitializeComponent();
            passowrd.UseSystemPasswordChar = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            string username_ = username.Text.ToString();
            string password_ = passowrd.Text.ToString();
            if (verify(username_, password_))
            {
                UserInterface userInterface = new UserInterface();
                this.Hide();
                userInterface.Show();

            }
            else
                MessageBox.Show("error");
         
            
                
                
        }
        public bool verify(string uname, string pass)
        {
            AlumniLogin obj1 = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("AlumniLogins/" + uname);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                    readTask.Wait();
                    AlumniLogin obj = JsonConvert.DeserializeObject<AlumniLogin>(readTask.Result);
                    obj1 = obj;
                }
            }
            if (obj1 != null)
            {
                //Session["UserID"] = obj1.username.ToString();
                //Session["UserName"] = obj1.username.ToString();
                if (obj1.password.Equals(pass))
                {
                    LoginInfo.UserID = obj1.AlumniId;
                    MessageBox.Show("Login SuccessFul");
                    return true;
                }
                else
                {
                    MessageBox.Show("Error");
                    return false;
                }
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    public static class LoginInfo
    {
        public static int UserID;
    }
}

