using Mopups.Interfaces;

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

        if ((username == null && password == null) || (username == null || password == null))
        {
            await DisplayAlert("Грешка!", "Моля, попълнете задължителните полета!", "Добре!");
        }
        else if (username.Trim() == "admin123" && password.Trim() == "ZoomKY123" || username.Trim() == "admin" && password.Trim() == "ZoomInside2024")
        {
            await Navigation.PushAsync(new MainPage());
        }
        else
		{
            await DisplayAlert("Грешка!", "Неправилно въведени данни!", "Добре!");
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

    IPopupNavigation popupNavigation;
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CameraAccessment(popupNavigation));
    }
}