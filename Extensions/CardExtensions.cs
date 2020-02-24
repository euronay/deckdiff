using System.Linq;
using System.Runtime.CompilerServices;
using ScryfallApi.Client.Models;

namespace deckdiff.Extensions
{
    public static class CardExtensions
    {
        public static (string SuperType, string[] Types, string[] SubTypes) GetCardTypes(this Card card)
        {
            var superTypes = new string[] {"Basic", "Legendary", "Snow", "World"};   

            var typeLine = card.TypeLine;
            var superType = superTypes.FirstOrDefault(s => typeLine.StartsWith(s)) ?? string.Empty;
            
            if(!string.IsNullOrEmpty(superType))
            {
                typeLine = typeLine.TrimStart(superType.ToCharArray());
            }
            
            var subTypes = new string[0];
            if(typeLine.Contains("-"))
            {
                subTypes = typeLine.Split('-')[1].Trim().Split(' ');
            }

            var types = typeLine.Split('-')[1].Trim().Split(' ');
            
            return(superType, types, subTypes);
            
        }
    }
}
