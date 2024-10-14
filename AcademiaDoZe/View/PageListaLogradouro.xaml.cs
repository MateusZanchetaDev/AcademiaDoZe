using AcademiaDoZe.DataAccess;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AcademiaDoZe.ViewModel
{
    /// <summary>
    /// Interaction logic for PageListaLogradouro.xaml
    /// </summary>
    public partial class PageListaLogradouro : Page
    {
        private LogradouroViewModel ViewModelLogradouro;

        public PageListaLogradouro()
        {
            InitializeComponent();

            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.Loaded += Page_Loaded;

            try
            {
                ViewModelLogradouro = new LogradouroViewModel();
                ViewModelLogradouro.GetAll();
                DataContext = ViewModelLogradouro;
            }
            catch
            {
                MessageBox.Show("Erro ao carregar a lista de logradouros!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClassFuncoes.AjustaResources(this);
        }

        private void ButtonNovo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
