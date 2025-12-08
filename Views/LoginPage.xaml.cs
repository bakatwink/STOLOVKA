namespace MauiApp1;

public partial class LoginPage : ContentPage
{
	private bool isEmployee;
	private readonly Dictionary<string, string> Users = new Dictionary<string, string>()
	{
		{"Student", "Student1"},
		{"Employee", "Employee"}
	};
	public LoginPage()
	{
		InitializeComponent();

	}

	public bool CheckCurrency()
	{

		if (Users.ContainsKey(LoginEntry.Text.TrimEnd()))
		{
			if (Users[LoginEntry.Text.TrimEnd()] == PasswordEntry.Text)
			{
				if (Users[LoginEntry.Text.TrimEnd()] == "Employee")
				{
					isEmployee = true;
				}
				else
				{
					isEmployee = false;
				}
			}
			else return false;
		}
		return false;
	}
	
    private void Button_Clicked(object sender, EventArgs e)
    {
		var message = new Label();
		if(LoginEntry is null || PasswordEntry is null)
		{
			message.TextColor = 
			LoginLayout.Children.Add(message);
		}
		if (CheckCurrency() && isEmployee)
		{
			Shell.Current.GoToAsync("//EmployeePage");
		}
		else if(CheckCurrency() && !isEmployee)
		{
            Shell.Current.GoToAsync("//StudentPage");
        }
    }
}