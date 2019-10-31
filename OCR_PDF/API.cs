using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OCR_PDF
{
    class API
    {
        //Dictionary<string, string> args = new Dictionary<string, string>();
        string APIKey = ""; //Personnal API Key
        string APIURLFree = "https://api.ocr.space/parse/image"; //Link to the API Service.

        RestClient client;

        public API()
        {
            client = new RestClient(APIURLFree);
        }
        
        
        public void OCRFile(string filePath)
        {
            RestRequest request = new RestRequest(Method.POST);
            string fileName = Path.GetFileName(filePath);


            //Add the options
            request.AddFile(fileName, filePath);
            request.AddParameter("apikey", APIKey);
            request.AddParameter("isCreateSearchablePdf", "true");
            request.AddParameter("isSearchablePdfHideTextLayer", "true");
            request.AddParameter("language", "fre");

            IRestResponse response = client.Execute(request);

            Files fichierReconnu = JsonConvert.DeserializeObject<Files>(response.Content);


            //File download
            using (WebClient myWebClient = new WebClient())
            {
                myWebClient.DownloadFile(fichierReconnu.SearchablePDFURL, @"I:\sortie\" + fileName);
            }

            Console.WriteLine(fichierReconnu.SearchablePDFURL);
            Console.WriteLine(filePath);
            Console.WriteLine("Temps Conversion :" + fichierReconnu.ProcessingTimeInMilliseconds);
            Console.WriteLine("ok");

        }
    }
}
