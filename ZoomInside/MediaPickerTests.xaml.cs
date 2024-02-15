
using Microsoft.Maui.Storage;

namespace ZoomInside;

public partial class MediaPickerTests : ContentPage
{
	public MediaPickerTests()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        apiManipulation apiManipulation = new apiManipulation();
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                // Save the photo to a specific directory
                var fullPath = apiManipulation.CreateDirectory();

                var stream = await photo.OpenReadAsync();
                byte[] bytes = null;
                if (stream != null)
                {
                    using (MemoryStream toBytes = new MemoryStream())
                    {
                        await stream.CopyToAsync(toBytes);
                        bytes = toBytes.ToArray();
                    }
                }

                await File.WriteAllBytesAsync(fullPath, bytes);

                testImg.Source = fullPath;

                //await DisplayAlert("Success", "Photo saved to: " + fullPath, "OK");
            }
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Feature not supported on the device
            await DisplayAlert("Error", fnsEx.Message, "OK");
        }
        catch (PermissionException pEx)
        {
            // Permissions not granted
            await DisplayAlert("Error", pEx.Message, "OK");
        }
        catch (Exception ex)
        {
            // Other errors
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}