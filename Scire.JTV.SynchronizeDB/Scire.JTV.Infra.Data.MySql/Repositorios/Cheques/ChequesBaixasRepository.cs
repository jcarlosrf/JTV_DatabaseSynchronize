using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;


namespace Scire.JTV.Infra.Data.MySql
{
    public class ChequeBaixasRepository : AbstractRepository
    {
        public ChequeBaixasRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SaveCheques(List<ChequeBaixas> Cheques)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (ChequeBaixas cheque in Cheques)
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

        public int SaveRecord(ChequeBaixas cheque, bool save)
        {
            var existingRecord = _context.ChequesBaixas.FirstOrDefault(
                ch => ch.CodigoCliente == cheque.CodigoCliente && ch.AutoIncrementoChequeBaixas == cheque.AutoIncrementoChequeBaixas);

            if (existingRecord == null)
            {
                _context.ChequesBaixas.Add(cheque);
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
