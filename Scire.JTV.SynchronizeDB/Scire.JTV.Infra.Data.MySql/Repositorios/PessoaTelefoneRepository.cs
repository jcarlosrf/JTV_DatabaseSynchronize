using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;


namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaTelefoneRepository : AbstractRepository
    {
        public PessoaTelefoneRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }


        public int SavePessoas(List<PessoaTelefone> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (PessoaTelefone pessoa in Pessoas)
                        {
                            SavePessoaTelefone(pessoa, false);
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

        public int SavePessoaTelefone(PessoaTelefone pessoaTelefone, bool save)
        {

            var existingPessoaReferencia = _context.PessoasReferencias.FirstOrDefault(pj => pj.CodigoCliente == pessoaTelefone.CodigoCliente && pj.AutoInc == pessoaTelefone.AutoInc);

            if (existingPessoaReferencia == null)
            {
                _context.PessoasTelefones.Add(pessoaTelefone);
            }
            else
            {
                pessoaTelefone.Id = existingPessoaReferencia.Id;
                _context.Entry(existingPessoaReferencia).CurrentValues.SetValues(pessoaTelefone);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;

        }

    }
}
