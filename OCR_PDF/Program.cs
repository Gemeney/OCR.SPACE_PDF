using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using RestSharp;
using System.Threading;

namespace OCR_PDF
{
    class Program
    {

        //This simple software ames to use the OCR.SPACE API, for analysing and makind an indexed PDF.
        static void Main()
        {
            //I/O files paths. Though the free version accepts file < 1024 kb and max 3 pages.
            string inputFolder = @"I:\input";
            string outputFodler = @"I:\output";
            string[] arrayFilesToOCR = Directory.GetFiles(inputFolder);
            string[] arrayDoneFiles = Directory.GetFiles(outputFodler);

            int timeBeforeTry = 10; //The API's server has a problem. It's necessary to relaunch the OCR.
            API api = new API();

            try { 
                foreach (string file in arrayFilesToOCR)
                {
                    //Check what files has been done
                    if (!(Array.Exists(arrayDoneFiles, element => Path.GetFileName(element) == Path.GetFileName(file))))
                    {   
                        
                        Console.WriteLine(file);
                        api.OCRFile(file, outputFodler);
                    }                
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                for (int time = timeBeforeTry; time != 0; time--)
                {
                    Console.Write("\nCount down before new atempt : " + time.ToString() + "s");
                    Thread.Sleep(1000);
                    ClearCurrentConsoleLine();
                }

                //Clear the line and write the new countdown  time.
                void ClearCurrentConsoleLine()
                {
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor - 1);
                }

                Main();
            }


            Console.WriteLine("************************************************");
            Console.WriteLine("********************* END **********************");
            Console.WriteLine("************************************************");
            Console.ReadKey();
        
        }
    }
}
