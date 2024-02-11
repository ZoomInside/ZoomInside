using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZoomInside
{
    public class apiManipulation
    {        
        public string Format(string text)
        {
            var item_to_find = "annotations";
            var index = Math.Abs(text.IndexOf(item_to_find));

            var substract = text.Substring(index);
            substract = substract.Replace("[", "");
            substract = substract.Replace("]", "");
            substract = substract.Replace("\"", "");
            substract = substract.Replace("\r\n", "");

            substract = substract.Remove(0, 13);
            substract = substract.Remove(substract.Length - 2);

            return substract;
        }
        public string CreateDirectory()
        {
            // Directory for saving the snapshots
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");

            // Ensure the folder exists; create it if it doesn't
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = "myImage.png";
            string fullPath = Path.Combine(folderPath, fileName);

            return fullPath;
        }
        public string TextExtract(byte[] imageBytes)
        {
            var client = new RestClient("https://api.apilayer.com/image_to_text/upload");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("apikey", "ReTR0RmPWqyc5CYhZM91V3iL3rmdHmAq");

            request.AddParameter("text/plain", imageBytes, ParameterType.RequestBody);

            // Getting the text from the image
            IRestResponse response = client.Execute(request);
            var text = response.Content;

            return text;
        }
    }
}
