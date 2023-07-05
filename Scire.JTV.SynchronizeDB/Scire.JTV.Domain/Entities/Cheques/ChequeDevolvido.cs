using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("cheque_devolvido", Schema = "public")]
    public class ChequeDevolvido
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_CHEDEV")]
        public long AutoIncrementoChequeDevolvido { get; set; }

        [Column("CHEQUE_CHEDEV")]
        public long ChequeChequeDevolvido { get; set; }

        [Column("DTDEVOLUCAO_CHEDEV")]
        public DateTime DataDevolucaoChequeDevolvido { get; set; }

        [Column("DTRECOLHIMENTO_CHEDEV")]
        public DateTime DataRecolhimentoChequeDevolvido { get; set; }

        [Column("ALINEA_CHEDEV")]
        public int AlineaChequeDevolvido { get; set; }

        [Column("CONCILIACAO_CHEDEV")]
        public long ConciliacaoChequeDevolvido { get; set; }

        [Column("BAIXAANTERIOR_CHEDEV")]
        public DateTime BaixaAnteriorChequeDevolvido { get; set; }

        [Column("CREDITOANTERIOR_CHEDEV")]
        public DateTime CreditoAnteriorChequeDevolvido { get; set; }

        [Column("TIPOBAIXAANTERIOR_CHEDEV")]
        public int TipoBaixaAnteriorChequeDevolvido { get; set; }

        [Column("TERCEIROANTERIOR_CHEDEV")]
        public string TerceiroAnteriorChequeDevolvido { get; set; }

        [Column("DOCDESTINOANTERIOR_CHEDEV")]
        public long DocumentoDestinoAnteriorChequeDevolvido { get; set; }

        [Column("DESTINOANTERIOR_CHEDEV")]
        public int DestinoAnteriorChequeDevolvido { get; set; }

        [Column("CONTABAIXAANTERIOR_CHEDEV")]
        public int ContaBaixaAnteriorChequeDevolvido { get; set; }

        [Column("DTDESCONTOANTERIOR_CHEDEV")]
        public DateTime DataDescontoAnteriorChequeDevolvido { get; set; }

        [Column("SEQCONCILIACAOARQ_CHEDEV")]
        public int SequenciaConciliacaoArquivoChequeDevolvido { get; set; }

        [Column("DATAHORAINCLUSAO_CHEDEV")]
        public DateTime DataHoraInclusaoChequeDevolvido { get; set; }

        [Column("DATAHORAALTERACAO_CHEDEV")]
        public DateTime DataHoraAlteracaoChequeDevolvido { get; set; }

        [Column("USUARIOINCLUSAO_CHEDEV")]
        public string UsuarioInclusaoChequeDevolvido { get; set; }

        [Column("USUARIOALTERACAO_CHEDEV")]
        public string UsuarioAlteracaoChequeDevolvido { get; set; }
    }

}
