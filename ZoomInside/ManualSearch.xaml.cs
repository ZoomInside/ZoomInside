using Firebase.Database;
using Mopups.Interfaces;
using Mopups.Services;

namespace ZoomInside;

public partial class ManualSearch : ContentPage
{
    FirebaseClient firebaseClient =
            new FirebaseClient("https://zoominside-2ccf3-default-rtdb.europe-west1.firebasedatabase.app/");

    private async Task LoadDataAsync()
    {
        // Simulate loading data asynchronously
        await Task.Delay(3000); // Placeholder for actual data loading code
    }

    IPopupNavigation popupNavigation;
    public ManualSearch(IPopupNavigation popupNavigation)
	{
		InitializeComponent();

        this.popupNavigation = popupNavigation;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (searchBox.Text == null)
        {
            await DisplayAlert("Грешка!", "Моля попълнете нужните полета!", "Добре!");
            return;
        }

        try
        {
            // Showing the activity indicator
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            await LoadDataAsync();

            var substance = searchBox.Text.ToLower();

            char[] splitCharacters = { '{', '}', ' ', ',', '[', ']' };
            List<string> resultTxt = substance
                .Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            for (var i = 0; i < resultTxt.Count; i++)
            {
                resultTxt[i] = resultTxt[i].ToLower();

                if (resultTxt[i][0] == 'e')
                    resultTxt[i] = resultTxt[i].Replace('e', 'е');
            }

            var firebaseObject = await firebaseClient.Child("Es").OnceAsync<EsItem>();
            List<EsItem> dataList = firebaseObject.Select(x => x.Object).ToList();


            //List<List<string>> propertyValues = new List<List<string>>(); var count_1 = 0;
            Dictionary<string, string> propertyValues = new Dictionary<string, string>();
            foreach (var item in dataList)
            {
                propertyValues.Add(item.DangerScale, item.Info);

                /*propertyValues.Add(new List<string>());
                propertyValues[count_1].Add(item.Info.ToLower());
                propertyValues[count_1].Add(item.DangerScale);
                count_1++;*/
            }
            /*foreach (var item in dataList)
            {
                propertyValues.Add(new List<string>());
                propertyValues[count_1].Add(item.Info.ToLower());
                propertyValues[count_1].Add(item.DangerScale);
                count_1++;
            }*/


            // propertyValues -> данните от файърбейз 
            // resultTxt -> данните от снимката 

            /*List<List<string>> final = new List<List<string>>(); var count_2 = 0;
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
            }*/

            // Hide the activity indicator
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;

            await popupNavigation.PushAsync(new MyMopup(propertyValues));
        }
        catch (Exception)
        {
            await DisplayAlert("Грешка!", "Нещо се обърка. Уверете се, че имате установена интернет връзка", "Добре!");
        }

        
    }


    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        infoBox.IsVisible = !infoBox.IsVisible;
    }
}