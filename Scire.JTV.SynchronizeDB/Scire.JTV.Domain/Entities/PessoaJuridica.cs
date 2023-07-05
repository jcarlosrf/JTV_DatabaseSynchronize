using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("pessoa_jurica", Schema = "public")]
    public class PessoaJuridica
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("PESSOA_PESSOA_JUR")]
        public int PessoaJuridicaId { get; set; }

        [Column("TIPO_PESSOA_JUR")]
        public string Tipo { get; set; }

        [Column("CNPJCEI_PESSOA_JUR")]
        public string CnpjCeI { get; set; }

        [Column("INSCRICAO_PESSOA_JUR")]
        public string Inscricao { get; set; }

        [Column("NOMECOMPRADOR_PESSOA_JUR")]
        public string NomeComprador { get; set; }

        [Column("DTNASCCOMPRADOR_PESSOA_JUR")]
        public DateTime DtnascComprador { get; set; }

        [Column("NOMEPROPRIET_PESSOA_JUR")]
        public string NomeProprietario { get; set; }

        [Column("DTNASCPROPRIET_PESSOA_JUR")]
        public DateTime DtnascProprietario { get; set; }

        [Column("NOMECONTATO_PESSOA_JUR")]
        public string NomeContato { get; set; }

        [Column("INSCRICAOMUNICIPAL_PESSOA_JUR")]
        public string InscricaoMunicipal { get; set; }

        [Column("CNAE_PADRAO_PESSOA_JUR")]
        public int CnaePadrao { get; set; }

        [Column("REGISTRO_ANS_JUR")]
        public string RegistroAns { get; set; }

        [Column("DATAHORAINCLUSAO_PESSOA_JUR")]
        public DateTime DataHoraInclusao { get; set; }

        [Column("DATAHORAALTERACAO_PESSOA_JUR")]
        public DateTime DataHoraAlteracao { get; set; }

        [Column("USUARIOINCLUSAO_PESSOA_JUR")]
        public string UsuarioInclusao { get; set; }

        [Column("USUARIOALTERACAO_PESSOA_JUR")]
        public string UsuarioAlteracao { get; set; }

        [Column("INDCPRB_PESSOA_JUR")]
        public int IndcPrb { get; set; }
    }

}
