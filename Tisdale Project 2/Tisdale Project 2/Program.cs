using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
* ***************************************************
* Course: CIS 4352-110
* Project 2
* Author: Chris Tisdale
* **************************************************** 
* Purpose of the Assignment:
* Allow user to log into a database. Allow user to: write,
* read, update, and delete data in at least two tables.
* **************************************************** 
* Date: February 23, 2018
* *****************************************************
*/
namespace Tisdale_Project_2
{
    /*
    * ***************************************************
    * Class Name: Program
    * Author: Chris Tisdale
    * **************************************************** 
    * Purpose of the class:
    * This class serves as main program execution.
    * **************************************************** 
    * Date: February 23, 2018
    * *****************************************************
    */
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
