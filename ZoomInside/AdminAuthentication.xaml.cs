namespace ZoomInside;

public partial class AdminAuthentication : ContentPage
{
	public AdminAuthentication()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var username = usernameEntry.Text;
		var password = passwordEntry.Text;

		if (username == "admin123" && password == "ZoomKY123")
		{
			await Navigation.PushAsync(new MainPage());
		}
		else if (username == null || password == null)
		{
            await DisplayAlert("Error!", "Please fill in all required data", "OK");
        }
		else
		{
            await DisplayAlert("Error!", "The username or the password was incorrect.", "OK");
        }
    }

    private void usernameEntry_Completed(object sender, EventArgs e)
    {
		passwordEntry.Focus();
    }

    private void passwordEntry_Completed(object sender, EventArgs e)
    {
        loginButton.Focus();
    }
}