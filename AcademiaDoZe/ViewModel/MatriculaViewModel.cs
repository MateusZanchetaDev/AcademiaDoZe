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
using ZstdSharp.Unsafe;

namespace AcademiaDoZe.ViewModel
{
    public class MatriculaViewModel : ViewModelBase
    {
        public ObservableCollection<Matricula> Matriculas { get; set; }
        private Matricula _selectedMatricula;

        public Matricula SelectedMatricula
        {
            get { return _selectedMatricula; }
            set
            {
                _selectedMatricula = value;
                OnPropertyChanged("SelectedMatricula");

                MatriculaAtualizarCommand.RaiseCanExecuteChanged();
                MatriculaRemoverCommand.RaiseCanExecuteChanged();
            }
        }

        private MatriculaRepository _repository;
        public RelayCommand MatriculaAdicionarCommand { get; set; }
        public RelayCommand MatriculaAtualizarCommand { get; set; }
        public RelayCommand MatriculaRemoverCommand { get; set; }

        public MatriculaViewModel()
        {
            Matriculas = new ObservableCollection<Matricula>();
            _repository = new MatriculaRepository();

            MatriculaAdicionarCommand = new RelayCommand(AdicionarMatricula);
            MatriculaAtualizarCommand = new RelayCommand(AtualizarMatricula, CanExecuteSubmit);
            MatriculaRemoverCommand = new RelayCommand(RemoverMatricula, CanExecuteSubmit);

            GetAll();
        }

        public void GetAll()
        {
            Matriculas.Clear();
            _repository.GetAll().ForEach(data => Matriculas.Add(data));
        }

        private bool CanExecuteSubmit(object parameter)
        {
            return SelectedMatricula != null;
        }

        private void AdicionarMatricula(object obj)
        {
            WindowMatricula janelaCadastro = new WindowMatricula();
            var viewModel = (MatriculaCadastroViewModel)janelaCadastro.DataContext;
            viewModel.MatriculaSalva += (sender, e) =>
            {
                try
                {
                    var novaMatricula = viewModel.GetMatricula();
                    _repository.Add(novaMatricula);
                    janelaCadastro.Close();
                    MessageBox.Show("Matrícula adicionada com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    GetAll();
                }
            };
            janelaCadastro.ShowDialog();
        }

        private void AtualizarMatricula(object obj)
        {
            if (SelectedMatricula == null) return;
            WindowMatricula janelaCadastro = new WindowMatricula();
            var viewModel = new MatriculaCadastroViewModel(SelectedMatricula);
            janelaCadastro.DataContext = viewModel;
            viewModel.MatriculaSalva += (sender, e) =>
            {
                try
                {
                    var matriculaEditada = viewModel.GetMatricula();
                    _repository.Update(matriculaEditada);
                    janelaCadastro.Close();
                    MessageBox.Show("Matrícula atualizada com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    GetAll();
                }
            };
            janelaCadastro.ShowDialog();
        }

        private void RemoverMatricula(object obj)
        {
            if (SelectedMatricula == null) return;
            if (MessageBox.Show("Tem certeza que deseja remover esta matrícula?", "Matrícula", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.Delete(SelectedMatricula);
                    MessageBox.Show("Matrícula removida com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    GetAll();
                }
            }
        }
    }
}