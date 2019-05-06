using System;
using System.Collections.Generic;
using ScryfallApi.Client.Models;

namespace deckdiff.Model
{
    public class Deck
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public Format Format { get; set; }
        public string Notes { get; set; }
        public IList<(int Count, Card Card)> Main { get; set; } = new List<(int, Card)>();
        public IList<(int Count, Card Card)> Sideboard { get; set; } = new List<(int, Card)>();
    }
}