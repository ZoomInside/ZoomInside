using Camera.MAUI;
using RestSharp;
using System.Reflection.Emit;
using System.Text;
using System.IO;

namespace ZoomInside;

public partial class CameraAccessment : ContentPage
{
	public CameraAccessment()
	{
		InitializeComponent();
    }

    string pathSaver = "";
    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        cameraView.Camera = cameraView.Cameras.First();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        });
    }
    
    private void Button_Clicked(object sender, EventArgs e)
    {
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");

        // Ensure the folder exists; create it if it doesn't
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "myImage.png";
        string fullPath = Path.Combine(folderPath, fileName);
        pathSaver = fullPath;

        // Saving the snapshot into the Snapshots folder
        ImageSource imagesource = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
        cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, fullPath);

        // Presenting the snapshot directly into the App
        myImage.Source = imagesource;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        // Turn the image to binary
        byte[] imageBytes = File.ReadAllBytes(pathSaver);
        
        // Accessing the API
        var client = new RestClient("https://api.apilayer.com/image_to_text/upload");
        client.Timeout = -1;

        var request = new RestRequest(Method.POST);
        request.AddHeader("apikey", "5vHByvhtnEVeseg5YQyJwSzoYYI6N4nq");

        request.AddParameter("text/plain", imageBytes, ParameterType.RequestBody);

        // Getting the text from the image
        IRestResponse response = client.Execute(request);
        var text = response.Content;


        // Using our class to be able to manipulate cyrillic text
        var extractor = new ExtractingCyrillic();
        var formattedText = extractor.Format(text);

        var toCyrillic = extractor.ToCyrillic(formattedText);

        extractEntry.Text = toCyrillic.ToLower();
    }
}