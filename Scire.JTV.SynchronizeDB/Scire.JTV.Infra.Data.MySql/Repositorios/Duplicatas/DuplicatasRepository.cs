using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;


namespace Scire.JTV.Infra.Data.MySql
{
    public class DuplicatasRepository : AbstractRepository
    {
        public DuplicatasRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SaveDuplicatas(List<Duplicata> Duplicatas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (Duplicata duplicata in Duplicatas)
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

        public int SaveRecord(Duplicata duplicata, bool save)
        {
            var existingRecord = _context.Duplicatas.FirstOrDefault(
                ch => ch.CodigoCliente == duplicata.CodigoCliente && ch.AutoIncrementoDuplicata == duplicata.AutoIncrementoDuplicata);

            if (existingRecord == null)
            {
                _context.Duplicatas.Add(duplicata);
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
