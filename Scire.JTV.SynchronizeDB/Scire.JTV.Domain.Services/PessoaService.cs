using System;
using System.Threading.Tasks;
using Scire.JTV.Infra.Data.Firebird;

namespace Scire.JTV.Domain.Services
{
    public class PessoaService
    {
        private string FireString;
        private string MyString;

        public PessoaService(string fireString , string mysqlString )
        {
            FireString = fireString;
            MyString = mysqlString;
        }

        public async Task<int> ImportarPessoas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoas(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                return pessoaMy.SavePessoas(pessoasFire);

            return 0;
        }

        public async Task<int> ImportarPessoasClientes(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaClienteRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoasClientes(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                return pessoaMy.SavePessoas(pessoasFire);

            return 0;
        }

        public async Task<int> ImportarPessoasFisicas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaFisicaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoasFisicas(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                return pessoaMy.SavePessoas(pessoasFire);

            return 0;
        }



        public bool TestarFire()
        {
            var fireContext = new FirebirdContext(FireString);

            return fireContext.TestConnection();
        }

        public bool TestarMy()
        {
            var pessoaMy = new Infra.Data.MySql.PessoaRepository(MyString);

            return pessoaMy.TestaConexao();
        }

    }
}
