using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tisdale_Project_2
{
    /*
    * ***************************************************
    * Class Name: DatabaseManager
    * Author: Chris Tisdale
    * **************************************************** 
    * Purpose of the class:
    * This class provides connection to db and methods for calling queries.
    * **************************************************** 
    * Date: February 23, 2018
    * *****************************************************
    */
    static class DatabaseManager
    {
        // static declarations for use with connectionString
        static string databaseString = "CTASV20R2DRW.tamuct.edu";
        static string databaseNameString = "Initial Catalog = ChristopherFirstAssignment";
        static string userString = "User ID = Christopher";
        static string passwordString = "Password = Tisdale016";

        static string connectionString = String.Format("Data Source= {0}; {1}; {2}; {3};",
            databaseString, databaseNameString, userString, passwordString);

        /*
        * ***************************************************
        * Method Name: getCustomerNames() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to retreive the names of all custoemrs to populate comboboxes for the corresponding tabs. 
        * Method parameters: none
        * Return value: string[]
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static string[] getCustomerNames()
        {
            List<string> customerNames = new List<string>();
            try
            {
                // connection and command variables to establish connection and create new command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // query joins customer and person IDs which are the unique identifier that ties the person and customer table together
                // determines which person rows correspond to customers 
                sqlCmd.CommandText = "SELECT p.LastName + ', ' + p.FirstName FROM ChristopherFirstAssignment.db_owner.Person AS p JOIN Customer c ON c.ID = p.ID";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                customerNames.Add(""); //blank value for combo box

                // reads all customer names from query into list
                while (reader.Read())
                {
                    customerNames.Add(reader[0].ToString());
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return customerNames.ToArray();

        }

        /*
        * ***************************************************
        * Method Name: getEmployeeNames() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to retreive the names of all custoemrs to populate comboboxes for the corresponding tabs. 
        * Method parameters: none
        * Return value: string[]
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static string[] getEmployeeNames()
        {
            List<string> employeeNames = new List<string>();
            try
            {
                // connection and command variables to establish connection and create new command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // query joins employee and person IDs which are the unique identifier that ties the person and employee table together
                // determines which person rows correspond to employees 
                sqlCmd.CommandText = "SELECT p.LastName + ', ' + p.FirstName FROM ChristopherFirstAssignment.db_owner.Person AS p JOIN Employee e ON e.ID = p.ID";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                employeeNames.Add(""); //blank value for combo box

                // reads all employee names from query into list
                while (reader.Read())
                {
                    employeeNames.Add(reader[0].ToString());
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return employeeNames.ToArray();

        }

        /*
        * ***************************************************
        * Method Name: viewCustomer() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to retreive the customer info to display in data grid viewer. 
        * Method parameters: string lastName, string firstName
        * Return value: DataTable
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static DataTable viewCustomer(string lastName, string firstName)
        {
            DataTable dataRecord = null;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                
                // Selects all columns from Person using the parameter lastName and firstName
                sqlCmd.CommandText = "SELECT * FROM ChristopherFirstAssignment.db_owner.Person WHERE LastName = '" + lastName + "' AND FirstName = '" + firstName + "'";

                con.Open();

                //create adapter and dataTable to return row
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
                dataRecord = new DataTable();
                sqlDataAdap.Fill(dataRecord);

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return dataRecord;
        }

        /*
        * ***************************************************
        * Method Name: viewEmployee() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to retreive the employee info to display in data grid viewer. 
        * Method parameters: string lastName, string firstName
        * Return value: DataTable
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static DataTable viewEmployee(string lastName, string firstName)
        {
            DataTable dataRecord = null;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // Selects all columns from Person using the parameter lastName and firstName
                sqlCmd.CommandText = "SELECT * FROM ChristopherFirstAssignment.db_owner.Person WHERE LastName = '" + lastName + "' AND FirstName = '" + firstName + "'";

                con.Open();

                //create adapter and dataTable to return row
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
                dataRecord = new DataTable();
                sqlDataAdap.Fill(dataRecord);

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return dataRecord;
        }

        /*
        * ***************************************************
        * Method Name: getNextIdNumber() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to determine the next unique ID number to be entered in the db. 
        * Method parameters: NA
        * Return value: int id
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static int getNextIdNumber()
        {
            int id = 0;
            try
            {
                // establish connection and command variables
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // Selects the highest value ID from Person by ordering them by descending value
                sqlCmd.CommandText = "SELECT TOP 1 ID FROM ChristopherFirstAssignment.db_owner.Person ORDER BY ID DESC";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                // reads the query result
                while (reader.Read())
                {
                    id = Convert.ToInt16(reader[0]);
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return ++id; // increments the highest ID values in the db by one to ensure that it will be unique
        }

        /*
        * ***************************************************
        * Method Name: getPersonIdNumber() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to determine the ID of a person given their name. 
        * Method parameters: string firstName, string lastName
        * Return value: int id
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static int getPersonIdNumber(string firstName, string lastName)
        {
            int id = 0;
            try
            {
                // establish connectoin and create commdn variables
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // selects the ID from Person table where their name matches passed parameters
                sqlCmd.CommandText = "SELECT ID FROM ChristopherFirstAssignment.db_owner.Person " +
                    "WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                // reads ID from query, changing the smallInt to Int16
                while (reader.Read())
                {
                    id = Convert.ToInt16(reader[0]);
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return id;
        }

        /*
        * ***************************************************
        * Method Name: addCustomer() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to add customer to Person and Customer table given the user's input. 
        * Method parameters: string firstName, string lastName, string address, string dateOfBirth, string favoriteDepartment
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static void addCustomer(string firstName, string lastName, string address, string dateOfBirth, string favoriteDepartment)
        {
            int id = DatabaseManager.getNextIdNumber(); // retrieve a unique ID

            try
            {
                // establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // Insert new Person to table with passed parameters
                sqlCmd.CommandText = String.Format("INSERT INTO [ChristopherFirstAssignment].[db_owner].[Person]" +
                    "([ID],[FirstName],[LastName],[Address],[DateOfBirth])" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}')",
                    id, firstName, lastName, address, dateOfBirth);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                // Insert new Customer to table with passed parameters
                sqlCmd.CommandText = String.Format("INSERT INTO [ChristopherFirstAssignment].[db_owner].[Customer]" +
                    "([ID],[FavoriteDepartment])" +
                    "VALUES('{0}','{1}')",
                    id, favoriteDepartment);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        /*
        * ***************************************************
        * Method Name: addEmployee() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to add employee to Person and Employee table given the user's input. 
        * Method parameters: string firstName, string lastName, string address, string dateOfBirth, string department
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static void addEmployee(string firstName, string lastName, string address, string dateOfBirth, string department)
        {
            int id = DatabaseManager.getNextIdNumber();

            try
            {
                // establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // Insert new Person to table with passed parameters
                sqlCmd.CommandText = String.Format("INSERT INTO [ChristopherFirstAssignment].[db_owner].[Person]" +
                    "([ID],[FirstName],[LastName],[Address],[DateOfBirth])" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}')",
                    id, firstName, lastName, address, dateOfBirth);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                // Insert new Employee to table with passed parameters
                sqlCmd.CommandText = String.Format("INSERT INTO [ChristopherFirstAssignment].[db_owner].[Employee]" +
                    "([ID],[Department])" +
                    "VALUES('{0}','{1}')",
                    id, department);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        /*
        * ***************************************************
        * Method Name: deleteCustomer() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to delete a customer from Person and Customer tables given their ID. 
        * Method parameters: int personID
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static void deleteCustomer(int personID)
        {
            try
            {
                // establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // delete row from Person table where ID matches
                sqlCmd.CommandText = String.Format("DELETE FROM [ChristopherFirstAssignment].[db_owner].[Person]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                // delete row from Customer table where ID matches
                sqlCmd.CommandText = String.Format("DELETE FROM [ChristopherFirstAssignment].[db_owner].[Customer]" +
                    "WHERE ID = '" + personID + "'");

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        /*
        * ***************************************************
        * Method Name: deleteEmployee() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to delete a employee from Person and Employee tables given their ID. 
        * Method parameters: int personID
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static void deleteEmployee(int personID)
        {
            try
            {
                // establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // delete row from Person table where ID matches
                sqlCmd.CommandText = String.Format("DELETE FROM [ChristopherFirstAssignment].[db_owner].[Person]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                // delete row from Employee table where ID matches
                sqlCmd.CommandText = String.Format("DELETE FROM [ChristopherFirstAssignment].[db_owner].[Employee]" +
                    "WHERE ID = '" + personID + "'");

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        /*
        * ***************************************************
        * Method Name: getCustomerToModify() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to find the customer to modify from Person and Customer tables given their ID. 
        * Method parameters: int personID
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static string getCustomerToModify(int personID)
        {
            //st<string> customerInfo = new List<string>();
            string customerInfo = "";
            try
            {
                //establish connection
                //declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // Selects all columns from Person where the ID matches and seperates them with a '|' for later parsing into textboxes for user to modify
                sqlCmd.CommandText = String.Format("SELECT FirstName + '|' + LastName + '|' + Address + '|' + convert(nvarchar(10), DateOfBirth, 101) FROM [ChristopherFirstAssignment].[db_owner].[Person]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();

                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                //reads the row into return string
                while (reader.Read())
                {
                    customerInfo = (reader[0].ToString());
                }


                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return customerInfo;
        }

        /*
        * ***************************************************
        * Method Name: getCustomerFavoriteDepartment() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to find the customer's favorite department to modify from Person and Customer tables given their ID. 
        * Method parameters: int personID
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static string getCustomerFavoriteDepartment(int personID)
        {
            string customerFavDept = "";
            //List<string> customerfavDept = new List<string>();
            try
            {
                //establish connectoin and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // selects the FavoriteDepartment from Customer table that matches the passed id
                sqlCmd.CommandText = String.Format("SELECT FavoriteDepartment FROM [ChristopherFirstAssignment].[db_owner].[Customer]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();

                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                //reads into return string
                while (reader.Read())
                {
                    customerFavDept = (reader[0].ToString());
                }


                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return customerFavDept;
        }

        /*
        * ***************************************************
        * Method Name: modifyCustomer() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to modify a customer in both Person and Customer tables given the user's input. 
        * Method parameters: int personID, string firstName, string lastName, string address, string dateOfBirth, string favoriteDepartment
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static void modifyCustomer(int personID, string firstName, string lastName, string address, string dateOfBirth, string favoriteDepartment)
        {

            try
            {
                // establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // updates the row in Person table where ID matches, using passed parameters
                sqlCmd.CommandText = String.Format("UPDATE [ChristopherFirstAssignment].[db_owner].[Person] " +
                    "SET FirstName = '" + firstName + "', LastName = '" + lastName + "', Address = '" + address + "', " +
                    "DateOfBirth = '" + dateOfBirth + "' WHERE ID = " + personID);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                // updates the FavoriteDepartment in Customer table where ID matches, using passed favoriteDepartment
                sqlCmd.CommandText = String.Format("UPDATE [ChristopherFirstAssignment].[db_owner].[Customer]" +
                    "SET FavoriteDepartment = '" + favoriteDepartment + "' WHERE ID = " + personID);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        /*
        * ***************************************************
        * Method Name: getEmployeeToModify() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to find the employee to modify from Person and Employee tables given their ID. 
        * Method parameters: int personID
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static string getEmployeeToModify(int personID)
        {

            string employeeInfo = "";
            try
            {
                //establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // Selects all columns from Person where the ID matches and seperates them with a '|' for later parsing into textboxes for user to modify
                sqlCmd.CommandText = String.Format("SELECT FirstName + '|' + LastName + '|' + Address + '|' + convert(nvarchar(10), DateOfBirth, 101) FROM [ChristopherFirstAssignment].[db_owner].[Person]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();

                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                // reads into return string
                while (reader.Read())
                {
                    employeeInfo = (reader[0].ToString());
                }


                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return employeeInfo;
        }

        /*
        * ***************************************************
        * Method Name: getEmployeeDepartment() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to find the employee's department to modify from Person and Employee tables given their ID. 
        * Method parameters: int personID
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static string getEmployeeDepartment(int personID)
        {
            string employeeDept = "";

            try
            {
                // establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // selects the Department from Employee table that matches the passed id
                sqlCmd.CommandText = String.Format("SELECT Department FROM [ChristopherFirstAssignment].[db_owner].[Employee]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();

                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                // read into return string
                while (reader.Read())
                {
                    employeeDept = (reader[0].ToString());
                }


                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return employeeDept;
        }

        /*
        * Method Name: modifyEmployee() 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: this method is used to modify a employee in both Person and Employee tables given the user's input. 
        * Method parameters: int personID, string firstName, string lastName, string address, string dateOfBirth, string department
        * Return value: void
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public static void modifyEmployee(int personID, string firstName, string lastName, string address, string dateOfBirth, string department)
        {

            try
            {
                //establish connection and declare command
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;

                // updates the row in Person table where ID matches, using passed parameters
                sqlCmd.CommandText = String.Format("UPDATE [ChristopherFirstAssignment].[db_owner].[Person] " +
                    "SET FirstName = '" + firstName + "', LastName = '" + lastName + "', Address = '" + address + "', " +
                    "DateOfBirth = '" + dateOfBirth + "' WHERE ID = " + personID);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                // updates the Department in Employee table where ID matches, using passed parameters
                sqlCmd.CommandText = String.Format("UPDATE [ChristopherFirstAssignment].[db_owner].[Employee]" +
                    "SET Department = '" + department + "' WHERE ID = " + personID);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }

}