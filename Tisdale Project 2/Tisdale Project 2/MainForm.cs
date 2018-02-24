using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
    
namespace Tisdale_Project_2
{
    /*
    * ***************************************************
    * Class Name: MainForm
    * Author: Chris Tisdale
    * **************************************************** 
    * Purpose of the class:
    * This class provides a GUI for the application.
    * **************************************************** 
    * Date: February 23, 2018
    * *****************************************************
    */
    public partial class MainForm : Form
    {
        int personID; // class variable added for use with modifying customer
                        //since name changes, the ID must be retrieved before executing modify method

        public MainForm()
        {
            InitializeComponent();

            updateComboBoxes();

        }

        /*
        * ***************************************************
        * Method Name: updateComboBoxes() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to populate comboboxes for the corresponding tabs.
        *       Since the comboboxes share a DataSource they must be coppied to different arrays, otherwise
        *       one combobox selection on one tab would change the others.
        * Method parameters: none
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void updateComboBoxes()
        {
            string[] employeeArrayView = DatabaseManager.getEmployeeNames();
            string[] employeeArrayModify = new string[employeeArrayView.Length];
            string[] employeeArrayDelete = new string[employeeArrayView.Length];

            employeeArrayView.CopyTo(employeeArrayDelete, 0);
            employeeArrayView.CopyTo(employeeArrayModify, 0);

            cbViewEmployee.DataSource = employeeArrayView;
            cbViewEmployee.SelectedIndex = 0;

            cbDeleteEmployee.DataSource = employeeArrayDelete;
            cbDeleteEmployee.SelectedIndex = 0;

            cbModifyEmployee.DataSource = employeeArrayModify;
            cbModifyEmployee.SelectedIndex = 0;

            string[] customerArrayView = DatabaseManager.getCustomerNames();
            string[] customerArrayDelete = new string[customerArrayView.Length];
            string[] customerArrayModify = new string[customerArrayView.Length];

            customerArrayView.CopyTo(customerArrayDelete, 0);
            customerArrayView.CopyTo(customerArrayModify, 0);

            cbViewCustomer.DataSource = customerArrayView;
            cbViewCustomer.SelectedIndex = 0;

            cbDeleteCustomer.DataSource = customerArrayDelete;
            cbDeleteCustomer.SelectedIndex = 0;

            cbModifyCustomer.DataSource = customerArrayModify;
            cbModifyCustomer.SelectedIndex = 0;
        }

        /*
        * ***************************************************
        * Method Name: bnAddCustomer_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for adding a customer with the add button.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnAddCustomer_Click(object sender, EventArgs e)
        {
            Customer temp = null;
            bool legalCustomerValues = false; // flag for successful creation of Customer to prompt query
            string dateString = "";

            // attempt to create Customer object from user input, if successful set flag to true
            try
            {
                //remove spaces from user input
                string firstName = tbCustomerFirstName.Text.Replace(" ", "");
                string lastName = tbCustomerLastName.Text.Replace(" ", "");

                
                temp = new Customer(firstName, lastName, tbCustomerAddress.Text, dtpCustomerDateOfBirth.Value, tbCustomerFavoriteDepartment.Text);
                dateString = dtpCustomerDateOfBirth.Value.ToString("yyyy-MM-dd");
                legalCustomerValues = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // successful creation of customer object, therefore call query to add customer to db, refresh comboboxes, and reset textboxes
            if (legalCustomerValues)
            {
                Console.WriteLine(temp.FirstName);
                DatabaseManager.addCustomer(temp.FirstName, temp.LastName, temp.Address, dateString, temp.FavoriteDepartment);
                MessageBox.Show("Customer Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                updateComboBoxes();

                tbCustomerFirstName.Text = "";
                tbCustomerLastName.Text = "";
                tbCustomerAddress.Text = "";
                dtpCustomerDateOfBirth.Value = DateTime.Now;
                tbCustomerFavoriteDepartment.Text = "";
            }

            updateComboBoxes();

        }

        /*
        * ***************************************************
        * Method Name: cbViewEmployee_SelectedIndexChanged() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for selecting which employee to view.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void cbViewEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viewSelection = cbViewEmployee.Text;

            //if the selection changes then call viewEmployee query and refresh comboboxes
            if (viewSelection != "")
            {
                // parse name from "last name, first name" selection
                string[] name = new string[1];

                viewSelection = viewSelection.Replace(",", "");
                name = viewSelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                dgvViewEmployee.DataSource = DatabaseManager.viewEmployee(name[0], name[1]);
                updateComboBoxes();
            }

        }

        /*
        * ***************************************************
        * Method Name: cbDeleteCustomer_SelectedIndexChanged() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for selecting which custoemr to delete.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void cbDeleteCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deleteSelection = cbDeleteCustomer.Text;

            //if the selection changes then call deleteCustomer query and refresh comboboxes
            if (deleteSelection != "")
            {

                // parse name from "last name, first name" selection
                string[] name = new string[1];

                deleteSelection = deleteSelection.Replace(",", "");
                name = deleteSelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                // retrieves id to pass to deleteCustomer()
                int personID = DatabaseManager.getPersonIdNumber(name[1], name[0]);
                DatabaseManager.deleteCustomer(personID);
                updateComboBoxes();
                MessageBox.Show("Customer deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        /*
        * ***************************************************
        * Method Name: cbViewCustomer_SelectedIndexChanged() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for selecting which customer to view.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void cbViewCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

            string viewSelection = cbViewCustomer.Text;

            //if the selection changes then call viewEmployee query and refresh comboboxes
            if (viewSelection != "")
            {
                // parse name from comboBox
                string[] name = new string[1];

                viewSelection = viewSelection.Replace(",", "");
                name = viewSelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                dgvViewCustomer.DataSource = DatabaseManager.viewCustomer(name[0], name[1]);
                updateComboBoxes();
            }



        }
        

        /*
        * ***************************************************
        * Method Name: cbModifyCustomer_SelectedIndexChanged() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for selecting which customer to modify.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void cbModifyCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modifySelection = cbModifyCustomer.Text;

            // if selection changes
            if (modifySelection != "")
            {
                // parse name
                string[] name = new string[1];

                modifySelection = modifySelection.Replace(",", "");
                name = modifySelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                personID = DatabaseManager.getPersonIdNumber(name[1], name[0]);

                pnlModifyCustomer.Visible = true;

                // retrieves customer information from db and populates text boxes with customer info
                string cusInfo = DatabaseManager.getCustomerToModify(personID);

                string[] cleanCusInfo = new string[3];
                cleanCusInfo = cusInfo.Split('|');

                tbModifyCustomerFirstName.Text = cleanCusInfo[0];
                tbModifyCustomerLastName.Text = cleanCusInfo[1];
                tbModifyCustomerAddress.Text = cleanCusInfo[2];
                dtpModifyCustomerDateOfBirth.Value = Convert.ToDateTime(cleanCusInfo[3]);
                tbModifyCustomerFavoriteDepartment.Text = DatabaseManager.getCustomerFavoriteDepartment(personID);


                updateComboBoxes();

                //MessageBox.Show("Customer modified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }


        /*
        * ***************************************************
        * Method Name: bnCancelModify_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method hides pnlModifyCustomer when cancel button is clicked.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnCancelModify_Click(object sender, EventArgs e)
        {
            pnlModifyCustomer.Visible = false;
        }

        /*
        * ***************************************************
        * Method Name: bnModifyCustomer_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method calls modifyCustomer query when clicked.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnModifyCustomer_Click(object sender, EventArgs e)
        {
            // get dateTime as a string
            string dateString = dtpModifyCustomerDateOfBirth.Value.ToString("yyyy-MM-dd");
            //Console.WriteLine(personID);

            // call query passing personID and values from text boxes 
            DatabaseManager.modifyCustomer(personID, tbModifyCustomerFirstName.Text, tbModifyCustomerLastName.Text,
                tbModifyCustomerAddress.Text, dateString, tbModifyCustomerFavoriteDepartment.Text);

            pnlModifyCustomer.Visible = false;
            MessageBox.Show("Customer modified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            updateComboBoxes();
        }

        /*
        * ***************************************************
        * Method Name: bnAddEmployee_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for adding an employee with the add button.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee temp = null;
            bool legalEmployeeValues = false;
            string dateString = "";

            //attempt to create Employee onject
            try
            {
                // remove spaces from user input
                string firstName = tbEmployeeFirstName.Text.Replace(" ", "");
                string lastName = tbEmployeeLastName.Text.Replace(" ", "");


                temp = new Employee(firstName, lastName, tbEmployeeAddress.Text, dtpEmployeeDateOfBirth.Value, tbEmployeeDepartment.Text);
                dateString = dtpEmployeeDateOfBirth.Value.ToString("yyyy-MM-dd");
                legalEmployeeValues = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // successful creation of customer object, therefore call query to add customer to db, refresh comboboxes, and reset textboxes
            if (legalEmployeeValues)
            {
                Console.WriteLine(temp.FirstName);
                DatabaseManager.addEmployee(temp.FirstName, temp.LastName, temp.Address, dateString, temp.Department);
                MessageBox.Show("Employee Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                updateComboBoxes();

                tbEmployeeFirstName.Text = "";
                tbEmployeeLastName.Text = "";
                tbEmployeeAddress.Text = "";
                dtpEmployeeDateOfBirth.Value = DateTime.Now;
                tbEmployeeDepartment.Text = "";
            }

            updateComboBoxes();

        }

        /*
        * ***************************************************
        * Method Name: cbDeleteCustomer_SelectedIndexChanged() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for selecting which custoemr to delete.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void cbDeleteEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deleteSelection = cbDeleteEmployee.Text;

            //if selection changes
            if (deleteSelection != "")
            {
                // parse names
                string[] name = new string[1];

                deleteSelection = deleteSelection.Replace(",", "");
                name = deleteSelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                int personID = DatabaseManager.getPersonIdNumber(name[1], name[0]);
                DatabaseManager.deleteEmployee(personID);
                updateComboBoxes();
                MessageBox.Show("Employee deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        /*
        * ***************************************************
        * Method Name: cbModifyEmployee_SelectedIndexChanged() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method handles logic for selecting which employee to modify.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void cbModifyEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modifySelection = cbModifyEmployee.Text;

            // if selection changes
            if (modifySelection != "")
            {
                // parse name
                string[] name = new string[1];

                modifySelection = modifySelection.Replace(",", "");
                name = modifySelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                personID = DatabaseManager.getPersonIdNumber(name[1], name[0]);

                pnlModifyEmployee.Visible = true;

                // retrieves employee information from db and populates text boxes with employee info
                string empInfo = DatabaseManager.getCustomerToModify(personID);

                string[] cleanEmpInfo = new string[3];
                cleanEmpInfo = empInfo.Split('|');

                tbModifyEmployeeFirstName.Text = cleanEmpInfo[0];
                tbModifyEmployeeLastName.Text = cleanEmpInfo[1];
                tbModifyEmployeeAddress.Text = cleanEmpInfo[2];
                dtpModifyEmployeeDateOfBirth.Value = Convert.ToDateTime(cleanEmpInfo[3]);
                tbModifyEmployeeDepartment.Text = DatabaseManager.getEmployeeDepartment(personID);


                updateComboBoxes();

                //MessageBox.Show("Customer modified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        /*
        * ***************************************************
        * Method Name: bnCancelModifyEmployee_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method hides pnlModifyEmployee when cancel button is clicked.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnCancelModifyEmployee_Click(object sender, EventArgs e)
        {
            pnlModifyEmployee.Visible = false;
        }

        /*
        * ***************************************************
        * Method Name: bnModifyEmployee_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method calls modifyEmployee query when clicked.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnModifyEmployee_Click(object sender, EventArgs e)
        {
            // get dateTime as a string
            string dateString = dtpModifyEmployeeDateOfBirth.Value.ToString("yyyy-MM-dd");
            //Console.WriteLine(personID);

            DatabaseManager.modifyEmployee(personID, tbModifyEmployeeFirstName.Text, tbModifyEmployeeLastName.Text,
                tbModifyEmployeeAddress.Text, dateString, tbModifyEmployeeDepartment.Text);

            // refresh tab and combobox
            pnlModifyEmployee.Visible = false;
            MessageBox.Show("Employee modified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            updateComboBoxes();
        }

        /*
        * ***************************************************
        * Method Name: bnLogin_Click() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method calls attempts to login when clicked.
        * Method parameters: object sender, EventArgs e
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        private void bnLogin_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text;
            string password = tbPassword.Text;

            // if credentials match these, the panel is invisble
            if(user.Equals("DrWoodcock") && (password.Equals("panWobble") || password.Equals("PanWobble")))
            {
                pnlLogin.Visible = false;
            }

            // login fail messageBox and reset textboxes
            else
            {
                MessageBox.Show("User and password do not match.\nPlease try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbUser.Text = "";
                tbPassword.Text = "";
            }
        }

    }
}
