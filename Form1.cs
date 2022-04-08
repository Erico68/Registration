using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Registration
{
    public partial class Form1 : Form

    {
        string integrationdb_conn_string = "Server=HM;Database=registration;uid=sa;pwd=Jumpman535;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adp;
        DataTable dt;
       
        int ID;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(integrationdb_conn_string);
            displaydata();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;  

        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text==""||txtFname.Text==""||txtEmail.Text==""||txtID.Text==""||txtOcc.Text==""||txtAddrss.Text==""||Gender.Text=="" )
            {
                MessageBox.Show("Please Fill Columns");
            }
            else
            {
                try
                {

                    string gender;

                    if (rdbMale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    con.Open();
                    cmd = new SqlCommand("insert into Employee(Employee_Name,Employee_FName,Employees_Occupation,Employee_Email,Emp_ID,Gender,Addrss) values ( '" + txtName.Text + " ', '" + txtFname.Text + " ', '" + txtOcc.Text + " ',  '" + txtEmail.Text + " ','" + txtID.Text + " ',  '" + gender + " ', '" + txtAddrss.Text+  " ') ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    MessageBox.Show("Employee saved in Database");
                    clear();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void clear()
        {
            txtName.Text = "";
            txtFname.Text = "";
            txtOcc.Text = "";
            textBox5.Text = "";
            txtEmail.Text = "";
            txtAddrss.Text = "";
        }

      public void displaydata()
        {
            try
            {

                dt = new DataTable();
                con.Open();
                adp = new SqlDataAdapter("select * from Employee", con);
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFname.Text= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text= dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtOcc.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();  
            

            rdbFemale.Checked = false;
            rdbMale.Checked = true;

            if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()=="Female")
            {
                rdbFemale.Checked=true;    
                rdbMale.Checked=false;   

            }
            txtAddrss.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string gender;

                if (rdbFemale.Checked)
                {
                    gender = "Female";

                }
                else
                {
                    gender = "Male";
                }

                con.Open();
                cmd = new SqlCommand("update Employee set Employee_Name= '" + txtName.Text + " ',Employee_FName='" + txtFname.Text + " ', Employees_Occupation='" + txtOcc.Text + " ',Employee_Email='" + txtEmail.Text + " ',Emp_ID='" + txtID.Text + " ',Gender='" +gender + " ',Addrss='" + txtAddrss.Text + " ' where Employee_ID = '" +ID + " '", con);
                cmd.ExecuteNonQuery();
                con.Close();
                displaydata();
                MessageBox.Show("Your Data has been updated");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("delete from Employee where Employee_Id= '" + ID + " ' ",con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record has been deleted");
                displaydata();  

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            adp = new SqlDataAdapter("select * from Employee where Employee_Name like '%" + txtSearch.Text+ "%'", con);
            dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource= dt;   
            con.Close();

        }
    }
}




