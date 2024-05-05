using Multi_Mind.Models;
using static Multi_Mind.Services.Utilize;
using Multi_Mind.Services;


namespace Multi_Mind.Views;

public partial class CharactersMenu : ContentPage
{

    private Characters characters = new Characters();

    public CharactersMenu()
    {
        InitializeComponent();
        CheckScreenDimensions();

        characters = new Characters();
        characters.GetDefaultCharacters();
        GenerateAndBindingCharactersButtons();



    }

    //This method is called when the page is first displayed and have override to update the characters buttons
    protected override void OnAppearing()
    {
        base.OnAppearing();
        string agentModel = Global.Agent.GetModelLabel(Global.Agent.Id);
        TitleLabel.Text = $"Agent {agentModel}";
        GenerateAndBindingCharactersButtons();

    }


    private async void CheckScreenDimensions()
    {
        var displayInfo = Global.DeviceDisplayInfo;
        await AlertDialogCustom(
            $"Agent {Global.Agent.Model}",
            $"KEY {Global.Agent.ApiKey}\nDensity: {displayInfo.Density}\nWidthDP: {displayInfo.WidthDp}\nHeightDP: {displayInfo.HeightDp}\nOrientation: {displayInfo.Orientation}",
            "OK"
            );

    }

    private void GenerateAndBindingCharactersButtons()
    {
        buttonVertStackLayout.Children.Clear();
        for (short i = 0; i < characters.charactersList.Count; i++)
        {
            short characterId = i;
            string character = characters.charactersList[i];
            Button button = new Button
            {
                Text = character,
                HeightRequest = 120,
                CornerRadius = 300,
                BackgroundColor = Global.Agent.Color,
                TextColor = Colors.White,
                FontSize = 32,
                FontAttributes = FontAttributes.Bold,
            };

            button.Clicked += async (sender, e) => await HandleCharacterButtonSelection(characterId);

            buttonVertStackLayout.Children.Add(button);
        }
    }

    private async Task HandleCharacterButtonSelection(short characterId)
    {
        string character = characters.charactersList[characterId];
        await AlertDialogCustom(
                       $"\"{character}\" Selected",
                        characters.dialogsList[characterId],
                        "OK"
                        );


        await Shell.Current.GoToAsync("//HubAI");
        //await Shell.Current.GoToAsync($"//Chat?character={character}");
    }

}