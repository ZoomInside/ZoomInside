using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System;
using System.Drawing;


namespace ZoomInside
{    
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<EsItem> EsItems { get; set; } = new ObservableCollection<EsItem>();

        FirebaseClient firebaseClient = 
            new FirebaseClient("https://zoominside-2ccf3-default-rtdb.europe-west1.firebasedatabase.app/");
                
        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            var collection = firebaseClient
            .Child("Es")
            .AsObservable<EsItem>()
            .Subscribe((item) =>
            {
                if (item.Object != null)
                {
                    EsItems.Add(item.Object);
                }
            });

            string[] arr = { "1", "2", "3", "4", "5", "6" };


        }
        
        private void GetByKey(object sender, EventArgs e)
        {
            var fullText = eType.Text + ": " + explanationEntry.Text;
            var a = firebaseClient.Child("Es").PostAsync(new EsItem
            {
                Info = fullText,
            });

            eType.Text = string.Empty;
            explanationEntry.Text = string.Empty;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraAccessment());
        }
    }
}