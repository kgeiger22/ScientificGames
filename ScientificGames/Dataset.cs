/* Dataset.cs
 * 
 * This file holds the Dataset class and contains related methods and structs.
 * 
 * Datasets are generated randomly using the Random class.
 * Birth years are uniformally distrubuted between 1900 and 1999.
 * Lifetimes are uniformally distributed between 1 year and 99 years.
 * 
 * Because the instructions say that all birth and end years are between 1900 and 2000, any individual
 *  with an end year passed 2000 is thrown away.
 * This leads to a seemingly smaller amount of people being born later in the century.
 * Combined with the uniform birth dates and an average life expectancy of 50 years we can expect our highest
 *  population year to occur between 1930-1970, with results approaching 1950 with higher sample sizes.
 * We can also expect that the population at that time will be about half the total sample size
 *  
 * In the case of a tie, Year_Of_Highest_Population will always choose the earliest year
 */

using System;
using System.Collections.Generic;
using System.IO;


namespace ScientificGames
{
    //Structure used to store the birth and end of an individual
    public struct Data_Lifetime
    {
        public int m_year_of_birth;
        public int m_year_of_end;

        public Data_Lifetime(int _birth, int _end)
        {
            m_year_of_birth = _birth;
            m_year_of_end = _end;
        }

        public void Print()
        {
            Console.WriteLine("Year of Birth: {0}, Year of End: {1}", m_year_of_birth, m_year_of_end);
        }
    };

    //Structure used for outputting the highest population and year it occurred
    public struct Data_Best_Year
    {
        public int m_year;
        public int m_population;

        public Data_Best_Year(int _year, int _population)
        {
            m_year = _year;
            m_population = _population;
        }

        public void Print()
        {
            Console.WriteLine("Best Year: {0}, Population: {1}", m_year, m_population);
        }
    };



    //Generates and stores the dataset of birth/end years of any number of individuals
    //Information regarding dataset generation is located at the top of the file
    public class Dataset
    {
        Random m_rand = new Random();
        List<Data_Lifetime> m_data = new List<Data_Lifetime>();

        //Create an empty dataset
        public Dataset()
        {
        }

        //Create a new dataset of length '_size'
        public Dataset(uint _size)
        {
            for (uint i = 0; i < _size; ++i)
            {
                m_data.Add(Create_Data());
            }
        }

        //Create a new dataset from a file at location '_path'
        public Dataset(string _path)
        {
            Read_From_File(_path);
        }

        //Randomly generate a birth year/end for a person
        //Information regarding dataset generation is located at the top of the file
        private Data_Lifetime Create_Data()
        {
            //Generate a year of birth and a lifespan
            int year_of_birth = m_rand.Next(1900, 1999);
            int lifetime = m_rand.Next(1, 99);
            int year_of_end = year_of_birth + lifetime;

            //Repeat if the randomly generated person lives beyond the year 2000
            if (year_of_end > 1999) return Create_Data();
            //Otherwise return the data
            else return new Data_Lifetime(year_of_birth, year_of_end);
        }

        //Send the dataset to a file at location '_path'
        public void Output_To_File(string _path)
        {
            StreamWriter file = new StreamWriter(_path);
            foreach (var data in m_data)
            {
                file.WriteLine("{0}-{1}", data.m_year_of_birth, data.m_year_of_end);
            }
            file.Close();
        }

        //Read in a previously generated dataset from a file at location 'path'
        public void Read_From_File(string _path)
        {
            //Clear the existing dataset
            m_data.Clear();
            string[] lines;
            try
            {
                 lines = File.ReadAllLines(_path);
            }
            catch(Exception)
            {
                Console.WriteLine("File " + _path + " cannot be read");
                return;
            }
            foreach (string line in lines)
            {
                try
                {
                    //Parse the string into year of birth and year of end
                    string[] string_split = line.Split('-');
                    //Add the new data to the list if it is valid
                    if (string_split.Length != 2) continue;
                    int year_of_birth = Convert.ToInt32(string_split[0]);
                    int year_of_end = Convert.ToInt32(string_split[1]);
                    if (year_of_birth < 1900 || year_of_end > 1999) continue;
                    else m_data.Add(new Data_Lifetime(Convert.ToInt32(string_split[0]), Convert.ToInt32(string_split[1])));
                }
                //Ignore invalid input
                catch(Exception)
                {
                }
            }
        }

        //Display the contents of the dataset to the console
        public void Print()
        {
            foreach (var data in m_data)
            {
                Console.WriteLine("{0}-{1}", data.m_year_of_birth, data.m_year_of_end);
            }
        }

        //Calcute and return the highest year of population
        public Data_Best_Year Year_Of_Highest_Population()
        {
            //Return 0 if dataset is empty
            if (m_data.Count == 0) return new Data_Best_Year(0, 0);

            //Store the population change of each year
            List<int> population_change = new List<int>(new int[100]);
            foreach(var data in m_data)
            {
                //Increase in population when someone is born
                population_change[data.m_year_of_birth - 1900]++;
                //Decrease in population when someone passes away
                population_change[data.m_year_of_end - 1900]--;
            }

            //Keep track of our current population and our highest so far
            int current_population = 0;
            int highest_population = 0;
            int best_year = 0;
            for(int year = 0; year < 100; ++year)
            {
                //Update our current population every year
                current_population += population_change[year];
                //If our current population is higher than our highest seen, update our highest seen
                if (current_population > highest_population)
                {
                    highest_population = current_population;
                    best_year = year + 1900;
                }
            }
            return new Data_Best_Year(best_year, highest_population);
        }
    }
}
