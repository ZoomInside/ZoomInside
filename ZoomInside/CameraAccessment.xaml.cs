using Camera.MAUI;
using RestSharp;

namespace ZoomInside;

public partial class CameraAccessment : ContentPage
{
	public CameraAccessment()
	{
		InitializeComponent();
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        cameraView.Camera = cameraView.Cameras[0];

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        });
    }
    
    private void Button_Clicked(object sender, EventArgs e)
    {
        // Saving the snapshot into the Snapshots folder
        ImageSource imagesource = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
        cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, "C:\\Users\\Yanis\\Documents\\GitHub\\ZoomInside\\ZoomInside\\Snapshots\\myImage.png");

        // Presenting the snapshot directly into the App
        myImage.Source = imagesource;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        // ??????????? ?? ??????, ?? ?? ?? ????? ????????? ????????
        /*var client = new RestClient("https://api.apilayer.com/image_to_text/upload");
        client.Timeout = -1;

        var request = new RestRequest(Method.POST);
        request.AddHeader("apikey", "5vHByvhtnEVeseg5YQyJwSzoYYI6N4nq");

        request.AddParameter("text/plain", "C:\\Users\\Yanis\\Documents\\GitHub\\ZoomInside\\ZoomInside\\Snapshots\\myImage.png", ParameterType.RequestBody); // ?!?!?

        IRestResponse response = client.Execute(request);
        //Console.WriteLine(response.Content);
        extractEntry.Text = response.Content;*/
    }
}