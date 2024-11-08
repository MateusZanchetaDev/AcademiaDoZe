﻿using AcademiaDoZe.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.DataAccess
{
    public class MatriculaRepository
    {
        private readonly DbProviderFactory factory;
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        public MatriculaRepository()
        {
            ProviderName = ConfigurationManager.ConnectionStrings["BD"].ProviderName;
            ConnectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;

            factory = DbProviderFactories.GetFactory(ProviderName);
        }

        public List<Matricula> GetAll()
        {
            using var conexao = factory.CreateConnection();
            conexao!.ConnectionString = ConnectionString;
            using var comando = factory.CreateCommand();
            comando!.Connection = conexao;
            conexao.Open();
            comando.CommandText = @"SELECT id_matricula, aluno_id, colaborador_id, plano, data_inicio, data_fim, objetivo, restricao_medica, obs_restricao, laudo_medico FROM tb_matricula;";

            using var reader = comando.ExecuteReader();
            List<Matricula> dadosRetorno = new List<Matricula>();

            while (reader.Read())
            {
                dadosRetorno.Add(new Matricula
                {
                    Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    AlunoId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                    ColaboradorId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                    Plano = (PlanoMatricula)reader.GetString(3)[0],
                    DataInicio = reader.GetDateTime(4),
                    DataFim = reader.GetDateTime(5),
                    Objetivo = reader.IsDBNull(6) ? null : reader.GetString(6),
                    RestricaoMedica = (RestricaoMedica)reader.GetString(7)[0],
                    ObsRestricao = reader.IsDBNull(8) ? null : reader.GetString(8),
                    LaudoMedico = reader.IsDBNull(9) ? null : (byte[])reader[9]
                });
            }
            return dadosRetorno;
        }

        public void Add(Matricula dado)
        {
            using var conexao = factory.CreateConnection();
            conexao!.ConnectionString = ConnectionString;
            using var comando = factory.CreateCommand();
            comando!.Connection = conexao;

            var aluno_id = comando.CreateParameter();
            aluno_id.ParameterName = "@aluno_id";
            aluno_id.Value = dado.AlunoId;
            comando.Parameters.Add(aluno_id);

            var colaborador_id = comando.CreateParameter();
            colaborador_id.ParameterName = "@colaborador_id";
            colaborador_id.Value = dado.ColaboradorId;
            comando.Parameters.Add(colaborador_id);

            var plano = comando.CreateParameter();
            plano.ParameterName = "@plano";
            plano.Value = (char)dado.Plano;
            comando.Parameters.Add(plano);

            var data_inicio = comando.CreateParameter();
            data_inicio.ParameterName = "@data_inicio";
            data_inicio.Value = dado.DataInicio;
            comando.Parameters.Add(data_inicio);

            var data_fim = comando.CreateParameter();
            data_fim.ParameterName = "@data_fim";
            data_fim.Value = dado.DataFim;
            comando.Parameters.Add(data_fim);

            var objetivo = comando.CreateParameter();
            objetivo.ParameterName = "@objetivo";
            objetivo.Value = dado.Objetivo;
            comando.Parameters.Add(objetivo);

            var restricao_medica = comando.CreateParameter();
            restricao_medica.ParameterName = "@restricao_medica";
            restricao_medica.Value = (char)dado.RestricaoMedica;
            comando.Parameters.Add(restricao_medica);

            var obs_restricao = comando.CreateParameter();
            obs_restricao.ParameterName = "@obs_restricao";
            obs_restricao.Value = dado.ObsRestricao;
            comando.Parameters.Add(obs_restricao);

            var laudo_medico = comando.CreateParameter();
            laudo_medico.ParameterName = "@laudo_medico";
            laudo_medico.Value = dado.LaudoMedico ?? (object)DBNull.Value;
            comando.Parameters.Add(laudo_medico);

            conexao.Open();
            comando.CommandText = @"INSERT INTO tb_matricula (aluno_id, colaborador_id, plano, data_inicio, data_fim, objetivo, restricao_medica, obs_restricao, laudo_medico) 
                            VALUES (@aluno_id, @colaborador_id, @plano, @data_inicio, @data_fim, @objetivo, @restricao_medica, @obs_restricao, @laudo_medico);";

            var linhas = comando.ExecuteNonQuery();
        }

        public void Update(Matricula dado)
        {
            using var conexao = factory.CreateConnection();
            conexao!.ConnectionString = ConnectionString;
            using var comando = factory.CreateCommand();
            comando!.Connection = conexao;

            var id_matricula = comando.CreateParameter();
            id_matricula.ParameterName = "@id_matricula";
            id_matricula.Value = dado.Id;
            comando.Parameters.Add(id_matricula);

            var aluno_id = comando.CreateParameter();
            aluno_id.ParameterName = "@aluno_id";
            aluno_id.Value = dado.AlunoId;
            comando.Parameters.Add(aluno_id);

            var colaborador_id = comando.CreateParameter();
            colaborador_id.ParameterName = "@colaborador_id";
            colaborador_id.Value = dado.ColaboradorId;
            comando.Parameters.Add(colaborador_id);

            var plano = comando.CreateParameter();
            plano.ParameterName = "@plano";
            plano.Value = (char)dado.Plano;
            comando.Parameters.Add(plano);

            var data_inicio = comando.CreateParameter();
            data_inicio.ParameterName = "@data_inicio";
            data_inicio.Value = dado.DataInicio;
            comando.Parameters.Add(data_inicio);

            var data_fim = comando.CreateParameter();
            data_fim.ParameterName = "@data_fim";
            data_fim.Value = dado.DataFim;
            comando.Parameters.Add(data_fim);

            var objetivo = comando.CreateParameter();
            objetivo.ParameterName = "@objetivo";
            objetivo.Value = dado.Objetivo;
            comando.Parameters.Add(objetivo);

            var restricao_medica = comando.CreateParameter();
            restricao_medica.ParameterName = "@restricao_medica";
            restricao_medica.Value = (char)dado.RestricaoMedica;
            comando.Parameters.Add(restricao_medica);

            var obs_restricao = comando.CreateParameter();
            obs_restricao.ParameterName = "@obs_restricao";
            obs_restricao.Value = dado.ObsRestricao;
            comando.Parameters.Add(obs_restricao);

            var laudo_medico = comando.CreateParameter();
            laudo_medico.ParameterName = "@laudo_medico";
            laudo_medico.Value = dado.LaudoMedico ?? (object)DBNull.Value;
            comando.Parameters.Add(laudo_medico);

            conexao.Open();
            comando.CommandText = @"UPDATE tb_matricula 
                            SET aluno_id = @aluno_id, colaborador_id = @colaborador_id, plano = @plano, data_inicio = @data_inicio, data_fim = @data_fim,
                            objetivo = @objetivo, restricao_medica = @restricao_medica, obs_restricao = @obs_restricao, laudo_medico = @laudo_medico
                            WHERE id_matricula = @id_matricula;";

            var linhas = comando.ExecuteNonQuery();
        }

        public void Delete(Matricula dado)
        {
            using var conexao = factory.CreateConnection();
            conexao!.ConnectionString = ConnectionString;
            using var comando = factory.CreateCommand();
            comando!.Connection = conexao;

            var id_matricula = comando.CreateParameter();
            id_matricula.ParameterName = "@id_matricula";
            id_matricula.Value = dado.Id;
            comando.Parameters.Add(id_matricula);

            conexao.Open();
            comando.CommandText = @"DELETE FROM tb_matricula WHERE id_matricula = @id_matricula;";

            var linhas = comando.ExecuteNonQuery();
        }
    }
}
