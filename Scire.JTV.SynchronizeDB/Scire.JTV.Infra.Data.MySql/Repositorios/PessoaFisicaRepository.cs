using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaFisicaRepository : AbstractRepository
    {
        public PessoaFisicaRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SavePessoas(List<PessoaFisica> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (PessoaFisica pessoa in Pessoas)
                        {
                            SavePessoaFisica(pessoa, false);
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

        public int SavePessoaFisica(PessoaFisica pessoaFisica, bool save)
        {
            var existingPessoaFisica = _context.PessoasFisicas.FirstOrDefault(pf => pf.CodigoCliente == pessoaFisica.CodigoCliente && pf.Pessoa == pessoaFisica.Pessoa);

            if (existingPessoaFisica == null)
            {
                _context.PessoasFisicas.Add(pessoaFisica);
            }
            else
            {
                pessoaFisica.Id = existingPessoaFisica.Id;
                _context.Entry(existingPessoaFisica).CurrentValues.SetValues(pessoaFisica);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;
        }
    }
}
