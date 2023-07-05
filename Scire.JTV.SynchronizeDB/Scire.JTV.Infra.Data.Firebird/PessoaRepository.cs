using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Infra.Data.Firebird
{
    public class PessoaRepository
    {
        private readonly string connectionString;

        public PessoaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Pessoa> GetPessoas(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM PESSOA " +
                    "WHERE (DATAHORAALTERACAO_PESSOA >= @DataAtualizacao and DATAHORAALTERACAO_PESSOA < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_PESSOA  is Null) ";

                query += "order by DATAHORAALTERACAO_PESSOA";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pessoa pessoa = ReadPessoaFromDataReader(reader, codigoCliente);
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }

            return pessoas;
        }

        public List<PessoaCliente> GetPessoasClientes(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<PessoaCliente> pessoas = new List<PessoaCliente>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM PESSOA_CLIENTE " +
                    "WHERE (DATAHORAALTERACAO_PESSOA_CLI >= @DataAtualizacao and DATAHORAALTERACAO_PESSOA_CLI < @DataAgora) ";
                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_PESSOA_CLI  is Null) ";

                query += "order by DATAHORAALTERACAO_PESSOA_CLI";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PessoaCliente pessoa = ReadPessoaClienteFromDataReader(reader, codigoCliente);
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }

            return pessoas;
        }

        public List<PessoaFisica> GetPessoasFisicas(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<PessoaFisica> pessoas = new List<PessoaFisica>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM PESSOA_FISICA " +
                    "WHERE (DATAHORAALTERACAO_PESSOA_FIS >= @DataAtualizacao and DATAHORAALTERACAO_PESSOA_FIS < @DataAgora) ";

                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_PESSOA_FIS  is Null) ";

                query += "order by DATAHORAALTERACAO_PESSOA_FIS";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);                    
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PessoaFisica pessoa = ReadPessoaFisicaFromDataReader(reader, codigoCliente);
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }

            return pessoas;
        }

        public List<PessoaJuridica> GetPessoasJuridicas(DateTime dataAtualizacao, DateTime dataAgora, int codigoCliente)
        {
            List<PessoaJuridica> pessoas = new List<PessoaJuridica>();

            using (FbConnection connection = new FbConnection(connectionString))
            {
                string query = "SELECT * FROM PESSOA_JURICA " +
                    "WHERE (DATAHORAALTERACAO_PESSOA_JUR >= @DataAtualizacao and DATAHORAALTERACAO_PESSOA_JUR < @DataAgora) ";

                if (dataAtualizacao < new DateTime(2000, 1, 1))
                    query += "or (DATAHORAALTERACAO_PESSOA_JUR  is Null) ";

                query += "order by DATAHORAALTERACAO_PESSOA_JUR";

                using (FbCommand command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);
                    command.Parameters.AddWithValue("@DataAgora", dataAgora);
                    connection.Open();

                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PessoaJuridica pessoa = ReadPessoaJuridicaFromDataReader(reader, codigoCliente);
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }

            return pessoas;
        }

        private Pessoa ReadPessoaFromDataReader(IDataReader reader, int codigocliente)
        { 
            Pessoa pessoa = new Pessoa();
            pessoa.CodigoCliente = codigocliente;
            pessoa.CodigoPessoa = Convert.ToInt32(reader["CODIGO_PESSOA"]);
            pessoa.RazaoSocialPessoa = Convert.ToString(reader["RAZAOSOCIAL_PESSOA"]);
            pessoa.NomeFantasiaPessoa = Convert.ToString(reader["NOMEFANTASIA_PESSOA"]);
            pessoa.TipoPessoa = Convert.ToString(reader["TIPO_PESSOA"]);
            pessoa.DocumentoPessoa = Convert.ToString(reader["DOCUMENTO_PESSOA"]);
            pessoa.IdentificacaoExPessoa = Convert.ToString(reader["IDENTIFICACAOEX_PESSOA"]);
            pessoa.CeiPessoa = Convert.ToString(reader["CEI_PESSOA"]);            
            pessoa.EmailPadraoPessoa = Convert.ToInt32(reader["EMAILPADRAO_PESSOA"]);
            pessoa.StatusPessoa = Convert.ToInt32(reader["STATUS_PESSOA"]);
            pessoa.GrupoPessoa = Convert.ToInt32(reader["GRUPO_PESSOA"]);
            pessoa.EnderecoPessoa = Convert.ToInt32(reader["ENDERECO_PESSOA"]);
            pessoa.EnderecoEntregaPessoa = Convert.ToInt32(reader["ENDERECOENTREGA_PESSOA"]);
            pessoa.EnderecoCobrancaPessoa = Convert.ToInt32(reader["ENDERECOCOBRANCA_PESSOA"]);
            pessoa.ImpeEnderecoEntregaNfPessoa = Convert.ToString(reader["IMPENDERECOENTREGANF_PESSOA"]);
            pessoa.ResponsavelPessoa = Convert.ToString(reader["RESPONSAVEL_PESSOA"]);
            pessoa.ClientePessoa = Convert.ToString(reader["CLIENTE_PESSOA"]);
            pessoa.FornecedorPessoa = Convert.ToString(reader["FORNECEDOR_PESSOA"]);
            pessoa.ConsVendasPessoa = Convert.ToString(reader["CONSVENDAS_PESSOA"]);
            pessoa.SupervisorVendasPessoa = Convert.ToString(reader["SUPERVISORVENDAS_PESSOA"]);
            pessoa.TransportadorPessoa = Convert.ToString(reader["TRANSPORTADOR_PESSOA"]);
            pessoa.FuncionarioPessoa = Convert.ToString(reader["FUNCIONARIO_PESSOA"]);
            pessoa.TomadorPessoa = Convert.ToString(reader["TOMADOR_PESSOA"]);
            pessoa.OutrosPessoa = Convert.ToString(reader["OUTROS_PESSOA"]);
            pessoa.EmpresaPessoa = Convert.ToString(reader["EMPRESA_PESSOA"]);
            pessoa.PrepostoPessoa = Convert.ToString(reader["PREPOSTO_PESSOA"]);
            pessoa.ContadorPessoa = Convert.ToString(reader["CONTADOR_PESSOA"]);
            pessoa.SeguradoraPessoa = Convert.ToString(reader["SEGURADORA_PESSOA"]);
            pessoa.AgenCiadorPessoa = Convert.ToString(reader["AGENCIADOR_PESSOA"]);
            pessoa.ComercialExportadorPessoa = Convert.ToString(reader["COMERCIAL_EXPORTADOR_PESSOA"]);
            pessoa.OperadoraPlanoSaudePessoa = Convert.ToString(reader["OPERADORA_PLANO_SAUDE_PESSOA"]);
            pessoa.OutroComissionadoPessoa = Convert.ToString(reader["OUTROCOMISSIONADO_PESSOA"]);
            pessoa.GrupoResultadoPessoa = Convert.ToInt32(reader["GRUPORESULTADO_PESSOA"]);
            pessoa.ConsumidorFinalPessoa = Convert.ToString(reader["CONSUMIDORFINAL_PESSOA"]);
            pessoa.SuframaPessoa = Convert.ToString(reader["SUFRAMA_PESSOA"]);
            pessoa.SenhaSitePessoa = Convert.ToString(reader["SENHASITE_PESSOA"]);
            pessoa.ChaveSitePessoa = Convert.ToString(reader["CHAVESITE_PESSOA"]);
            pessoa.ProfissaoSegmentoPessoa = Convert.ToInt32(reader["PROFISSAOSEGMENTO_PESSOA"]);
            pessoa.InformaOrgaoProtCredPessoa = Convert.ToString(reader["INFORMAORGAOPROTCRED_PESSOA"]);
            pessoa.ClassificacaoPessoa = Convert.ToInt32(reader["CLASSIFICACAO_PESSOA"]);
            pessoa.OptanteSimplesPessoa = Convert.ToString(reader["OPTANTE_SIMPLES_PESSOA"]);
            pessoa.PlanoCntbDebRedPessoa = Convert.ToString(reader["PLANOCNTBDEB_RED_PESSOA"]);
            pessoa.PlanoCntbDebAnalitPessoa = Convert.ToString(reader["PLANOCNTBDEB_ANALIT_PESSOA"]);
            pessoa.PlanoCntbCredRedPessoa = Convert.ToString(reader["PLANOCNTBCRED_RED_PESSOA"]);
            pessoa.PlanoCntbCredAnalitPessoa = Convert.ToString(reader["PLANOCNTBCRED_ANALIT_PESSOA"]);
            pessoa.BancoContaPessoa = Convert.ToInt32(reader["BANCOCONTA_PESSOA"]);
            pessoa.AgenciaContaPessoa = Convert.ToString(reader["AGENCIACONTA_PESSOA"]);
            pessoa.DigitoAgenciaContaPessoa = Convert.ToString(reader["DIGITOAGENCIACONTA_PESSOA"]);
            pessoa.TipoContaPessoa = Convert.ToInt32(reader["TIPOCONTA_PESSOA"]);
            pessoa.SubtipoContaPessoa = Convert.ToInt32(reader["SUBTIPOCONTA_PESSOA"]);
            pessoa.NumeroContaPessoa = Convert.ToString(reader["NUMEROCONTA_PESSOA"]);
            pessoa.DigitoContaPessoa = Convert.ToString(reader["DIGITOCONTA_PESSOA"]);
            pessoa.FavorecidoContaPessoa = Convert.ToString(reader["FAVORECIDOCONTA_PESSOA"]);
            pessoa.DigitoAgenciaEContaPessoa = Convert.ToString(reader["DIGITOAGENCIAECONTA_PESSOA"]);
            pessoa.TipoChavePixPessoa = Convert.ToInt32(reader["TIPOCHAVEPIX_PESSOA"]);
            pessoa.ChavePixPessoa = Convert.ToString(reader["CHAVEPIX_PESSOA"]);
            pessoa.RegiaoPessoa = Convert.ToInt32(reader["REGIAO_PESSOA"]);
            pessoa.RecebeCobrancaEmailPessoa = Convert.ToString(reader["RECEBECOBRANCAEMAIL_PESSOA"]);
            pessoa.ParticipantePessoa = Convert.ToInt32(reader["PARTICIPANTE_PESSOA"]);
            pessoa.RegraLancamentoPessoa = Convert.ToInt64(reader["REGRALANCAMENTO_PESSOA"]);
            pessoa.RegraLancamentoNFSaidaPessoa = Convert.ToInt64(reader["REGRALANC_NFSAIDA_PESSOA"]);
            pessoa.TransacaoPadraoPessoa = Convert.ToInt32(reader["TRANSACAO_PADRAO_PESSOA"]);
            pessoa.AliquotaIcmsPessoa = Convert.ToDecimal(reader["ALIQUOTA_ICMS_PESSOA"]);
            pessoa.SenhaPessoa = Convert.ToString(reader["SENHA_PESSOA"]);
            pessoa.CandidatoPessoa = Convert.ToString(reader["CANDIDATO_PESSOA"]);
            pessoa.ProdutorRuralPessoa = Convert.ToString(reader["PRODUTORRURAL_PESSOA"]);
            pessoa.DataAtualizacaoDadosPessoa = !reader.IsDBNull(reader.GetOrdinal("DATA_ATUALIZACAO_DADOS_PESSOA")) ? Convert.ToDateTime(reader["DATA_ATUALIZACAO_DADOS_PESSOA"]) : (DateTime?)null;
            pessoa.DataAnonimizacaoPessoa = !reader.IsDBNull(reader.GetOrdinal("DATA_ANONIMIZACAO_PESSOA")) ? Convert.ToDateTime(reader["DATA_ANONIMIZACAO_PESSOA"]) : (DateTime?)null;
            pessoa.DataAutAnonimizacaoPessoa = !reader.IsDBNull(reader.GetOrdinal("DATA_AUT_ANONIMIZACAO_PESSOA")) ? Convert.ToDateTime(reader["DATA_AUT_ANONIMIZACAO_PESSOA"]) : (DateTime?)null;
            pessoa.UsuarioAnonimizacaoPessoa = Convert.ToString(reader["USUARIO_ANONIMIZACAO_PESSOA"]);
            pessoa.DataHoraInclusaoPessoa = Convert.ToDateTime(reader["DATAHORAINCLUSAO_PESSOA"]);
            pessoa.DataHoraAlteracaoPessoa = Convert.ToDateTime(reader["DATAHORAALTERACAO_PESSOA"]);
            pessoa.UsuarioInclusaoPessoa = Convert.ToString(reader["USUARIOINCLUSAO_PESSOA"]);
            pessoa.UsuarioAlteracaoPessoa = Convert.ToString(reader["USUARIOALTERACAO_PESSOA"]);
            pessoa.ProdutorRuralCpfpPessoa = Convert.ToString(reader["PRODUTORRURAL_CPFP_PESSOA"]);
            pessoa.IndNatRetPessoa = Convert.ToString(reader["IND_NAT_RET_PESSOA"]);

            return pessoa;
        }
        
        private PessoaCliente ReadPessoaClienteFromDataReader(IDataReader reader, int codigoCliente)
        {
            PessoaCliente pessoaCliente = new PessoaCliente();

            pessoaCliente.Id = 0;
            pessoaCliente.CodigoCliente = codigoCliente;
            pessoaCliente.PessoaClienteId = Convert.ToInt32(reader["PESSOA_PESSOA_CLI"]);
            pessoaCliente.OpcaoFrete = Convert.ToInt16(reader["OPCAOFRETE_PESSOA_CLI"]);
            pessoaCliente.FreteAtual = Convert.ToDecimal(reader["FRETEATUAL_PESSOA_CLI"]);
            pessoaCliente.FreteIdeal = Convert.ToDecimal(reader["FRETEIDEAL_PESSOA_CLI"]);
            pessoaCliente.TipoFrete = Convert.ToInt16(reader["TIPOFRETE_PESSOA_CLI"]);
            pessoaCliente.TipoFreteCT = Convert.ToInt16(reader["TIPOFRETECT_PESSOA_CLI"]);
            pessoaCliente.CondicaoPagamento = Convert.ToInt16(reader["CONDPAGTO_PESSOA_CLI"]);
            pessoaCliente.LimiteCredito = Convert.ToInt32(reader["LIMITE_CRED_PESSOA_CLI"]);
            pessoaCliente.PrazoMedioMaximo = Convert.ToInt32(reader["PRAZOMEDIOMAX_PESSOA_CLI"]);
            pessoaCliente.PrimeiraCompra = !reader.IsDBNull(reader.GetOrdinal("PRIMEIRACOMPRA_CLI")) ? Convert.ToDateTime(reader["PRIMEIRACOMPRA_CLI"]) : (DateTime?)null;            
            pessoaCliente.FamiliaPedido = Convert.ToInt32(reader["FAMILIAPEDIDO_PESSOA_CLI"]);
            pessoaCliente.FamiliaAssistencia = Convert.ToInt32(reader["FAMILIAASSIST_PESSOA_CLI"]);
            pessoaCliente.BuscaMercadoria = Convert.ToChar(reader["BUSCA_MERCADORIA_PESSOA_CLI"]);
            pessoaCliente.Situacao = Convert.ToInt32(reader["SITUACAO_PESSOA_CLI"]);
            pessoaCliente.SituacaoAssistencia = Convert.ToInt32(reader["SITUACAOASSIST_PESSOA_CLI"]);
            pessoaCliente.Banco = Convert.ToInt32(reader["BANCO_PESSOA_CLI"]);
            pessoaCliente.Financeira = Convert.ToInt32(reader["FINANCEIRA_PESSOA_CLI"]);
            pessoaCliente.Redespacho = Convert.ToInt32(reader["REDESPACHO_PESSOA_CLI"]);
            pessoaCliente.EmpresaPadrao = Convert.ToInt32(reader["EMPRESAPADRAO_PESSOA_CLI"]);
            pessoaCliente.RedespachoPago = Convert.ToChar(reader["REDESPACHOPAGO_PESSOA_CLI"]);
            pessoaCliente.CofinsSuframa = Convert.ToChar(reader["COFINSSUFRAMA_PESSOA_CLI"]);
            pessoaCliente.PisSuframa = Convert.ToChar(reader["PISSUFRAMA_PESSOA_CLI"]);
            pessoaCliente.IcmsSuframa = Convert.ToChar(reader["ICMSSUFRAMA_PESSOA_CLI"]);
            pessoaCliente.DiasParaIpi = Convert.ToInt32(reader["DIASPARAIPI_PESSOA_CLI"]);
            pessoaCliente.DiasParaDesp = Convert.ToInt32(reader["DIASPARADESP_PESSOA_CLI"]);
            pessoaCliente.DiasParaSt = Convert.ToInt32(reader["DIASPARAST_PESSOA_CLI"]);
            pessoaCliente.AceitaEntregaParcial = Convert.ToChar(reader["ACEITAENTREGAPARCIAL_PESSOA_CLI"]);
            pessoaCliente.RateiaDespesas = Convert.ToChar(reader["RATEIADESPESAS_PESSOA_CLI"]);
            pessoaCliente.RateiaIpi = Convert.ToChar(reader["RATEIAIPI_PESSOA_CLI"]);
            pessoaCliente.RateiaSt = Convert.ToChar(reader["RATEIAST_PESSOA_CLI"]);
            pessoaCliente.FormatoDuplicataIpi = Convert.ToInt16(reader["FORMATO_DUP_IPI_PESSOA_CLI"]);
            pessoaCliente.FormatoDuplicataDesp = Convert.ToInt16(reader["FORMATO_DUP_DESP_PESSOA_CLI"]);
            pessoaCliente.FormatoDuplicataSt = Convert.ToInt16(reader["FORMATO_DUP_ST_PESSOA_CLI"]);
            pessoaCliente.CondicaoPagamentoCte = Convert.ToInt32(reader["CONDICAO_PAG_CTE_PESSOA_CLI"]);
            pessoaCliente.BancoCte = Convert.ToInt32(reader["BANCO_CTE_PESSOA_CLI"]);
            pessoaCliente.SituacaoCte = Convert.ToInt32(reader["SITUACAO_CTE_PESSOA_CLI"]);
            pessoaCliente.ConsultorPadrao = Convert.ToInt32(reader["CONSULTORPADRAO_PESSOA_CLI"]);
            pessoaCliente.DataHoraInclusao = Convert.ToDateTime(reader["DATAHORAINCLUSAO_PESSOA_CLI"]);
            pessoaCliente.DataHoraAlteracao = Convert.ToDateTime(reader["DATAHORAALTERACAO_PESSOA_CLI"]);
            pessoaCliente.UsuarioInclusao = reader["USUARIOINCLUSAO_PESSOA_CLI"].ToString();
            pessoaCliente.UsuarioAlteracao = reader["USUARIOALTERACAO_PESSOA_CLI"].ToString();

            return pessoaCliente;
        }
        
        private PessoaFisica ReadPessoaFisicaFromDataReader(IDataReader reader, int codigoCliente)
        {
            PessoaFisica pessoaFisica = new PessoaFisica();

            pessoaFisica.Id = 0;
            pessoaFisica.CodigoCliente = codigoCliente;
            pessoaFisica.Pessoa = Convert.ToInt32(reader["PESSOA_PESSOA_FIS"]);
            pessoaFisica.CPF = reader["CPF_PESSOA_FIS"].ToString();
            pessoaFisica.MatriculaINSS = reader["MATRICULAINSS_PESSOA_PFIS"].ToString();
            pessoaFisica.Identidade = reader["IDENTIDADE_PESSOA_FIS"].ToString();
            pessoaFisica.Inscricao = reader["INSCRICAO_PESSOA_FIS"].ToString();
            pessoaFisica.DataEmissaoIdentidade = reader["DTEMISSAOIDENT_PESSOA_FIS"] as DateTime?;
            pessoaFisica.EmissorIdentidade = reader["EMISSORIDENT_PESSOA_FIS"].ToString();
            pessoaFisica.EstadoCivil = reader["ESTADOCIVIL_PESSOA_FIS"].ToString();
            pessoaFisica.CodigoConjuge = Convert.ToInt32(reader["CODIGOCONJUGE_PESSOA_FIS"]);
            pessoaFisica.NomeConjuge = reader["NOMECONJUGE_PESSOA_FIS"].ToString();
            pessoaFisica.Sexo = reader["SEXO_PESSOA_FIS"].ToString();
            pessoaFisica.DataNascimento = !reader.IsDBNull(reader.GetOrdinal("DTNASCIMENTO_PESSOA_FIS")) ? Convert.ToDateTime(reader["DTNASCIMENTO_PESSOA_FIS"]) : (DateTime?)null; 
            pessoaFisica.CidadeNascimento = Convert.ToInt32(reader["CIDADENASCIMENTO_PESSOA_FIS"]);
            pessoaFisica.NomePai = reader["NOMEPAI_PESSOA_FIS"].ToString();
            pessoaFisica.NomeMae = reader["NOMEMAE_PESSOA_FIS"].ToString();
            pessoaFisica.LocalTrabalho = reader["LOCALTRABALHO_PESSOA_FIS"].ToString();
            pessoaFisica.TelefoneTrabalho = reader["TELEFONETRABALHO_PESSOA_FIS"].ToString();
            pessoaFisica.Profissao = reader["PROFISSAO_PESSOA_FIS"].ToString();
            pessoaFisica.DataAdmissao = !reader.IsDBNull(reader.GetOrdinal("DTADMISSAO_PESSOA_FIS")) ? Convert.ToDateTime(reader["DTADMISSAO_PESSOA_FIS"]) : (DateTime?)null;            
            pessoaFisica.ValorRenda = Convert.ToDecimal(reader["VLRRENDA_PESSOA_FISICA"]);
            pessoaFisica.CarteiraTrabalho = reader["CARTEIRATRABALHO_PESSOA_FIS"].ToString();
            pessoaFisica.UFEmissorIdentidade = reader["UF_EMISSORIDENT_PESSOA_FIS"].ToString();
            pessoaFisica.NumeroRIC = reader["NUMERORIC_PESSOA_FIS"].ToString();
            pessoaFisica.EmissorRIC = reader["EMISSORRIC_PESSOA_FIS"].ToString();
            pessoaFisica.DataEmissaoRIC = !reader.IsDBNull(reader.GetOrdinal("DTEMISSAORIC_PESSOA_FIS")) ? Convert.ToDateTime(reader["DTEMISSAORIC_PESSOA_FIS"]) : (DateTime?)null;            
            pessoaFisica.NomeSocial = reader["NOME_SOCIAL_PESSOA_FIS"].ToString();
            pessoaFisica.PaisNacionalidade = Convert.ToInt32(reader["PAISNACIONALIDADE_PESSOA_FIS"]);
            pessoaFisica.DataHoraInclusao = Convert.ToDateTime(reader["DATAHORAINCLUSAO_PESSOA_FIS"]);
            pessoaFisica.DataHoraAlteracao = !reader.IsDBNull(reader.GetOrdinal("DATAHORAALTERACAO_PESSOA_FIS")) ? Convert.ToDateTime(reader["DATAHORAALTERACAO_PESSOA_FIS"]) : new DateTime(1900,1,1); 
            pessoaFisica.UsuarioInclusao = reader["USUARIOINCLUSAO_PESSOA_FIS"].ToString();
            pessoaFisica.UsuarioAlteracao = reader["USUARIOALTERACAO_PESSOA_FIS"].ToString();

            return pessoaFisica;
        }

        private PessoaJuridica ReadPessoaJuridicaFromDataReader(IDataReader reader, int codigoCliente)
        {
            PessoaJuridica pessoaJuridica = new PessoaJuridica();

            pessoaJuridica.Id = 0;
            pessoaJuridica.CodigoCliente = codigoCliente;
            pessoaJuridica.PessoaJuridicaId = Convert.ToInt32(reader["PESSOA_PESSOA_JUR"]);
            pessoaJuridica.Tipo = reader["TIPO_PESSOA_JUR"].ToString();
            pessoaJuridica.CnpjCeI = reader["CNPJCEI_PESSOA_JUR"].ToString();
            pessoaJuridica.Inscricao = reader["INSCRICAO_PESSOA_JUR"].ToString();
            pessoaJuridica.NomeComprador = reader["NOMECOMPRADOR_PESSOA_JUR"].ToString();
            pessoaJuridica.DtnascComprador = !reader.IsDBNull(reader.GetOrdinal("DTNASCCOMPRADOR_PESSOA_JUR")) ? Convert.ToDateTime(reader["DTNASCCOMPRADOR_PESSOA_JUR"]) : new DateTime(1900, 1, 1); 
            pessoaJuridica.NomeProprietario = reader["NOMEPROPRIET_PESSOA_JUR"].ToString();
            pessoaJuridica.DtnascProprietario = !reader.IsDBNull(reader.GetOrdinal("DTNASCPROPRIET_PESSOA_JUR")) ? Convert.ToDateTime(reader["DTNASCPROPRIET_PESSOA_JUR"]) : new DateTime(1900, 1, 1);  
            pessoaJuridica.NomeContato = reader["NOMECONTATO_PESSOA_JUR"].ToString();
            pessoaJuridica.InscricaoMunicipal = reader["INSCRICAOMUNICIPAL_PESSOA_JUR"].ToString();
            pessoaJuridica.CnaePadrao = Convert.ToInt32(reader["CNAE_PADRAO_PESSOA_JUR"]);
            pessoaJuridica.RegistroAns = reader["REGISTRO_ANS_JUR"].ToString();
            pessoaJuridica.DataHoraInclusao = !reader.IsDBNull(reader.GetOrdinal("DATAHORAINCLUSAO_PESSOA_JUR")) ? Convert.ToDateTime(reader["DATAHORAINCLUSAO_PESSOA_JUR"]) : new DateTime(1900, 1, 1); 
            pessoaJuridica.DataHoraAlteracao = !reader.IsDBNull(reader.GetOrdinal("DATAHORAALTERACAO_PESSOA_JUR")) ? Convert.ToDateTime(reader["DATAHORAALTERACAO_PESSOA_JUR"]) : new DateTime(1900, 1, 1); 
            pessoaJuridica.UsuarioInclusao = reader["USUARIOINCLUSAO_PESSOA_JUR"].ToString();
            pessoaJuridica.UsuarioAlteracao = reader["USUARIOALTERACAO_PESSOA_JUR"].ToString();
            pessoaJuridica.IndcPrb = Convert.ToInt32(reader["INDCPRB_PESSOA_JUR"]);

            return pessoaJuridica;
        }

    }

}

