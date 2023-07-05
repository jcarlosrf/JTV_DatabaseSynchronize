using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.MySql
{
    public class PessoaClienteRepository : AbstractRepository
    {
        public PessoaClienteRepository(string connecitonString)
        {
            CreateConnection(connecitonString);
        }

        public int SavePessoas(List<PessoaCliente> Pessoas)
        {
            int retorno = 0;

            using (MySqlConnection connection = new MySqlConnection(MyConnection))
            {
                connection.Open();

                try
                {
                    using (_context = new ScireDbContext(connection, false))
                    {
                        foreach (PessoaCliente pessoa in Pessoas)
                        {
                            SavePessoaCliente(pessoa, false);
                        }

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

        public int SavePessoaCliente(PessoaCliente pessoaCliente, bool save)
        {
            var existingPessoaCliente = GetEntity(pessoaCliente.CodigoCliente, pessoaCliente.PessoaClienteId);

            if (existingPessoaCliente == null)
            {
                _context.PessoasClientes.Add(pessoaCliente);
            }
            else
            {
                existingPessoaCliente.OpcaoFrete = pessoaCliente.OpcaoFrete;
                existingPessoaCliente.FreteAtual = pessoaCliente.FreteAtual;
                existingPessoaCliente.FreteIdeal = pessoaCliente.FreteIdeal;
                existingPessoaCliente.TipoFrete = pessoaCliente.TipoFrete;
                existingPessoaCliente.TipoFreteCT = pessoaCliente.TipoFreteCT;
                existingPessoaCliente.CondicaoPagamento = pessoaCliente.CondicaoPagamento;
                existingPessoaCliente.LimiteCredito = pessoaCliente.LimiteCredito;
                existingPessoaCliente.PrazoMedioMaximo = pessoaCliente.PrazoMedioMaximo;
                existingPessoaCliente.PrimeiraCompra = pessoaCliente.PrimeiraCompra;
                existingPessoaCliente.FamiliaPedido = pessoaCliente.FamiliaPedido;
                existingPessoaCliente.FamiliaAssistencia = pessoaCliente.FamiliaAssistencia;
                existingPessoaCliente.BuscaMercadoria = pessoaCliente.BuscaMercadoria;
                existingPessoaCliente.Situacao = pessoaCliente.Situacao;
                existingPessoaCliente.SituacaoAssistencia = pessoaCliente.SituacaoAssistencia;
                existingPessoaCliente.Banco = pessoaCliente.Banco;
                existingPessoaCliente.Financeira = pessoaCliente.Financeira;
                existingPessoaCliente.Redespacho = pessoaCliente.Redespacho;
                existingPessoaCliente.EmpresaPadrao = pessoaCliente.EmpresaPadrao;
                existingPessoaCliente.RedespachoPago = pessoaCliente.RedespachoPago;
                existingPessoaCliente.CofinsSuframa = pessoaCliente.CofinsSuframa;
                existingPessoaCliente.PisSuframa = pessoaCliente.PisSuframa;
                existingPessoaCliente.IcmsSuframa = pessoaCliente.IcmsSuframa;
                existingPessoaCliente.DiasParaIpi = pessoaCliente.DiasParaIpi;
                existingPessoaCliente.DiasParaDesp = pessoaCliente.DiasParaDesp;
                existingPessoaCliente.DiasParaSt = pessoaCliente.DiasParaSt;
                existingPessoaCliente.AceitaEntregaParcial = pessoaCliente.AceitaEntregaParcial;
                existingPessoaCliente.RateiaDespesas = pessoaCliente.RateiaDespesas;
                existingPessoaCliente.RateiaIpi = pessoaCliente.RateiaIpi;
                existingPessoaCliente.RateiaSt = pessoaCliente.RateiaSt;
                existingPessoaCliente.FormatoDuplicataIpi = pessoaCliente.FormatoDuplicataIpi;
                existingPessoaCliente.FormatoDuplicataDesp = pessoaCliente.FormatoDuplicataDesp;
                existingPessoaCliente.FormatoDuplicataSt = pessoaCliente.FormatoDuplicataSt;
                existingPessoaCliente.CondicaoPagamentoCte = pessoaCliente.CondicaoPagamentoCte;
                existingPessoaCliente.BancoCte = pessoaCliente.BancoCte;
                existingPessoaCliente.SituacaoCte = pessoaCliente.SituacaoCte;
                existingPessoaCliente.ConsultorPadrao = pessoaCliente.ConsultorPadrao;
                existingPessoaCliente.DataHoraInclusao = pessoaCliente.DataHoraInclusao;
                existingPessoaCliente.DataHoraAlteracao = pessoaCliente.DataHoraAlteracao;
                existingPessoaCliente.UsuarioInclusao = pessoaCliente.UsuarioInclusao;
                existingPessoaCliente.UsuarioAlteracao = pessoaCliente.UsuarioAlteracao;
            }

            if (save)
                return _context.SaveChanges();
            else
                return 1;
        }

        public PessoaCliente GetEntity(int codigocliente, int codigopessoa)
        {
            return _context.PessoasClientes.FirstOrDefault(c => c.CodigoCliente.Equals(codigocliente) && c.PessoaClienteId.Equals(codigopessoa));
        }

    }
}
