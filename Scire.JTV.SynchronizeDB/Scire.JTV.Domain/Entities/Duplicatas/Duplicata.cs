using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("duplicata", Schema = "public")]
    public class Duplicata
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_DUP")]
        public long AutoIncrementoDuplicata { get; set; }

        [Column("EMPRESA_DUP")]
        public int EmpresaDuplicata { get; set; }

        [Column("TIPO_DUP")]
        public int TipoDuplicata { get; set; }

        [Column("TIPODOC_DUP")]
        public int TipoDocumentoDuplicata { get; set; }

        [Column("PESSOA_DUP")]
        public int PessoaDuplicata { get; set; }

        [Column("DOCUMENTO_DUP")]
        public string DocumentoDuplicata { get; set; }

        [Column("BANCO_DUP")]
        public int BancoDuplicata { get; set; }

        [Column("ORIGEM_DUP")]
        public int OrigemDuplicata { get; set; }

        [Column("SUBORIGEM_DUP")]
        public int SubOrigemDuplicata { get; set; }

        [Column("DOCORIGEM_DUP")]
        public long DocumentoOrigemDuplicata { get; set; }

        [Column("EMISSAO_DUP")]
        public DateTime EmissaoDuplicata { get; set; }

        [Column("VENCIMENTOORIGINAL_DUP")]
        public DateTime VencimentoOriginalDuplicata { get; set; }

        [Column("VENCIMENTO_DUP")]
        public DateTime VencimentoDuplicata { get; set; }

        [Column("DTDESCONTO_DUP")]
        public DateTime DataDescontoDuplicata { get; set; }

        [Column("PROMESSAPGTO_DUP")]
        public DateTime PromessaPagamentoDuplicata { get; set; }

        [Column("PAGAMENTO_DUP")]
        public DateTime PagamentoDuplicata { get; set; }

        [Column("VALORNOMINALORIGINAL_DUP")]
        public decimal ValorNominalOriginalDuplicata { get; set; }

        [Column("VALOR_DUP")]
        public decimal ValorDuplicata { get; set; }

        [Column("PREDESCONTO_DUP")]
        public decimal PrecoDescontoDuplicata { get; set; }

        [Column("VALORPAGO_DUP")]
        public decimal ValorPagoDuplicata { get; set; }

        [Column("VALORABERTO_DUP")]
        public decimal ValorAbertoDuplicata { get; set; }

        [Column("QUALIFICACAO_DUP")]
        public int QualificacaoDuplicata { get; set; }

        [Column("DATABAIXA_DUP")]
        public DateTime DataBaixaDuplicata { get; set; }

        [Column("CARGA_DUP")]
        public long CargaDuplicata { get; set; }

        [Column("SITUACAO_DUP")]
        public int SituacaoDuplicata { get; set; }

        [Column("BOLETO_DUP")]
        public long BoletoDuplicata { get; set; }

        [Column("DIGITOBOLETO_DUP")]
        public string DigitoBoletoDuplicata { get; set; }

        [Column("SUBSTITUIDA_DUP")]
        public char SubstituidaDuplicata { get; set; }

        [Column("DESCONSIDERADA_DUP")]
        public char DesconsideradaDuplicata { get; set; }

        [Column("CONTA_DUP")]
        public int ContaDuplicata { get; set; }

        [Column("CODIGOBARRAS_DUP")]
        public string CodigoBarrasDuplicata { get; set; }

        [Column("LINHADIGITAVEL_DUP")]
        public string LinhaDigitavelDuplicata { get; set; }

        [Column("URL_QRCODE_PIX_DUP")]
        public string UrlQRCodePixDuplicata { get; set; }

        [Column("EMPRESAANT_DUP")]
        public int EmpresaAnteriorDuplicata { get; set; }

        [Column("AUTOINCANT_DUP")]
        public long AutoIncrementoAnteriorDuplicata { get; set; }

        [Column("NUMERADOR_DUP")]
        public int NumeradorDuplicata { get; set; }

        [Column("DENOMINADOR_DUP")]
        public int DenominadorDuplicata { get; set; }

        [Column("PROJETO_DUP")]
        public int ProjetoDuplicata { get; set; }

        [Column("CONTABILIZADAPREVISAO_DUP")]
        public char ContabilizadaPrevisaoDuplicata { get; set; }

        [Column("DATAHORAINCLUSAO_DUP")]
        public DateTime DataHoraInclusaoDuplicata { get; set; }

        [Column("DATAHORAALTERACAO_DUP")]
        public DateTime DataHoraAlteracaoDuplicata { get; set; }

        [Column("DATAHORAAUDITORIA_DUP")]
        public DateTime DataHoraAuditoriaDuplicata { get; set; }

        [Column("INTEGRACAOCTB_DUP")]
        public int IntegracaoContabilDuplicata { get; set; }

        [Column("CONTA_CTB_DUP")]
        public long ContaContabilDuplicata { get; set; }

        [Column("FINANCEIRA_DUP")]
        public int FinanceiraDuplicata { get; set; }

        [Column("USUARIOINCLUSAO_DUP")]
        public string UsuarioInclusaoDuplicata { get; set; }

        [Column("USUARIOALTERACAO_DUP")]
        public string UsuarioAlteracaoDuplicata { get; set; }

        [Column("USUARIOAUDITORIA_DUP")]
        public string UsuarioAuditoriaDuplicata { get; set; }

        [Column("MOTIVONAOAUTORIZACAO_DUP")]
        public string MotivoNaoAutorizacaoDuplicata { get; set; }

        [Column("SITUACAOAUDITORIA_DUP")]
        public int SituacaoAuditoriaDuplicata { get; set; }
    }

}
