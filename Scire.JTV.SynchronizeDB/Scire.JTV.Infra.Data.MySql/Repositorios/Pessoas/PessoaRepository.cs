using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaRepository : AbstractRepository
    {
        public PessoaRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SavePessoas(List<Pessoa> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (Pessoa pessoa in Pessoas)
                        {
                            SavePessoa(pessoa, false);
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

        public Pessoa GetEntity(int codigocliente, int codigopessoa)
        {
            return _context.Pessoas.FirstOrDefault(c => c.CodigoCliente.Equals(codigocliente) && c.CodigoPessoa.Equals(codigopessoa));
        }

        public int SavePessoa(Pessoa pessoa, bool save)
        {
            var existingPessoa = GetEntity(pessoa.CodigoCliente, pessoa.CodigoPessoa);

            if (existingPessoa == null)
            {
                _context.Pessoas.Add(pessoa);
            }
            else
            {
                pessoa.Id = existingPessoa.Id;
                _context.Entry(existingPessoa).CurrentValues.SetValues(pessoa);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;
        }        
    }
}