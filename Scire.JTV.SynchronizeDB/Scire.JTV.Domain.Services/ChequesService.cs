using System;
using System.Linq;
using System.Threading.Tasks;
using Scire.JTV.Infra.Data.Firebird;

namespace Scire.JTV.Domain.Services
{
    public class ChequesService
    {
        private string FireString;
        private string MyString;

        public DateTime DhAtualizar { get; set; }

        public ChequesService(string fireString, string mysqlString)
        {
            FireString = fireString;
            MyString = mysqlString;
            DhAtualizar = new DateTime();
        }

        public DateTime GetDataMinima()
        {
            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            return pessoaFire.GetDataMinima();
        }

        public async Task<int> ImportarCheques(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.ChequesRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetCheques(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                retorno = pessoaMy.SaveCheques(dupFire);

            if (retorno > 0)
            {
                var dhmax = dupFire.Max(p => p.DataHoraAlteracaoCheque);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = dupFire.Max(p => p.DataHoraInclusaoCheque);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }

        public async Task<int> ImportarChequesBaixas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.ChequeBaixasRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetChequesBaixas(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                retorno = pessoaMy.SaveCheques(dupFire);

            if (retorno > 0)
            {
                var dhmax = dupFire.Max(p => p.DataHoraInclusaoChequeBaixas);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = new DateTime(2000, 1, 1);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }

        public async Task<int> ImportarChequesDevolvidos(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.ChequesRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.ChequeDevolvidoRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetChequesDevolvidos(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                retorno = pessoaMy.SaveCheques(dupFire);

            if (retorno > 0)
            {
                var dhmax = dupFire.Max(p => p.DataHoraAlteracaoChequeDevolvido);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = dupFire.Max(p => p.DataHoraInclusaoChequeDevolvido);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }
    }
}
