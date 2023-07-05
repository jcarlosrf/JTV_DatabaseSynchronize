using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaRepository : AbstractRepository
    {   
        public PessoaRepository(string  connecitonString)
        {
            CreateConnection(connecitonString);            
        }
        
        public int SavePessoas(List<Pessoa> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();
               
                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (Pessoa pessoa in Pessoas)
                        {
                            SavePessoa(pessoa, false);
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

        public Pessoa GetEntity(int codigocliente, int codigopessoa)
        {
            return _context.Pessoas.FirstOrDefault(c => c.CodigoCliente.Equals(codigocliente) && c.CodigoPessoa.Equals(codigopessoa));
        }

        public int SavePessoa(Pessoa pessoa, bool save)
        {
            var existingPessoa = GetEntity(pessoa.CodigoCliente, pessoa.CodigoPessoa);

            if (existingPessoa == null)
            {
                _context.Pessoas.Add(pessoa);
            }
            else
            {
                existingPessoa.NomeFantasiaPessoa = pessoa.NomeFantasiaPessoa;
                existingPessoa.TipoPessoa = pessoa.TipoPessoa;
                existingPessoa.DocumentoPessoa = pessoa.DocumentoPessoa;
                existingPessoa.IdentificacaoExPessoa = pessoa.IdentificacaoExPessoa;
                existingPessoa.CeiPessoa = pessoa.CeiPessoa;
                existingPessoa.EmailPadraoPessoa = pessoa.EmailPadraoPessoa;
                existingPessoa.StatusPessoa = pessoa.StatusPessoa;
                existingPessoa.GrupoPessoa = pessoa.GrupoPessoa;
                existingPessoa.EnderecoPessoa = pessoa.EnderecoPessoa;
                existingPessoa.EnderecoEntregaPessoa = pessoa.EnderecoEntregaPessoa;
                existingPessoa.EnderecoCobrancaPessoa = pessoa.EnderecoCobrancaPessoa;
                existingPessoa.ImpeEnderecoEntregaNfPessoa = pessoa.ImpeEnderecoEntregaNfPessoa;
                existingPessoa.ResponsavelPessoa = pessoa.ResponsavelPessoa;
                existingPessoa.ClientePessoa = pessoa.ClientePessoa;
                existingPessoa.FornecedorPessoa = pessoa.FornecedorPessoa;
                existingPessoa.ConsVendasPessoa = pessoa.ConsVendasPessoa;
                existingPessoa.SupervisorVendasPessoa = pessoa.SupervisorVendasPessoa;
                existingPessoa.TransportadorPessoa = pessoa.TransportadorPessoa;
                existingPessoa.FuncionarioPessoa = pessoa.FuncionarioPessoa;
                existingPessoa.TomadorPessoa = pessoa.TomadorPessoa;
                existingPessoa.OutrosPessoa = pessoa.OutrosPessoa;
                existingPessoa.EmpresaPessoa = pessoa.EmpresaPessoa;
                existingPessoa.PrepostoPessoa = pessoa.PrepostoPessoa;
                existingPessoa.ContadorPessoa = pessoa.ContadorPessoa;
                existingPessoa.SeguradoraPessoa = pessoa.SeguradoraPessoa;
                existingPessoa.AgenCiadorPessoa = pessoa.AgenCiadorPessoa;
                existingPessoa.ComercialExportadorPessoa = pessoa.ComercialExportadorPessoa;
                existingPessoa.OperadoraPlanoSaudePessoa = pessoa.OperadoraPlanoSaudePessoa;
                existingPessoa.OutroComissionadoPessoa = pessoa.OutroComissionadoPessoa;
                existingPessoa.GrupoResultadoPessoa = pessoa.GrupoResultadoPessoa;
                existingPessoa.ConsumidorFinalPessoa = pessoa.ConsumidorFinalPessoa;
                existingPessoa.SuframaPessoa = pessoa.SuframaPessoa;
                existingPessoa.SenhaSitePessoa = pessoa.SenhaSitePessoa;
                existingPessoa.ChaveSitePessoa = pessoa.ChaveSitePessoa;
                existingPessoa.ProfissaoSegmentoPessoa = pessoa.ProfissaoSegmentoPessoa;
                existingPessoa.InformaOrgaoProtCredPessoa = pessoa.InformaOrgaoProtCredPessoa;
                existingPessoa.ClassificacaoPessoa = pessoa.ClassificacaoPessoa;
                existingPessoa.OptanteSimplesPessoa = pessoa.OptanteSimplesPessoa;
                existingPessoa.PlanoCntbDebRedPessoa = pessoa.PlanoCntbDebRedPessoa;
                existingPessoa.PlanoCntbDebAnalitPessoa = pessoa.PlanoCntbDebAnalitPessoa;
                existingPessoa.PlanoCntbCredRedPessoa = pessoa.PlanoCntbCredRedPessoa;
                existingPessoa.PlanoCntbCredAnalitPessoa = pessoa.PlanoCntbCredAnalitPessoa;
                existingPessoa.BancoContaPessoa = pessoa.BancoContaPessoa;
                existingPessoa.AgenciaContaPessoa = pessoa.AgenciaContaPessoa;
                existingPessoa.DigitoAgenciaContaPessoa = pessoa.DigitoAgenciaContaPessoa;
                existingPessoa.TipoContaPessoa = pessoa.TipoContaPessoa;
                existingPessoa.SubtipoContaPessoa = pessoa.SubtipoContaPessoa;
                existingPessoa.NumeroContaPessoa = pessoa.NumeroContaPessoa;
                existingPessoa.DigitoContaPessoa = pessoa.DigitoContaPessoa;
                existingPessoa.FavorecidoContaPessoa = pessoa.FavorecidoContaPessoa;
                existingPessoa.DigitoAgenciaEContaPessoa = pessoa.DigitoAgenciaEContaPessoa;
                existingPessoa.TipoChavePixPessoa = pessoa.TipoChavePixPessoa;
                existingPessoa.ChavePixPessoa = pessoa.ChavePixPessoa;
                existingPessoa.RegiaoPessoa = pessoa.RegiaoPessoa;
                existingPessoa.RecebeCobrancaEmailPessoa = pessoa.RecebeCobrancaEmailPessoa;
                existingPessoa.ParticipantePessoa = pessoa.ParticipantePessoa;
                existingPessoa.RegraLancamentoPessoa = pessoa.RegraLancamentoPessoa;
                existingPessoa.RegraLancamentoNFSaidaPessoa = pessoa.RegraLancamentoNFSaidaPessoa;
                existingPessoa.TransacaoPadraoPessoa = pessoa.TransacaoPadraoPessoa;
                existingPessoa.AliquotaIcmsPessoa = pessoa.AliquotaIcmsPessoa;
                existingPessoa.SenhaPessoa = pessoa.SenhaPessoa;
                existingPessoa.CandidatoPessoa = pessoa.CandidatoPessoa;
                existingPessoa.ProdutorRuralPessoa = pessoa.ProdutorRuralPessoa;
                existingPessoa.DataAtualizacaoDadosPessoa = pessoa.DataAtualizacaoDadosPessoa;
                existingPessoa.DataAnonimizacaoPessoa = pessoa.DataAnonimizacaoPessoa;
                existingPessoa.DataAutAnonimizacaoPessoa = pessoa.DataAutAnonimizacaoPessoa;
                existingPessoa.UsuarioAnonimizacaoPessoa = pessoa.UsuarioAnonimizacaoPessoa;
                existingPessoa.DataHoraInclusaoPessoa = pessoa.DataHoraInclusaoPessoa;
                existingPessoa.DataHoraAlteracaoPessoa = pessoa.DataHoraAlteracaoPessoa;
                existingPessoa.UsuarioInclusaoPessoa = pessoa.UsuarioInclusaoPessoa;
                existingPessoa.UsuarioAlteracaoPessoa = pessoa.UsuarioAlteracaoPessoa;
                existingPessoa.ProdutorRuralCpfpPessoa = pessoa.ProdutorRuralCpfpPessoa;
                existingPessoa.IndNatRetPessoa = pessoa.IndNatRetPessoa;
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;
        }
    }

}