using System;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("pessoa_telefone", Schema = "public")]
    public class PessoaTelefone
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("AUTOINC_PESSOA_TEL")]
        public int AutoInc { get; set; }

        [Column("ENDERECO_PESSOA_TEL")]
        public int Endereco { get; set; }

        [Column("PESSOA_PESSOA_TEL")]
        public int Pessoa { get; set; }

        [Column("TELEFONE_PESSOA_TEL")]
        public string Telefone { get; set; }

        [Column("OBSERVACAO_PESSOA_TEL")]
        public string Observacao { get; set; }

        [Column("TELEFONEPADRAO_PESSOA_TEL")]
        public char TelefonePadrao { get; set; }

        [Column("TIPOTELEFONE_PESSOA_TEL")]
        public string TipoTelefone { get; set; }

        [Column("DATAHORAINCLUSAO_PESSOA_TEL")]
        public DateTime DataHoraInclusao { get; set; }

        [Column("DATAHORAALTERACAO_PESSOA_TEL")]
        public DateTime DataHoraAlteracao { get; set; }

        [Column("USUARIOINCLUSAO_PESSOA_TEL")]
        public string UsuarioInclusao { get; set; }

        [Column("USUARIOALTERACAO_PESSOA_TEL")]
        public string UsuarioAlteracao { get; set; }
    }

}
