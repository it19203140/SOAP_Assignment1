using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServiceProvider.Models
{
    public static class PrimeNumbers
    {
        /*
         * 
         * This function generates the all prime numbers to @param value from 0
         * 
         * References : StackOverFlow : https://stackoverflow.com/questions/1042902/most-elegant-way-to-generate-prime-numbers
         *              
         */
        public static List<string> GeneratePrimeNumbers(int value)
        {
            List<string> primesList = new List<string>();

            //Calling previous function and entering 0 as the starting value
            primesList = PrimeNumbers.GeneratePrimeNumbers(0, value);

            //Removing 0 from list of primes as zero also get returned
            primesList.Remove("0");

            //returning all prime numbers
            return primesList;
        }

        /*
         * 
         * This function generates prime numbers in a range
         * where starting value > 0
         * 
         */

        public static List<string> GeneratePrimeNumbers(int startingRange, int endingRange)
        {
            List<string> primesList = new List<string>();

            int number, flag;

            //nested for loop
            for (number = startingRange; number <= endingRange; number++)
            {
                flag = 0;

                //i = 2 as two is the lowest prime number
                for (int i = 2; i <= number / 2; i++)
                {
                    //getting numbers which are divisible by 2(means they are not prime)
                    if (number % i == 0)
                    {
                        //increment to identify as not prime
                        flag++;
                        break;
                    }
                }

                //if ctr == 0 -> if not a prime number and it is not -> 1
                if (flag == 0 && number != 1)
                    primesList.Add(number.ToString());
            }

            return primesList;

        }

        public static bool isPrime(int number)
        {
            int flag = 0;

            for(int i = 2; i<= number/2; ++i)
            {
                //if no remainder
                if(number % i == 0)
                {
                    flag = 1;
                    break;
                }
            }

            if (flag == 1)
                return false;

            if (flag == 0)
                return true;
            else
                return false;
        }

        /*
         * 
         * This function generates the first n number of Prime Numbers
         * Additional Function
         * 
         */

        public static List<int> GenerateFirstNPrimeNumbers(int n)
        {
            List<int> primesList = new List<int>();

            //Add first prime number 2
            primesList.Add(2);
            int nextPrime = 3;
            while (primesList.Count < n)
            {
                //Getting integer of the square root of the next prime number
                int squareRoot = (int)Math.Sqrt(nextPrime);

                //Initailize as true
                bool isPrime = true;
                for (int i = 0; (int)primesList[i] <= squareRoot; i++)
                {
                    //If remainder is zero then it is not a prime
                    if (nextPrime % primesList[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                //Add to list if prime number
                if (isPrime)
                {
                    primesList.Add(nextPrime);
                }
                nextPrime += 2;
            }

            //returning all prime numbers
            return primesList;
        }
    }
}