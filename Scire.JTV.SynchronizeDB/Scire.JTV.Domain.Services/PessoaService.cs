using System;
using System.Threading.Tasks;
using Scire.JTV.Infra.Data.Firebird;
using System.Linq;

namespace Scire.JTV.Domain.Services
{
    public class PessoaService
    {
        private string FireString;
        private string MyString;

        public DateTime DhAtualizar { get; set; }

        public PessoaService(string fireString , string mysqlString )
        {
            FireString = fireString;
            MyString = mysqlString;

            DhAtualizar = new DateTime();
        }

        public async Task<int> ImportarPessoas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoas(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                retorno = pessoaMy.SavePessoas(pessoasFire);

            if (retorno > 0)
            {
                var dhmax = pessoasFire.Max(p => p.DataHoraAlteracaoPessoa);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = pessoasFire.Max(p => p.DataHoraInclusaoPessoa);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }

        public async Task<int> ImportarPessoasClientes(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaClienteRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoasClientes(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                retorno = pessoaMy.SavePessoas(pessoasFire);

            if (retorno > 0)
            {
                var dhmax = pessoasFire.Max(p => p.DataHoraAlteracao);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = pessoasFire.Max(p => p.DataHoraInclusao);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;


        }

        public async Task<int> ImportarPessoasFisicas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaFisicaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoasFisicas(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                retorno = pessoaMy.SavePessoas(pessoasFire);

            if (retorno > 0)
            {
                var dhmax = pessoasFire.Max(p => p.DataHoraAlteracao);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = pessoasFire.Max(p => p.DataHoraInclusao);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;

        }

        public async Task<int> ImportarPessoasJuricas(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;
            
            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaJuricaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoasJuridicas(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                retorno = pessoaMy.SavePessoas(pessoasFire);

            if (retorno > 0)
            {
                var dhmax = pessoasFire.Max(p => p.DataHoraAlteracao);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = pessoasFire.Max(p => p.DataHoraInclusao);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }
        
        public async Task<int> ImportarPessoasReferencias(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaReferenciaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoaReferencia(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                retorno = pessoaMy.SavePessoas(pessoasFire);

            if (retorno > 0)
            {
                var dhmax = pessoasFire.Max(p => p.DataHoraAlteracao);
                if (dhmax < new DateTime(2000, 1, 1))
                    dhmax = pessoasFire.Max(p => p.DataHoraInclusao);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }
        
        public async Task<int> ImportarPessoasTelefones(DateTime dhAlteracao, DateTime dhAgora, int codigoCliente)
        {
            int retorno = 0;

            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.PessoaTelefoneRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetPessoaTelefone(dhAlteracao, dhAgora, codigoCliente));

            if (pessoasFire.Count > 0)
                retorno = pessoaMy.SavePessoas(pessoasFire);

            if (retorno > 0)
            {
                var dhmax = pessoasFire.Max(p => p.DataHoraAlteracao);
                if (dhmax < new DateTime(2000,1,1))
                    dhmax = pessoasFire.Max(p => p.DataHoraInclusao);

                if (DhAtualizar == new DateTime() || dhmax < DhAtualizar)
                    DhAtualizar = dhmax;
            }

            return retorno;
        }

        public async Task<int> ImportarEmpresas(int codigoCliente)
        {
            var pessoaFire = new Infra.Data.Firebird.EmpresaRepository(FireString);
            var pessoaMy = new Infra.Data.MySql.EmpresaRepository(MyString);

            var pessoasFire = await Task.Run(() => pessoaFire.GetEmpresas(codigoCliente));

            if (pessoasFire.Count > 0)
                return pessoaMy.SaveEmpresas(pessoasFire);

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

        public DateTime GetDataMinima()
        {
            var pessoaFire = new Infra.Data.Firebird.PessoaRepository(FireString);
            return pessoaFire.GetDataMinima();
        }

    }
}
