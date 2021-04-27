using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace SOA_Utilities
{
    /*
     * 
     * This is a static Utility Class which allows for filehandling purpose
     * 
     *  References : Filehandling : https://www.tutorialspoint.com/vb.net/vb.net_file_handling.htm
     *                              https://stackoverflow.com/questions/7706467/access-to-the-path-denied-error-in-c-sharp
     */


    public static class FileHandling
    {
        /* Function to write save details in local text file */

        public static void writeDataToTextFile(String Details, String textFilePath, int type)
        {

            //Checking if file exists in path
            if (!File.Exists(textFilePath))
            {
                try
                {
                    //Create file if it does not exist
                    using (StreamWriter sw = File.CreateText(textFilePath))
                    {
                        //Creating header for username and password
                        if (type == 1)
                        {
                            sw.WriteLine("USERNAME\tPASSWORD");
                            sw.WriteLine();
                        }
                        // If token create header for token
                        else if(type == 2)
                        {
                            sw.WriteLine("TOKEN");
                            sw.WriteLine();
                        }

                        sw.WriteLine(Details);
                        sw.Close();
                    }
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.Message);

                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            else
            {
                //Append file if it does exist in path
                try
                {
                    FileStream fs = new FileStream(textFilePath, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(Details);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }



    }
}



/*            //Checking if Register successfull

            allNameAndPassword = writeObject.readDataFromTextFile(name, password);
            foreach(String textFileDetails in allNameAndPassword)
            {
                if(textFileDetails.Equals(Details))
                {
                    return "Successfully Registered";
                }
                else
                {
                    return "Not Registered";
                }
            }*/


/*            Random tokengen = new Random();
            List<String> allNameAndPassword = new List<String>();
            FileHandling fileHandling = new FileHandling();
            allNameAndPassword = fileHandling.readDataFromTextFile(pathForRegistrationText);

            foreach (String usernameAndPassword in allNameAndPassword)
                if (details.Equals(usernameAndPassword))
                    flag = true;

            if (flag)
            {
                int token = tokengen.Next(9999);
                fileHandling.writeDataToTextFile(token.ToString(), pathForTokenText);
                return token;
            }*/





/*
         * 
         * Function to read details from a existing file
         * @returns List<String> which all the users registered.
         * 
         * 
         */


/*        public List<String> readDataFromTextFile(String textFilePath)
        {
            List<String> loginDetails = new List<string>();

            String path = @"c:\test.txt";
            if (!File.Exists(textFilePath))
            {
                using (StreamReader SR = new StreamReader(textFilePath))
                {
                    string line = null;
                    while ((line = SR.ReadLine()) != null)
                    {
                        loginDetails.Add(line);
                        SR.Close();
                    }
                }

                return loginDetails;
            }

            else
            {
                return loginDetails;
            }
        }*/
