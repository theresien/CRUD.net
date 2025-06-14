using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UserCRUD.Models;
using UserCRUD.Services;

namespace UserCRUD
{
    public partial class MainWindow : Window
    {
        private readonly UserService _userService;
        private bool _modeModification = false;
        private string _matriculeOriginal = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            ChargerUsers();
        }

        private async void ChargerUsers()
        {
            try
            {
                txtStatus.Text = "Chargement des utilisateurs...";
                var users = await _userService.ObtenirTousLesUsersAsync();
                dgUsers.ItemsSource = users;
                txtStatus.Text = $"Nombre d'utilisateurs: {users.Count}";
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
                // Vérifier si l'email existe déjà
                if (await _userService.EmailExisteAsync(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Cet email est déjà utilisé par un autre utilisateur.", "Email existant", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = new User
                {
                    Matricule = txtMatricule.Text.Trim(),
                    Nom = txtNom.Text.Trim(),
                    Prenom = txtPrenom.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                bool succes = await _userService.AjouterUserAsync(user);
                
                if (succes)
                {
                    MessageBox.Show("Utilisateur ajouté avec succès!", "Succès", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ViderChamps();
                    ChargerUsers();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout. Le matricule existe peut-être déjà.", "Erreur", 
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
                MessageBox.Show("Veuillez sélectionner un utilisateur dans la liste pour le modifier.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!ValiderChamps()) return;

            try
            {
                // Vérifier si l'email existe déjà (sauf pour l'utilisateur actuel)
                if (await _userService.EmailExisteAsync(txtEmail.Text.Trim(), _matriculeOriginal))
                {
                    MessageBox.Show("Cet email est déjà utilisé par un autre utilisateur.", "Email existant", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = new User
                {
                    Matricule = _matriculeOriginal, // Utilise le matricule original
                    Nom = txtNom.Text.Trim(),
                    Prenom = txtPrenom.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                bool succes = await _userService.ModifierUserAsync(user);
                
                if (succes)
                {
                    MessageBox.Show("Utilisateur modifié avec succès!", "Succès", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ViderChamps();
                    ChargerUsers();
                    _modeModification = false;
                    txtMatricule.IsReadOnly = false;
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
            if (string.IsNullOrWhiteSpace(txtMatricule.Text))
            {
                MessageBox.Show("Veuillez saisir ou sélectionner un matricule.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer l'utilisateur avec le matricule: {txtMatricule.Text}?", 
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool succes = await _userService.SupprimerUserAsync(txtMatricule.Text.Trim());
                    
                    if (succes)
                    {
                        MessageBox.Show("Utilisateur supprimé avec succès!", "Succès", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        ViderChamps();
                        ChargerUsers();
                        _modeModification = false;
                        txtMatricule.IsReadOnly = false;
                    }
                    else
                    {
                        MessageBox.Show("Utilisateur non trouvé ou erreur lors de la suppression.", "Erreur", 
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
            txtMatricule.IsReadOnly = false;
        }

        private void BtnActualiser_Click(object sender, RoutedEventArgs e)
        {
            ChargerUsers();
        }

        private void BtnVoirListe_Click(object sender, RoutedEventArgs e)
        {
            var listeWindow = new ListeUserWindow();
            listeWindow.Show();
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem is User user)
            {
                txtMatricule.Text = user.Matricule;
                txtNom.Text = user.Nom;
                txtPrenom.Text = user.Prenom;
                txtEmail.Text = user.Email;
                
                _modeModification = true;
                _matriculeOriginal = user.Matricule;
                txtMatricule.IsReadOnly = true; // Empêche la modification du matricule
            }
        }

        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtMatricule.Text))
            {
                MessageBox.Show("Le matricule est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMatricule.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNom.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                MessageBox.Show("Le prénom est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPrenom.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("L'email est obligatoire.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validation du format email
            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Le format de l'email n'est pas valide.", "Validation", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private void ViderChamps()
        {
            txtMatricule.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtEmail.Text = string.Empty;
            dgUsers.SelectedItem = null;
        }

        protected override void OnClosed(EventArgs e)
        {
            _userService?.Dispose();
            base.OnClosed(e);
        }
    }
}