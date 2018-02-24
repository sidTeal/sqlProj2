using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisdale_Project_2
{
    /*
    * ***************************************************
    * Class Name: Customer : Person
    * Author: Chris Tisdale
    * **************************************************** 
    * Purpose of the class:
    * This class provides constructor and properties to handle some user input validation
    * for Customer which is a child of Person.
    * **************************************************** 
    * Date: February 23, 2018
    * *****************************************************
    */
    class Customer : Person
    {
        private string favoriteDepartment;

        /*
        * ***************************************************
        * Method Name: FavoriteDepartment 
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
        public string FavoriteDepartment
        {
            get => favoriteDepartment;
            set
            {
                if (value.Length > 1)
                {
                    favoriteDepartment = value;
                }
                else
                {
                    throw new ArgumentException("Favorite department must be at least 2 characters long.");
                }

            }
        }

        /*
        * ***************************************************
        * Constructor: Customer
        * Author: Chris Tisdale
        * ***************************************************** 
        * Purpose of the Method: Creates Customer object using properties & calling Person constructor.
        * Method parameters: string firstName, string lastName, string address, DateTime dateOfBirth, string favoriteDepartment
        * Return value: NA
        * ***************************************************** 
        * Date: February 23, 2018
        * ****************************************************
        */
        public Customer(string firstName, string lastName, string address, DateTime dateOfBirth, string favoriteDepartment) 
            : base(firstName, lastName, address, dateOfBirth)
        {
            FavoriteDepartment = favoriteDepartment;
        }
    }
}
