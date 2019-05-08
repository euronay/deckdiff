using System;
using System.Collections.Generic;

namespace deckdiff.Model
{
    public class Deck
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Format Format { get; set; }
        public string Notes { get; set; }
        public IList<CardInfo> Main { get; set; } = new List<CardInfo>();
        public IList<CardInfo> Sideboard { get; set; } = new List<CardInfo>();
    }
}