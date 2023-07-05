using System;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("pessoa_referencia", Schema = "public")]
    public class PessoaReferencia
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_PESSOA_REF")]
        public int AutoInc { get; set; }

        [Column("PESSOA_PESSOA_REF")]
        public int PessoaRef { get; set; }

        [Column("REFERENCIA_PESSOA_REF")]
        public string Referencia { get; set; }

        [Column("TIPOREFERENCIA_PESSOA_REF")]
        public int TipoReferencia { get; set; }

        [Column("TELEFONE_PESSOA_REF")]
        public string Telefone { get; set; }

        [Column("OBSERVACAO_PESSOA_REF")]
        public string Observacao { get; set; }

        [Column("USUARIOINCLUSAO_PESSOA_REF")]
        public string UsuarioInclusao { get; set; }

        [Column("USUARIOALTERACAO_PESSOA_REF")]
        public string UsuarioAlteracao { get; set; }

        [Column("DATAHORAINCLUSAO_PESSOA_REF")]
        public DateTime DataHoraInclusao { get; set; }

        [Column("DATAHORAALTERACAO_PESSOA_REF")]
        public DateTime DataHoraAlteracao { get; set; }
    }

}
