using AcademiaDoZe.ViewModel;
using System;
using System.Collections.Generic;
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

namespace AcademiaDoZe.View
{
    /// <summary>
    /// Interaction logic for WindowColaborador.xaml
    /// </summary>
    public partial class WindowColaborador : Window
    {
        public WindowColaborador()
        {
            InitializeComponent();

            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.Loaded += Page_Loaded;

            ComboBoxTipo.ItemsSource = Enum.GetValues(typeof(Model.EnumColaboradorTipo));
            ComboBoxVinculo.ItemsSource = Enum.GetValues(typeof(Model.EnumColaboradorVinculo));

            DataContext = new ColaboradorCadastroViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClassFuncoes.AjustaResources(this);
        }
    }
}
