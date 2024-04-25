using Firebase.Auth.Providers;
using FireSharp.Config;

namespace ZoomInside;

public partial class Authentication : ContentPage
{
    private string fullText = "Натиснете един от двата бутона по-долу. С първия можете да направите снимка на съдържанието на продукт. С втория - да напишете директно веществото, което ви интересува.";
    private bool isFulltextDisplayed = false;

    public Authentication()
	{
		InitializeComponent();

        displayLabel.Text = fullText.Substring(0, Math.Min(100, fullText.Length));
	}
        
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (isFulltextDisplayed)
        {
            displayLabel.Text = fullText.Substring(0, Math.Min(100, fullText.Length));
            ((Button)sender).Text = "...more";
        }
        else
        {
            displayLabel.Text = fullText;
            ((Button)sender).Text = "less";
        }
        isFulltextDisplayed = !isFulltextDisplayed;
    }




    private void loginButton_Clicked(object sender, EventArgs e)
    {
		//FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(""));
    }
}