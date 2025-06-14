using Microsoft.EntityFrameworkCore;
using TelephoneCRUD.Data;
using TelephoneCRUD.Models;

namespace TelephoneCRUD.Services
{
    public class TelephoneService
    {
        private readonly TelephoneContext _context;

        public TelephoneService()
        {
            _context = new TelephoneContext();
            _context.Database.EnsureCreated();
        }

        // Créer un nouveau téléphone
        public async Task<bool> AjouterTelephoneAsync(Telephone telephone)
        {
            try
            {
                _context.Telephones.Add(telephone);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Lire tous les téléphones
        public async Task<List<Telephone>> ObtenirTousLesTelephoneAsync()
        {
            return await _context.Telephones.OrderBy(t => t.Marque).ToListAsync();
        }

        // Lire un téléphone par IMEI
        public async Task<Telephone?> ObtenirTelephoneParImeiAsync(string imei)
        {
            return await _context.Telephones.FindAsync(imei);
        }

        // Mettre à jour un téléphone
        public async Task<bool> ModifierTelephoneAsync(Telephone telephone)
        {
            try
            {
                _context.Telephones.Update(telephone);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Supprimer un téléphone
        public async Task<bool> SupprimerTelephoneAsync(string imei)
        {
            try
            {
                var telephone = await _context.Telephones.FindAsync(imei);
                if (telephone != null)
                {
                    _context.Telephones.Remove(telephone);
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}