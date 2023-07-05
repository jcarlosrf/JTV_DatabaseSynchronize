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

            clienteRepository = new Infra.Data.MySql.EmpresaImportacaoRepository(MyString);
        }

        public EmpresaImportacao ConfiguracoesCliente(int codigoCliente)
        {
            return clienteRepository.GetEntity(codigoCliente);
        }

        public bool UpdateDhAlteracao(int codigoCliente , DateTime DhAtualizacao)
        {
            var retorno = clienteRepository.UpdateDataHora(codigoCliente, DhAtualizacao);
            return retorno > 0;
        }


    }
}
