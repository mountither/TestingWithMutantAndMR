using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssign2
{
    class Program
    {
        static void Main(string[] args)
        {
            //object to apply MR on a list. 
            MetamorphicRelations applyMR = new MetamorphicRelations();

            /*
            About the test cases:
            variate the test cases that cover wider cases.
            these test cases are meant to be used to kill the mutants. This is noticed by the output 
            of program and mutant.
            the manipulatelist class is the program that acts as the test oracle for the mutant.
            */

            /*
            How to know MT is killed:
            the test of eqiuvalence of the program (ManipulateList class) is required to determine
            whether the mutant compared is killed or not
            *must already know the output of each test case (dependent on P) that passes through a mutant.
            actual result: ManipulateList test result: FirstMutant and SecondMutant
            */

            /*
            mutation score (MS):
            ratio of killed mutants and total number of mutants present.
            total will always be 2. 
            */

            /* d. calc the effectiveness for MR1 and MR2. use the metric.
               e. discuss the results about effectiveness 
            the purpose:
            computing the effectiveness of the chosen MRs (two).
            such effectiveness is realised by using the classes of both mutants and the test cases. 
            effectiveness: amount of voilations/(number of voilations + no. of satis)
            *make table for this
            */

            //create 10 test cases (List of integers) here and send to method in that obj
            //b. describe all the elements of TC
            //List<int> testcase1 = new List<int>(){56, 10, 5, 6, 4, 12, 13, 43, 13, 20};

            

            ApplyTests test = new ApplyTests();
            Console.WriteLine("\nTESTS TESTCASES PASS THROUGH PROGRAM\n");
            test.ApplyTCandProgram();
            Console.WriteLine("\nTESTS MR AND PROGRAM\n");
            test.ApplyFirstMRWithProgram();
            test.ApplySecondMRWithProgram();

            Console.WriteLine("\nTESTS MR AND MUTANTS\n");
            test.ApplyFirstMRWithAllMutants();
            test.ApplySecondMRWithAllMutants();

           


            
            //Console.WriteLine(Enumerable.SequenceEqual(SO1, FO1));
        }
    }
}

class AccessCases{
    private List<List<int>> testCases;
    public AccessCases(){
        List<int> testcase1 = new List<int>(){56, 10, -5, 6, 4, 12, 13, 43, 13, 20}, //list with dupilicated integers
        testcase2 = new List<int>(){16, 13, 12, 10, 9, 8, 7, 5, 3, -2},   // list in descending order without duplicates
        testcase3 = new List<int>(){98, 98, 73, 71, 65, 29, 23, 21, 19, 8}, //list in descending order with duplicates
        testcase4 = new List<int>(){-20, -17, 32, 3, -17, 40, 45, 86, 89, 90, 93}, // list in ascending order with duplicates
        testcase5 = new List<int>(){2, 11, 20, 44, 54, 65, 73, 82, 91, 92},      // list in ascending order without duplicates
        testcase6 = new List<int>(){62, -31, -12, 95, 100, 7, 75, 2, 2, 21, 45},     // list not ascending nor descending with duplicates
        testcase7 = new List<int>(){82, 99, 16, 27, 81, 18, 32, 71, 50, 31},    // list not ascending nor descending without duplicates
        testcase8 = new List<int>(){50, 75, -37, 86, -8, 44, 94, -1, 52, 50},       // list with duplicates at start and end of list
        testcase9 = new List<int>(){59, 1, 24, 48, 6, 16, 34, 59, 12, 15},       // list with duplicates that are largest in list
        testcase10 = new List<int>(){49, 48, -3, 36, -1, 21, 35, 44, -3, 31};    //  list with duplicates that are smallest in list

        testCases = new List<List<int>>() {testcase1, testcase2, testcase3, testcase4, testcase5, testcase6,
                                            testcase7, testcase8, testcase9, testcase10};
    }
    public List<List<int>> GetCases{get => testCases; set => testCases = value;
    }
}

class ApplyTests{


    public void ApplyFirstMRWithAllMutants(){

        MetamorphicRelations mr = new MetamorphicRelations();
        AccessCases testCases = new AccessCases();

        //test with first mutant
        FirstMutatedManipulateList m1ForSI = new FirstMutatedManipulateList();
        FirstMutatedManipulateList m1ForFI = new FirstMutatedManipulateList();

       int countVoilations = 0;
        
        foreach(List<int> testCase in testCases.GetCases){

            List<int> sourceInputM1 = new List<int>(testCase);
            Console.WriteLine(string.Join(",", sourceInputM1));
            List<int> followUpInputM1 = new List<int>(testCase);
            
            List<int> sourceOutputM1 = m1ForSI.OrderAscAndRemoveDuplicates(sourceInputM1);
            mr.AddFirstElement(followUpInputM1);
            Console.WriteLine(string.Join(",", followUpInputM1));
            List<int> followUpOutputM1 = m1ForFI.OrderAscAndRemoveDuplicates(followUpInputM1);

            bool isEqualOutputPandM1 = Enumerable.SequenceEqual(sourceOutputM1,followUpOutputM1);

            if(!(isEqualOutputPandM1)){
                countVoilations += 1 ;
            }

            Console.WriteLine(string.Format("\nMTG/MUTANT 1\nSO --> {0}\nFO --> {1}\nIs MR1 Satisfied with mutant 1? -->{2}\n",
                            string.Join(",",sourceOutputM1), string.Join(",",followUpOutputM1), isEqualOutputPandM1));
        }

        //test with second mutant
        SecondMutatedManipulateList m2forSI = new SecondMutatedManipulateList();
        SecondMutatedManipulateList m2ForFI = new SecondMutatedManipulateList();
        
        foreach(List<int> testCase in testCases.GetCases){

            List<int> sourceInputM2 = new List<int>(testCase);
            Console.WriteLine(string.Join(",", sourceInputM2));
            List<int> followUpInputM2 = new List<int>(testCase);
            
            List<int> sourceOutputM2 = m2forSI.OrderAscAndRemoveDuplicates(sourceInputM2);
            mr.AddFirstElement(followUpInputM2);
            Console.WriteLine(string.Join(",", followUpInputM2));
            List<int> followUpOutputM2 = m2ForFI.OrderAscAndRemoveDuplicates(followUpInputM2);

            bool isEqualOutputPandM2 = Enumerable.SequenceEqual(sourceOutputM2,followUpOutputM2);
            if(!(isEqualOutputPandM2)){
                countVoilations += 1 ;
            }

            Console.WriteLine(string.Format("\nMTG/MUTANT 2\nSO --> {0}\nFO --> {1}\nIs MR1 Satisfied with mutant 2? -->{2}",
                            string.Join(",",sourceOutputM2), string.Join(",",followUpOutputM2), isEqualOutputPandM2));
        }

        Console.WriteLine("Amount of voilations MR1 reveals: " + countVoilations);
        Console.WriteLine("Effectiveness of MR1: " + (double)countVoilations / (double)(testCases.GetCases.Count()*2));
    }


    public void ApplySecondMRWithAllMutants(){
        
        MetamorphicRelations mr = new MetamorphicRelations();
        AccessCases testCases = new AccessCases();

        
        //test with first mutant
        FirstMutatedManipulateList m1ForSI = new FirstMutatedManipulateList();
        FirstMutatedManipulateList m1ForFI = new FirstMutatedManipulateList();

       int countVoilations = 0;
        
        foreach(List<int> testCase in testCases.GetCases){
            
            List<int> sourceInputM1 = new List<int>(testCase);
            Console.WriteLine(string.Join(",", sourceInputM1));
            List<int> followUpInputM1 = new List<int>(testCase);
            //The MTG Pair
            List<int> sourceOutputM1 = m1ForSI.OrderAscAndRemoveDuplicates(sourceInputM1);
            mr.PermutateList(followUpInputM1);
            Console.WriteLine(string.Join(",", followUpInputM1));
            List<int> followUpOutputM1 = m1ForFI.OrderAscAndRemoveDuplicates(followUpInputM1);
            
            bool isEqualM1OutputSOandFO = Enumerable.SequenceEqual(sourceOutputM1,followUpOutputM1);
            if(!(isEqualM1OutputSOandFO)){
                countVoilations += 1 ;
            }

            Console.WriteLine(string.Format("\nMTG/MUTANT 1\nSO --> {0}\nFO --> {1}\nIs MR2 Satisfied with mutant 1? -->{2}\n",
                            string.Join(",",sourceOutputM1), string.Join(",",followUpOutputM1), isEqualM1OutputSOandFO));
        }

        //test with second mutant
        SecondMutatedManipulateList m2forSI = new SecondMutatedManipulateList();
        SecondMutatedManipulateList m2ForFI = new SecondMutatedManipulateList();
        
        foreach(List<int> testCase in testCases.GetCases){
            List<int> sourceInputM2 = new List<int>(testCase);
            List<int> followUpInputM2 = new List<int>(testCase);
            //The MTG Pair
            List<int> sourceOutputM2 = m2forSI.OrderAscAndRemoveDuplicates(sourceInputM2);
            mr.PermutateList(followUpInputM2);
            List<int> followUpOutputM2 = m2ForFI.OrderAscAndRemoveDuplicates(followUpInputM2);

            bool isEqualM2OutputSOandFO = Enumerable.SequenceEqual(sourceOutputM2,followUpOutputM2);
            //the amount of voilations (false) / the amount of testcases used by MR1 in both mutants (20) (true + false)
            if(!(isEqualM2OutputSOandFO)){
                countVoilations += 1 ;
            }
            Console.WriteLine(string.Format("\nMTG/MUTANT 2 \nSO --> {0}\nFO --> {1}\nIs MR2 Satisfied with mutant 2? -->{2}",
                            string.Join(",",sourceOutputM2), string.Join(",",followUpOutputM2), isEqualM2OutputSOandFO));
        }
        Console.WriteLine("Amount of voilations MR2 reveals: " + countVoilations);
        Console.WriteLine("Effectiveness of MR2: " + (double)countVoilations / (double)(testCases.GetCases.Count()*2));
    }

    public void ApplyFirstMRWithProgram(){
        AccessCases testCases = new AccessCases();
        MetamorphicRelations mr = new MetamorphicRelations();
        //test with program 
        ManipulateList program = new ManipulateList();

        foreach(List<int> testCase in testCases.GetCases){

            List<int> sourceInputProgram = new List<int>(testCase);
            List<int> followUpInputProgram = new List<int>(testCase);
            
            List<int> sourceOutputProgram = program.OrderAscAndRemoveDuplicates(sourceInputProgram);
            mr.AddFirstElement(followUpInputProgram);
            List<int> followUpOutputProgram = program.OrderAscAndRemoveDuplicates(followUpInputProgram);

             //FI = SI, FO = SO
            bool isEqualOutputP = Enumerable.SequenceEqual(sourceOutputProgram,followUpOutputProgram);

            Console.WriteLine(string.Format("MTG/Program \nSO --> {0}\nFO --> {1}\nIs MR1 Satisfied with Program? -->{2}",
                            string.Join(",",sourceOutputProgram), string.Join(",",followUpOutputProgram), isEqualOutputP));
         }
    
    }

    public void ApplySecondMRWithProgram(){
        AccessCases testCases = new AccessCases();
        MetamorphicRelations mr = new MetamorphicRelations();
        //test with program 
        ManipulateList program = new ManipulateList();

        foreach(List<int> testCase in testCases.GetCases){
            List<int> sourceInputProgram = new List<int>(testCase);
            List<int> followUpInputProgram = new List<int>(testCase);
            
            List<int> sourceOutputProgram = program.OrderAscAndRemoveDuplicates(sourceInputProgram);
            mr.PermutateList(followUpInputProgram);
            List<int> followUpOutputProgram = program.OrderAscAndRemoveDuplicates(followUpInputProgram);

            bool isEqualOutputP = Enumerable.SequenceEqual(sourceOutputProgram,followUpOutputProgram);

            Console.WriteLine(string.Format("MTG/Program \nSO --> {0}\nFO --> {1}\nIs MR2 Satisfied with Program? -->{2}",
                            string.Join(",",sourceOutputProgram), string.Join(",",followUpOutputProgram), isEqualOutputP));
         }
    
    }

     public void ApplyTCandProgram(){

        AccessCases testCases = new AccessCases();

        ManipulateList program = new ManipulateList();
        
        foreach(List<int> testCase in testCases.GetCases){

            List<int> sourceOutputProgram = program.OrderAscAndRemoveDuplicates(testCase);

            Console.WriteLine(string.Join(",", sourceOutputProgram));
        }


    }

}


//create a class that takes in a source input (SI) list and outputs the follow input (FI) list.
//this class going to have two methods. First MR and Second MR. 
class MetamorphicRelations{
    public void AddFirstElement(List<int> followUpInputlist){

        followUpInputlist.Add(followUpInputlist[0]);
    }
    public void PermutateList(List<int> sourceInputList){

        // return new List<int>(sourceOuputList){sourceOuputList[sourceOuputList.Count - 1] + 2}; 
        for (int i = 0; i < sourceInputList.Count; i += 2) {
            if(i+1 >= sourceInputList.Count) {
                break;
            }
            int temp = sourceInputList[i];
            sourceInputList[i] = sourceInputList[i+1];
            sourceInputList[i+1] = temp;
        }
        
    } 

}


//this is the orginal program (P)
class ManipulateList{
    public List<int> OrderAscAndRemoveDuplicates(List<int> list){
        List<int> finalList = new List<int>();
        for(int i = 0; i<list.Count - 1 ; i++){
            for(int j = 0; j < list.Count -1; j++){
                if(list[j] > list[j + 1]){
                    int temp = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = temp;
                }
            }
        }
        for (int i = 0; i < list.Count - 1; i++){
            if(list[i] != list[i+1]){
                finalList.Add(list[i]);
            }
        }
        finalList.Add(list[list.Count - 1]);
        return finalList;
    }
}


//create two classes that are mutant of the above class. a different interperation than the og class      
//creating the mutants - logical and relational operators
//c. describe mutated classes (based on ManipulateList) below. - only the changed aspects 
//d. explain the process applied to make sure both mutants are not equivalent. 

//first mutant. mutation: replacement of vairable/ logical operator. 
class FirstMutatedManipulateList{
    public List<int> OrderAscAndRemoveDuplicates(List<int> list){
        List<int> finalList = new List<int>();
        for(int i = 0; i < list.Count -1 ; i++){
            for(int j = 0; j < list.Count - 1; j++){
                if(list[i] > list[j + 1]){
                    int temp = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = temp;
                }
            }
        }
        for (int i = 0; i < list.Count - 1; i++){
            if(!(list[i] != list[i+1])){
                finalList.Add(list[i]);
            }
        }
        finalList.Add(list[list.Count - 1]);
        return finalList;
    }
}

//second mutatant
//change of constant value and relational operator.
class SecondMutatedManipulateList{
    public List<int> OrderAscAndRemoveDuplicates(List<int> list){
        List<int> finalList = new List<int>();
        for(int i = 0; i < list.Count - 5; i++){
            for(int j = 0; j < list.Count -1; j++){
                if(list[j] <= list[j + 1]){
                    int temp = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = temp;
                }
            }
        }
        for (int i = 0; i < list.Count - 1; i++){
            if(list[i] != list[i+1]){
                finalList.Add(list[i]);
            }
        }
        finalList.Add(list[list.Count - 1]);
        return finalList;
       
    }
}
