using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kerstdiner.Models;
using Kerstdiner.DAL;

namespace Kerstdiner.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileOperations _fileOperations = new FileOperations();
        private readonly List<Gerecht> _hoofdgerechten = new List<Gerecht>();
        private readonly List<Gerecht> _nagerechten = new List<Gerecht>();
        private readonly List<DinerReservatie> _reservaties = new List<DinerReservatie>();
        private const int _tafels = 5;

        public MainWindow()
        {
            InitializeComponent();
            InitProgram();
        }

        // private methods
        private void InitProgram()
        {
            // check diner radiobutton on start
            rdbKerstdiner.IsChecked = false;
            rdbDiner.IsChecked = true;

            // load in gerechtdata from csv file
            var alleGerechten = _fileOperations.BestandInlezen();

            if (alleGerechten != null)
            {
                foreach (Gerecht gerecht in alleGerechten)
                {
                    switch (gerecht.Type.ToLower().Trim())
                    {
                        case "hoofdgerecht":
                            _hoofdgerechten.Add(gerecht);
                            break;
                        case "nagerecht":
                            _nagerechten.Add(gerecht);
                            break;
                    }
                }
            }

            // link comboboxes to gerechten lists
            cboHoofdgerecht.ItemsSource = _hoofdgerechten;
            cboNagerecht.ItemsSource = _nagerechten;
        }

        private void BoekReservatie()
        {
            //only continue when all input is valid
            if (!ValidateFields())
            {
                return;
            }

            try
            {
                DinerReservatie reservatie;

                if (rdbDiner.IsChecked.HasValue && rdbDiner.IsChecked.Value)
                {
                    reservatie = new DinerReservatie(
                        txtNaam.Text.Trim(),
                        int.Parse(txtAantal.Text.Trim()),
                        _hoofdgerechten[cboHoofdgerecht.SelectedIndex].Benaming,
                        _hoofdgerechten[cboHoofdgerecht.SelectedIndex].Prijs);
                }
                else
                {
                    reservatie = new KerstdinerReservatie(
                        txtNaam.Text.Trim(),
                        int.Parse(txtAantal.Text.Trim()),
                        _hoofdgerechten[cboHoofdgerecht.SelectedIndex].Benaming,
                        _hoofdgerechten[cboHoofdgerecht.SelectedIndex].Prijs,
                        _nagerechten[cboNagerecht.SelectedIndex].Benaming,
                        _nagerechten[cboNagerecht.SelectedIndex].Prijs);
                }

                if (ReservationExists(reservatie))
                {
                    MessageBox.Show("U heeft reeds geboekt! Wijzigingen zijn niet toegelaten!");
                }
                else
                {
                    _reservaties.Add(reservatie);
                }
            }
            catch (CustomException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                _fileOperations.LogException(ex);
            }

            // clear fields once reservation is added
            // not asked but improves useability
            txtAantal.Text = "";
            txtNaam.Text = "";
            cboHoofdgerecht.SelectedIndex = -1;
            cboNagerecht.SelectedIndex = -1;

            // disable reservaties when count reaches max tables
            if (_reservaties.Count >= _tafels)
            {
                btnBoeken.IsEnabled = false;
                MessageBox.Show("Er worden geen reservaties meer aanvaard. Restaurant zit vol!");
            }
        }

        private bool ValidateFields()
        {
            var bob = new StringBuilder();

            // cboboxes need to have an item selected
            if (cboHoofdgerecht.IsEnabled && cboHoofdgerecht.SelectedIndex == -1)
            {
                bob.AppendLine("Selecteer een hoofdgerecht!");
            }

            if (cboNagerecht.IsEnabled && cboNagerecht.SelectedIndex == -1)
            {
                bob.AppendLine("Selecteer een nagerecht!");
            }

            // aantal has to be numeric
            if (!int.TryParse(txtAantal.Text.Trim(), out int _))
            {
                bob.AppendLine("Aantal personen moet een numerieke waarde zijn!");
            }

            if (!string.IsNullOrWhiteSpace(bob.ToString()))
            {
                MessageBox.Show(bob.ToString(), "Fouten", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool ReservationExists(DinerReservatie res)
        {
            if (_reservaties.Count > 0)
            {
                foreach (DinerReservatie reservatie in _reservaties)
                {
                    if (res.Equals(reservatie))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // event handlers
        private void DinerType_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbDiner.IsChecked.HasValue && rdbDiner.IsChecked.Value)
            {
                lblNagerecht.IsEnabled = false;
                cboNagerecht.IsEnabled = false;

                lblNagerecht.Visibility = Visibility.Hidden;
                cboNagerecht.Visibility = Visibility.Hidden;
            }
            else
            {
                lblNagerecht.IsEnabled = true;
                cboNagerecht.IsEnabled = true;

                lblNagerecht.Visibility = Visibility.Visible;
                cboNagerecht.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Name)
            {
                case "btnBoeken":
                    BoekReservatie();
                    break;
                case "btnSluiten":
                    Application.Current.Shutdown();
                    break;
                case "btnToonReservaties":
                    var bob = new StringBuilder();
                    foreach (DinerReservatie reservatie in _reservaties)
                    {
                        bob.AppendLine(reservatie.ToString());
                    }
                    lblReservaties.Content = bob.ToString();
                    break;
            }
        }
    }
}
