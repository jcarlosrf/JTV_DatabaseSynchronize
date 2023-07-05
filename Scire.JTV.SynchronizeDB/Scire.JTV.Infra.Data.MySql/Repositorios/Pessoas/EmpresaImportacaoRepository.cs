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
            var empresa =  contexto.EmpresasImportacao.FirstOrDefault(e => e.CodigoCliente.Equals(codigoEmpresa));

            return empresa;
        }

        public int UpdateDataHora(int codigoEmpresa, DateTime DhAtualizacao)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                using (_context = new ScireDbContext(connection, false))
                {
                    var empresa = GetEntity(codigoEmpresa, _context);

                    if (empresa == null)
                        return 0;

                    empresa.DataHoraImportacao = DhAtualizacao;

                    retorno = _context.SaveChanges();
                }
            }

            return retorno;
        }
    }
}
