using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System;
using System.Drawing;
using Mopups.Interfaces;


namespace ZoomInside
{    
    public partial class MainPage : ContentPage
    {

        FirebaseClient firebaseClient = 
            new FirebaseClient("https://zoominside-2ccf3-default-rtdb.europe-west1.firebasedatabase.app/");

        IPopupNavigation popupNavigation;
        public MainPage(IPopupNavigation popupNavigation)
        {
            InitializeComponent();

            this.popupNavigation = popupNavigation;
        }
        
        private async void GetByKey(object sender, EventArgs e)
        {
            var fullText = eType.Text + ": " + explanationEntry.Text;
            var a = firebaseClient.Child("Es").PostAsync(new EsItem
            {
                Info = fullText,
            });

            eType.Text = string.Empty;
            explanationEntry.Text = string.Empty;

            //SavingFireValues();

            var firebaseObject = await firebaseClient.Child("Es").OnceAsync<EsItem>();
            List<EsItem> dataList = firebaseObject.Select(x => x.Object).ToList();

            List<string> propertyValues = new List<string>();
            foreach (var item in dataList)
            {
                propertyValues.Add(item.Info);
            }
            foreach (var item in propertyValues)
            {
                resultLabel.Text += item + "\n";
            }
                        
            await popupNavigation.PushAsync(new MyMopup(propertyValues));
        }        

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraAccessment());
        }
    }
}