using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scire.JTV.Domain.Entities
{
    [Serializable]
    [Table("pessoa_fisica", Schema = "public")]
    public class PessoaFisica
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; } = 0;

        [Column("CODIGO_CLIENTE")]
        public int CodigoCliente { get; set; }

        [Column("PESSOA_PESSOA_FIS")]
        public int Pessoa { get; set; }

        [Column("CPF_PESSOA_FIS")]
        public string CPF { get; set; }

        [Column("MATRICULAINSS_PESSOA_PFIS")]
        public string MatriculaINSS { get; set; }

        [Column("IDENTIDADE_PESSOA_FIS")]
        public string Identidade { get; set; }

        [Column("INSCRICAO_PESSOA_FIS")]
        public string Inscricao { get; set; }

        [Column("DTEMISSAOIDENT_PESSOA_FIS")]
        public DateTime? DataEmissaoIdentidade { get; set; }

        [Column("EMISSORIDENT_PESSOA_FIS")]
        public string EmissorIdentidade { get; set; }

        [Column("ESTADOCIVIL_PESSOA_FIS")]
        public string EstadoCivil { get; set; }

        [Column("CODIGOCONJUGE_PESSOA_FIS")]
        public int CodigoConjuge { get; set; }

        [Column("NOMECONJUGE_PESSOA_FIS")]
        public string NomeConjuge { get; set; }

        [Column("SEXO_PESSOA_FIS")]
        public string Sexo { get; set; }

        [Column("DTNASCIMENTO_PESSOA_FIS")]
        public DateTime? DataNascimento { get; set; }

        [Column("CIDADENASCIMENTO_PESSOA_FIS")]
        public int CidadeNascimento { get; set; }

        [Column("NOMEPAI_PESSOA_FIS")]
        public string NomePai { get; set; }

        [Column("NOMEMAE_PESSOA_FIS")]
        public string NomeMae { get; set; }

        [Column("LOCALTRABALHO_PESSOA_FIS")]
        public string LocalTrabalho { get; set; }

        [Column("TELEFONETRABALHO_PESSOA_FIS")]
        public string TelefoneTrabalho { get; set; }

        [Column("PROFISSAO_PESSOA_FIS")]
        public string Profissao { get; set; }

        [Column("DTADMISSAO_PESSOA_FIS")]
        public DateTime? DataAdmissao { get; set; }

        [Column("VLRRENDA_PESSOA_FISICA")]
        public decimal ValorRenda { get; set; }

        [Column("CARTEIRATRABALHO_PESSOA_FIS")]
        public string CarteiraTrabalho { get; set; }

        [Column("UF_EMISSORIDENT_PESSOA_FIS")]
        public string UFEmissorIdentidade { get; set; }

        [Column("NUMERORIC_PESSOA_FIS")]
        public string NumeroRIC { get; set; }

        [Column("EMISSORRIC_PESSOA_FIS")]
        public string EmissorRIC { get; set; }

        [Column("DTEMISSAORIC_PESSOA_FIS")]
        public DateTime? DataEmissaoRIC { get; set; }

        [Column("NOME_SOCIAL_PESSOA_FIS")]
        public string NomeSocial { get; set; }

        [Column("PAISNACIONALIDADE_PESSOA_FIS")]
        public int PaisNacionalidade { get; set; }

        [Column("DATAHORAINCLUSAO_PESSOA_FIS")]
        public DateTime DataHoraInclusao { get; set; }

        [Column("DATAHORAALTERACAO_PESSOA_FIS")]
        public DateTime DataHoraAlteracao { get; set; }

        [Column("USUARIOINCLUSAO_PESSOA_FIS")]
        public string UsuarioInclusao { get; set; }

        [Column("USUARIOALTERACAO_PESSOA_FIS")]
        public string UsuarioAlteracao { get; set; }
    }

}
