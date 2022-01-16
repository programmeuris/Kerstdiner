using System;
using System.Collections.Generic;
using System.Text;

namespace Kerstdiner.Models
{
    public class KerstdinerReservatie : DinerReservatie
    {
        // constructor
        public KerstdinerReservatie(
            string naam,
            int aantalPersonen,
            string hoofdgerecht,
            double prijsHoofdgerecht,
            string nagerecht,
            double prijsNagerecht) : base(
                naam,
                aantalPersonen,
                hoofdgerecht,
                prijsHoofdgerecht)
        {
            Nagerecht = nagerecht;
            PrijsNagerecht = prijsNagerecht;
        }

        // public methods
        // using n2 instead of c2 and adding € manually because my locale is set to en-US
        public override double Totaal() => base.Totaal() + AantalPersonen * PrijsNagerecht;

        // public properties
        public string Nagerecht
        {
            get => _nagerecht;
            set => _nagerecht = value;
        }

        public double PrijsNagerecht
        {
            get => _prijsNagerecht;
            set => _prijsNagerecht = value;
        }

        // private fields
        private string _nagerecht;
        private double _prijsNagerecht;
    }
}
