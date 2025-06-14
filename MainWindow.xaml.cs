using System.Windows;
using System.Windows.Controls;
using TelephoneCRUD.Models;
using TelephoneCRUD.Services;

namespace TelephoneCRUD
{
    public partial class MainWindow : Window
    {
        private readonly TelephoneService _telephoneService;
        private bool _modeModification = false;
        private string _imeiOriginal = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            _telephoneService = new TelephoneService();
            ChargerTelephones();
        }

        private async void ChargerTelephones()
        {
            try
            {
                txtStatus.Text = "Chargement des téléphones...";
                var telephones = await _telephoneService.ObtenirTousLesTelephoneAsync();
                dgTelephones.ItemsSource = telephones;
                txtStatus.Text = $"Nombre de téléphones: {telephones.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtStatus.Text = "Erreur de chargement";
            }
        }

        private async void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!ValiderChamps()) return;

            try
            {
                var telephone = new Telephone
                {
                    Imei = txtImei.Text.Trim(),
                    Marque = txtMarque.Text.Trim(),
                    Modele = txtModele.Text.Trim(),
                    Prix = decimal.Parse(txtPrix.Text.Trim())
                };

                bool succes = await _telephoneService.AjouterTelephoneAsync(telephone);
                
                if (succes)
                {
                    MessageBox.Show("Téléphone ajouté avec succès!", "Succès", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ViderChamps();
                    ChargerTelephones();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout. L'IMEI existe peut-être déjà.", "Erreur", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (!_modeModification)
            {
                MessageBox.Show("Veuillez sélectionner un téléphone dans la liste pour le modifier.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!ValiderChamps()) return;

            try
            {
                var telephone = new Telephone
                {
                    Imei = _imeiOriginal, // Utilise l'IMEI original
                    Marque = txtMarque.Text.Trim(),
                    Modele = txtModele.Text.Trim(),
                    Prix = decimal.Parse(txtPrix.Text.Trim())
                };

                bool succes = await _telephoneService.ModifierTelephoneAsync(telephone);
                
                if (succes)
                {
                    MessageBox.Show("Téléphone modifié avec succès!", "Succès", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ViderChamps();
                    ChargerTelephones();
                    _modeModification = false;
                    txtImei.IsReadOnly = false;
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification.", "Erreur", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImei.Text))
            {
                MessageBox.Show("Veuillez saisir ou sélectionner un IMEI.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le téléphone avec l'IMEI: {txtImei.Text}?", 
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool succes = await _telephoneService.SupprimerTelephoneAsync(txtImei.Text.Trim());
                    
                    if (succes)
                    {
                        MessageBox.Show("Téléphone supprimé avec succès!", "Succès", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        ViderChamps();
                        ChargerTelephones();
                        _modeModification = false;
                        txtImei.IsReadOnly = false;
                    }
                    else
                    {
                        MessageBox.Show("Téléphone non trouvé ou erreur lors de la suppression.", "Erreur", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnVider_Click(object sender, RoutedEventArgs e)
        {
            ViderChamps();
            _modeModification = false;
            txtImei.IsReadOnly = false;
        }

        private void BtnActualiser_Click(object sender, RoutedEventArgs e)
        {
            ChargerTelephones();
        }

        private void DgTelephones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTelephones.SelectedItem is Telephone telephone)
            {
                txtImei.Text = telephone.Imei;
                txtMarque.Text = telephone.Marque;
                txtModele.Text = telephone.Modele;
                txtPrix.Text = telephone.Prix.ToString("F2");
                
                _modeModification = true;
                _imeiOriginal = telephone.Imei;
                txtImei.IsReadOnly = true; // Empêche la modification de l'IMEI
            }
        }

        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtImei.Text))
            {
                MessageBox.Show("L'IMEI est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtImei.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMarque.Text))
            {
                MessageBox.Show("La marque est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMarque.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModele.Text))
            {
                MessageBox.Show("Le modèle est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtModele.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrix.Text, out decimal prix) || prix <= 0)
            {
                MessageBox.Show("Le prix doit être un nombre positif.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPrix.Focus();
                return false;
            }

            return true;
        }

        private void ViderChamps()
        {
            txtImei.Text = string.Empty;
            txtMarque.Text = string.Empty;
            txtModele.Text = string.Empty;
            txtPrix.Text = string.Empty;
            dgTelephones.SelectedItem = null;
        }

        protected override void OnClosed(EventArgs e)
        {
            _telephoneService?.Dispose();
            base.OnClosed(e);
        }
    }
}