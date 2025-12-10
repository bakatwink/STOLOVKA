

using MauiApp1.Properties;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
    private Label message = new Label();
    private bool isEmployee;
	public readonly Dictionary<string, string> users = new Dictionary<string, string>()
	{
		{"Student1", "student"},
		{"Student2", "student"},
		{"Employee1", "employee"}
	};
	public LoginPage()
	{
		InitializeComponent();
        message.TextColor = Color.FromArgb("#FF0000");
        message.Text = "Неправильный логин или пароль";
        message.HorizontalTextAlignment = TextAlignment.Center;
        message.VerticalTextAlignment = TextAlignment.Center;
    }

	public bool CheckCurrency()
	{

		if (users.ContainsKey(LoginEntry.Text.TrimEnd()))
		{
			if (users[LoginEntry.Text.TrimEnd()] == PasswordEntry.Text)
			{
				if (LoginEntry.Text.Contains("Employee"))
				{
					isEmployee = true;
					
                    return true;
				}
				else
				{
					isEmployee = false;
					return true;
				}
			}
		}
        IncorectLabel();
        return false;
	}
	
	private void IncorectLabel()
	{
        LoginLayout.Children.Remove(message);
        LoginLayout.Children.Add(message);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		
		if (LoginEntry.Text is null || PasswordEntry.Text is null)
		{
			IncorectLabel();
		}
		else
		{
			if (CheckCurrency()) {
                Users.loged.Item1 = LoginEntry.Text.TrimEnd();
                Users.loged.Item2 = PasswordEntry.Text;
                if (isEmployee)
				{
					Shell.Current.GoToAsync("//EmployeePage");
				}
				else if (!isEmployee)
				{
					Shell.Current.GoToAsync("//StudentPage");
				} 
			}
		}
    }
}