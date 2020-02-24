using ScryfallApi.Client.Models;
using System.Collections.Generic;

namespace deckdiff.Model
{
    public class Deck
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Format Format { get; set; }
        public string Notes { get; set; }
        public List<Card> Main { get; set; } = new List<Card>();
        public List<Card> Sideboard { get; set; } = new List<Card>();
    }
}