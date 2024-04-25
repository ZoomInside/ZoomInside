using Mopups.Services;
using System.Text;
//using Windows.Graphics.Printing3D;

namespace ZoomInside;

public partial class MyMopup
{
	public MyMopup(List<List<string>> res, List<string> resultTxt)
	{
		InitializeComponent();

        //TODO: if -> while
		if (res.Count > 0)
		{
            foreach (var sublist in res)
            {
				if (sublist[1] == "3")
				{
                    thirdLabel.Text += sublist[0] + "\n\n";
                }
                else if (sublist[1] == "2")
                {
                    secondLabel.Text += sublist[0] + "\n\n";
                }
                else
                {
                    firstLabel.Text += sublist[0] + "\n\n";
                }
            }
        }
		else
		{
            thirdLabel.TextColor = Color.FromHex("#1A2123");
			thirdLabel.Text = "В продукта няма нищо притеснително.";
		}



        var allergensData = new Dictionary<string, List<string>>() {
            {"Може да навреди на хора с непоносимост към глутен", new List<string>(){"пшеница", "ръж", "ечемик", "овес", "спелта", "камут" }},
            {"Може да навреди на хора с непоносимост към ракообразни", new List<string>() { "раци", "омари", "охлюви", "миди", "стриди" } },
            {"Може да навреди на хора с непоносимост към яйца", new List<string>() { "яйце", "жълтък", "белтък", "овомукоид", "овалбумин", "овотрансферин", "лизозим" } },
            {"Може да навреди на хора с непоносимост към хистамин (риба и рибни продукти)", new List<string>() { "хистамин", "риба тон", "скумрия", "сьомга", "пъстърва", "шаран", "сом", "лосос", "ставрида", "хайвер" } },
            {"Може да навреди на хора с непоносимост към фъстъци", new List<string>() { "фъстък", "фъстъци", "фъстъчен", "фъстъчена", "фъстъчено", "фъстъчени" } },
            {"Може да навреди на хора с непоносимост към соя", new List<string>() { "соя", "соев сос" } },
            {"Може да навреди на хора с непоносимост към лактоза (мляко и млечни продукти)", new List<string>() { "мляко", "масло", "сирене", "кашкавал", "сметана", "извара", "пудинг" } },
            {"Може да навреди на хора с непоносимост към ядки", new List<string>() { "бадеми", "лешник", "орехи", "кашу", "пекан", "орех", "шамфъстък", "пистачо", "макадамия" } }
        };

        var resultList = new List<string>();
        foreach (var line in allergensData)
        {
            foreach (var item in line.Value)
            {
                foreach (var resItem in resultTxt)
                {
                    if (item == resItem)
                    {
                        //allergensLabel.Text += line.Key + "\n\n";
                        resultList.Add(line.Key);
                    }
                }
            }
        }

        foreach (var item in resultList.Distinct())
        {
            allergensLabel.Text += item + "\n\n";
        }

	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }
}