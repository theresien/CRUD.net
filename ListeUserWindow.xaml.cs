using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using UserCRUD.Models;
using UserCRUD.Services;

namespace UserCRUD
{
    public partial class ListeUserWindow : Window
    {
        private readonly UserService _userService;
        private ObservableCollection<User> _users;
        private ICollectionView _usersView;

        public ListeUserWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            _users = new ObservableCollection<User>();
            ChargerUsers();
        }

        private async void ChargerUsers()
        {
            try
            {
                var users = await _userService.ObtenirTousLesUsersAsync();
                _users.Clear();
                
                foreach (var user in users)
                {
                    _users.Add(user);
                }

                _usersView = CollectionViewSource.GetDefaultView(_users);
                _usersView.Filter = FiltrerUsers;
                dgUsersList.ItemsSource = _usersView;
                
                MettreAJourStatistiques();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement: {ex.Message}", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool FiltrerUsers(object item)
        {
            if (item is User user && !string.IsNullOrWhiteSpace(txtRecherche.Text))
            {
                string recherche = txtRecherche.Text.ToLower();
                return user.Nom.ToLower().Contains(recherche) ||
                       user.Prenom.ToLower().Contains(recherche) ||
                       user.Email.ToLower().Contains(recherche) ||
                       user.Matricule.ToLower().Contains(recherche);
            }
            return true;
        }

        private void MettreAJourStatistiques()
        {
            if (_users.Count > 0)
            {
                int nombreTotal = _users.Count;
                var domainesUniques = _users.Select(u => u.Email.Split('@')[1]).Distinct().Count();
                var dernierAjout = _users.OrderByDescending(u => u.DateCreation).First().DateCreation;

                txtNombreTotal.Text = $"Total: {nombreTotal} utilisateur{(nombreTotal > 1 ? "s" : "")}";
                txtDomainesEmail.Text = $"Domaines: {domainesUniques}";
                txtDernierAjout.Text = $"Dernier ajout: {dernierAjout:dd/MM/yyyy HH:mm}";
            }
            else
            {
                txtNombreTotal.Text = "Total: 0 utilisateurs";
                txtDomainesEmail.Text = "Domaines: 0";
                txtDernierAjout.Text = "Dernier ajout: -";
            }
        }

        private void BtnActualiser_Click(object sender, RoutedEventArgs e)
        {
            ChargerUsers();
        }

        private void TxtRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            _usersView?.Refresh();
        }

        private void BtnExporter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Fichier CSV (*.csv)|*.csv|Fichier texte (*.txt)|*.txt",
                    DefaultExt = "csv",
                    FileName = $"utilisateurs_{DateTime.Now:yyyyMMdd_HHmmss}"
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
            sb.AppendLine("Matricule;Nom;Prénom;Email;Date de création");
            
            // Données
            foreach (var user in _users)
            {
                sb.AppendLine($"{user.Matricule};{user.Nom};{user.Prenom};{user.Email};{user.DateCreation:dd/MM/yyyy HH:mm}");
            }
            
            File.WriteAllText(cheminFichier, sb.ToString(), Encoding.UTF8);
        }

        protected override void OnClosed(EventArgs e)
        {
            _userService?.Dispose();
            base.OnClosed(e);
        }
    }
}