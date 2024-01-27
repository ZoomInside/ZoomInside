using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Util;
using Tesseract;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Maui.Controls;
using System;
using System.Drawing;
using Pix = Emgu.CV.OCR.Pix;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace ZoomInside
{
    public partial class MainPage : ContentPage
    {        
        public MainPage()
        {
            InitializeComponent();
        }

        FirebaseClient firebaseClient = new FirebaseClient("https://zoominside-2ccf3-default-rtdb.europe-west1.firebasedatabase.app/");

        private void OnCounterClicked(object sender, EventArgs e)
        {
            firebaseClient.Child("Todo").PostAsync(new TodoItem
            {
                Title = TitleEntry.Text,
            });
        }

        private async void OnPushAsyncBtn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraAccessment());
        }
    }
}