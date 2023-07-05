using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;


namespace Scire.JTV.Infra.Data.MySql
{
    public class ChequeDevolvidoRepository : AbstractRepository
    {
        public ChequeDevolvidoRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SaveCheques(List<ChequeDevolvido> Cheques)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (ChequeDevolvido cheque in Cheques)
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

        public int SaveRecord(ChequeDevolvido cheque, bool save)
        {
            var existingRecord = _context.ChequesDevolvidos.FirstOrDefault(
                ch => ch.CodigoCliente == cheque.CodigoCliente && ch.AutoIncrementoChequeDevolvido == cheque.AutoIncrementoChequeDevolvido);

            if (existingRecord == null)
            {
                _context.ChequesDevolvidos.Add(cheque);
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
