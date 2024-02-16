
using Firebase.Database;
using Microsoft.Maui.Storage;

namespace ZoomInside;

public partial class MediaPickerTests : ContentPage
{
    FirebaseClient firebaseClient =
            new FirebaseClient("https://zoominside-2ccf3-default-rtdb.europe-west1.firebasedatabase.app/");
    EsItem esItems = new EsItem();

    public MediaPickerTests()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {/*
        var firebaseObject = await firebaseClient.Child("Es").OnceAsync<EsItem>();
        List<EsItem> dataList = firebaseObject.Select(x => x.Object).ToList();

        List<string> propertyValues = new List<string>();
        foreach (var item in dataList)
        {
            propertyValues.Add(item.Info);
        }
        for (int i = 0; i < propertyValues.Count; i++)
        {
            propertyValues[i] = propertyValues[i].ToLower();
        }

        var fullText = "Blah blah";
        var addedItem = await firebaseClient.Child("Es").PostAsync(new EsItem
        {
            Info = fullText,
        });
        
        var addedItem = */
    }
}