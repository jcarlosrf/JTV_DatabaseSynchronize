using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.Firebird
{
    public class ChequesRepository
    {
        private readonly string connectionString;

        public ChequesRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Cheque> GetCheques(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<Cheque> cheques = new List<Cheque>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM Cheque " +
                    "WHERE (DATAHORAALTERACAO_CHE >= @DataAtualizacao and DATAHORAALTERACAO_CHE < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_CHE  is Null) ";

                query += "order by DATAHORAALTERACAO_CHE";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cheque cheque = ReadChequeFromDataReader(reader, codigoCliente);
                            cheques.Add(cheque);
                        }
                    }
                }
            }

            return cheques;
        }

        public List<ChequeBaixas> GetChequesBaixas(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<ChequeBaixas> cheques = new List<ChequeBaixas>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM CHEQUE_BAIXAS " +
                    "WHERE (DATAHORAINCLUSAO_CHEBX >= @DataAtualizacao and DATAHORAINCLUSAO_CHEBX < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAINCLUSAO_CHEBX  is Null) ";

                query += "order by DATAHORAINCLUSAO_CHEBX";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChequeBaixas cheque = ReadChequeBaixasFromDataReader(reader, codigoCliente);
                            cheques.Add(cheque);
                        }
                    }
                }
            }

            return cheques;
        }

        public List<ChequeDevolvido> GetChequesDevolvidos(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<ChequeDevolvido> cheques = new List<ChequeDevolvido>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM CHEQUE_DEVOLVIDO " +
                    "WHERE (DATAHORAALTERACAO_CHEDEV >= @DataAtualizacao and DATAHORAALTERACAO_CHEDEV < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_CHEDEV  is Null) ";

                query += "order by DATAHORAALTERACAO_CHEDEV";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChequeDevolvido cheque = ReadChequeDevolvidoFromDataReader(reader, codigoCliente);
                            cheques.Add(cheque);
                        }
                    }
                }
            }

            return cheques;
        }

        private Cheque ReadChequeFromDataReader(IDataReader reader, int codigocliente)
        {
            Cheque cheque = new Cheque();

            cheque.Id = 0;
            cheque.CodigoCliente = codigocliente;
            cheque.AutoIncrementoCheque = Convert.ToInt64(reader["AUTOINC_CHE"]);
            cheque.EmpresaCheque = Convert.ToInt32(reader["EMPRESA_CHE"]);
            cheque.TipoCheque = Convert.ToInt32(reader["TIPO_CHE"]);
            cheque.NumeroCheque = reader["NUMERO_CHE"].ToString();
            cheque.PessoaCheque = Convert.ToInt32(reader["PESSOA_CHE"]);
            cheque.DataEmissaoCheque = !reader.IsDBNull(reader.GetOrdinal("DTEMISSAO_CHE")) ? Convert.ToDateTime(reader["DTEMISSAO_CHE"]) : new DateTime(1900, 1, 1);            
            cheque.DataVencimentoOriginalCheque = !reader.IsDBNull(reader.GetOrdinal("DTVENCIMENTOORIGINAL_CHE")) ? Convert.ToDateTime(reader["DTVENCIMENTOORIGINAL_CHE"]) : new DateTime(1900, 1, 1);
            cheque.DataVencimentoCheque = !reader.IsDBNull(reader.GetOrdinal("DTVENCIMENTO_CHE")) ? Convert.ToDateTime(reader["DTVENCIMENTO_CHE"]) : new DateTime(1900, 1, 1);
            cheque.DataBaixaCheque = !reader.IsDBNull(reader.GetOrdinal("DTBAIXA_CHE")) ? Convert.ToDateTime(reader["DTBAIXA_CHE"]) : new DateTime(1900, 1, 1);
            cheque.ValorCheque = Convert.ToDecimal(reader["VALOR_CHE"]);
            cheque.ValorPagoCheque = Convert.ToDecimal(reader["VALORPAGO_CHE"]);
            cheque.ValorAbertoCheque = Convert.ToDecimal(reader["VALORABERTO_CHE"]);
            cheque.SacadoCheque = reader["SACADO_CHE"].ToString();
            cheque.CPFCNPJSacadoCheque = reader["CPFCNPJSACADO_CHE"].ToString();
            cheque.ProprioCheque = Convert.ToChar(reader["PROPRIO_CHE"]);
            cheque.PortadorCheque = reader["PORTADOR_CHE"].ToString();
            cheque.SituacaoCheque = Convert.ToInt32(reader["SITUACAO_CHE"]);
            cheque.QualificacaoCheque = Convert.ToInt32(reader["QUALIFICACAO_CHE"]);
            cheque.StatusCheque = Convert.ToInt32(reader["STATUS_CHE"]);
            cheque.SubstituidoCheque = Convert.ToChar(reader["SUBSTITUIDO_CHE"]);
            cheque.ApresentavelCheque = Convert.ToChar(reader["APRESENTAVEL_CHE"]);
            cheque.DevolvidoCheque = Convert.ToChar(reader["DEVOLVIDO_CHE"]);
            cheque.CanceladoCheque = Convert.ToChar(reader["CANCELADO_CHE"]);
            cheque.SustadoCheque = Convert.ToChar(reader["SUSTADO_CHE"]);
            cheque.DataHoraInclusaoCheque = !reader.IsDBNull(reader.GetOrdinal("DATAHORAINCLUSAO_CHE")) ? Convert.ToDateTime(reader["DATAHORAINCLUSAO_CHE"]) : new DateTime(1900, 1, 1);
            cheque.DataHoraAlteracaoCheque = !reader.IsDBNull(reader.GetOrdinal("DATAHORAALTERACAO_CHE")) ? Convert.ToDateTime(reader["DATAHORAALTERACAO_CHE"]) : new DateTime(1900, 1, 1);
            cheque.UsuarioInclusaoCheque = reader["USUARIOINCLUSAO_CHE"].ToString();
            cheque.UsuarioAlteracaoCheque = reader["USUARIOALTERACAO_CHE"].ToString();

            return cheque;
        }


        private ChequeBaixas ReadChequeBaixasFromDataReader(IDataReader reader, int codigocliente)
        {
            ChequeBaixas chequeBaixas = new ChequeBaixas();

            chequeBaixas.Id = 0;
            chequeBaixas.CodigoCliente = codigocliente;
            chequeBaixas.AutoIncrementoChequeBaixas = Convert.ToInt64(reader["AUTOINC_CHEBX"]);
            chequeBaixas.ChequeChequeBaixas = Convert.ToInt64(reader["CHEQUE_CHEBX"]);
            chequeBaixas.BaixaChequeBaixas = !reader.IsDBNull(reader.GetOrdinal("BAIXA_CHEBX")) ? Convert.ToDateTime(reader["BAIXA_CHEBX"]) : new DateTime(1900, 1, 1);            
            chequeBaixas.CreditoChequeBaixas = !reader.IsDBNull(reader.GetOrdinal("CREDITO_CHEBX")) ? Convert.ToDateTime(reader["CREDITO_CHEBX"]) : new DateTime(1900, 1, 1);
            chequeBaixas.ValorChequeBaixas = Convert.ToDecimal(reader["VALOR_CHEBX"]);
            chequeBaixas.JurosChequeBaixas = Convert.ToDecimal(reader["JUROS_CHEBX"]);
            chequeBaixas.DescontosChequeBaixas = Convert.ToDecimal(reader["DESCONTOS_CHEBX"]);
            chequeBaixas.OrigemChequeBaixas = Convert.ToInt32(reader["ORIGEM_CHEBX"]);
            chequeBaixas.DocumentoOrigemChequeBaixas = Convert.ToInt64(reader["DOCORIGEM_CHEBX"]);
            chequeBaixas.ContaChequeBaixas = Convert.ToInt32(reader["CONTA_CHEBX"]);
            chequeBaixas.TipoBaixaChequeBaixas = Convert.ToInt32(reader["TIPOBAIXA_CHEBX"]);
            chequeBaixas.TerceiroChequeBaixas = reader["TERCEIRO_CHEBX"].ToString();
            chequeBaixas.SequenciaConciliacaoArquivoChequeBaixas = Convert.ToInt32(reader["SEQCONCILIACAOARQ_CHEBX"]);
            chequeBaixas.DataHoraInclusaoChequeBaixas = !reader.IsDBNull(reader.GetOrdinal("DATAHORAINCLUSAO_CHEBX")) ? Convert.ToDateTime(reader["DATAHORAINCLUSAO_CHEBX"]) : new DateTime(1900, 1, 1);
            chequeBaixas.UsuarioInclusaoChequeBaixas = reader["USUARIOINCLUSAO_CHEBX"].ToString();
            chequeBaixas.ContabilizadoChequeBaixas = Convert.ToChar(reader["CONTABILIZADO_CHEBX"]);
            chequeBaixas.CodigoImportacaoContabilizacaoChequeBaixas = Convert.ToInt64(reader["CODIGOIMPORTACAOCTB_CHEBX"]);

            return chequeBaixas;
        }

        private ChequeDevolvido ReadChequeDevolvidoFromDataReader(IDataReader reader, int codigocliente)
        {
            ChequeDevolvido chequeDevolvido = new ChequeDevolvido();

            chequeDevolvido.Id = 0;
            chequeDevolvido.CodigoCliente = codigocliente;
            chequeDevolvido.AutoIncrementoChequeDevolvido = Convert.ToInt64(reader["AUTOINC_CHEDEV"]);
            chequeDevolvido.ChequeChequeDevolvido = Convert.ToInt64(reader["CHEQUE_CHEDEV"]);
            chequeDevolvido.DataDevolucaoChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("DTDEVOLUCAO_CHEDEV")) ? Convert.ToDateTime(reader["DTDEVOLUCAO_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.DataRecolhimentoChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("DTRECOLHIMENTO_CHEDEV")) ? Convert.ToDateTime(reader["DTRECOLHIMENTO_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.AlineaChequeDevolvido = Convert.ToInt32(reader["ALINEA_CHEDEV"]);
            chequeDevolvido.ConciliacaoChequeDevolvido = Convert.ToInt64(reader["CONCILIACAO_CHEDEV"]);
            chequeDevolvido.BaixaAnteriorChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("BAIXAANTERIOR_CHEDEV")) ? Convert.ToDateTime(reader["BAIXAANTERIOR_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.CreditoAnteriorChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("CREDITOANTERIOR_CHEDEV")) ? Convert.ToDateTime(reader["CREDITOANTERIOR_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.TipoBaixaAnteriorChequeDevolvido = Convert.ToInt32(reader["TIPOBAIXAANTERIOR_CHEDEV"]);
            chequeDevolvido.TerceiroAnteriorChequeDevolvido = reader["TERCEIROANTERIOR_CHEDEV"].ToString();
            chequeDevolvido.DocumentoDestinoAnteriorChequeDevolvido = Convert.ToInt64(reader["DOCDESTINOANTERIOR_CHEDEV"]);
            chequeDevolvido.DestinoAnteriorChequeDevolvido = Convert.ToInt32(reader["DESTINOANTERIOR_CHEDEV"]);
            chequeDevolvido.ContaBaixaAnteriorChequeDevolvido = Convert.ToInt32(reader["CONTABAIXAANTERIOR_CHEDEV"]);
            chequeDevolvido.DataDescontoAnteriorChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("DTDESCONTOANTERIOR_CHEDEV")) ? Convert.ToDateTime(reader["DTDESCONTOANTERIOR_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.SequenciaConciliacaoArquivoChequeDevolvido = Convert.ToInt32(reader["SEQCONCILIACAOARQ_CHEDEV"]);
            chequeDevolvido.DataHoraInclusaoChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("DATAHORAINCLUSAO_CHEDEV")) ? Convert.ToDateTime(reader["DATAHORAINCLUSAO_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.DataHoraAlteracaoChequeDevolvido = !reader.IsDBNull(reader.GetOrdinal("DATAHORAALTERACAO_CHEDEV")) ? Convert.ToDateTime(reader["DATAHORAALTERACAO_CHEDEV"]) : new DateTime(1900, 1, 1);
            chequeDevolvido.UsuarioInclusaoChequeDevolvido = reader["USUARIOINCLUSAO_CHEDEV"].ToString();
            chequeDevolvido.UsuarioAlteracaoChequeDevolvido = reader["USUARIOALTERACAO_CHEDEV"].ToString();

            return chequeDevolvido;
        }

    }
}
