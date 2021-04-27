using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace AuthenticatorServiceLibrary
{
    /*
     * 
     * This class has the implementation of IAuthenticator
     * Also include the implementation of a FileHandling as an internal class
     * 
     * 
     */

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = true)]
    internal class Authenticate : IAuthenticate
    {
        //The path for registration text file
        private string pathForRegistrationText = @"c:\reg.txt";

        //The path for token save text file
        private string pathForTokenText = @"c:\token.txt";

        //Implementation of the IAuthenticate interface which will be access by server

        public string Register(String name, String password)
        {
            String loginAndReg = name + "\t" + password;

            FileHandling writeObject = new FileHandling();
            writeObject.writeDataToTextFile(loginAndReg, pathForRegistrationText);

            return "Successfully Registered";

        }

        /*
         * 
         * Login function validates all the component
         * Return a postive token value if Login() successful
         * Return a negative if unsuccessful
         *
         */
        public int Login(String name, String password)
        {
            Random tokengen = new Random();
            String details = name + "\t" + password;
            FileHandling fileHandling = new FileHandling();

                if (File.ReadAllLines(pathForRegistrationText).Contains(details))
                {
                    int token = tokengen.Next(9999);
                    fileHandling.writeDataToTextFile(token.ToString(), pathForTokenText);
                //return random positive token if match is found
                    return token;
                }
                else
                { 
                //return negative integer as unsuccessfull
                    return -1;
                }

        }
    }

internal class FileHandling
    {

        /* Function to write save details in local text file */
    
        public void writeDataToTextFile(String Details, String textFilePath)
        {

            //Checking if file exists in path
            if (!File.Exists(textFilePath))
            {
                try
                {
                    //Create file if it does not exist
                    using (StreamWriter sw = File.CreateText(textFilePath))
                    {
                        sw.WriteLine(Details);
                        sw.Close();
                    }
                } catch (NotSupportedException e) {
                    Console.WriteLine(e.Message);

                } catch (DirectoryNotFoundException e) {
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

                } catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                } catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                } catch (FileNotFoundException e)
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
