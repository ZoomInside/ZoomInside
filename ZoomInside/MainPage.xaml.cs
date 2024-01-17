using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

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
    }
}