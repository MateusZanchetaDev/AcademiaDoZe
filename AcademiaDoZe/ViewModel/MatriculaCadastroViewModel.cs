using AcademiaDoZe.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademiaDoZe.ViewModel
{
    public class MatriculaCadastroViewModel : AlunoViewModel
    {
        private Matricula _matricula;
        public int Id { get { return _matricula.Id; } set { _matricula.Id = value; OnPropertyChanged("Id"); } }
        public int AlunoId { get { return _matricula.AlunoId; } set { _matricula.AlunoId = value; OnPropertyChanged("IdAluno"); } }
        public int ColaboradorId { get { return _matricula.ColaboradorId; } set { _matricula.ColaboradorId = value; OnPropertyChanged("IdAtendente"); } }
        public PlanoMatricula Plano { get { return _matricula.Plano; } set { _matricula.Plano = value; OnPropertyChanged("Plano"); } }
        public DateTime DataInicio { get { return _matricula.DataInicio; } set { _matricula.DataInicio = value; OnPropertyChanged("DataInicio"); } }
        public DateTime DataFim { get { return _matricula.DataFim; } set { _matricula.DataFim = value; OnPropertyChanged("DataFim"); } }
        public RestricaoMedica RestricaoMedica { get { return _matricula.RestricaoMedica; } set { _matricula.RestricaoMedica = value; OnPropertyChanged("RestricaoMedica"); } }
        public string ObsRestricao { get { return _matricula.ObsRestricao; } set { _matricula.ObsRestricao = value; OnPropertyChanged("ObsRestricao"); } }
        public string Objetivo { get { return _matricula.Objetivo; } set { _matricula.Objetivo = value; OnPropertyChanged("Objetivo"); } }
        public byte[] LaudoMedico { get { return _matricula.LaudoMedico; } set { _matricula.LaudoMedico = value; OnPropertyChanged("LaudoMedico"); } }

        public ICommand SalvarMatriculaCommand { get; set; }
        public RelayCommand SelecionarLaudoCommand { get; set; }
        public event EventHandler MatriculaSalva;
        public ColaboradorViewModel ColaboradoresViewModel { get; set; }
        public MatriculaCadastroViewModel(Matricula matricula = null)
        {
            _matricula = matricula ?? new Matricula();
            SelecionarLaudoCommand = new RelayCommand(SelecionarLaudo);
            SalvarMatriculaCommand = new RelayCommand(SalvarMatricula);
            ColaboradoresViewModel = new ColaboradorViewModel();
        }

        private void SelecionarLaudo(object parameter)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                LaudoMedico = System.IO.File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void SalvarMatricula(object obj)
        {
            MatriculaSalva?.Invoke(this, EventArgs.Empty);
        }

        public Matricula GetMatricula()
        {
            SelectedAluno = Alunos.FirstOrDefault(a => a.Id == _matricula.AlunoId);
            ColaboradoresViewModel.SelectedColaborador = ColaboradoresViewModel.Colaboradores.FirstOrDefault(c => c.Id == _matricula.ColaboradorId);
            
            _matricula.ColaboradorId = ColaboradoresViewModel.SelectedColaborador.Id;
            _matricula.AlunoId = SelectedAluno.Id;
            return _matricula;
        }
    }
}
