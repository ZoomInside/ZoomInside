using Mopups.Services;
using System.Text;

namespace ZoomInside;

public partial class MyMopup
{
	public MyMopup(/*List<List<string>> res*/ Dictionary<string, string> res)
	{
		InitializeComponent();

        //TODO: if -> while
		if (res.Count > 0)
		{
            foreach (var /*sublist*/ line in res)
            {
				if (/*sublist[1]*/ line.Key == "3")
				{
                    thirdLabel.Text += /*sublist[0]*/ line.Value + "\n\n";
                }
                else if (/*sublist[1]*/ line.Key == "2")
                {
                    secondLabel.Text += /*sublist[0]*/ line.Value + "\n\n";
                }
                else
                {
                    firstLabel.Text += /*sublist[0]*/ line.Value + "\n\n";
                }
            }
        }
		else
		{
            thirdLabel.TextColor = Color.FromHex("#1A2123");
			thirdLabel.Text = "В продукта няма нищо притеснително.";
		}
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
}