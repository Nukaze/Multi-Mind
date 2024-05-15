using Multi_Mind.Models;
using static Multi_Mind.Services.Utilize;
using Multi_Mind.Services;


namespace Multi_Mind.Views;

public partial class CharactersMenu : ContentPage
{

    private Characters characters = new Characters();
    private short _characterId = -1;

    public CharactersMenu()
    {
        InitializeComponent();
        CheckScreenDimensions();

        characters = new Characters();
        characters.GetDefaultCharacters();
        GenerateAndBindingCharactersButtons();

    }

    //This method is called when the page is first displayed and have override to update the characters buttons
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        string agentModel = Global.Agent.GetModelLabel(Global.Agent.Id);
        TitleLabel.Text = $"Characters {agentModel}";
        TitleLabel.BackgroundColor = Global.Agent.Color;
        GenerateAndBindingCharactersButtons();
        
        if (Global.Agent.Id != 1)
        {
            await AlertDialogCustom("Notify", "Please select an AI agent in AI Hub before selecting characters currently allowed for [ ChatGPT ]");
            await Shell.Current.GoToAsync("//HubAI");
        }

    }


    private void CheckScreenDimensions()
    {
        var displayInfo = Global.DeviceDisplayInfo;
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
        if (Global.Agent.Id == 0)
        {
            await AlertDialogCustom("Notify", "Please select ai agent in AI Hub before select characters");
            return;
        }

        string character = characters.charactersList[characterId];
        _characterId = characterId;
        bool isAccept = await AlertDialogCustom(
                        "Notify",
                        $"\"{character}\" Selected",
                        "Select and Start Chat",
                        "Cancel"
                        );

        if (isAccept)
        {
            await SetCharacterId(characterId);
        }

        await AlertDialogCustom("Notify", $"Selected {characterId}\n\"global {Global.characterId}\"");
        if (Global.characterId < 0)
        {
            return;
        }
        if (Global.Agent.Id == 1)
        {
            await Shell.Current.GoToAsync("//Chat");
            return;
        } 
    }

    private async Task SetCharacterId(short _characterId)
    {
        if (_characterId < 0)
        {
            await AlertDialogCustom("Notify", "Please select a character first!");
            return;
        }
        Global.characterId = _characterId;
        await AlertDialogCustom("Notify", $"character id has been set!");
    }



}