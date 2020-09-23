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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlumniDesktopApplication
{
    public partial class AddJob : Form
    {
        private static string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"].ToString();
        
        public AddJob()
        {
            InitializeComponent();
            List<Organization> organizations = GetOrganizations();
            organizationDropDown.DisplayMember = "Text";
            organizationDropDown.ValueMember = "Value";
            foreach (Organization organization in organizations)
            {
                organizationDropDown.Items.Add(new { Text = organization.name, Value = organization.id });
            }
            
        }
       
        private List<Organization> GetOrganizations()
        {

            List<Organization> organizations = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("organizations");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync(); //<IList<JobPosting>>();
                    readTask.Wait();
                    dynamic obj = JsonConvert.DeserializeObject<List<Organization>>(readTask.Result);
                    organizations = obj;
                }

                else //web api sent error response 
                {
                    //log response status here..
                    MessageBox.Show("Server Error");
                }
                return organizations;
            }
        }

        private void addJobPost_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(titleTxt.Text) || String.IsNullOrEmpty(skillsTxt.Text) ||
                String.IsNullOrEmpty(descTxt.Text) || String.IsNullOrEmpty(expTxt.Text))
            {
                MessageBox.Show("All Fields Are Requiered") ;
                return;
            }
            Regex nonNumericRegex = new Regex(@"\D");
            if (nonNumericRegex.IsMatch(expTxt.Text))
            {
                MessageBox.Show("Error, Expirence must be numeric");
                //Contains non numeric characters.
                return ;
            }
            JobPosting jobPosting = new JobPosting { title = titleTxt.Text, description = descTxt.Text
                , skils = skillsTxt.Text, experience = Convert.ToInt32(expTxt.Text),
                organizationId = Convert.ToInt32((organizationDropDown.SelectedItem as dynamic).Value),
                alumniId = LoginInfo.UserID
                    };
            addJobinDB(jobPosting);
           

        }

        private void addJobinDB(JobPosting jobPosting)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link + "job");
                

                //HTTP POST
                var postTask = client.PostAsJsonAsync<JobPosting>("job_posting", jobPosting);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Successfully Added");
                    this.Close();
                    UserInterface userInterface = new UserInterface();
                    userInterface.Show();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }

            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            UserInterface userInterface = new UserInterface();
            userInterface.Show();

        }


        

        
    }
}
