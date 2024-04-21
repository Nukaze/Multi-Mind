using Multi_Mind.Services;

namespace Multi_Mind.Models
{

    internal class Characters
    {
        public short Id { get; private set; } = 0;
        public List<string> charactersList { get; set; }
        public List<string> defaultCharacters = new List<string> {
                "General",
                "Doctor",
                "Programmer",
                "Engineer",
                "Scientist",
                "Farmer",
                "Merchant",
                "Musician",
                "Gamer",
                "Digital Marketer",
                "Financial Advisor",
                "Teacher",
                "Artist",
                "Soldier"
        };

        public string dialog = "";
        public string[] dialogsList = new string[] {
            "",
            "You're a Professional general Doctor, you can provide and suggest me anything about healthcare information",
            "You're a Professional general Programmer, you can provide and suggest me anything about programming information",
            "You're a Professional general Engineer, you can provide and suggest me anything about engineering information",
            "You're a Professional general Scientist, you can provide and suggest me anything about science information",
            "You're a Professional general Farmer, you can provide and suggest me anything about farming information",
            "You're a Professional general Merchant, you can provide and suggest me anything about merchant information",
            "You're a Professional general Musician, you can provide and suggest me anything about music information",
            "You're a Professional general Gamer, you can provide and suggest me anything about gaming information",
            "You're a Professional general Digital Marketer, you can provide and suggest me anything about digital marketing information",
            "You're a Professional general Financial Advisor, you can provide and suggest me anything about financial information",
            "You're a Professional general Teacher, you can provide and suggest me anything about teaching information",
            "You're a Professional general Artist, you can provide and suggest me anything about art information",
            "You're a Professional general Soldier, you can provide and suggest me anything about military information"
        };


        public Characters()
        {
            charactersList = new List<string>();
        }

        public void AddCharacter(string character)
        {
            charactersList.Add(character);
        }

        public void RemoveCharacter(string character)
        {
            charactersList.Remove(character);
        }

        public void ClearCharacters()
        {
            charactersList.Clear();
        }

        public void GetDefaultCharacters()
        {
            foreach (string value in defaultCharacters)
            {
                charactersList.Add(value);
            }
        }

        public void setCharacterId(short id)
        {
            this.Id = id;
            this.dialog = dialogsList[id];
            Global.characterId = id;
        }
    }
}
