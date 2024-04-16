using Multi_Mind.Models;
using static Multi_Mind.Services.Utilize;
using Microsoft.Maui.Graphics;
using Multi_Mind.Services;


namespace Multi_Mind.Views;

public partial class CharactersMenu : ContentPage
{

    private Characters characters = new Characters();

    public CharactersMenu()
    {
        InitializeComponent();

        characters = new Characters();
        characters.GetDefaultCharacters();
        GenerateAndBindingCharactersButtons();

        CheckScreenDimensions();


    }

    private void GenerateAndBindingCharactersButtons()
    {
        foreach (string character in characters.charactersList)
        {
            Button button = new Button
            {
                Text = character,
                HeightRequest = 120,
                CornerRadius = 100,
                BackgroundColor = Global.Agent.Color,
                TextColor = Colors.White,
            };

            buttonVertStackLayout.Children.Add(button);
        }
    }

    private async void CheckScreenDimensions()
    {
        var displayInfo = Global.DeviceDisplayInfo;
        await AlertDialog(
            $"Agent {Global.Agent.Model}",
            $"Density: {displayInfo.Density}\nWidthDP: {displayInfo.WidthDp}\nHeightDP: {displayInfo.HeightDp}\nOrientation: {displayInfo.Orientation}",
            "OK"
            );

    }


}