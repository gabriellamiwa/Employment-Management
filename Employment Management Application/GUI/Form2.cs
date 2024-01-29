using Employment_Management_Application.BAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employment_Management_Application
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            addProjectToDataList();
            addEmployeeToDataList();
            addRegistrationToDataList();

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private bool projectExists(string projectNumber)
        {
            bool isAlreadyExists = false;
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {

                    var projectQuery = from project in context.Projects
                                       where project.ProjectNumber == projectNumber
                                       select project;

                    isAlreadyExists = context.Projects.Any(p => p.ProjectNumber == projectNumber);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        

            return isAlreadyExists;

        }
        private void addProjectToDataList()
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var projectQuery = from project in context.Projects
                                       select project;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Project Number");
                    dt.Columns.Add("Project Title");
                    dt.Columns.Add("Duration");

                    foreach (var project in projectQuery)
                    {
                        dt.Rows.Add(project.ProjectNumber, project.ProjectTitle, project.Duration);
                    }

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddProject_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    string projectNumber = txtProjectNumber.Text;
                   

                    if (projectExists(projectNumber))
                    {
                        MessageBox.Show($"The project number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var projectToAdd = new Projects();
                    projectToAdd.ProjectNumber = projectNumber;
                    projectToAdd.ProjectTitle = txtProjectTitle.Text;
                    projectToAdd.Duration = Int32.Parse(txtDuration.Text);

                    context.Projects.Add(projectToAdd);
                    context.SaveChanges();
                    MessageBox.Show($"Project added Successfully.", "Sucess",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    addProjectToDataList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditEmp_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var employeeID = Convert.ToInt32(txtEmpID.Text);
                    var empToEdit = context.Employee.SingleOrDefault(emp => emp.EmployeeID == employeeID);

                    if (empToEdit != null)
                    {
                        empToEdit.FirstName = txtFirstName.Text;
                        empToEdit.LastName = txtLastName.Text;
                        empToEdit.PhoneNumber = txtPhone.Text;
                        empToEdit.Email = txtEmail.Text;

                        context.SaveChanges();
                        MessageBox.Show($"Employee Edited Successfully.", "Sucess",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        addEmployeeToDataList();
                    }
                    else
                    {
                        MessageBox.Show($"The employee id does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var employeeID = Convert.ToInt32(txtEmpID.Text);
                    var empToDelete = context.Employee.SingleOrDefault(emp => emp.EmployeeID == employeeID);

                    if (empToDelete != null)
                    {
                        context.Employee.Remove(empToDelete);
                        context.SaveChanges();

                        MessageBox.Show($"Employee deleted Successfully.", "Sucess",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show($"The employee id does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    addEmployeeToDataList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchEmp_Click(object sender, EventArgs e)
        {
            lblInfoID.Text = "";
            lblInfoFirstName.Text = "";
            lblInfoLastName.Text = "";
            lblInfoPhone.Text = "";
            lblInfoEmail.Text = "";
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var employeeID = Convert.ToInt32(txtSearchEmp.Text);
                    var empToShow = context.Employee.SingleOrDefault(emp => emp.EmployeeID == employeeID);

                    if (empToShow != null)
                    {
                        lblInfoID.Text = empToShow.EmployeeID.ToString();
                        lblInfoFirstName.Text = empToShow.FirstName.ToString();
                        lblInfoLastName.Text = empToShow.LastName.ToString();
                        lblInfoPhone.Text = empToShow.PhoneNumber.ToString();
                        lblInfoEmail.Text = empToShow.Email.ToString();

                    }
                    else
                    {
                        MessageBox.Show($"The employee id does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchProject_Click(object sender, EventArgs e)
        {
            lblInfoNumber.Text = "";
            lblInfoTitle.Text = "";
            lblInfoDays.Text = "";
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {

                    var projectToShow = context.Projects.SingleOrDefault(p => p.ProjectNumber == txtSearchProject.Text);

                    if (projectToShow != null)
                    {
                        lblInfoNumber.Text = projectToShow.ProjectNumber.ToString();
                        lblInfoTitle.Text = projectToShow.ProjectTitle.ToString();
                        lblInfoDays.Text = projectToShow.Duration.ToString();

                    }
                    else
                    {
                        MessageBox.Show($"The project number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditProject_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {

                    var projectToEdit = context.Projects.SingleOrDefault(p => p.ProjectNumber == txtProjectNumber.Text);

                    if (projectToEdit != null)
                    {
                        projectToEdit.ProjectTitle = txtProjectTitle.Text;
                        projectToEdit.Duration = Int32.Parse(txtDuration.Text);

                        context.SaveChanges();
                        MessageBox.Show($"Project Edited Successfully.", "Sucess",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        addProjectToDataList();
                    }
                    else
                    {
                        MessageBox.Show($"The project number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteProject_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {

                    var projectToDelete = context.Projects.SingleOrDefault(p => p.ProjectNumber == txtProjectNumber.Text);

                    if (projectToDelete != null)
                    {
                        context.Projects.Remove(projectToDelete);
                        context.SaveChanges();

                        MessageBox.Show($"Project deleted Successfully.", "Sucess",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show($"The project number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    addProjectToDataList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchProject_TextChanged(object sender, EventArgs e)
        {

        }
        private void addEmployeeToDataList()
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var employeeQuery = from employee in context.Employee
                                       select employee;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Employee ID");
                    dt.Columns.Add("First Name");
                    dt.Columns.Add("Last Name");
                    dt.Columns.Add("Phone Number");
                    dt.Columns.Add("Email");

                    foreach (var employee in employeeQuery)
                    {
                        dt.Rows.Add(employee.EmployeeID, employee.FirstName, employee.LastName, employee.PhoneNumber, employee.Email);
                    }

                    dataGridView2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var employeeID = Convert.ToInt32(txtEmpID.Text);
                    var addEmp = context.Employee.SingleOrDefault(emp => emp.EmployeeID == employeeID);


                    if (addEmp != null)
                    {
                        MessageBox.Show($"The employee id already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var employeeToAdd = new Employee();
                    employeeToAdd.EmployeeID = employeeID;
                    employeeToAdd.FirstName = txtFirstName.Text;
                    employeeToAdd.LastName = txtLastName.Text;
                    employeeToAdd.PhoneNumber = txtPhone.Text;
                    employeeToAdd.Email = txtEmail.Text;

                    context.Employee.Add(employeeToAdd);
                    context.SaveChanges();
                    MessageBox.Show($"Employee added Successfully.", "Sucess",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    addEmployeeToDataList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblInfoID_Click(object sender, EventArgs e)
        {

        }
        private void addRegistrationToDataList()
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var registrationQuery = from reg in context.EmployeeProject
                                       select reg;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Project Number");
                    dt.Columns.Add("Employee ID");

                    foreach (var reg in registrationQuery)
                    {
                        dt.Rows.Add(reg.RegistrationID, reg.ProjectNumber, reg.EmployeeID);
                    }

                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddReg_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var employeeID = Convert.ToInt32(txtRegEmpID.Text);
                    var employeeName = txtRegEmpName.Text;
                    var projectNumber = txtRegNumber.Text;
                    var projectTitle = txtProjectTitle.Text;
                    var regID = Convert.ToInt32(txtRegID.Text);

                    var regExist = context.EmployeeProject.SingleOrDefault(emp => emp.RegistrationID == regID);
                    var empExist = context.Employee.SingleOrDefault(emp => emp.EmployeeID == employeeID);
                    var projectExist = context.Projects.SingleOrDefault(p => p.ProjectNumber == projectNumber);

                    
                    
                    if (regExist != null)
                    {
                        MessageBox.Show($"The project is already assigned to the employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (empExist != null)
                    {
                        if (projectExist != null)
                        {
                          
                            var registrationToAdd = new EmployeeProject();
                            registrationToAdd.RegistrationID = regID;
                            registrationToAdd.ProjectNumber = txtRegNumber.Text;
                            registrationToAdd.EmployeeID = Int32.Parse(txtRegEmpID.Text);

                            context.EmployeeProject.Add(registrationToAdd);
                            context.SaveChanges();
                            MessageBox.Show($"Employee Assigned Successfully to the Project.", "Sucess",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            addRegistrationToDataList();
                        }
                        else
                        {
                            MessageBox.Show($"The Project number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else {
                        MessageBox.Show($"The employee id does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditReg_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {

                    var employeeID = Convert.ToInt32(txtRegEmpID.Text);
                    var projectNumber = txtRegNumber.Text;
                    var projectExist = context.Projects.SingleOrDefault(p => p.ProjectNumber == projectNumber);
                    var empExist = context.Employee.SingleOrDefault(emp => emp.EmployeeID == employeeID);

                    var regID = Convert.ToInt32(txtRegID.Text);

                    var regExist = context.EmployeeProject.SingleOrDefault(emp => emp.RegistrationID == regID);

                    if (regExist != null)
                    {
                        if (empExist != null)
                        {
                            if (projectExist != null)
                            {
                                regExist.ProjectNumber = projectNumber;
                                regExist.EmployeeID = employeeID;

                                context.SaveChanges();
                                MessageBox.Show($"Registration Edited Successfully.", "Sucess",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                addRegistrationToDataList();
                            }
                            else
                            {
                                MessageBox.Show($"The project number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            MessageBox.Show($"The employee id does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show($"The Registration does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteReg_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                  
                    var regID = Convert.ToInt32(txtRegID.Text);

                    var regExist = context.EmployeeProject.SingleOrDefault(emp => emp.RegistrationID == regID);

                    if (regExist != null)
                    {
                        context.EmployeeProject.Remove(regExist);
                        context.SaveChanges();

                        MessageBox.Show($"Registration deleted Successfully.", "Sucess",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show($"The Registration does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    addRegistrationToDataList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRegID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
