using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextStats;

namespace CP
{
    // Assumptions; 
    //  Numeric and special characters are treated like space.
    //      "20th" is treated as the word "th".
    //      'I co-wrote' is treated as 3 words: 'I', 'co' and 'wrote'
    //  Words are to be treated as the uppercase version.
    //      'AMBIENT', 'Ambient' and 'ambient' are treated as "AMBIENT".
    //  Lower and upper case characters are not differentiated.
    //      'A' and 'a' are both 'A'.
    //      Special characters are ignored. Not interested in frequency of '-'.
    public class Algorithm
    {
        // To implement ..
        private string inputTextFileLocation = "";
        private readonly string outputTextFileLocation = @"C:\Users\Adura\Documents\Personal Project\Bro Owolabi\C# Assignments\TextStats\TextStats\";

        public Algorithm(string inputFileLocation)
        {
            this.inputTextFileLocation = inputFileLocation;           
        }      

        private string GetInputTextFileContents()
        {  
            //read the contents of the text file
            string inputTextFileContents = File.ReadAllText(inputTextFileLocation);

            //check if content is empty or not
            if (!string.IsNullOrEmpty(inputTextFileContents))
            {
                //based on assumption, 'Ico-wrote' is treated as 3 words: 'I', 'co' and 'wrote' 
                inputTextFileContents = inputTextFileContents.ToUpper().Replace("co-wrote".ToUpper(), "co wrote".ToUpper());

                //by inference, "-" should be converted to a single space
                inputTextFileContents = inputTextFileContents.Replace("-", " ");

                //‘20th’ is treated as the word ‘th’
                inputTextFileContents = inputTextFileContents.Replace("20th".ToUpper(), "th".ToUpper());

                //get rid of all special characters
                inputTextFileContents = inputTextFileContents.Replace("’", " ").Replace(".", "").Replace(",", "").Replace("\'", " ").Replace(":", " "); 
            }

            return inputTextFileContents;
        }
        
        //method
        public void GetCharFrequency()
        {
            //to store the count of occurrence of each alphabet in the text file
            Dictionary<char, int> dictionaryAlphabetCount = new Dictionary<char, int>();

            //get the textfilecontents from GetTextFileContents method
            string inputTextFileContents = GetInputTextFileContents();

            //name of output text file
            string outputTextFilename = "ProgramOutput.txt";
            string outputFileLocation = outputTextFileLocation + outputTextFilename;

            if (string.IsNullOrEmpty(inputTextFileContents))
            {
                //write to an output file that input text file is empty
                File.WriteAllText(outputFileLocation, "Input text file is empty");
            }
            else
            {
                //remove all empty spaces from the input text file contents
                inputTextFileContents = inputTextFileContents.Replace(" ", "");

                //convert the input text file contents to an array of character
                char[] arrayInputTextFileContents = inputTextFileContents.ToCharArray();

                //convert the english alphabets to an array of character and use each charater to search through the input text file content
                //for the number of times the character appears in the input text file contents

                string englishAlphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                //convert the english alphabets to an array of character
                char[] arrayEnglishAlphabets = englishAlphabets.ToCharArray();

                //loop through arrayEnglishAlphabets and use each charater to search for the number of times the character appears in the character array of input text file contents
                for (int a = 0; a < arrayEnglishAlphabets.Length; a++)
                {
                    char alphabet = arrayEnglishAlphabets[a];

                    int frequencyCount = 0;

                    for (int i = 0; i < arrayInputTextFileContents.Length; i++)
                    {
                        char alphabetFromInputTextFile = arrayInputTextFileContents[i];

                        if (alphabet == alphabetFromInputTextFile)
                        {
                            frequencyCount = frequencyCount + 1;
                        }
                    }

                    dictionaryAlphabetCount.Add(alphabet, frequencyCount);
                }

                //write the frequency count to an output file

                //loop through dictionaryAlphabetCount and fetch the frequency count
                string output = "Item\t\t\t\t\tFrequency\n";

                foreach (var item in dictionaryAlphabetCount)
                {
                    output += (item.Key.ToString() + "\t\t\t\t\t\t" + item.Value.ToString() + "\n");
                }

                File.WriteAllText(outputFileLocation, output);
            }

        }

        //method
        public void GetWordFrequency()
        {
            //to store the count of occurrence of each work in the text file
            Dictionary<string, int> dictionaryWordCount = new Dictionary<string, int>();           

            //get the textfilecontents from GetTextFileContents method
            string inputTextFileContents = GetInputTextFileContents();

            //name of output text file
            string outputTextFilename = "ProgramOutput.txt";
            string outputFileLocation = outputTextFileLocation + outputTextFilename;           

            if (string.IsNullOrEmpty(inputTextFileContents))
            {
                //write to an output file that input text file is empty
                File.WriteAllText(outputFileLocation, "Input text file is empty");
            }
            else
            {
                //convert all empty spaces that appear twice in the input text file contents to a single space 
                inputTextFileContents = inputTextFileContents.Replace("  ", " ");

                //convert the input text file contents to an array of words
                string[] arrayInputTextFileContents = inputTextFileContents.Split(' ');

                //store all the word in a list object
                List<string> mainListWords = new List<string>();

                foreach (var item in arrayInputTextFileContents)
                {
                    //do not insert empty word; do not insert if word is @
                    if (!string.IsNullOrEmpty(item))
                    {
                        //from assumption, letter "@" should be ignored
                        if (item != "@")
                        {
                            //do not add number
                            int number = 0;

                            if(!int.TryParse(item, out number))
                            {
                                mainListWords.Add(item.Trim());
                            }                          
                        }
                    }
                }

                //write the contents of mainListWords to another list object
                List<string> listWords = mainListWords;

                //loop through mainListWords and use each word to search for the number of times the word appears in listWords
                foreach (string item in mainListWords)
                {
                    string wordFromMainListWords = item;

                    int frequencyCount = 0;

                    foreach (string item2 in listWords)
                    {
                        string wordFromListWords = item2;

                        if (wordFromMainListWords == wordFromListWords)
                        {
                            frequencyCount = frequencyCount + 1;
                        }
                    }

                    //check if key (or wordFromMainListWords) has once been added to dictionaryWordCount
                    if (!dictionaryWordCount.ContainsKey(wordFromMainListWords))
                    {
                        dictionaryWordCount.Add(wordFromMainListWords, frequencyCount);
                    } 
                }

                List<KeyValuePair<string, int>> sortedDictionaryWordCount = dictionaryWordCount.OrderBy(m => m.Key).ToList();               

                //write the frequency count to an output file

                //loop through dictionaryAlphabetCount and fetch the frequency count
                string output = "\n";
                output += "Item\t\t\t\t\tFrequency\n";

                foreach (var item in sortedDictionaryWordCount)
                {
                    if (item.Key.Length < 4)
                    {
                        output += (item.Key.ToString() + "\t\t\t\t\t\t" + item.Value.ToString() + "\n");
                    }
                    else if (item.Key.Length > 7)
                    {
                        output += (item.Key.ToString() + "\t\t\t\t" + item.Value.ToString() + "\n");
                    }
                    else
                    {
                        output += (item.Key.ToString() + "\t\t\t\t\t" + item.Value.ToString() + "\n");
                    }
                }

                File.AppendAllText(outputFileLocation, output);
            }
        }
    }
}
