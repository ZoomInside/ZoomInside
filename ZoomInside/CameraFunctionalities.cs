using Camera.MAUI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomInside
{
    public class CameraFunctionalities
    {
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
            request.AddHeader("apikey", "5vHByvhtnEVeseg5YQyJwSzoYYI6N4nq");

            request.AddParameter("text/plain", imageBytes, ParameterType.RequestBody);

            // Getting the text from the image
            IRestResponse response = client.Execute(request);
            var text = response.Content;

            return text;
        }
    }
}
