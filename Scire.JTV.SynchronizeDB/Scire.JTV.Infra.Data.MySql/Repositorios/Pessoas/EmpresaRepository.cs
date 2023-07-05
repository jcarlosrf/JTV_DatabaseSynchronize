using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class EmpresaRepository : AbstractRepository
    {
        public EmpresaRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SaveEmpresas(List<Empresa> Empresas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (Empresa empresa in Empresas)
                        {
                            SaveEmpresa(empresa, false);
                        }

                        retorno = _context.SaveChanges();
                    }
                }
                catch
                {
                    throw;
                }
            }

            return retorno;
        }

        private int SaveEmpresa(Empresa empresa, bool save)
        {

            var existingPessoaReferencia = _context.Empresas.FirstOrDefault(pj => pj.CodigoCliente == empresa.CodigoCliente && pj.CodigoEmpresa == empresa.CodigoEmpresa);

            if (existingPessoaReferencia == null)
            {
                _context.Empresas.Add(empresa);
            }
            else
            {
                empresa.Id = existingPessoaReferencia.Id;
                _context.Entry(existingPessoaReferencia).CurrentValues.SetValues(empresa);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;

        }
    }
}
