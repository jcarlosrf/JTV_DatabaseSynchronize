using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("cheque", Schema = "public")]
    public class Cheque
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_CHE")]
        public long AutoIncrementoCheque { get; set; }

        [Column("EMPRESA_CHE")]
        public int EmpresaCheque { get; set; }

        [Column("TIPO_CHE")]
        public int TipoCheque { get; set; }

        [Column("NUMERO_CHE")]
        public string NumeroCheque { get; set; }

        [Column("PESSOA_CHE")]
        public int PessoaCheque { get; set; }

        [Column("DTEMISSAO_CHE")]
        public DateTime DataEmissaoCheque { get; set; }

        [Column("DTVENCIMENTOORIGINAL_CHE")]
        public DateTime DataVencimentoOriginalCheque { get; set; }

        [Column("DTVENCIMENTO_CHE")]
        public DateTime DataVencimentoCheque { get; set; }

        [Column("DTBAIXA_CHE")]
        public DateTime DataBaixaCheque { get; set; }

        [Column("VALOR_CHE")]
        public decimal ValorCheque { get; set; }

        [Column("VALORPAGO_CHE")]
        public decimal ValorPagoCheque { get; set; }

        [Column("VALORABERTO_CHE")]
        public decimal ValorAbertoCheque { get; set; }

        [Column("SACADO_CHE")]
        public string SacadoCheque { get; set; }

        [Column("CPFCNPJSACADO_CHE")]
        public string CPFCNPJSacadoCheque { get; set; }

        [Column("PROPRIO_CHE")]
        public char ProprioCheque { get; set; }

        [Column("PORTADOR_CHE")]
        public string PortadorCheque { get; set; }

        [Column("SITUACAO_CHE")]
        public int SituacaoCheque { get; set; }

        [Column("QUALIFICACAO_CHE")]
        public int QualificacaoCheque { get; set; }

        [Column("STATUS_CHE")]
        public int StatusCheque { get; set; }

        [Column("SUBSTITUIDO_CHE")]
        public char SubstituidoCheque { get; set; }

        [Column("APRESENTAVEL_CHE")]
        public char ApresentavelCheque { get; set; }

        [Column("DEVOLVIDO_CHE")]
        public char DevolvidoCheque { get; set; }

        [Column("CANCELADO_CHE")]
        public char CanceladoCheque { get; set; }

        [Column("SUSTADO_CHE")]
        public char SustadoCheque { get; set; }

        [Column("DATAHORAINCLUSAO_CHE")]
        public DateTime DataHoraInclusaoCheque { get; set; }

        [Column("DATAHORAALTERACAO_CHE")]
        public DateTime DataHoraAlteracaoCheque { get; set; }

        [Column("USUARIOINCLUSAO_CHE")]
        public string UsuarioInclusaoCheque { get; set; }

        [Column("USUARIOALTERACAO_CHE")]
        public string UsuarioAlteracaoCheque { get; set; }
    }

}
