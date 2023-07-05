using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql.Repositorios
{
    public class PessoaReferenciaRepository : AbstractRepository
    {
        public PessoaReferenciaRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }


        public int SavePessoas(List<PessoaReferencia> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (PessoaReferencia pessoa in Pessoas)
                        {
                            SavePessoaReferencia(pessoa, false);
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

        public int SavePessoaReferencia(PessoaReferencia pessoaReferencia, bool save)
        {

            var existingPessoaReferencia = _context.PessoasReferencias.FirstOrDefault(pj => pj.CodigoCliente == pessoaReferencia.CodigoCliente && pj.AutoInc == pessoaReferencia.AutoInc);

            if (existingPessoaReferencia == null)
            {
                _context.PessoasReferencias.Add(pessoaReferencia);
            }
            else
            {
                pessoaReferencia.Id = existingPessoaReferencia.Id;
                _context.Entry(existingPessoaReferencia).CurrentValues.SetValues(pessoaReferencia);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;

        }

    }
}
