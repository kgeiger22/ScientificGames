/* Program.cs
 * Written by Keith Geiger 4/25/2018
 * Compiled in Visual Studio 2017
 * 
 * This program creates and/or reads a dataset of individuals with birth and end years between 
 *      1900 and 2000 and outputs the year of highest living population.
 *      
 * Tools and information regarding datasets can be found Dataset.cs
 * 
 * This file executes Main and will create and/or read datasets to use for testing.
 * Dataset text files can be located in the 'datasets' directory
 * Each entry is written in the format YYYY-YYYY followed by a new line
 */

using System;
using System.IO;

namespace ScientificGames
{
    public class Program
    {
        //Outputs highest population and year from file '_path'
        //Set 'print_all' to true to output the dataset to the console
        static public void Test_From_File(string _path, bool print_all = false)
        {
            Dataset test_set = new Dataset();
            test_set.Read_From_File(_path);
            if (print_all) test_set.Print();
            test_set.Year_Of_Highest_Population().Print();
            Console.WriteLine();
        }

        //Outputs highest population and year a randomly generated dataset of size '_size'
        //Set 'print_all' to true to output the dataset to the console
        static public void Test_New_Dataset(uint _size, bool print_all = false)
        {
            Dataset test_set = new Dataset(_size);
            if (print_all) test_set.Print();
            test_set.Year_Of_Highest_Population().Print();
            Console.WriteLine();
        }

        static void Main()
        {

#if false   //Set to 'true' to redirect console output to a text file
            FileStream stream = new FileStream("../../../ConsoleOutput.txt", FileMode.Create);
            StreamWriter file = new StreamWriter(stream);
            file.AutoFlush = true;
            Console.SetOut(file);
#endif
#if false   //Set to 'true' to output new dataset files
            
            Console.WriteLine("Outputting datasets to files...\n");
            Dataset dataset_10 = new Dataset(10);
            dataset_10.Output_To_File(@"../../../datasets/TestSize10.txt");

            Dataset dataset_100 = new Dataset(100);
            dataset_100.Output_To_File(@"../../../datasets/TestSize100.txt");

            Dataset dataset_1000 = new Dataset(1000);
            dataset_1000.Output_To_File(@"../../../datasets/TestSize1000.txt");

            Dataset dataset_100000 = new Dataset(100000);
            dataset_100000.Output_To_File(@"../../../datasets/TestSize100000.txt");
#endif

            Console.WriteLine("BEGIN TESTS\n");

            //To print the dataset to the console, call Test_From_File(path, true) like the two below

            //Test datasets randomly generated at runtime
            Console.WriteLine("Testing New Dataset Size 5");
            Test_New_Dataset(5, true);

            Console.WriteLine("Testing New Dataset Size 10");
            Test_New_Dataset(10, true);

            //To test from a file, make sure you move to the correct local directory first
            //The default directory is deep in the debug/release folder, so you have to go 3 levels up to ge to the project folder

            //Test size 10
            Console.WriteLine("Testing Dataset From File Size 10");
            Test_From_File(@"../../../datasets/TestSize10.txt");

            //Test size 100
            Console.WriteLine("Testing Dataset From File Size 100");
            Test_From_File(@"../../../datasets/TestSize100.txt");

            //Test size 1000
            Console.WriteLine("Testing Dataset From File Size 1000");
            Test_From_File(@"../../../datasets/TestSize1000.txt");

            //Test size 100000
            Console.WriteLine("Testing Dataset From File Size 100000");
            Test_From_File(@"../../../datasets/TestSize100000.txt");


            //INSERT ADDITIONAL TESTS HERE


            Console.WriteLine("\nPress Any Key to Close...");
            Console.ReadKey();


        }
    }
}
