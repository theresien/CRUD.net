using Microsoft.EntityFrameworkCore;
using UserCRUD.Data;
using UserCRUD.Models;

namespace UserCRUD.Services
{
    public class UserService
    {
        private readonly UserContext _context;

        public UserService()
        {
            _context = new UserContext();
            _context.Database.EnsureCreated();
        }

        // Créer un nouvel utilisateur
        public async Task<bool> AjouterUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Lire tous les utilisateurs
        public async Task<List<User>> ObtenirTousLesUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.Nom).ThenBy(u => u.Prenom).ToListAsync();
        }

        // Lire un utilisateur par matricule
        public async Task<User?> ObtenirUserParMatriculeAsync(string matricule)
        {
            return await _context.Users.FindAsync(matricule);
        }

        // Mettre à jour un utilisateur
        public async Task<bool> ModifierUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Supprimer un utilisateur
        public async Task<bool> SupprimerUserAsync(string matricule)
        {
            try
            {
                var user = await _context.Users.FindAsync(matricule);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Vérifier si un email existe déjà
        public async Task<bool> EmailExisteAsync(string email, string? matriculeExclus = null)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Matricule != matriculeExclus);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}