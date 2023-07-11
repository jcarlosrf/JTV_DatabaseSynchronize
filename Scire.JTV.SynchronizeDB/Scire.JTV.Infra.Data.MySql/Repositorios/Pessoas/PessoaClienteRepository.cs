using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaClienteRepository : AbstractRepository
    {
        public PessoaClienteRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SavePessoas(List<PessoaCliente> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (PessoaCliente pessoa in Pessoas)
                        {
                            SavePessoaCliente(pessoa, false);
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

        public int SavePessoaCliente(PessoaCliente pessoaCliente, bool save)
        {
            var existingPessoaCliente = GetEntity(pessoaCliente.CodigoCliente, pessoaCliente.PessoaClienteId);

            if (existingPessoaCliente == null)
            {
                _context.PessoasClientes.Add(pessoaCliente);
            }
            else
            {
                pessoaCliente.Id = existingPessoaCliente.Id;
                _context.Entry(existingPessoaCliente).CurrentValues.SetValues(pessoaCliente);

            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;
        }

        public PessoaCliente GetEntity(int codigocliente, int codigopessoa)
        {
            return _context.PessoasClientes.FirstOrDefault(c => c.CodigoCliente.Equals(codigocliente) && c.PessoaClienteId.Equals(codigopessoa));
        }

    }
}
