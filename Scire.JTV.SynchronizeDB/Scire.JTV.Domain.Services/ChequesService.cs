using System;
using System.Threading.Tasks;
using Scire.JTV.Infra.Data.Firebird;

namespace Scire.JTV.Domain.Services
{
    public class ChequesService
    {
        private string FireString;
        private string MyString;

        public ChequesService(string fireString, string mysqlString)
        {
            FireString = fireString;
            MyString = mysqlString;
        }

        public async Task<int> ImportarCheques(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.ChequesRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetCheques(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                return pessoaMy.SaveCheques(dupFire);

            return 0;
        }

        public async Task<int> ImportarChequesBaixas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.ChequeBaixasRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetChequesBaixas(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                return pessoaMy.SaveCheques(dupFire);

            return 0;
        }

        public async Task<int> ImportarChequesDevolvidos(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.ChequeDevolvidoRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetChequesDevolvidos(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                return pessoaMy.SaveCheques(dupFire);

            return 0;
        }
    }
}
