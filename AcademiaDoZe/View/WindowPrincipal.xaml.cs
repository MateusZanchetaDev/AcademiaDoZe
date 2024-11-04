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
using Microsoft.Windows.Themes;

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
            LiberaMenus(true, '1');
            //LiberaMenus(false, '0');
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
            MainFrame.Navigate(new PageHome());
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
            if (ButtonHome.IsEnabled)
            {
                LiberaMenus(false, '0');
                return;
            }
            WindowLogin windowLogin = new WindowLogin();
            windowLogin.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var colaboradorViewModel = windowLogin.DataContext as ColaboradorViewModel;
            windowLogin.ShowDialog();

            if (colaboradorViewModel.SelectedColaborador != null && colaboradorViewModel.SelectedColaborador.Id > 0)
            {
                txtTop.Text = $"Bem-vindo, {colaboradorViewModel.SelectedColaborador.Nome} - Tipo: {colaboradorViewModel.SelectedColaborador.Tipo}";
                LiberaMenus(true, (char)colaboradorViewModel.SelectedColaborador.Tipo);
            }
            else
            {
                txtTop.Text = "Login falhou. Nenhum colaborador selecionado.";
                LiberaMenus(false, '0');
            }
        }

        private void ButtonMatricula_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageListaMatricula());
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

        public void LiberaMenus(bool liberar, char grupo)
        {
            if (!liberar)
            {
                ButtonHome.IsEnabled = liberar; ButtonLogradouro.IsEnabled = liberar; ButtonAluno.IsEnabled = liberar; ButtonColaborador.IsEnabled = liberar; ButtonMatricula.IsEnabled = liberar;
                ButtonAvaliacao.IsEnabled = liberar; ButtonFrequencia.IsEnabled = liberar; ButtonAulas.IsEnabled = liberar; ButtonTreinos.IsEnabled = liberar; ButtonLogin.IsEnabled = !liberar; ButtonConfig.IsEnabled = liberar;
                
                ButtonHome_Click(null, null);
            }
            else if (grupo == '1') // Administrador - 1
            {
                ButtonHome.IsEnabled = liberar; ButtonLogradouro.IsEnabled = liberar; ButtonAluno.IsEnabled = liberar; ButtonColaborador.IsEnabled = liberar; ButtonMatricula.IsEnabled = liberar;
                ButtonAvaliacao.IsEnabled = liberar; ButtonFrequencia.IsEnabled = liberar; ButtonAulas.IsEnabled = liberar; ButtonTreinos.IsEnabled = liberar; ButtonLogin.IsEnabled = liberar; ButtonConfig.IsEnabled = liberar;
            }
            else if (grupo == '2') // Atendente - 2
            {
                ButtonHome.IsEnabled = liberar; ButtonLogradouro.IsEnabled = liberar; ButtonAluno.IsEnabled = liberar; ButtonColaborador.IsEnabled = !liberar; ButtonMatricula.IsEnabled = liberar;
                ButtonAvaliacao.IsEnabled = !liberar; ButtonFrequencia.IsEnabled = liberar; ButtonAulas.IsEnabled = liberar; ButtonTreinos.IsEnabled = !liberar; ButtonLogin.IsEnabled = liberar; ButtonConfig.IsEnabled = !liberar;
            }
            else if (grupo == '3') // Instrutor – 3
            {
                ButtonHome.IsEnabled = liberar; ButtonLogradouro.IsEnabled = !liberar; ButtonAluno.IsEnabled = !liberar; ButtonColaborador.IsEnabled = !liberar; ButtonMatricula.IsEnabled = !liberar;
                ButtonAvaliacao.IsEnabled = liberar; ButtonFrequencia.IsEnabled = liberar; ButtonAulas.IsEnabled = liberar; ButtonTreinos.IsEnabled = liberar; ButtonLogin.IsEnabled = liberar; ButtonConfig.IsEnabled = !liberar;
            }
        }
    }
}