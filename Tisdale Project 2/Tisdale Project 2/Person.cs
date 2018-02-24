using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisdale_Project_2
{
    /*
    * ***************************************************
    * Class Name: Person
    * Author: Chris Tisdale
    * **************************************************** 
    * Purpose of the class:
    * This class provides constructor and properties to handle some user input validation.
    * **************************************************** 
    * Date: February 23, 2018
    * *****************************************************
    */
    class Person
    {
        private string firstName;
        private string lastName;
        private string address;
        private DateTime dateOfBirth;

        /*
        * ***************************************************
        * Method Name: FirstName 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Serves as getter and setter. 
        *                           Sets if string has no digits and lengths is at least 2 chars.
        * Method parameters: string
        * Return value: string
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public string FirstName
        {
            get => firstName;
            set {
                if (!value.Any(char.IsDigit) && value.Length > 1)
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentException("First name must be at least two letters long and cannot contain any digits.");
                 }
            }
        }


        /*
        * ***************************************************
        * Method Name: LastName 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Serves as getter and setter. 
        *                           Sets if string has no digits and lengths is at least 2 chars.
        * Method parameters: string
        * Return value: string
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public string LastName
        {
            get => lastName;
            set
            {
                if (!value.Any(char.IsDigit) && value.Length > 1)
                {
                    lastName = value;
                }
                else
                {
                    throw new ArgumentException("Last name must be at least two letters long and cannot contain any digits.");
                 }
            }
        }

        /*
        * ***************************************************
        * Method Name: Address 
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Serves as getter and setter. 
        *                           Sets if string is at least 6 chars.
        * Method parameters: string
        * Return value: string
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public string Address
        {
            get => address;
            set
            {
                if (value.Length > 5)
                {
                    address = value;
                }
                else
                {
                    throw new ArgumentException("Address name must be at least 6 characters long.");
                }
            }
        }

        /*
        * ***************************************************
        * Method Name: DateOfBirth
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Serves as getter and setter for DOB.
        * Method parameters: DateTime
        * Return value: DateTime
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        /*
        * ***************************************************
        * Constructor: Person
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Creates Person object using properties.
        *                           Protected since it will only ever be called from child constructor.
        * Method parameters: string firstName, string lastName, string address, DateTime dateOfBirth
        * Return value: NA
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        protected Person(string firstName, string lastName, string address, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            DateOfBirth = dateOfBirth;
        }
    }
}
