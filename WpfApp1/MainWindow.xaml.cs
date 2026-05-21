using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TransportCompanyWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Masina> masini =
            new ObservableCollection<Masina>();

        private ObservableCollection<Traseu> trasee =
            new ObservableCollection<Traseu>();

        public MainWindow()
        {
            InitializeComponent();

            dgMasini.ItemsSource = masini;
            dgTrasee.ItemsSource = trasee;

            cmbMasini.ItemsSource = masini;

            DataContext = this;
        }

        // MENIU ADAUGARE
        private void BtnMeniuAdauga_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Visible;
            panelCauta.Visibility = Visibility.Collapsed;
            panelAfiseaza.Visibility = Visibility.Collapsed;
        }

        // MENIU CAUTARE
        private void BtnMeniuCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Visible;
            panelAfiseaza.Visibility = Visibility.Collapsed;
        }

        // MENIU AFISARE
        private void BtnMeniuAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Collapsed;
            panelAfiseaza.Visibility = Visibility.Visible;
        }

        // ADAUGARE MASINA
        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtNumar.Text))
            {
                MessageBox.Show("Completează toate câmpurile!");
                return;
            }

            string combustibil = "";

            if (rbBenzina.IsChecked == true)
                combustibil = "Benzină";
            else if (rbMotorina.IsChecked == true)
                combustibil = "Motorină";

            Masina masinaNoua = new Masina
            {
                Model = txtModel.Text,
                NumarInmatriculare = txtNumar.Text,
                Combustibil = combustibil,
                GPS = cbGPS.IsChecked == true,
                AerConditionat = cbAC.IsChecked == true,
                DataRevizie = dpRevizie.SelectedDate ?? DateTime.Today
            };

            foreach (System.Windows.Controls.ListBoxItem item
                     in lstOptiuni.SelectedItems)
            {
                masinaNoua.Optiuni.Add(item.Content.ToString());
            }

            masini.Add(masinaNoua);
            MessageBox.Show("Mașina a fost adăugată!");
            ResetFields();
        }

        // CAUTARE
        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            string cautare = txtCautare.Text.ToLower();

            var rezultate = masini.Where(m =>
                m.Model.ToLower().Contains(cautare) ||
                m.NumarInmatriculare.ToLower().Contains(cautare))
                .ToList();

            dgMasini.ItemsSource = rezultate;
        }

        // SELECTARE MASINA DIN COMBO
        private void cmbMasini_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbMasini.SelectedItem is Masina m)
            {
                txtModel.Text = m.Model;
                txtNumar.Text = m.NumarInmatriculare;
                dpRevizie.SelectedDate = m.DataRevizie;

                if (m.Combustibil == "Benzină")
                    rbBenzina.IsChecked = true;
                else if (m.Combustibil == "Motorină")
                    rbMotorina.IsChecked = true;

                cbGPS.IsChecked = m.GPS;
                cbAC.IsChecked = m.AerConditionat;

                foreach (System.Windows.Controls.ListBoxItem item
                         in lstOptiuni.Items)
                {
                    item.IsSelected = false;
                }

                foreach (System.Windows.Controls.ListBoxItem item
                         in lstOptiuni.Items)
                {
                    if (m.Optiuni.Contains(item.Content.ToString()))
                        item.IsSelected = true;
                }
            }
        }

        // ACTUALIZARE MASINA
        private void BtnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMasini.SelectedItem is Masina m)
            {
                m.Model = txtModel.Text;
                m.NumarInmatriculare = txtNumar.Text;

                if (rbBenzina.IsChecked == true)
                    m.Combustibil = "Benzină";
                else if (rbMotorina.IsChecked == true)
                    m.Combustibil = "Motorină";

                m.GPS = cbGPS.IsChecked == true;
                m.AerConditionat = cbAC.IsChecked == true;
                m.DataRevizie = dpRevizie.SelectedDate ?? DateTime.Today;

                m.Optiuni.Clear();

                foreach (System.Windows.Controls.ListBoxItem item
                         in lstOptiuni.SelectedItems)
                {
                    m.Optiuni.Add(item.Content.ToString());
                }

                dgMasini.Items.Refresh();
                MessageBox.Show("Mașina a fost actualizată!");
            }
        }

        // ADAUGARE TRASEU
        private void BtnAdaugaTraseu_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlecare.Text) ||
                string.IsNullOrWhiteSpace(txtDestinatie.Text) ||
                string.IsNullOrWhiteSpace(txtDistanta.Text))
            {
                MessageBox.Show("Completează toate câmpurile!");
                return;
            }

            if (!int.TryParse(txtDistanta.Text, out int distanta))
            {
                MessageBox.Show("Distanța trebuie să fie un număr întreg!");
                return;
            }

            Traseu t = new Traseu
            {
                Plecare = txtPlecare.Text,
                Destinatie = txtDestinatie.Text,
                Distanta = distanta
            };

            trasee.Add(t);

            txtPlecare.Clear();
            txtDestinatie.Clear();
            txtDistanta.Clear();

            MessageBox.Show("Traseu adăugat!");
        }

        // STERGERE TRASEU
        private void BtnStergeTraseu_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrasee.SelectedItem is Traseu t)
            {
                trasee.Remove(t);
                MessageBox.Show("Traseu șters!");
            }
        }

        // RESET CAMPURI
        private void ResetFields()
        {
            txtModel.Clear();
            txtNumar.Clear();
            txtCautare.Clear();

            rbBenzina.IsChecked = false;
            rbMotorina.IsChecked = false;

            cbGPS.IsChecked = false;
            cbAC.IsChecked = false;

            dpRevizie.SelectedDate = null;

            foreach (System.Windows.Controls.ListBoxItem item
                     in lstOptiuni.Items)
            {
                item.IsSelected = false;
            }
        }
    }

    // CLASA MASINA
    public class Masina : INotifyPropertyChanged, IDataErrorInfo
    {
        private string model;
        private string numarInmatriculare;
        private bool gps;
        private bool aerConditionat;
        private string combustibil;

        public string Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EsteValid));
            }
        }

        public string NumarInmatriculare
        {
            get => numarInmatriculare;
            set
            {
                numarInmatriculare = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EsteValid));
            }
        }

        public string Combustibil
        {
            get => combustibil;
            set
            {
                combustibil = value;
                OnPropertyChanged();
            }
        }

        public bool GPS
        {
            get => gps;
            set
            {
                gps = value;
                OnPropertyChanged();
            }
        }

        public bool AerConditionat
        {
            get => aerConditionat;
            set
            {
                aerConditionat = value;
                OnPropertyChanged();
            }
        }

        public List<string> Optiuni { get; set; }

        public DateTime DataRevizie { get; set; }

        public Masina()
        {
            Optiuni = new List<string>();
            DataRevizie = DateTime.Today;
        }

        // VALIDARE (IDataErrorInfo)
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Model):
                        if (string.IsNullOrWhiteSpace(Model))
                            return "Modelul este obligatoriu!";
                        if (Model.Length < 2)
                            return "Model prea scurt!";
                        break;

                    case nameof(NumarInmatriculare):
                        if (string.IsNullOrWhiteSpace(NumarInmatriculare))
                            return "Numărul este obligatoriu!";
                        if (NumarInmatriculare.Length < 5)
                            return "Număr invalid!";
                        break;
                }

                return null;
            }
        }

        public string Error => null;

        public bool EsteValid =>
            string.IsNullOrEmpty(this[nameof(Model)]) &&
            string.IsNullOrEmpty(this[nameof(NumarInmatriculare)]);

        // INOTIFYPROPERTYCHANGED
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }

    // CLASA TRASEU
    public class Traseu
    {
        public string Plecare { get; set; }
        public string Destinatie { get; set; }
        public int Distanta { get; set; }
    }
}
