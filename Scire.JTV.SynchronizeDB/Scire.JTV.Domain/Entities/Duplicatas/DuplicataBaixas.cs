using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("duplicata_baixas", Schema = "public")]
    public class DuplicataBaixas
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_DUPBX")]
        public long AutoIncrementoDuplicataBaixas { get; set; }

        [Column("DUPLICATA_DUPBX")]
        public long DuplicataDuplicataBaixas { get; set; }

        [Column("PAGAMENTO_DUPBX")]
        public DateTime PagamentoDuplicataBaixas { get; set; }

        [Column("DATABAIXAAJUSTADA_DUPBX")]
        public DateTime DataBaixaAjustadaDuplicataBaixas { get; set; }

        [Column("CREDITO_DUPBX")]
        public DateTime CreditoDuplicataBaixas { get; set; }

        [Column("VENCTOCOMISSAO_DUPBX")]
        public DateTime VencimentoComissaoDuplicataBaixas { get; set; }

        [Column("VALOR_DUPBX")]
        public decimal ValorDuplicataBaixas { get; set; }

        [Column("JUROS_DUPBX")]
        public decimal JurosDuplicataBaixas { get; set; }

        [Column("DESCONTOS_DUPBX")]
        public decimal DescontosDuplicataBaixas { get; set; }

        [Column("ORIGEM_DUPBX")]
        public int OrigemDuplicataBaixas { get; set; }

        [Column("DOCORIGEM_DUPBX")]
        public long DocumentoOrigemDuplicataBaixas { get; set; }

        [Column("AUTENTICACAOMECANICA_DUPBX")]
        public string AutenticacaoMecanicaDuplicataBaixas { get; set; }

        [Column("CONTABILIZADO_DUPBX")]
        public char ContabilizadoDuplicataBaixas { get; set; }

        [Column("CODIGOIMPORTACAOCTB_DUPBX")]
        public long CodigoImportacaoContabilDuplicataBaixas { get; set; }

        [Column("DATAHORAINCLUSAO_DUPBX")]
        public DateTime DataHoraInclusaoDuplicataBaixas { get; set; }

        [Column("DATAHORAALTERACAO_DUPBX")]
        public DateTime DataHoraAlteracaoDuplicataBaixas { get; set; }

        [Column("USUARIOINCLUSAO_DUPBX")]
        public string UsuarioInclusaoDuplicataBaixas { get; set; }

        [Column("USUARIOALTERACAO_DUPBX")]
        public string UsuarioAlteracaoDuplicataBaixas { get; set; }
    }

}
