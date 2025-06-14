using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using TelephoneCRUD.Models;
using TelephoneCRUD.Services;

namespace TelephoneCRUD
{
    public partial class ListeTelephoneWindow : Window
    {
        private readonly TelephoneService _telephoneService;
        private ObservableCollection<Telephone> _telephones;
        private ICollectionView _telephonesView;

        public ListeTelephoneWindow()
        {
            InitializeComponent();
            _telephoneService = new TelephoneService();
            _telephones = new ObservableCollection<Telephone>();
            ChargerTelephones();
        }

        private async void ChargerTelephones()
        {
            try
            {
                var telephones = await _telephoneService.ObtenirTousLesTelephoneAsync();
                _telephones.Clear();
                
                foreach (var telephone in telephones)
                {
                    _telephones.Add(telephone);
                }

                _telephonesView = CollectionViewSource.GetDefaultView(_telephones);
                _telephonesView.Filter = FiltrerTelephones;
                dgTelephonesList.ItemsSource = _telephonesView;
                
                MettreAJourStatistiques();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool FiltrerTelephones(object item)
        {
            if (item is Telephone telephone && !string.IsNullOrWhiteSpace(txtRecherche.Text))
            {
                string recherche = txtRecherche.Text.ToLower();
                return telephone.Marque.ToLower().Contains(recherche) ||
                       telephone.Modele.ToLower().Contains(recherche) ||
                       telephone.Imei.ToLower().Contains(recherche);
            }
            return true;
        }

        private void MettreAJourStatistiques()
        {
            if (_telephones.Count > 0)
            {
                int nombreTotal = _telephones.Count;
                decimal prixMoyen = _telephones.Average(t => t.Prix);
                decimal prixTotal = _telephones.Sum(t => t.Prix);

                txtNombreTotal.Text = $"Total: {nombreTotal} téléphone{(nombreTotal > 1 ? "s" : "")}";
                txtPrixMoyen.Text = $"Prix moyen: {prixMoyen:F2} €";
                txtPrixTotal.Text = $"Valeur totale: {prixTotal:F2} €";
            }
            else
            {
                txtNombreTotal.Text = "Total: 0 téléphones";
                txtPrixMoyen.Text = "Prix moyen: 0,00 €";
                txtPrixTotal.Text = "Valeur totale: 0,00 €";
            }
        }

        private void BtnActualiser_Click(object sender, RoutedEventArgs e)
        {
            ChargerTelephones();
        }

        private void TxtRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            _telephonesView?.Refresh();
        }

        private void BtnExporter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Fichier CSV (*.csv)|*.csv|Fichier texte (*.txt)|*.txt",
                    DefaultExt = "csv",
                    FileName = $"telephones_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    ExporterVersFichier(saveDialog.FileName);
                    MessageBox.Show($"Export réussi vers:\n{saveDialog.FileName}", "Export terminé", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'export: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExporterVersFichier(string cheminFichier)
        {
            StringBuilder sb = new StringBuilder();
            
            // En-têtes
            sb.AppendLine("IMEI;Marque;Modèle;Prix;Date d'ajout");
            
            // Données
            foreach (var telephone in _telephones)
            {
                sb.AppendLine($"{telephone.Imei};{telephone.Marque};{telephone.Modele};{telephone.Prix:F2};{telephone.DateAjout:dd/MM/yyyy HH:mm}");
            }
            
            File.WriteAllText(cheminFichier, sb.ToString(), Encoding.UTF8);
        }

        protected override void OnClosed(EventArgs e)
        {
            _telephoneService?.Dispose();
            base.OnClosed(e);
        }
    }
}