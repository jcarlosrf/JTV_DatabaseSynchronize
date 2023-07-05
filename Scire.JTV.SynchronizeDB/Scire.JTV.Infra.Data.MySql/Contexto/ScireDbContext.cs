using MySql.Data.EntityFramework;
using Scire.JTV.Domain.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace Scire.JTV.Infra.Data.MySql
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ScireDbContext : DbContext
    {
        public ScireDbContext(): base()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public ScireDbContext(DbConnection existingConnection, bool contextOwnsConnection) 
            : base(existingConnection, contextOwnsConnection)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         
        }

        public DbSet<EmpresaImportacao> EmpresasImportacao { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaCliente> PessoasClientes { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<PessoaReferencia> PessoasReferencias { get; set; }
        public DbSet<PessoaTelefone> PessoasTelefones { get; set; }
        public DbSet<Empresa> Empresas{ get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<ChequeDevolvido> ChequesDevolvidos { get; set; }
        public DbSet<ChequeBaixas> ChequesBaixas { get; set; }
        public DbSet<Duplicata> Duplicatas { get; set; }
        public DbSet<DuplicataBaixas> DuplicatasBaixas { get; set; }

    }
}
