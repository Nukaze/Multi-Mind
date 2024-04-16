using Multi_Mind.Models;
using static Multi_Mind.Services.Utilize;
using Microsoft.Maui.Graphics;
using Multi_Mind.Services;


namespace Multi_Mind.Views;

public partial class CharactersMenu : ContentPage
{

    private Characters characters = new Characters();
    private DisplayInfo displayInfo = new DisplayInfo();

    internal class DisplayInfo
    {
        public double widthPx { get; set; }
        public double heightPx { get; set; }
        public double density { get; set; }
        public double widthDp => widthPx / density;
        public double heightDp => heightPx / density;
        public string? orientation { get; set; }
    };


    public CharactersMenu()
    {
        InitializeComponent();

        characters = new Characters();
        characters.GetDefaultCharacters();
        GenerateAndBindingCharactersButtons();

        GetScreenDimensions();


    }

    private void GenerateAndBindingCharactersButtons()
    {
        foreach (string character in characters.charactersList)
        {
            Button button = new Button
            {
                Text = character,
                HeightRequest = 120,
                CornerRadius = 10,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
            };

            buttonVertStackLayout.Children.Add(button);
        }
    }

    private async void GetScreenDimensions()
    {
        var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

        displayInfo.widthPx = mainDisplayInfo.Width;
        displayInfo.heightPx = mainDisplayInfo.Height;
        displayInfo.density = mainDisplayInfo.Density;
        displayInfo.orientation = mainDisplayInfo.Orientation.ToString();

        await AlertDialog(
            $"Agent {Global.Agent.Model}",
            $"Density: {displayInfo.density}\nWidthDP: {displayInfo.widthDp}\nHeightDP: {displayInfo.heightDp}\nOrientation: {displayInfo.orientation}",
            "OK"
            );

    }


}