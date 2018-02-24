using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisdale_Project_2
{
    /*
    * ***************************************************
    * Class Name: Employee : Person
    * Author: Chris Tisdale
    * **************************************************** 
    * Purpose of the class:
    * This class provides constructor and properties to handle some user input validation
    * for Employee which is a child of Person.
    * **************************************************** 
    * Date: February 23, 2018
    * *****************************************************
    */
    class Employee : Person
    {
        private string department;

        /*
        * ***************************************************
        * Method Name: Department 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Serves as getter and setter. 
        *                           Sets if string is at least 2 chars.
        * Method parameters: string
        * Return value: string
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public string Department
        {
            get => department;
            set
            {
                if (value.Length > 1)
                {
                    department = value;
                }
                else
                {
                    throw new ArgumentException("Department must be at least 2 characters long.");
                }

            }
        }

        /*
        * ***************************************************
        * Constructor: Employee
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Creates Employee object using properties & calling Person constructor.
        * Method parameters: string firstName, string lastName, string address, DateTime dateOfBirth, string department
        * Return value: NA
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public Employee(string firstName, string lastName, string address, DateTime dateOfBirth, string department) 
            : base(firstName, lastName, address, dateOfBirth)
        {
            Department = department;
        }
    }
}
