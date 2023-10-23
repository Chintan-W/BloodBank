using BloodBankManagementSystem.BLL;
using BloodBankManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class frmDonors : Form
    {
        public frmDonors()
        {
            InitializeComponent();
        }
        
        donorgtst d = new donorgtst();
        donorDAL dal = new donorDAL();
        user udal = new user();

        

        private void frmDonors_Load(object sender, EventArgs e)
        {
            //Display Donors in DataGrid View
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the DAta from Manage Donors Form
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            d.added_date = DateTime.Now;

            //Get The ID of Logged In USer
            string loggedInUser = frmLogin.loggedInUser;
            usergtst usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id; 
            
            //Create a Boolean Variable to Insert DAta into DAtabase and check whether the data inserted successfully of not
            bool isSuccess = dal.Insert(d);

            if(isSuccess==true)
            {
                MessageBox.Show("New Donor Added Successfully");

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Add new Donor.");
            }
        }

        public void Clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtDonorID.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";

            
            //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);(not needed in my system)
        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            int RowIndex = e.RowIndex;

            txtDonorID.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();

           
            //string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 2);(not from root dir in my system)
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            d.donor_id = int.Parse(txtDonorID.Text);
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            string loggedInUser = frmLogin.loggedInUser;
            usergtst usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id;

            

            //Create a Boolean Variable to Check whether the data updated successfully or not
            bool isSuccess = dal.Update(d);



            //If the data updated successfully then the value of isSuccess will be true else it will be false
            if (isSuccess == true)
            {
                MessageBox.Show("Donor updated Successfully.");
                Clear();

                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

            }
            else
            {
                MessageBox.Show("Failed to update donors.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDonorID.Text, out int donorID))
            {
                d.donor_id = donorID;

                bool isSuccess = dal.Delete(d);

                if (isSuccess == true)
                {
                    MessageBox.Show("Donor Deleted Successfully.");

                    Clear();

                    DataTable dt = dal.Select();
                    dgvDonors.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Failed to Delete Donor");
                }
            }
            else
            {
                MessageBox.Show("Donor ID must be a valid integer.");
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void txtDonorID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
                //Retrieve the donor record by ID
                int donorId = Convert.ToInt32(txtSearch.Text);
                DataTable dt = dal.SelectById(donorId);

                //If the record exists, populate the textboxes on the form
                if (dt.Rows.Count > 0)
                {
                    txtDonorID.Text = dt.Rows[0]["donor_id"].ToString();
                    txtFirstName.Text = dt.Rows[0]["first_name"].ToString();
                    txtLastName.Text = dt.Rows[0]["last_name"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    txtContact.Text = dt.Rows[0]["contact"].ToString();
                    cmbGender.Text = dt.Rows[0]["gender"].ToString();
                    txtAddress.Text = dt.Rows[0]["address"].ToString();
                    cmbBloodGroup.Text = dt.Rows[0]["blood_group"].ToString();
                }
                else
                {
                    MessageBox.Show("Donor not found.");
                }
            

        }

        private void dgvDonors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
