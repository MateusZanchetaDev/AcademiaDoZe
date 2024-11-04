using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Configuration;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AcademiaDoZe
{
    public class ClassFuncoes
    {
        ///<summary>
        /// Realiza a definição da cultura e ajusta o idioma / região
        ///</summary>
        public static void AjustaIdiomaRegiao()
        {
            CultureInfo culture = new((ConfigurationManager.AppSettings.Get("Idioma") is not null) ? ConfigurationManager.AppSettings.Get("Idioma") : ""!);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public static void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var focusedElement = Keyboard.FocusedElement as UIElement;
                focusedElement?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                if (sender is Window window)
                {
                    window.Close();
                }
                else
                {
                    if (Application.Current.MainWindow is TelaPrincipal mainWindow)
                    {
                        mainWindow.ButtonHome_Click(sender, e);
                    }
                }
            }
        }

        public static void AjustaResources(DependencyObject parent)
        {
            if (parent == null) return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement element)
                {
                    ComponentResourceManager resources = new(typeof(Properties.Idioma));
                    resources.ApplyResources(element, element.Name);
                }

                AjustaResources(child);
            }
        }

        public static string Sha256Hash(string senha)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] bytes =
            sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }
}
