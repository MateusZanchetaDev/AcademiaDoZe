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

namespace AcademiaDoZe
{
    /// <summary>
    /// Interaction logic for WindowFrequencia.xaml
    /// </summary>
    public partial class WindowFrequencia : Window
    {
        public WindowFrequencia()
        {
            InitializeComponent();
            DatePickerEntrada.PreviewTextInput += Validacoes.TxtDataHora_PreviewTextInput;
            DatePickerSaida.PreviewTextInput += Validacoes.TxtDataHora_PreviewTextInput;
            this.KeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);
            this.Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClassFuncoes.AjustaResources(this);
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
