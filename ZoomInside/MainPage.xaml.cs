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

        string dangerScale;
        private void dangerScale_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                dangerScale = radioButton.Content.ToString();
            }
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
                await DisplayAlert("Грешка!", "Моля, попълнете задължителните полета!", "Добре!");
                return;
            }
            if (dangerScale_1.IsChecked == false && dangerScale_2.IsChecked == false && dangerScale_3.IsChecked == false)
            {
                await DisplayAlert("Грешка!", "Моля, попълнете задължителните полета!", "Добре!");
                return;
            }

            var fullText = eType.Text + ": " + explanationEntry.Text;

            try
            {
                var a = await firebaseClient.Child("Es").PostAsync(new EsItem
                {
                    Info = fullText,
                    DangerScale = dangerScale,
                });
            }
            catch (Exception)
            {
                await DisplayAlert("Грешка!", "Нещо се обърка. Уверете се, че имате установена интернет връзка", "Добре!");
            }

            eType.Text = string.Empty;
            explanationEntry.Text = string.Empty;
            dangerScale_1.IsChecked = false; dangerScale_2.IsChecked = false; dangerScale_3.IsChecked = false;
        }        

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraAccessment(popupNavigation));
        }

        private void eType_Completed(object sender, EventArgs e)
        {
            explanationEntry.Focus();
        }       
    }
}