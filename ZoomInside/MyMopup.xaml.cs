namespace ZoomInside;

public partial class MyMopup
{
	public MyMopup(List<string> res)
	{
		InitializeComponent();

		foreach (var item in res)
		{
			resultLabel.Text += item + "\n" + "\n";
		}
	}
}