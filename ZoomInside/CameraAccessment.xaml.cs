using Camera.MAUI;
using RestSharp;
using System.Reflection.Emit;
using System.Text;
using System.IO;

namespace ZoomInside;

public partial class CameraAccessment : ContentPage
{
    public double ScreenWidth { get; set; }
    public CameraAccessment()
	{
		InitializeComponent();

        // Get the device's screen height
        ScreenWidth = Math.Round(DeviceDisplay.MainDisplayInfo.Width / 3);

        // Set the binding context to the current instance of the page (this)
        this.BindingContext = this;        
    }
    
    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        cameraView.Camera = cameraView.Cameras.First();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        });
    }   

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        CameraFunctionalities cameraFunctionalities = new CameraFunctionalities();

        var fullPath = cameraFunctionalities.CreateDirectory();

        // Saving the snapshot into the Snapshots folder
        ImageSource imagesource = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
        cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, fullPath);

        // Presenting the snapshot directly into the App
        //myImage.Source = imagesource;


        // Turn the image to binary
        byte[] imageBytes = File.ReadAllBytes(fullPath);

        // Accessing the API
        var extractedText = cameraFunctionalities.TextExtract(imageBytes);


        // Using our class to be able to manipulate cyrillic text
        var extractor = new ExtractingCyrillic();
        var formattedText = extractor.Format(extractedText);
        var toCyrillic = extractor.ToCyrillic(formattedText);

        extractLabel.Text = toCyrillic.ToLower();

        tester.Source = imagesource;
    }
}