using System;
using System.Resources;

namespace CP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Path to testFile.txt must be changed to your download
                //Algorithm algorithm = new Algorithm(@"C:\Users\cmsmr2\Desktop\TextStats\TextStats\testfile.txt");

                Algorithm algorithm = new Algorithm(@"C:\Users\Adura\Documents\Personal Project\Bro Owolabi\C# Assignments\TextStats\TextStats\testfile.txt");
                
                algorithm.GetCharFrequency();
                algorithm.GetWordFrequency();
            }
            catch (Exception eX)
            {
                //Console.WriteLine("Hello World!");
                Console.WriteLine("An error has occured: " + eX.Message);
                Console.ReadLine();
            }
            
        }
    }
}
