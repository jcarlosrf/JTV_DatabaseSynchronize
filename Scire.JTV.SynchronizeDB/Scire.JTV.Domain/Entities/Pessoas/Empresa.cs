using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("empresa", Schema = "public")]
    public class Empresa
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("CODIGO_EMP")]
        public int CodigoEmpresa { get; set; }

        [Column("NOME_EMP")]
        public string NomeEmpresa { get; set; }

        [Column("PESSOA_EMP")]
        public int PessoaEmpresa { get; set; }

        [Column("DATA_EMP")]
        public DateTime DataEmpresa { get; set; }
    }

}
