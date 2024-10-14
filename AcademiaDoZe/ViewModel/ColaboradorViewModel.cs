using AcademiaDoZe.DataAccess;
using AcademiaDoZe.Model;
using AcademiaDoZe.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AcademiaDoZe.ViewModel
{
    public class ColaboradorViewModel : ViewModelBase
    {
        public ObservableCollection<Colaborador> Colaboradors { get; set; }
        private Colaborador _selectedColaborador;
        private ColaboradorRepository _repository;
        public RelayCommand ColaboradorAdicionarCommand { get; set; }
        public RelayCommand ColaboradorAtualizarCommand { get; set; }
        public RelayCommand ColaboradorRemoverCommand { get; set; }

        public Colaborador SelectedColaborador
        {
            get { return _selectedColaborador; }
            set
            {
                _selectedColaborador = value;
                OnPropertyChanged("SelectedColaborador");

                ColaboradorAtualizarCommand.RaiseCanExecuteChanged();
                ColaboradorRemoverCommand.RaiseCanExecuteChanged();
            }
        }
      
        public ColaboradorViewModel()
        {
            Colaboradors = new ObservableCollection<Colaborador>();
            _repository = new ColaboradorRepository();

            ColaboradorAdicionarCommand = new RelayCommand(AdicionarColaborador);
            ColaboradorAtualizarCommand = new RelayCommand(AtualizarColaborador, CanExecuteSubmit);
            ColaboradorRemoverCommand = new RelayCommand(RemoverColaborador, CanExecuteSubmit);

            GetAll();
        }
        private bool CanExecuteSubmit(object parameter)
        {
            return SelectedColaborador != null;
        }

        public void GetAll()
        {
            Colaboradors.Clear();
            _repository.GetAll().ForEach(data => Colaboradors.Add(data));
        }

        private void AdicionarColaborador(object obj)
        {
            WindowColaborador janelaCadastro = new WindowColaborador();
            var viewModel = (ColaboradorCadastroViewModel)janelaCadastro.DataContext;
            viewModel.ColaboradorSalvo += (sender, e) =>
            {
                try
                {
                    var novoColaborador = viewModel.GetColaborador();
                    _repository.Add(novoColaborador);
                    janelaCadastro.Close();
                    MessageBox.Show("Sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    GetAll();
                }
            };
            janelaCadastro.ShowDialog();
        }

        private void AtualizarColaborador(object obj)
        {
            WindowColaborador janelaCadastro = new WindowColaborador();
            var viewModel = new ColaboradorCadastroViewModel(SelectedColaborador);
            janelaCadastro.DataContext = viewModel;
            viewModel.ColaboradorSalvo += (sender, e) =>
            {
                try
                {
                    var ColaboradorEditado = viewModel.GetColaborador();
                    _repository.Update(ColaboradorEditado);
                    janelaCadastro.Close();
                    MessageBox.Show("Sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    GetAll();
                }
            };
            janelaCadastro.ShowDialog();
        }

        private void RemoverColaborador(object obj)
        {
            if (SelectedColaborador == null) return;
            if (MessageBox.Show("Confirma?", "Colaborador", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.Delete(SelectedColaborador);
                    MessageBox.Show("Sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    GetAll();
                }
            }
        }
    }
}
