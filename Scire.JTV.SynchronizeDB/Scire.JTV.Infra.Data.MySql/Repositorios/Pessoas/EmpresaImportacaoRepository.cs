using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class EmpresaImportacaoRepository : AbstractRepository
    {
        public enum Servico
        {
            Pessoa, Cheques, Duplicatas
        }
        private static readonly object empresaLock = new object();

        public EmpresaImportacaoRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public EmpresaImportacao GetEntity(int codigoEmpresa)
        {
            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                using (_context = new ScireDbContext(connection, false))
                {   
                    return GetEntity(codigoEmpresa, _context);                    
                }
            }
        }

        private EmpresaImportacao GetEntity(int codigoEmpresa, ScireDbContext contexto)
        {           
            var empresa = contexto.EmpresasImportacao.FirstOrDefault(e => e.CodigoCliente.Equals(codigoEmpresa));
            return empresa;                       
        }

        public int UpdateDataHora(int codigoEmpresa, DateTime DhAtualizacao, Servico servico)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                using (_context = new ScireDbContext(connection, false))
                {
                    lock (empresaLock)
                    {
                        var empresa = _context.EmpresasImportacao.FirstOrDefault(e => e.CodigoCliente.Equals(codigoEmpresa));

                        if (empresa == null)
                            return 0;

                        if (servico == Servico.Pessoa)
                            empresa.DataHoraPessoas = DhAtualizacao;
                        else if (servico == Servico.Cheques)
                            empresa.DataHoraCheques = DhAtualizacao;
                        else if (servico == Servico.Duplicatas)
                            empresa.DataHoraDuplicatas = DhAtualizacao;

                        retorno = _context.SaveChanges();
                    }
                }
            }

            return retorno;
        }
    }
}
