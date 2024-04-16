using Multi_Mind.Models;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Views;

public partial class CharactersMenu : ContentPage
{

    private Characters characters = new Characters();
    public CharactersMenu()
    {
        InitializeComponent();

        characters = new Characters();
        characters.GetDefaultCharacters();


        BindCharactersButtons();
        GetScreenDimensions();
    }

    private void BindCharactersButtons()
    {
        foreach (string character in characters.charactersList)
        {
            Button button = new Button
            {
                Text = character,
                HeightRequest = 60,
            };

            buttonVertStackLayout.Children.Add(button);
        }
    }

    private async void GetScreenDimensions()
    {
        var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        var width = mainDisplayInfo.Width;
        var height = mainDisplayInfo.Height;
        var orientation = mainDisplayInfo.Orientation;
        await AlertDialog("Screen Dimensions", $"Width: {width}, Height: {height}, Orientation: {orientation}", "OK");
    }
}