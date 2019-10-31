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
        static void Main(string[] args)
        {
            //I/O files paths. Though the free version accepts file < 1024 kb and max 3 pages.
            string[] arrayFilesToOCR = Directory.GetFiles(@"I:\input");
            string[] arrayDoneFiles = Directory.GetFiles(@"I:\output");

            foreach (string file in arrayFilesToOCR)
            {
                //Check what files has been done
                if (!(Array.Exists(arrayDoneFiles, element => Path.GetFileName(element) == Path.GetFileName(file))))
                {   
                    API api = new API();
                    Console.WriteLine(file);
                    api.OCRFile(file);
                    //The server of the API has a problem. It 
                    Thread.Sleep(30000);
                }                
            }

            Console.WriteLine("************************************************");
            Console.WriteLine("********************* END **********************");
            Console.WriteLine("************************************************");
            Console.ReadKey();
        
        }
    }
}
