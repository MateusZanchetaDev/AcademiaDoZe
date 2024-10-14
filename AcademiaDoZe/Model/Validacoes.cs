using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademiaDoZe
{
    class Validacoes
    {
        public static void ValidaConexaoDB()
        {
            DbProviderFactory factory;
            string provider = ConfigurationManager.ConnectionStrings["BD"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;

            try
            {
                factory = DbProviderFactories.GetFactory(provider);
                using var conexao = factory.CreateConnection();
                conexao!.ConnectionString = connectionString;
                using var comando = factory.CreateCommand();
                comando!.Connection = conexao;
                conexao.Open();
            }
            catch (DbException ex)
            {
                MessageBox.Show($"{ex.Source}\n\n{ex.Message}\n\n{ex.ErrorCode}\n\n{ex.SqlState}\n\n{ex.StackTrace}");
                var auxConfig = new WindowConfig(provider, connectionString);
                auxConfig.ShowDialog();
                ValidaConexaoDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Source}\n\n{ex.Message}\n\n{ex.StackTrace}");
                var auxConfig = new WindowConfig(provider, connectionString);
                auxConfig.ShowDialog();
                ValidaConexaoDB();
            }
        }

        public static void TxtCPF_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxCpf)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxCpf.Text;
                    text = Regex.Replace(text, @"[^\d]", "");
                    text += e.Text;

                    if (text.Length <= 11)
                    {
                        if (text.Length > 9)
                        {
                            textBoxCpf.Text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6, 3)}-{text.Substring(9)}";
                        }
                        else if (text.Length > 6)
                        {
                            textBoxCpf.Text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6)}";
                        }
                        else if (text.Length > 3)
                        {
                            textBoxCpf.Text = $"{text.Substring(0, 3)}.{text.Substring(3)}";
                        }
                        else
                        {
                            textBoxCpf.Text = text;
                        }
                        textBoxCpf.CaretIndex = textBoxCpf.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        public static void TxtCNPJ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxCnpj)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxCnpj.Text;
                    text = Regex.Replace(text, @"[^\d]", "");
                    text += e.Text;

                    if (text.Length <= 14)
                    {
                        if (text.Length > 12)
                        {
                            textBoxCnpj.Text = $"{text.Substring(0, 2)}.{text.Substring(2, 3)}.{text.Substring(5, 3)}/{text.Substring(8, 4)}-{text.Substring(12)}";
                        }
                        else if (text.Length > 8)
                        {
                            textBoxCnpj.Text = $"{text.Substring(0, 2)}.{text.Substring(2, 3)}.{text.Substring(5, 3)}/{text.Substring(8)}";
                        }
                        else if (text.Length > 5)
                        {
                            textBoxCnpj.Text = $"{text.Substring(0, 2)}.{text.Substring(2, 3)}.{text.Substring(5)}";
                        }
                        else if (text.Length > 2)
                        {
                            textBoxCnpj.Text = $"{text.Substring(0, 2)}.{text.Substring(2)}";
                        }
                        else
                        {
                            textBoxCnpj.Text = text;
                        }
                        textBoxCnpj.CaretIndex = textBoxCnpj.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        public static void TxtCEP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxCep)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxCep.Text + e.Text;
                    text = Regex.Replace(text, @"[^\d]", "");

                    if (text.Length > 8)
                    {
                        text = text.Substring(0, 8);
                    }

                    if (text.Length <= 5)
                    {
                        textBoxCep.Text = text;
                    }
                    else
                    {
                        textBoxCep.Text = $"{text.Substring(0, 5)}-{text.Substring(5)}";
                    }

                    textBoxCep.CaretIndex = textBoxCep.Text.Length;
                    e.Handled = true; 
                }
            }
        }

        public static void TxtTelefone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxTelefone)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxTelefone.Text;
                    text = Regex.Replace(text, @"[^\d]", "");
                    text += e.Text;

                    if (text.Length <= 11)
                    {
                        if (text.Length > 6)
                        {
                            textBoxTelefone.Text = $"({text.Substring(0, 2)}) {text.Substring(2, 5)}-{text.Substring(7)}";
                        }
                        else if (text.Length > 2)
                        {
                            textBoxTelefone.Text = $"({text.Substring(0, 2)}) {text.Substring(2)}";
                        }
                        else
                        {
                            textBoxTelefone.Text = text;
                        }
                        textBoxTelefone.CaretIndex = textBoxTelefone.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        public static void TxtData_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxData)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxData.Text;
                    text = Regex.Replace(text, @"[^\d]", "");
                    text += e.Text;

                    if (text.Length <= 8)
                    {
                        if (text.Length > 4)
                        {
                            textBoxData.Text = $"{text.Substring(0, 2)}/{text.Substring(2, 2)}/{text.Substring(4)}";
                        }
                        else if (text.Length > 2)
                        {
                            textBoxData.Text = $"{text.Substring(0, 2)}/{text.Substring(2)}";
                        }
                        else
                        {
                            textBoxData.Text = text;
                        }
                        textBoxData.CaretIndex = textBoxData.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        public static void TxtDataHora_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxDataHora)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxDataHora.Text;
                    text = Regex.Replace(text, @"[^\d]", "");
                    text += e.Text;

                    if (text.Length <= 14)
                    {
                        if (text.Length > 10)
                        {
                            textBoxDataHora.Text = $"{text.Substring(0, 2)}/{text.Substring(2, 2)}/{text.Substring(4, 4)} {text.Substring(8, 2)}:{text.Substring(10)}";
                        }
                        else if (text.Length > 8)
                        {
                            textBoxDataHora.Text = $"{text.Substring(0, 2)}/{text.Substring(2, 2)}/{text.Substring(4)}";
                        }
                        else if (text.Length > 4)
                        {
                            textBoxDataHora.Text = $"{text.Substring(0, 2)}/{text.Substring(2, 2)}/{text.Substring(4)}";
                        }
                        else if (text.Length > 2)
                        {
                            textBoxDataHora.Text = $"{text.Substring(0, 2)}/{text.Substring(2)}";
                        }
                        else
                        {
                            textBoxDataHora.Text = text;
                        }
                        textBoxDataHora.CaretIndex = textBoxDataHora.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        public static void TxtValor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBoxValor)
            {
                e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");

                if (!e.Handled)
                {
                    var text = textBoxValor.Text;
                    text = Regex.Replace(text, @"[^\d]", "");
                    text += e.Text;

                    if (text.Length <= 12)
                    {
                        if (text.Length > 2)
                        {
                            textBoxValor.Text = $"{text.Substring(0, text.Length - 2)},{text.Substring(text.Length - 2)}";
                        }
                        else
                        {
                            textBoxValor.Text = text;
                        }
                        textBoxValor.CaretIndex = textBoxValor.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }
    }
}
