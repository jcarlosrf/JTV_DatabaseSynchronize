using System;
using System.Linq;
using System.Threading.Tasks;
using Scire.JTV.Infra.Data.Firebird;

namespace Scire.JTV.Domain.Services
{
    public class DuplicatasService
    {
        private string FireString;
        private string MyString;

        public DateTime DhAtualizar { get; set; }

        public DuplicatasService(string fireString, string mysqlString)
        {
            FireString = fireString;
            MyString = mysqlString;
            DhAtualizar = new DateTime();
        }

        public DateTime GetDataMinima()
        {
            var pessoaFire = new Infra.Data.Firebird.DuplicatasRepository(FireString);
            return pessoaFire.GetDataMinima();
        }

        public async Task<int> ImportarDuplicatas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.DuplicatasRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.DuplicatasRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetDuplicatas(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                retorno = pessoaMy.SaveDuplicatas(dupFire);

            if (retorno > 0)
            {
                var dhmax = dupFire.Max(p => p.DataHoraAlteracaoDuplicata);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = dupFire.Max(p => p.DataHoraInclusaoDuplicata);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }

        public async Task<int> ImportarDuplicatasBaixas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;
            var pessoaFire = new Infra.Data.Firebird.DuplicatasRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.DuplicataBaixaRepository(MyString);

            var dupFire = await Task.Run(() => pessoaFire.GetDuplicatasBaixas(dhAlteracao, dhAgora, codigoCliente));

            if (dupFire.Count > 0)
                retorno = pessoaMy.SaveDuplicatasBaixas(dupFire);
            
            if (retorno > 0)
            {
                var dhmax = dupFire.Max(p => p.DataHoraAlteracaoDuplicataBaixas);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = dupFire.Max(p => p.DataHoraInclusaoDuplicataBaixas);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }
    }
}
