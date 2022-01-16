using System;
using System.Collections.Generic;
using System.IO;
using Kerstdiner.Models;
using System.Globalization;
using System.Text;

namespace Kerstdiner.DAL
{
    public class FileOperations
    {
        private const char _valueSeparator = ';';

        // if file exists, return all lines as array
        // if not, return null
        private string[] ReadToArr(string fName) => File.Exists(fName) ?
            File.ReadAllLines(fName) : null;

        public List<Gerecht> BestandInlezen(string bestand = "Gerechten.txt")
        {
            var lines = ReadToArr(bestand);

            if (lines == null)
            {
                throw new FileNotFoundException($"Bestand {bestand} bestaat niet!");
            }

            var gerechten = new List<Gerecht>();

            foreach (var line in lines)
            {
                // skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var split = line.Split(_valueSeparator);

                // prices in file have ',' as decimal separator, so using nl-BE as cultureinfo
                double.TryParse(split[1].Trim(), NumberStyles.Currency, new CultureInfo("nl-BE"), out double prijs);

                var gerecht = new Gerecht(split[2].Trim(), split[0].Trim(), prijs);

                gerechten.Add(gerecht);
            }

            return gerechten.Count > 0 ?
                gerechten : null;
        }

        public void LogException(CustomException exception)
        {
            string msg =
                $"{DateTime.Now:HH:mm:ss}\n" +
                $"{exception.GetType().Name}\n" +
                $"{exception.Message}\n" +
                $"{exception.StackTrace}\n" +
                new string('-', 69) + "\n";

            File.AppendAllText("Kerstdiner.log.txt", msg);
        }
    }
}
