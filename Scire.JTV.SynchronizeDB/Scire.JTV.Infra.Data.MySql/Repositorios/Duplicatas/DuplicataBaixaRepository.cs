using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;


namespace Scire.JTV.Infra.Data.MySql
{
    public class DuplicataBaixaRepository : AbstractRepository
    {
        public DuplicataBaixaRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SaveDuplicatasBaixas(List<DuplicataBaixas> Duplicatas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (DuplicataBaixas duplicata in Duplicatas)
                        {
                            SaveRecord(duplicata, false);
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

        public int SaveRecord(DuplicataBaixas duplicata, bool save)
        {
            var existingRecord = _context.DuplicatasBaixas.FirstOrDefault(
                ch => ch.CodigoCliente == duplicata.CodigoCliente && ch.AutoIncrementoDuplicataBaixas == duplicata.AutoIncrementoDuplicataBaixas);

            if (existingRecord == null)
            {
                _context.DuplicatasBaixas.Add(duplicata);
            }
            else
            {
                duplicata.Id = existingRecord.Id;
                _context.Entry(existingRecord).CurrentValues.SetValues(duplicata);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;

        }
    }
}
