using Multi_Mind.Models;
using Multi_Mind.Services;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class UserProfile : ContentPage
{
    private User user = Global.CURRENT_USER;
    private DatabaseService _databaseService = new DatabaseService();


    public UserProfile()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await AlertDialogCustom("Welcome", "Welcome to your profile page\n" + user);
    }


    private void accountDeleteBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void accountEditBtn_Clicked(object sender, EventArgs e)
    {

    }
}