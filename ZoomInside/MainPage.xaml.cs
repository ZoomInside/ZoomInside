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
        public MainPage(/*IPopupNavigation popupNavigation*/)
        {
            InitializeComponent();

            //this.popupNavigation = popupNavigation;
        }
        
        private async void AddValue(object sender, EventArgs e)
        {
            if (eType.Text == null)
            {
                await DisplayAlert("Грешка!", "Моля, попълнете задължителните полета!", "Добре!");
                return;
            }
            if (explanationEntry.Text == null)
            {
                await DisplayAlert("Error!", "Моля, попълнете задължителните полета!", "Добре!");
                return;
            }
            if (dangerScaleEntry.Text == null)
            {
                await DisplayAlert("Error!", "Моля, попълнете задължителните полета!", "Добре!");
                return;
            }

            var fullText = eType.Text + ": " + explanationEntry.Text;
            var dangerScale = dangerScaleEntry.Text;
            var a = await firebaseClient.Child("Es").PostAsync(new EsItem
            {
                Info = fullText,
                DangerScale = dangerScale,
            });

            eType.Text = string.Empty;
            explanationEntry.Text = string.Empty;
            dangerScaleEntry.Text = string.Empty;

            var firebaseObject = await firebaseClient.Child("Es").OnceAsync<EsItem>();
            List<EsItem> dataList = firebaseObject.Select(x => x.Object).ToList();

            List<string> propertyValues = new List<string>();
            foreach (var item in dataList)
            {
                propertyValues.Add(item.Info);
            }

            //await popupNavigation.PushAsync(new MyMopup(propertyValues));
        }        

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraAccessment(popupNavigation));
        }

        private void eType_Completed(object sender, EventArgs e)
        {
            explanationEntry.Focus();
        }

        private void explanationEntry_Completed(object sender, EventArgs e)
        {
            dangerScaleEntry.Focus();
        }

        private void dangerScaleEntry_Completed(object sender, EventArgs e)
        {
            dataButton.Focus();
        }
    }
}