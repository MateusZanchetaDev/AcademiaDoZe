using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Resources;
using System.ComponentModel;
using System.Configuration;
using AcademiaDoZe.ViewModel;
using AcademiaDoZe.View;

namespace AcademiaDoZe
{
    /// <summary>
    /// Interaction logic for TelaPrincipal.xaml
    /// </summary>
    public partial class TelaPrincipal : Window
    {
        private ResourceManager _resourceManager;
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        public TelaPrincipal()
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.Closing += MainWindow_Closing;

            _resourceManager = new ResourceManager("AcademiaDoZe.Properties.Idioma", typeof(TelaPrincipal).Assembly);
            
            Validacoes.ValidaConexaoDB();
            ProviderName = ConfigurationManager.ConnectionStrings["BD"].ProviderName;
            ConnectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
            MainFrame.Navigate(new PageHome());
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(GetLocalizedString("CloseConfirmation"), GetLocalizedString("ConfirmationCaption"), MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private string GetLocalizedString(string key)
        {
            return _resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture) ?? key;
        }

        private void ButtonLogradouro_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is not PageListaLogradouro)
            {
                MainFrame.Content = new PageListaLogradouro();
            }
        }

        private void ButtonAluno_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageListaAluno());
        }

        public void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate (new PageHome());
        }

        private void ButtonColaborador_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageListaColaborador());
        }

        private void ButtonSenha_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageHome());
            WindowSenha windowSenha = new();
            windowSenha.ShowDialog();
        }

        private void ButtonFrequencia_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageHome());
            WindowFrequencia windowFrequencia = new();
            windowFrequencia.ShowDialog();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageHome());
            WindowLogin windowLogin = new();
            windowLogin.ShowDialog();
        }

        private void ButtonMatricula_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageMatricula());
        }

        private void ButtonAvaliacao_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAvaliacao());
        }

        private void ButtonTreinos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAulas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            WindowConfig windowConfig = new(ProviderName, ConnectionString);
            windowConfig.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowConfig.ShowDialog();
            TelaPrincipal WindowMain = new();
            Application.Current.MainWindow = WindowMain;
            WindowMain.Show();
            Close();
        }
    }
}
