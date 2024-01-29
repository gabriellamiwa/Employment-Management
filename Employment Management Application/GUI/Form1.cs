using Employment_Management_Application.BAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employment_Management_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SDF_ConstructionEntities())
                {
                    var selectUserQuery = from user in context.Users
                                          where user.UserID.ToString() == txtUserName.Text &&
                                          user.Password == txtPassword.Text
                                          select user;

                    if (selectUserQuery.Any())
                    {
                        MessageBox.Show("User logged in.", "Sucess",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form2 employeeProject = new Form2();
                        employeeProject.Show();

                    }
                    else
                    {
                        MessageBox.Show("Username or password incorrect.", "Fail",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
