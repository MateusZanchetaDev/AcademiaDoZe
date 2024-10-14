using AcademiaDoZe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademiaDoZe.ViewModel
{
    public class AlunoCadastroViewModel : LogradouroViewModel
    {
        private Aluno _aluno;
        public int Id { get { return _aluno.Id; } set { _aluno.Id = value; OnPropertyChanged("Id"); } }
        public string Cpf { get { return _aluno.Cpf; } set { _aluno.Cpf = value; OnPropertyChanged("Cpf"); } }
        public string Telefone { get { return _aluno.Telefone; } set { _aluno.Telefone = value; OnPropertyChanged("Telefone"); } }
        public string Nome { get { return _aluno.Nome; } set { _aluno.Nome = value; OnPropertyChanged("Nome"); } }
        public DateTime Nascimento { get { return _aluno.Nascimento; } set { _aluno.Nascimento = value; OnPropertyChanged("Nascimento"); } }
        public string Email { get { return _aluno.Email; } set { _aluno.Email = value; OnPropertyChanged("Email"); } }
        public int LogradouroId { get { return _aluno.LogradouroId; } set { _aluno.LogradouroId = value; OnPropertyChanged("LogradouroId"); } }
        public string Numero { get { return _aluno.Numero; } set { _aluno.Numero = value; OnPropertyChanged("Numero"); } }
        public string Complemento { get { return _aluno.Complemento; } set { _aluno.Complemento = value; OnPropertyChanged("Complemento"); } }
        public string Senha { get { return _aluno.Senha; } set { _aluno.Senha = value; OnPropertyChanged("Senha"); } }
        public ICommand SalvarAlunoCommand { get; set; }
        public event EventHandler AlunoSalvo;

        public AlunoCadastroViewModel(Aluno aluno = null)
        {
            _aluno = aluno ?? new Aluno();
            SalvarAlunoCommand = new RelayCommand(SalvarAluno);
           
            SelectedLogradouro = Logradouros.FirstOrDefault(l => l.Id == _aluno.LogradouroId);
        }
        private void SalvarAluno(object obj)
        {
            AlunoSalvo?.Invoke(this, EventArgs.Empty);
        }
        public Aluno GetAluno()
        {
            _aluno.LogradouroId = SelectedLogradouro.Id;
            return _aluno;
        }
    }
}
