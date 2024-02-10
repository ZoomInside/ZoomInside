using Camera.MAUI;
using RestSharp;
using System.Reflection.Emit;
using System.Text;
using System.IO;

namespace ZoomInside;

public partial class CameraAccessment : ContentPage
{
    public double ScreenWidth { get; set; }

    private async Task LoadDataAsync()
    {
        // Simulate loading data asynchronously
        await Task.Delay(3000); // Placeholder for actual data loading code
    }

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

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        CameraFunctionalities cameraFunctionalities = new CameraFunctionalities();

        var fullPath = cameraFunctionalities.CreateDirectory();

        // Saving the snapshot into the Snapshots folder
        ImageSource imagesource = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
        await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, fullPath);

        // Show the activity indicator
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        await LoadDataAsync();

        // Turn the image to binary
        byte[] imageBytes = File.ReadAllBytes(fullPath);

        // Accessing the API
        var extractedText = cameraFunctionalities.TextExtract(imageBytes);

        // Using our class to be able to manipulate cyrillic text
        var extractor = new ExtractingCyrillic();
        var formattedText = extractor.Format(extractedText);

        // Hide the activity indicator
        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;

        var languageType = extractor.IsCyrillic(extractedText);

        string[] cyrillicLangs = { "ru", "bg", "kk", "tg", "be", "sr", "ua", "ky", "mn", "mk", "me", "md" };
        bool isCyrillic = false;
        foreach (var item in cyrillicLangs)
        {
            if (languageType == item)
            {
                isCyrillic = true;
                break;
            }
        }

        var result = "";
        if (isCyrillic == true)
            result = extractor.ToCyrillic(formattedText).ToLower();
        else
            result = formattedText.ToLower();

        extractLabel.Text = result;
        tester.Source = imagesource;

        List<string> splitedResult = result
            .Split()
            .ToList();
    }
}