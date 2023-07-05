using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaJuricaRepository : AbstractRepository
    {
        public PessoaJuricaRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SavePessoas(List<PessoaJuridica> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (PessoaJuridica pessoa in Pessoas)
                        {
                            SavePessoaJuridica(pessoa, false);
                        }

                        retorno = _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return retorno;
        }


        public int SavePessoaJuridica(PessoaJuridica pessoaJuridica, bool save)
        {

            var existingPessoaJuridica = _context.PessoasJuridicas.FirstOrDefault(pj => pj.CodigoCliente == pessoaJuridica.CodigoCliente && pj.PessoaJuridicaId == pessoaJuridica.PessoaJuridicaId);

            if (existingPessoaJuridica == null)
            {
                _context.PessoasJuridicas.Add(pessoaJuridica);
            }
            else
            {
                pessoaJuridica.Id = existingPessoaJuridica.Id;
                _context.Entry(existingPessoaJuridica).CurrentValues.SetValues(pessoaJuridica);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;

        }

    }
}
