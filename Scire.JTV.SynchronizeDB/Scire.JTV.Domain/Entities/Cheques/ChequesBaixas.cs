using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("cheque_baixas", Schema = "public")]
    public class ChequeBaixas
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_CHEBX")]
        public long AutoIncrementoChequeBaixas { get; set; }

        [Column("CHEQUE_CHEBX")]
        public long ChequeChequeBaixas { get; set; }

        [Column("BAIXA_CHEBX")]
        public DateTime BaixaChequeBaixas { get; set; }

        [Column("CREDITO_CHEBX")]
        public DateTime CreditoChequeBaixas { get; set; }

        [Column("VALOR_CHEBX")]
        public decimal ValorChequeBaixas { get; set; }

        [Column("JUROS_CHEBX")]
        public decimal JurosChequeBaixas { get; set; }

        [Column("DESCONTOS_CHEBX")]
        public decimal DescontosChequeBaixas { get; set; }

        [Column("ORIGEM_CHEBX")]
        public int OrigemChequeBaixas { get; set; }

        [Column("DOCORIGEM_CHEBX")]
        public long DocumentoOrigemChequeBaixas { get; set; }

        [Column("CONTA_CHEBX")]
        public int ContaChequeBaixas { get; set; }

        [Column("TIPOBAIXA_CHEBX")]
        public int TipoBaixaChequeBaixas { get; set; }

        [Column("TERCEIRO_CHEBX")]
        public string TerceiroChequeBaixas { get; set; }

        [Column("SEQCONCILIACAOARQ_CHEBX")]
        public int SequenciaConciliacaoArquivoChequeBaixas { get; set; }

        [Column("DATAHORAINCLUSAO_CHEBX")]
        public DateTime DataHoraInclusaoChequeBaixas { get; set; }

        [Column("USUARIOINCLUSAO_CHEBX")]
        public string UsuarioInclusaoChequeBaixas { get; set; }

        [Column("CONTABILIZADO_CHEBX")]
        public char ContabilizadoChequeBaixas { get; set; }

        [Column("CODIGOIMPORTACAOCTB_CHEBX")]
        public long CodigoImportacaoContabilizacaoChequeBaixas { get; set; }
    }

}
