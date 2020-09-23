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
    public partial class UserInterface : Form
    {
        private static string Link = ConfigurationManager.AppSettings["LinkToAccessAPI"].ToString();
        private static bool flag = false; //false for deletion
        public UserInterface()
        {
            InitializeComponent();
        }

        private void UserInterface_Load(object sender, EventArgs e)
        {
            List<JobPostFinal> finalJobs = GetLoadJobs();
            foreach(JobPostFinal jobPostFinal in finalJobs)
            {
                //  listBox1.Text = jobPostFinal.ConvertString();

                ListViewItem item = new ListViewItem(jobPostFinal.id.ToString());
                item.SubItems.Add(jobPostFinal.title.ToString());
                item.SubItems.Add(jobPostFinal.description.ToString());
                item.SubItems.Add(jobPostFinal.skils.ToString());
                item.SubItems.Add(jobPostFinal.OrganizationName.ToString());
                item.SubItems.Add(jobPostFinal.Location.ToString());
                listView1.Items.Add(item);
            }
        }

        private static List<JobPostFinal> GetLoadJobs()
        {
            IEnumerable<JobPosting> students = null;
            List<JobPosting> model = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);
                //HTTP GET
                var responseTask = client.GetAsync("job_posting");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();//<IList<JobPosting>>();
                    readTask.Wait();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JobPosting>>(readTask.Result);

                    students = model;
                }

                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<JobPosting>();

                }
            }

            Organization obj_1;
            List<JobPostFinal> postFinals = new List<JobPostFinal>();
            foreach (var jobPosting in students)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Link);
                    //HTTP GET
                    var responseTask = client.GetAsync("organizations/" + jobPosting.organizationId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync(); //<IList<JobPosting>>();
                        readTask.Wait();
                        Organization obj = JsonConvert.DeserializeObject<Organization>(readTask.Result);
                        obj_1 = obj;
                    }

                    else //web api sent error response 
                    {
                        //log response status here..

                        students = Enumerable.Empty<JobPosting>();

                        obj_1 = new Organization();
                    }

                }

                JobPostFinal jobPost = new JobPostFinal(jobPosting.id, jobPosting.title, jobPosting.description, jobPosting.skils, obj_1.name, obj_1.id, obj_1.location);
                postFinals.Add(jobPost);
            }


            return postFinals;
        }

        private void OrgAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nameTxtBox.Text) || String.IsNullOrEmpty(locationTxtBox.Text)
                || String.IsNullOrEmpty(countryTxtBox.Text) || String.IsNullOrEmpty(cityTextBox.Text)
                || String.IsNullOrEmpty(emailTxtBox.Text)){
                MessageBox.Show("Error, All Fields Are Required");
                return;
            }
            AddOrganizationInDB(nameTxtBox.Text,locationTxtBox.Text,countryTxtBox.Text,cityTextBox.Text,emailTxtBox.Text);
        }

        private void AddOrganizationInDB(string name1, string location1, string country1, string city1, string email1)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Link + "organizations");


                    //HTTP POST
                    Organization collection = new Organization { name = name1,location = location1,country = country1,city = city1,email = email1};
                    var postTask = client.PostAsJsonAsync<Organization>("organizations", collection);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Added Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            AddJob addJob = new AddJob ();
            this.Close();
            addJob.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select an item from below to Delete");
            flag = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                String text = listView1.Items[intselectedindex].Text;

                if (!flag) { 
                    //do something
                    var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                         "Confirm Delete!!",
                                         MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveItemFromDB(text);
                        listView1.Items[intselectedindex].Remove();

                    }
                    else
                    {
                        // If 'No', do something here.
                    }
                } else if (flag)
                {
                    EditJob addJob = new EditJob(text);
                    this.Close();
                    addJob.ShowDialog();
                    
                }
            }
        }

        private void RemoveItemFromDB(string text)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Link);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("job_posting/" + text);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    MessageBox.Show("Removed SuccessFully");
                }

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flag = true;
            MessageBox.Show("Select an item from below to edit item");
        }
    }
}
