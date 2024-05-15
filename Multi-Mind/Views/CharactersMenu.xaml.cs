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
        TitleLabel.Text = $"Characters {agentModel}";
        TitleLabel.BackgroundColor = Global.Agent.Color;
        GenerateAndBindingCharactersButtons();

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
        await AlertDialogCustom(
                        "Notify",
                        $"\"{character}\" Selected",
                        "Select and Start Chat",
                        onAccept: () => Global.characterId = characterId
                        );
        await AlertDialogCustom("Notify", $"\"global {Global.characterId}\"\nSelected {characterId}");
        if (Global.characterId < 0)
        {
            return;
        }
        if (Global.Agent.Id == 1)
        {
            await Shell.Current.GoToAsync("//Chat");
            return;
        } else
        {
            await AlertDialogCustom("Notify", "Please select an AI agent in AI Hub before selecting characters currently allowed for ChatGPT");
            await Shell.Current.GoToAsync("//HubAI");
            return;
        }

    }



}