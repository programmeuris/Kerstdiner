using System;

namespace Kerstdiner.Models
{
    public class Gerecht
    {
        // constructor
        public Gerecht(
            string type, 
            string benaming, 
            double prijs)
        {
            Type = type;
            Benaming = benaming;
            Prijs = prijs;
        }

        // public methods
        // using n2 instead of c2 and adding € manually because my locale is set to en-US
        public override string ToString() => $"{Benaming} - {Prijs:n2} €";

        // public properties
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public double Prijs
        {
            get { return _prijs; }
            set { _prijs = value; }
        }

        public string Benaming
        {
            get { return _benaming; }
            set { _benaming = value; }
        }

        // private fields
        private string _benaming;
        private double _prijs;
        private string _type;
    }
}
