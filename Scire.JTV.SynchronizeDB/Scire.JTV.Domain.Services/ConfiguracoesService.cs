using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scire.JTV.Domain.Entities;

namespace Scire.JTV.Domain.Services
{
    public class ConfiguracoesService
    {
        private string FireString;
        private string MyString;
        private Infra.Data.MySql.EmpresaImportacaoRepository clienteRepository;


        public ConfiguracoesService(string fireString, string mysqlString)
        {
            FireString = fireString;
            MyString = mysqlString;
        }

        public EmpresaImportacao ConfiguracoesCliente(int codigoCliente)
        {
            clienteRepository = new Infra.Data.MySql.EmpresaImportacaoRepository(MyString);
            return clienteRepository.GetEntity(codigoCliente);
        }

        public bool UpdateDhAlteracao(int codigoCliente , DateTime DhAtualizacao, Infra.Data.MySql.EmpresaImportacaoRepository.Servico servico)
        {
            clienteRepository = new Infra.Data.MySql.EmpresaImportacaoRepository(MyString);
            var retorno = clienteRepository.UpdateDataHora(codigoCliente, DhAtualizacao, servico);
            return retorno > 0;
        }

        public bool UpdateDhExecucao(int codigoCliente, DateTime DhAtualizacao)
        {
            clienteRepository = new Infra.Data.MySql.EmpresaImportacaoRepository(MyString);
            var retorno = clienteRepository.UpdateDataHoraExecucao(codigoCliente, DhAtualizacao);
            return retorno > 0;
        }

        public bool ResetBancoDados(int CodigoCliente)
        {
            var alterou = 
                this.UpdateDhAlteracao(CodigoCliente, new DateTime(2000, 1, 1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Pessoa)
                && this.UpdateDhAlteracao(CodigoCliente, new DateTime(2000, 1, 1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Cheques)
                && this.UpdateDhAlteracao(CodigoCliente, new DateTime(2000, 1, 1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Duplicatas);

            if (alterou)
            {
                clienteRepository = new Infra.Data.MySql.EmpresaImportacaoRepository(MyString);
                var retorno = clienteRepository.DeleteALL(CodigoCliente);

                return retorno > 0;
            }

            return false;
        }
    }
}
