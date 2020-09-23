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
    public partial class EditJob : Form
    {
        private static string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"].ToString();
        public EditJob(string Text_)
        {
            InitializeComponent();
            List<Organization> organizations = GetOrganizations();
            organizationDropDown.DisplayMember = "Text";
            organizationDropDown.ValueMember = "Value";
            foreach (Organization organization in organizations)
            {
                organizationDropDown.Items.Add(new { Text = organization.name, Value = organization.id });
            }
            edit(Text_);
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



        public bool edit(string text)
        {
            JobPosting jobposting = LoadData(text);
            titleTxt.Text = jobposting.title;
            descTxt.Text = jobposting.description;
            skillsTxt.Text = jobposting.skils;
            expTxt.Text = jobposting.experience.ToString();
            return false;
        }
        private JobPosting LoadData(string id)
        {
            IEnumerable<JobPosting> students = null;
            List<JobPosting> model = null;
            JobPosting obj1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("job_posting/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                    readTask.Wait();
                    JobPosting obj = JsonConvert.DeserializeObject<JobPosting>(readTask.Result);
                    obj1 = obj;
                    students = model;
                }

                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<JobPosting>();

                    obj1 = new JobPosting();
                }
            }

            return obj1;
        }
        private void EditJob_Load(object sender, EventArgs e)
        {

        }

        private void addJobPost_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(titleTxt.Text) || String.IsNullOrEmpty(skillsTxt.Text) ||
                String.IsNullOrEmpty(descTxt.Text) || String.IsNullOrEmpty(expTxt.Text))
            {
                MessageBox.Show("All Fields Are Requiered");
                return;
            }
            Regex nonNumericRegex = new Regex(@"\D");
            if (nonNumericRegex.IsMatch(expTxt.Text))
            {
                MessageBox.Show("Error, Expirence must be numeric");
                //Contains non numeric characters.
                return;
            }
            JobPosting jobPosting = new JobPosting
            {
                title = titleTxt.Text,
                description = descTxt.Text,
                skils = skillsTxt.Text,
                experience = Convert.ToInt32(expTxt.Text),
                organizationId = Convert.ToInt32((organizationDropDown.SelectedItem as dynamic).Value),
                alumniId = LoginInfo.UserID
            };
            SaveChangesInDb(jobPosting);
            
        }


        private void SaveChangesInDb(JobPosting jobPosting)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link + "job_posting");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<JobPosting>("job_posting", jobPosting);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Saved SuccessFully");
                    this.Close();
                    UserInterface userInterface = new UserInterface();
                    userInterface.Show();
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
