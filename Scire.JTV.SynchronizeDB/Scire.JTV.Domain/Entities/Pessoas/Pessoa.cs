using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("pessoa", Schema = "public")]
    public class Pessoa
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; } = 0;

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("CODIGO_PESSOA")]
        public int CodigoPessoa { get; set; }

        [Column("RAZAOSOCIAL_PESSOA")]
        public string RazaoSocialPessoa { get; set; }

        [Column("NOMEFANTASIA_PESSOA")]
        public string NomeFantasiaPessoa { get; set; }

        [Column("TIPO_PESSOA")]
        public string TipoPessoa { get; set; }

        [Column("DOCUMENTO_PESSOA")]
        public string DocumentoPessoa { get; set; }

        [Column("IDENTIFICACAOEX_PESSOA")]
        public string IdentificacaoExPessoa { get; set; }

        [Column("CEI_PESSOA")]
        public string CeiPessoa { get; set; }       

        [Column("EMAILPADRAO_PESSOA")]
        public int EmailPadraoPessoa { get; set; }

        [Column("STATUS_PESSOA")]
        public int StatusPessoa { get; set; }

        [Column("GRUPO_PESSOA")]
        public int GrupoPessoa { get; set; }

        [Column("ENDERECO_PESSOA")]
        public int EnderecoPessoa { get; set; }

        [Column("ENDERECOENTREGA_PESSOA")]
        public int EnderecoEntregaPessoa { get; set; }

        [Column("ENDERECOCOBRANCA_PESSOA")]
        public int EnderecoCobrancaPessoa { get; set; }

        [Column("IMPENDERECOENTREGANF_PESSOA")]
        public string ImpeEnderecoEntregaNfPessoa { get; set; }

        [Column("RESPONSAVEL_PESSOA")]
        public string ResponsavelPessoa { get; set; }

        [Column("CLIENTE_PESSOA")]
        public string ClientePessoa { get; set; }

        [Column("FORNECEDOR_PESSOA")]
        public string FornecedorPessoa { get; set; }

        [Column("CONSVENDAS_PESSOA")]
        public string ConsVendasPessoa { get; set; }

        [Column("SUPERVISORVENDAS_PESSOA")]
        public string SupervisorVendasPessoa { get; set; }

        [Column("TRANSPORTADOR_PESSOA")]
        public string TransportadorPessoa { get; set; }

        [Column("FUNCIONARIO_PESSOA")]
        public string FuncionarioPessoa { get; set; }

        [Column("TOMADOR_PESSOA")]
        public string TomadorPessoa { get; set; }

        [Column("OUTROS_PESSOA")]
        public string OutrosPessoa { get; set; }

        [Column("EMPRESA_PESSOA")]
        public string EmpresaPessoa { get; set; }

        [Column("PREPOSTO_PESSOA")]
        public string PrepostoPessoa { get; set; }

        [Column("CONTADOR_PESSOA")]
        public string ContadorPessoa { get; set; }

        [Column("SEGURADORA_PESSOA")]
        public string SeguradoraPessoa { get; set; }

        [Column("AGENCIADOR_PESSOA")]
        public string AgenCiadorPessoa { get; set; }

        [Column("COMERCIAL_EXPORTADOR_PESSOA")]
        public string ComercialExportadorPessoa { get; set; }

        [Column("OPERADORA_PLANO_SAUDE_PESSOA")]
        public string OperadoraPlanoSaudePessoa { get; set; }

        [Column("OUTROCOMISSIONADO_PESSOA")]
        public string OutroComissionadoPessoa { get; set; }

        [Column("GRUPORESULTADO_PESSOA")]
        public int GrupoResultadoPessoa { get; set; }

        [Column("CONSUMIDORFINAL_PESSOA")]
        public string ConsumidorFinalPessoa { get; set; }

        [Column("SUFRAMA_PESSOA")]
        public string SuframaPessoa { get; set; }

        [Column("SENHASITE_PESSOA")]
        public string SenhaSitePessoa { get; set; }

        [Column("CHAVESITE_PESSOA")]
        public string ChaveSitePessoa { get; set; }

        [Column("PROFISSAOSEGMENTO_PESSOA")]
        public int ProfissaoSegmentoPessoa { get; set; }

        [Column("INFORMAORGAOPROTCRED_PESSOA")]
        public string InformaOrgaoProtCredPessoa { get; set; }

        [Column("CLASSIFICACAO_PESSOA")]
        public int ClassificacaoPessoa { get; set; }

        [Column("OPTANTE_SIMPLES_PESSOA")]
        public string OptanteSimplesPessoa { get; set; }

        [Column("PLANOCNTBDEB_RED_PESSOA")]
        public string PlanoCntbDebRedPessoa { get; set; }

        [Column("PLANOCNTBDEB_ANALIT_PESSOA")]
        public string PlanoCntbDebAnalitPessoa { get; set; }

        [Column("PLANOCNTBCRED_RED_PESSOA")]
        public string PlanoCntbCredRedPessoa { get; set; }

        [Column("PLANOCNTBCRED_ANALIT_PESSOA")]
        public string PlanoCntbCredAnalitPessoa { get; set; }

        [Column("BANCOCONTA_PESSOA")]
        public int BancoContaPessoa { get; set; }

        [Column("AGENCIACONTA_PESSOA")]
        public string AgenciaContaPessoa { get; set; }

        [Column("DIGITOAGENCIACONTA_PESSOA")]
        public string DigitoAgenciaContaPessoa { get; set; }

        [Column("TIPOCONTA_PESSOA")]
        public int TipoContaPessoa { get; set; }

        [Column("SUBTIPOCONTA_PESSOA")]
        public int SubtipoContaPessoa { get; set; }

        [Column("NUMEROCONTA_PESSOA")]
        public string NumeroContaPessoa { get; set; }

        [Column("DIGITOCONTA_PESSOA")]
        public string DigitoContaPessoa { get; set; }

        [Column("FAVORECIDOCONTA_PESSOA")]
        public string FavorecidoContaPessoa { get; set; }

        [Column("DIGITOAGENCIAECONTA_PESSOA")]
        public string DigitoAgenciaEContaPessoa { get; set; }

        [Column("TIPOCHAVEPIX_PESSOA")]
        public int TipoChavePixPessoa { get; set; }

        [Column("CHAVEPIX_PESSOA")]
        public string ChavePixPessoa { get; set; }

        [Column("REGIAO_PESSOA")]
        public int RegiaoPessoa { get; set; }

        [Column("RECEBECOBRANCAEMAIL_PESSOA")]
        public string RecebeCobrancaEmailPessoa { get; set; }

        [Column("PARTICIPANTE_PESSOA")]
        public int ParticipantePessoa { get; set; }

        [Column("REGRALANCAMENTO_PESSOA")]
        public long RegraLancamentoPessoa { get; set; }

        [Column("REGRALANC_NFSAIDA_PESSOA")]
        public long RegraLancamentoNFSaidaPessoa { get; set; }

        [Column("TRANSACAO_PADRAO_PESSOA")]
        public int TransacaoPadraoPessoa { get; set; }

        [Column("ALIQUOTA_ICMS_PESSOA")]
        public decimal AliquotaIcmsPessoa { get; set; }

        [Column("SENHA_PESSOA")]
        public string SenhaPessoa { get; set; }

        [Column("CANDIDATO_PESSOA")]
        public string CandidatoPessoa { get; set; }

        [Column("PRODUTORRURAL_PESSOA")]
        public string ProdutorRuralPessoa { get; set; }

        [Column("DATA_ATUALIZACAO_DADOS_PESSOA")]
        public DateTime? DataAtualizacaoDadosPessoa { get; set; }

        [Column("DATA_ANONIMIZACAO_PESSOA")]
        public DateTime? DataAnonimizacaoPessoa { get; set; }

        [Column("DATA_AUT_ANONIMIZACAO_PESSOA")]
        public DateTime? DataAutAnonimizacaoPessoa { get; set; }

        [Column("USUARIO_ANONIMIZACAO_PESSOA")]
        public string UsuarioAnonimizacaoPessoa { get; set; }

        [Column("DATAHORAINCLUSAO_PESSOA")]
        public DateTime DataHoraInclusaoPessoa { get; set; }

        [Column("DATAHORAALTERACAO_PESSOA")]
        public DateTime DataHoraAlteracaoPessoa { get; set; }

        [Column("USUARIOINCLUSAO_PESSOA")]
        public string UsuarioInclusaoPessoa { get; set; }

        [Column("USUARIOALTERACAO_PESSOA")]
        public string UsuarioAlteracaoPessoa { get; set; }

        [Column("PRODUTORRURAL_CPFP_PESSOA")]
        public string ProdutorRuralCpfpPessoa { get; set; }

        [Column("IND_NAT_RET_PESSOA")]
        public string IndNatRetPessoa { get; set; }
    }

}
