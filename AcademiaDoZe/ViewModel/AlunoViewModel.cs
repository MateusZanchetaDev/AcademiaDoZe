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
    public class AlunoViewModel : ViewModelBase
    {
        public ObservableCollection<Aluno> Alunos { get; set; }
        private Aluno _selectedAluno;
        public Aluno SelectedAluno
        {
            get { return _selectedAluno; }
            set
            {
                _selectedAluno = value;
                OnPropertyChanged("SelectedAluno");
                // libera somente se houver um Aluno selecionado
                AlunoAtualizarCommand.RaiseCanExecuteChanged();
                AlunoRemoverCommand.RaiseCanExecuteChanged();
            }
        }
        private AlunoRepository _repository;
        public RelayCommand AlunoAdicionarCommand { get; set; }
        public RelayCommand AlunoAtualizarCommand { get; set; }
        public RelayCommand AlunoRemoverCommand { get; set; }
        public AlunoViewModel()
        {
            Alunos = new ObservableCollection<Aluno>();
            _repository = new AlunoRepository();

            AlunoAdicionarCommand = new RelayCommand(AdicionarAluno);
            AlunoAtualizarCommand = new RelayCommand(AtualizarAluno, CanExecuteSubmit);
            AlunoRemoverCommand = new RelayCommand(RemoverAluno, CanExecuteSubmit);

            GetAll();
        }

        public void GetAll()
        {
            Alunos.Clear();
            _repository.GetAll().ForEach(data => Alunos.Add(data));
        }

        private bool CanExecuteSubmit(object parameter)
        {
            return SelectedAluno != null;
        }

        private void AdicionarAluno(object obj)
        {
            WindowAluno janelaCadastro = new();
            var viewModel = (AlunoCadastroViewModel)janelaCadastro.DataContext;
            viewModel.AlunoSalvo += (sender, e) =>
            {
                try
                {
                    var novoAluno = viewModel.GetAluno();
                    _repository.Add(novoAluno);
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

        private void AtualizarAluno(object obj)
        {
            WindowAluno janelaCadastro = new WindowAluno();
            var viewModel = new AlunoCadastroViewModel(SelectedAluno);
            janelaCadastro.DataContext = viewModel;
            viewModel.AlunoSalvo += (sender, e) =>
            {
                try
                {
                    var AlunoEditado = viewModel.GetAluno();
                    _repository.Update(AlunoEditado);
                    //GetAll();
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

        private void RemoverAluno(object obj)
        {
            if (SelectedAluno == null) return;
            if (MessageBox.Show("Confirma?", "Aluno", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.Delete(SelectedAluno);
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
