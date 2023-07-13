using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class EmpresaImportacaoRepository : AbstractRepository
    {
        public enum Servico
        {
            Pessoa, Cheques, Duplicatas
        }
        private static readonly object empresaLock = new object();

        public EmpresaImportacaoRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public EmpresaImportacao GetEntity(int codigoEmpresa)
        {
            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                using (_context = new ScireDbContext(connection, false))
                {   
                    return GetEntity(codigoEmpresa, _context);                    
                }
            }
        }

        private EmpresaImportacao GetEntity(int codigoEmpresa, ScireDbContext contexto)
        {           
            var empresa = contexto.EmpresasImportacao.FirstOrDefault(e => e.CodigoCliente.Equals(codigoEmpresa));
            return empresa;                       
        }

        public int UpdateDataHora(int codigoEmpresa, DateTime DhAtualizacao, Servico servico)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                using (_context = new ScireDbContext(connection, false))
                {
                    lock (empresaLock)
                    {
                        var empresa = _context.EmpresasImportacao.FirstOrDefault(e => e.CodigoCliente.Equals(codigoEmpresa));

                        if (empresa == null)
                            return 0;

                        if (servico == Servico.Pessoa)
                            empresa.DataHoraPessoas = DhAtualizacao;
                        else if (servico == Servico.Cheques)
                            empresa.DataHoraCheques = DhAtualizacao;
                        else if (servico == Servico.Duplicatas)
                            empresa.DataHoraDuplicatas = DhAtualizacao;

                        retorno = _context.SaveChanges();
                    }
                }
            }

            return retorno;
        }

        public int UpdateDataHoraExecucao(int codigoEmpresa, DateTime DhAtualizacao)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                using (_context = new ScireDbContext(connection, false))
                {
                    lock (empresaLock)
                    {
                        var empresa = _context.EmpresasImportacao.FirstOrDefault(e => e.CodigoCliente.Equals(codigoEmpresa));

                        if (empresa == null)
                            return 0;

                        empresa.DataHoraImportacao = DhAtualizacao;

                        retorno = _context.SaveChanges();
                    }
                }
            }

            return retorno;
        }

        public int DeleteALL(int CodigoCliente)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        _context.Pessoas.RemoveRange(_context.Pessoas.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.PessoasClientes.RemoveRange(_context.PessoasClientes.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.PessoasFisicas.RemoveRange(_context.PessoasFisicas.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.PessoasJuridicas.RemoveRange(_context.PessoasJuridicas.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.PessoasReferencias.RemoveRange(_context.PessoasReferencias.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.PessoasTelefones.RemoveRange(_context.PessoasTelefones.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.Cheques.RemoveRange(_context.Cheques.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.ChequesBaixas.RemoveRange(_context.ChequesBaixas.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.ChequesDevolvidos.RemoveRange(_context.ChequesDevolvidos.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.Duplicatas.RemoveRange(_context.Duplicatas.Where(p => p.CodigoCliente == CodigoCliente));
                        _context.DuplicatasBaixas.RemoveRange(_context.DuplicatasBaixas.Where(p => p.CodigoCliente == CodigoCliente));
                        
                        // Salva as alterações no banco de dados
                        retorno = _context.SaveChanges();
                    }
                }
                catch
                {
                    throw;
                }
            }

            return retorno;
        }
    }
}
