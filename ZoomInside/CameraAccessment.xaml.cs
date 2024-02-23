using Camera.MAUI;
using RestSharp;
using System.Reflection.Emit;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Firebase.Database;
using Mopups.Services;
using Mopups.Interfaces;

namespace ZoomInside;

public partial class CameraAccessment : ContentPage
{
    FirebaseClient firebaseClient =
            new FirebaseClient("https://zoominside-2ccf3-default-rtdb.europe-west1.firebasedatabase.app/");
    private async Task LoadDataAsync()
    {
        // Simulate loading data asynchronously
        await Task.Delay(3000); // Placeholder for actual data loading code
    }


    IPopupNavigation popupNavigation;
    public CameraAccessment(IPopupNavigation popupNavigation)
	{
		InitializeComponent();

        // Set the binding context to the current instance of the page (this)
        this.BindingContext = this;

        this.popupNavigation = popupNavigation;
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        apiManipulation apiManip = new apiManipulation();

        byte[] imageBytes = null;
        try
        {
            // Using MediaPicker to interact with the device camera
            var photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                // Save the photo to a specific directory
                var fullPath = apiManip.CreateDirectory();

                var stream = await photo.OpenReadAsync();
                if (stream != null)
                {
                    using (MemoryStream toBytes = new MemoryStream())
                    {
                        await stream.CopyToAsync(toBytes);
                        imageBytes = toBytes.ToArray();
                    }
                }
                //testImg.Source = fullPath;
                await File.WriteAllBytesAsync(fullPath, imageBytes);

                cameraButton.IsVisible = false;
                searchButtoon.IsVisible = false;
                //await DisplayAlert("Success", "Photo saved to: " + fullPath, "OK");
            }
            else
            {
                return;
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

        try
        {
            // Showing the activity indicator
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            await LoadDataAsync();

            // Accessing the API
            var extractedText = apiManip.TextExtract(imageBytes);

            // Removing the unnecessary characters from our text 
            var formattedText = apiManip.Format(extractedText);
            // Removing the escape symbols Visual Studio adds automatically to the unicode we received from the api
            var unescapedFormattedText = Regex.Unescape(formattedText);

            // Hide the activity indicator
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;

            cameraButton.IsVisible = true;
            searchButtoon.IsVisible = true;

            char[] splitCharacters = { '{', '}', ' ', ',', '[', ']' };
            List<string> resultTxt = unescapedFormattedText
                .Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            for (var i = 0; i < resultTxt.Count; i++)
            {
                resultTxt[i] = resultTxt[i].ToLower();
            }

            var firebaseObject = await firebaseClient.Child("Es").OnceAsync<EsItem>();
            List<EsItem> dataList = firebaseObject.Select(x => x.Object).ToList();


            List<List<string>> propertyValues = new List<List<string>>(); var count_1 = 0;
            foreach (var item in dataList)
            {
                propertyValues.Add(new List<string>());
                propertyValues[count_1].Add(item.Info.ToLower());
                propertyValues[count_1].Add(item.DangerScale);
                count_1++;
            }


            // propertyValues -> данните от файърбейз 
            // resultTxt -> данните от снимката 

            List<List<string>> final = new List<List<string>>(); var count_2 = 0;
            foreach (var subList in propertyValues)
            {
                var index = subList[0].IndexOf(':');
                var auxiliaryVar = subList[0].Substring(0, index);

                foreach (var item in resultTxt)
                {
                    if (item == auxiliaryVar)
                    {
                        final.Add(new List<string>());
                        final[count_2].Add(subList[0]);
                        final[count_2].Add(subList[1]);
                        count_2++;
                    }
                }
            }


            await popupNavigation.PushAsync(new MyMopup(final));
        }
        catch (Exception)
        {
            await DisplayAlert("Грешка!", "Нещо се обърка. Уверете се, че имате установена интернет връзка", "Добре!");
        }
        

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AdminAuthentication());
    }

    private void searchButtoon_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManualSearch(popupNavigation));
    }
}