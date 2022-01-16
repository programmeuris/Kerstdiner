using System;
using System.Collections.Generic;
using System.Text;

namespace Kerstdiner.Models
{
    public class DinerReservatie
    {
        // constructor
        public DinerReservatie(
            string naam,
            int aantalPersonen,
            string hoofdgerecht,
            double prijsHoofdgerecht)
        {
            Naam = naam;
            AantalPersonen = aantalPersonen;
            Hoofdgerecht = hoofdgerecht;
            PrijsHoofdgerecht = prijsHoofdgerecht;
        }

        // public methods
        // using n2 instead of c2 and adding € manually because my locale is set to en-US
        public override bool Equals(object obj) => (obj as DinerReservatie).Naam == Naam;

        public override string ToString() => $"{this.GetType().Name[0..^10]}\t{Naam}\t{Totaal():n2} €";

        public virtual double Totaal() => AantalPersonen * PrijsHoofdgerecht;

        // public properties
        public int AantalPersonen
        {
            get => _aantalPersonen;
            set => _aantalPersonen = value > 0 ?
                 value : throw new ValueZeroOrNegativeException(nameof(AantalPersonen));
        }

        public string Hoofdgerecht
        {
            get => _hoofdgerecht;
            set => _hoofdgerecht = value;
        }

        public string Naam
        {
            get => _naam;
            set => _naam = !string.IsNullOrWhiteSpace(value) ?
                value : throw new ValueRequiredException(nameof(Naam));
        }

        public double PrijsHoofdgerecht
        {
            get => _prijsHoofdgerecht;
            set => _prijsHoofdgerecht = value;
        }

        // private fields
        private int _aantalPersonen;
        private string _hoofdgerecht;
        private string _naam;
        private double _prijsHoofdgerecht;
    }
}
