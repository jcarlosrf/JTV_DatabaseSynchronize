using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;


namespace Scire.JTV.Infra.Data.MySql
{
    public class ChequesRepository : AbstractRepository
    {
        public ChequesRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SaveCheques(List<Cheque> Cheques)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (Cheque cheque in Cheques)
                        {
                            SaveRecord(cheque, false);
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

        public int SaveRecord(Cheque cheque, bool save)
        {
            var existingRecord = _context.Cheques.FirstOrDefault(
                ch => ch.CodigoCliente == cheque.CodigoCliente && ch.AutoIncrementoCheque == cheque.AutoIncrementoCheque);

            if (existingRecord == null)
            {
                _context.Cheques.Add(cheque);
            }
            else
            {
                cheque.Id = existingRecord.Id;
                _context.Entry(existingRecord).CurrentValues.SetValues(cheque);
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;

        }
    }
}
