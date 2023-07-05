using System;
using System.Threading.Tasks;
using Scire.JTV.Infra.Data.Firebird;

namespace Scire.JTV.Domain.Services
{
    public class DuplicatasService
    {
        private string FireString;
        private string MyString;

        public DuplicatasService(string fireString, string mysqlString)
        {
            FireString = fireString;
            MyString = mysqlString;
        }

        public async Task<int> ImportarDuplicatas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.DuplicatasRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.DuplicatasRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetDuplicatas(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                return pessoaMy.SaveDuplicatas(dupFire);

            return 0;
        }

        public async Task<int> ImportarDuplicatasBaixas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.DuplicatasRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.DuplicataBaixaRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetDuplicatasBaixas(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                return pessoaMy.SaveDuplicatasBaixas(dupFire);

            return 0;
        }
    }
}
