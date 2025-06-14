using System.Windows;

namespace TelephoneCRUD
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Configuration globale de l'application si nécessaire
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}