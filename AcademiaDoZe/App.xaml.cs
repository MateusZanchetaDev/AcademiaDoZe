using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Windows;

namespace AcademiaDoZe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // aplicando polimorfismo
        // reescrita do método OnStartup
        protected override void OnStartup(StartupEventArgs e)
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            
            base.OnStartup(e);

            ClassFuncoes.AjustaIdiomaRegiao();
        }
    }

}
