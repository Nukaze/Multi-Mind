using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Mind.Models
{

    internal class Characters
    {
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
    }
}
