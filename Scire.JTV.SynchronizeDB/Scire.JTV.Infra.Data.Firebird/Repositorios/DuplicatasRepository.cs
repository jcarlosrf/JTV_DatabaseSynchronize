using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.Firebird
{
    public class DuplicatasRepository
    {
        private readonly string connectionString;

        public DuplicatasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Duplicata> GetDuplicatas(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<Duplicata> duplicatas = new List<Duplicata>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM DUPLICATA " +
                    "WHERE (DATAHORAALTERACAO_DUP >= @DataAtualizacao and DATAHORAALTERACAO_DUP < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_DUP  is Null) ";

                query += "order by DATAHORAALTERACAO_DUP";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Duplicata duplicata = ReadDuplicataFromDataReader(reader, codigoCliente);
                            duplicatas.Add(duplicata);
                        }
                    }
                }
            }

            return duplicatas;
        }

        public List<DuplicataBaixas> GetDuplicatasBaixas(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<DuplicataBaixas> duplicatas = new List<DuplicataBaixas>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM DUPLICATA_BAIXAS " +
                    "WHERE (DATAHORAALTERACAO_DUPBX >= @DataAtualizacao and DATAHORAALTERACAO_DUPBX < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_DUPBX  is Null) ";

                query += "order by DATAHORAALTERACAO_DUPBX";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DuplicataBaixas duplicata = ReadDuplicataBaixasFromDataReader(reader, codigoCliente);
                            duplicatas.Add(duplicata);
                        }
                    }
                }
            }

            return duplicatas;
        }


        private Duplicata ReadDuplicataFromDataReader(IDataReader reader, int codigocliente)
        {
            Duplicata duplicata = new Duplicata();

            duplicata.Id = 0;
            duplicata.CodigoCliente = codigocliente;
            duplicata.AutoIncrementoDuplicata = Convert.ToInt64(reader["AUTOINC_DUP"]);
            duplicata.EmpresaDuplicata = Convert.ToInt32(reader["EMPRESA_DUP"]);
            duplicata.TipoDuplicata = Convert.ToInt32(reader["TIPO_DUP"]);
            duplicata.TipoDocumentoDuplicata = Convert.ToInt32(reader["TIPODOC_DUP"]);
            duplicata.PessoaDuplicata = Convert.ToInt32(reader["PESSOA_DUP"]);
            duplicata.DocumentoDuplicata = reader["DOCUMENTO_DUP"].ToString();
            duplicata.BancoDuplicata = Convert.ToInt32(reader["BANCO_DUP"]);
            duplicata.OrigemDuplicata = Convert.ToInt32(reader["ORIGEM_DUP"]);
            duplicata.SubOrigemDuplicata = Convert.ToInt32(reader["SUBORIGEM_DUP"]);
            duplicata.DocumentoOrigemDuplicata = Convert.ToInt64(reader["DOCORIGEM_DUP"]);
            duplicata.EmissaoDuplicata = !reader.IsDBNull(reader.GetOrdinal("EMISSAO_DUP")) ? Convert.ToDateTime(reader["EMISSAO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.VencimentoOriginalDuplicata = !reader.IsDBNull(reader.GetOrdinal("VENCIMENTOORIGINAL_DUP")) ? Convert.ToDateTime(reader["VENCIMENTOORIGINAL_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.VencimentoDuplicata = !reader.IsDBNull(reader.GetOrdinal("VENCIMENTO_DUP")) ? Convert.ToDateTime(reader["VENCIMENTO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.DataDescontoDuplicata = !reader.IsDBNull(reader.GetOrdinal("DTDESCONTO_DUP")) ? Convert.ToDateTime(reader["DTDESCONTO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.PromessaPagamentoDuplicata = !reader.IsDBNull(reader.GetOrdinal("PROMESSAPGTO_DUP")) ? Convert.ToDateTime(reader["PROMESSAPGTO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.PagamentoDuplicata = !reader.IsDBNull(reader.GetOrdinal("PAGAMENTO_DUP")) ? Convert.ToDateTime(reader["PAGAMENTO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.ValorNominalOriginalDuplicata = Convert.ToDecimal(reader["VALORNOMINALORIGINAL_DUP"]);
            duplicata.ValorDuplicata = Convert.ToDecimal(reader["VALOR_DUP"]);
            duplicata.PrecoDescontoDuplicata = Convert.ToDecimal(reader["PREDESCONTO_DUP"]);
            duplicata.ValorPagoDuplicata = Convert.ToDecimal(reader["VALORPAGO_DUP"]);
            duplicata.ValorAbertoDuplicata = Convert.ToDecimal(reader["VALORABERTO_DUP"]);
            duplicata.QualificacaoDuplicata = Convert.ToInt32(reader["QUALIFICACAO_DUP"]);
            duplicata.DataBaixaDuplicata = !reader.IsDBNull(reader.GetOrdinal("DATABAIXA_DUP")) ? Convert.ToDateTime(reader["DATABAIXA_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.CargaDuplicata = Convert.ToInt64(reader["CARGA_DUP"]);
            duplicata.SituacaoDuplicata = Convert.ToInt32(reader["SITUACAO_DUP"]);
            duplicata.BoletoDuplicata = Convert.ToInt64(reader["BOLETO_DUP"]);
            duplicata.DigitoBoletoDuplicata = reader["DIGITOBOLETO_DUP"].ToString();
            duplicata.SubstituidaDuplicata = Convert.ToChar(reader["SUBSTITUIDA_DUP"]);
            duplicata.DesconsideradaDuplicata = Convert.ToChar(reader["DESCONSIDERADA_DUP"]);
            duplicata.ContaDuplicata = Convert.ToInt32(reader["CONTA_DUP"]);
            duplicata.CodigoBarrasDuplicata = reader["CODIGOBARRAS_DUP"].ToString();
            duplicata.LinhaDigitavelDuplicata = reader["LINHADIGITAVEL_DUP"].ToString();
            duplicata.UrlQRCodePixDuplicata = reader["URL_QRCODE_PIX_DUP"].ToString();
            duplicata.EmpresaAnteriorDuplicata = Convert.ToInt32(reader["EMPRESAANT_DUP"]);
            duplicata.AutoIncrementoAnteriorDuplicata = Convert.ToInt64(reader["AUTOINCANT_DUP"]);
            duplicata.NumeradorDuplicata = Convert.ToInt32(reader["NUMERADOR_DUP"]);
            duplicata.DenominadorDuplicata = Convert.ToInt32(reader["DENOMINADOR_DUP"]);
            duplicata.ProjetoDuplicata = Convert.ToInt32(reader["PROJETO_DUP"]);
            duplicata.ContabilizadaPrevisaoDuplicata = Convert.ToChar(reader["CONTABILIZADAPREVISAO_DUP"]);
            duplicata.DataHoraInclusaoDuplicata = !reader.IsDBNull(reader.GetOrdinal("DATAHORAINCLUSAO_DUP")) ? Convert.ToDateTime(reader["DATAHORAINCLUSAO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.DataHoraAlteracaoDuplicata = !reader.IsDBNull(reader.GetOrdinal("DATAHORAALTERACAO_DUP")) ? Convert.ToDateTime(reader["DATAHORAALTERACAO_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.DataHoraAuditoriaDuplicata = !reader.IsDBNull(reader.GetOrdinal("DATAHORAAUDITORIA_DUP")) ? Convert.ToDateTime(reader["DATAHORAAUDITORIA_DUP"]) : new DateTime(1900, 1, 1);
            duplicata.IntegracaoContabilDuplicata = Convert.ToInt32(reader["INTEGRACAOCTB_DUP"]);
            duplicata.ContaContabilDuplicata = Convert.ToInt64(reader["CONTA_CTB_DUP"]);
            duplicata.FinanceiraDuplicata = Convert.ToInt32(reader["FINANCEIRA_DUP"]);
            duplicata.UsuarioInclusaoDuplicata = reader["USUARIOINCLUSAO_DUP"].ToString();
            duplicata.UsuarioAlteracaoDuplicata = reader["USUARIOALTERACAO_DUP"].ToString();
            duplicata.UsuarioAuditoriaDuplicata = reader["USUARIOAUDITORIA_DUP"].ToString();
            duplicata.MotivoNaoAutorizacaoDuplicata = reader["MOTIVONAOAUTORIZACAO_DUP"].ToString();
            duplicata.SituacaoAuditoriaDuplicata = Convert.ToInt32(reader["SITUACAOAUDITORIA_DUP"]);

            return duplicata;
        }

        private DuplicataBaixas ReadDuplicataBaixasFromDataReader(IDataReader reader, int codigocliente)
        {
            DuplicataBaixas duplicataBaixas = new DuplicataBaixas();

            duplicataBaixas.Id = 0;
            duplicataBaixas.CodigoCliente = codigocliente;
            duplicataBaixas.AutoIncrementoDuplicataBaixas = Convert.ToInt64(reader["AUTOINC_DUPBX"]);
            duplicataBaixas.DuplicataDuplicataBaixas = Convert.ToInt64(reader["DUPLICATA_DUPBX"]);
            duplicataBaixas.PagamentoDuplicataBaixas = !reader.IsDBNull(reader.GetOrdinal("PAGAMENTO_DUPBX")) ? Convert.ToDateTime(reader["PAGAMENTO_DUPBX"]) : new DateTime(1900, 1, 1);
            duplicataBaixas.DataBaixaAjustadaDuplicataBaixas = !reader.IsDBNull(reader.GetOrdinal("DATABAIXAAJUSTADA_DUPBX")) ? Convert.ToDateTime(reader["DATABAIXAAJUSTADA_DUPBX"]) : new DateTime(1900, 1, 1);
            duplicataBaixas.CreditoDuplicataBaixas = !reader.IsDBNull(reader.GetOrdinal("CREDITO_DUPBX")) ? Convert.ToDateTime(reader["CREDITO_DUPBX"]) : new DateTime(1900, 1, 1);
            duplicataBaixas.VencimentoComissaoDuplicataBaixas = !reader.IsDBNull(reader.GetOrdinal("VENCTOCOMISSAO_DUPBX")) ? Convert.ToDateTime(reader["VENCTOCOMISSAO_DUPBX"]) : new DateTime(1900, 1, 1);
            duplicataBaixas.ValorDuplicataBaixas = Convert.ToDecimal(reader["VALOR_DUPBX"]);
            duplicataBaixas.JurosDuplicataBaixas = Convert.ToDecimal(reader["JUROS_DUPBX"]);
            duplicataBaixas.DescontosDuplicataBaixas = Convert.ToDecimal(reader["DESCONTOS_DUPBX"]);
            duplicataBaixas.OrigemDuplicataBaixas = Convert.ToInt32(reader["ORIGEM_DUPBX"]);
            duplicataBaixas.DocumentoOrigemDuplicataBaixas = Convert.ToInt64(reader["DOCORIGEM_DUPBX"]);
            duplicataBaixas.AutenticacaoMecanicaDuplicataBaixas = reader["AUTENTICACAOMECANICA_DUPBX"].ToString();
            duplicataBaixas.ContabilizadoDuplicataBaixas = Convert.ToChar(reader["CONTABILIZADO_DUPBX"]);
            duplicataBaixas.CodigoImportacaoContabilDuplicataBaixas = Convert.ToInt64(reader["CODIGOIMPORTACAOCTB_DUPBX"]);
            duplicataBaixas.DataHoraInclusaoDuplicataBaixas = !reader.IsDBNull(reader.GetOrdinal("DATAHORAINCLUSAO_DUPBX")) ? Convert.ToDateTime(reader["DATAHORAINCLUSAO_DUPBX"]) : new DateTime(1900, 1, 1);
            duplicataBaixas.DataHoraAlteracaoDuplicataBaixas = !reader.IsDBNull(reader.GetOrdinal("DATAHORAALTERACAO_DUPBX")) ? Convert.ToDateTime(reader["DATAHORAALTERACAO_DUPBX"]) : new DateTime(1900, 1, 1);
            duplicataBaixas.UsuarioInclusaoDuplicataBaixas = reader["USUARIOINCLUSAO_DUPBX"].ToString();
            duplicataBaixas.UsuarioAlteracaoDuplicataBaixas = reader["USUARIOALTERACAO_DUPBX"].ToString();

            return duplicataBaixas;
        }


    }
}
