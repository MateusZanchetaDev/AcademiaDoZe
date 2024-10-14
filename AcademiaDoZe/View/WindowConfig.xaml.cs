using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AcademiaDoZe
{
    /// <summary>
    /// Interaction logic for WindowConfig.xaml
    /// </summary>
    public partial class WindowConfig : Window
    {
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        public WindowConfig(string providerName, string connectionString)
        {
            InitializeComponent();
            ConnectionString = connectionString;
            ProviderName = providerName;

            this.Loaded += Page_Loaded;
            this.KeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            ComboBoxIdioma.SelectedItem = ConfigurationManager.AppSettings.Get("IdiomaRegiao");

            comboBoxProvider.SelectedItem = ProviderName;
            textBoxStringDeConexao.Text = ConnectionString;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClassFuncoes.AjustaResources(this);
        }

        /// <summary>
        /// Abre o arquivo local como leitura/escrita e salva as alterações em AcademiaDoZe_WPF.dll.config
        /// </summary>
        /// <param name="ClassFuncoes.AjustaIdiomaRegiao()">Atualiza a cultura corrente</param>
        private void ButtonSalvar_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("Idioma");
                config.AppSettings.Settings.Add("Idioma", ComboBoxIdioma.Text);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                ClassFuncoes.AjustaIdiomaRegiao();
                _ = MessageBox.Show("Idioma/região alterada com sucesso!");
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            finally{
                Close();
            }
        }

        private void EncerrarAplicacao_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SalvaBD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["BD"].ProviderName = comboBoxProvider.Text;
                config.ConnectionStrings.ConnectionStrings["BD"].ConnectionString = textBoxStringDeConexao.Text;
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings"); 
                _ = MessageBox.Show("Dados alterados com sucesso!");
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            finally{
                Close();
            }
        }
    }
}
