using Mopups.Services;
using System.Text;

namespace ZoomInside;

public partial class MyMopup
{
	public MyMopup(List<string> res)
	{
		InitializeComponent();

		if (res.Count > 0)
		{
            foreach (var item in res)
            {
                resultLabel.Text += item + "\n" + "\n";
            }
        }
		else
		{
			resultLabel.Text = "Нещо се обърка!\nМоля, заснемете продукта си отново!";
		}
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
}