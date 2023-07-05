using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.Firebird
{
    public class EmpresaRepository
    {
        private readonly string connectionString;

        public EmpresaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Empresa> GetEmpresas(int codigoCliente)
        {
            List<Empresa> empresas = new List<Empresa>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT CODIGO_EMP,    NOME_EMP,    PESSOA_EMP,    DATA_EMP " +
                    "FROM empresa";

                using (FbCommand command = new FbCommand(query, connection))
                {                    
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empresa empresa = ReadEmpresasFromDataReader(reader, codigoCliente);
                            empresas.Add(empresa);
                        }
                    }
                }
            }

            return empresas;
        }

        private Empresa ReadEmpresasFromDataReader(IDataReader reader, int codigoCliente)
        {
            Empresa empresa = new Empresa();

            empresa.Id = 0;
            empresa.CodigoCliente = codigoCliente;
            empresa.CodigoEmpresa= Convert.ToInt32(reader["CODIGO_EMP"]);
            empresa.PessoaEmpresa = Convert.ToInt32(reader["PESSOA_EMP"]);
            empresa.NomeEmpresa = reader["NOME_EMP"].ToString();
            empresa.DataEmpresa= !reader.IsDBNull(reader.GetOrdinal("DATA_EMP")) ? Convert.ToDateTime(reader["DATA_EMP"]) : new DateTime(1900, 1, 1);

            return empresa;
        }

    }
}
