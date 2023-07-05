using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("pessoa_cliente", Schema = "public")]
    public class PessoaCliente
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("PESSOA_PESSOA_CLI")]
        public int PessoaClienteId { get; set; }

        [Column("OPCAOFRETE_PESSOA_CLI")]
        public short OpcaoFrete { get; set; }

        [Column("FRETEATUAL_PESSOA_CLI")]
        public decimal FreteAtual { get; set; }

        [Column("FRETEIDEAL_PESSOA_CLI")]
        public decimal FreteIdeal { get; set; }

        [Column("TIPOFRETE_PESSOA_CLI")]
        public short TipoFrete { get; set; }

        [Column("TIPOFRETECT_PESSOA_CLI")]
        public short TipoFreteCT { get; set; }

        [Column("CONDPAGTO_PESSOA_CLI")]
        public short CondicaoPagamento { get; set; }

        [Column("LIMITE_CRED_PESSOA_CLI")]
        public int LimiteCredito { get; set; }

        [Column("PRAZOMEDIOMAX_PESSOA_CLI")]
        public int PrazoMedioMaximo { get; set; }

        [Column("PRIMEIRACOMPRA_CLI")]
        public DateTime? PrimeiraCompra { get; set; }

        [Column("FAMILIAPEDIDO_PESSOA_CLI")]
        public int FamiliaPedido { get; set; }

        [Column("FAMILIAASSIST_PESSOA_CLI")]
        public int FamiliaAssistencia { get; set; }

        [Column("BUSCA_MERCADORIA_PESSOA_CLI")]
        public char BuscaMercadoria { get; set; }

        [Column("SITUACAO_PESSOA_CLI")]
        public int Situacao { get; set; }

        [Column("SITUACAOASSIST_PESSOA_CLI")]
        public int SituacaoAssistencia { get; set; }

        [Column("BANCO_PESSOA_CLI")]
        public int Banco { get; set; }

        [Column("FINANCEIRA_PESSOA_CLI")]
        public int Financeira { get; set; }

        [Column("REDESPACHO_PESSOA_CLI")]
        public int Redespacho { get; set; }

        [Column("EMPRESAPADRAO_PESSOA_CLI")]
        public int EmpresaPadrao { get; set; }

        [Column("REDESPACHOPAGO_PESSOA_CLI")]
        public char RedespachoPago { get; set; }

        [Column("COFINSSUFRAMA_PESSOA_CLI")]
        public char CofinsSuframa { get; set; }

        [Column("PISSUFRAMA_PESSOA_CLI")]
        public char PisSuframa { get; set; }

        [Column("ICMSSUFRAMA_PESSOA_CLI")]
        public char IcmsSuframa { get; set; }

        [Column("DIASPARAIPI_PESSOA_CLI")]
        public int DiasParaIpi { get; set; }

        [Column("DIASPARADESP_PESSOA_CLI")]
        public int DiasParaDesp { get; set; }

        [Column("DIASPARAST_PESSOA_CLI")]
        public int DiasParaSt { get; set; }

        [Column("ACEITAENTREGAPARCIAL_PESSOA_CLI")]
        public char AceitaEntregaParcial { get; set; }

        [Column("RATEIADESPESAS_PESSOA_CLI")]
        public char RateiaDespesas { get; set; }

        [Column("RATEIAIPI_PESSOA_CLI")]
        public char RateiaIpi { get; set; }

        [Column("RATEIAST_PESSOA_CLI")]
        public char RateiaSt { get; set; }

        [Column("FORMATO_DUP_IPI_PESSOA_CLI")]
        public short FormatoDuplicataIpi { get; set; }

        [Column("FORMATO_DUP_DESP_PESSOA_CLI")]
        public short FormatoDuplicataDesp { get; set; }

        [Column("FORMATO_DUP_ST_PESSOA_CLI")]
        public short FormatoDuplicataSt { get; set; }

        [Column("CONDICAO_PAG_CTE_PESSOA_CLI")]
        public int CondicaoPagamentoCte { get; set; }

        [Column("BANCO_CTE_PESSOA_CLI")]
        public int BancoCte { get; set; }

        [Column("SITUACAO_CTE_PESSOA_CLI")]
        public int SituacaoCte { get; set; }

        [Column("CONSULTORPADRAO_PESSOA_CLI")]
        public int ConsultorPadrao { get; set; }

        [Column("DATAHORAINCLUSAO_PESSOA_CLI")]
        public DateTime DataHoraInclusao { get; set; }

        [Column("DATAHORAALTERACAO_PESSOA_CLI")]
        public DateTime DataHoraAlteracao { get; set; }

        [Column("USUARIOINCLUSAO_PESSOA_CLI")]
        public string UsuarioInclusao { get; set; }

        [Column("USUARIOALTERACAO_PESSOA_CLI")]
        public string UsuarioAlteracao { get; set; }
    }

}
