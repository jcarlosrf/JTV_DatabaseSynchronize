using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("empresa_importacao", Schema = "public")]
    public class EmpresaImportacao
    {
        [Key]
        [Column("codigo_cliente")]
        public int CodigoCliente { get; set; }

        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Column("dhpessoas")]
        public DateTime? DataHoraPessoas { get; set; }

        [Column("dhcheques")]
        public DateTime? DataHoraCheques { get; set; }

        [Column("dhduplicatas")]
        public DateTime? DataHoraDuplicatas { get; set; }

        [Column("codigo_emp")]
        public int? CodigoEmpresa { get; set; }

        [Column("codigo_pessoa")]
        public int? CodigoPessoa { get; set; }
    }

}
