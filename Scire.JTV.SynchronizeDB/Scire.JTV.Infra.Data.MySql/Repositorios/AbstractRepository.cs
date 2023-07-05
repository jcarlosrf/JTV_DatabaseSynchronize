using System;
using MySql.Data.MySqlClient;

namespace Scire.JTV.Infra.Data.MySql
{
    public abstract class AbstractRepository
    {
        public ScireDbContext _context { get; set; }
        
        public string MyConnection { get; set; }
        
        public void CreateConnection(string conexao)
        {
            var dados = conexao.Split(';');

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = dados[0].Split('=')[1].ToString(),
                Port = uint.Parse(dados[1].Split('=')[1].ToString()),
                Database = dados[2].Split('=')[1].ToString(),
                UserID = dados[3].Split('=')[1].ToString(),
                Password = dados[4].Split('=')[1].ToString()
                ,
                PersistSecurityInfo = false
                , SslMode = MySqlSslMode.None
                , ConvertZeroDateTime = true
            };

            MyConnection = builder.ConnectionString;            
        }

        public bool TestaConexao()
        {
            MySqlConnection conectabd = new MySqlConnection(MyConnection);

            try
            {
                conectabd.Open();

                if (conectabd.State == System.Data.ConnectionState.Open)
                {
                    conectabd.Close();
                    return true;

                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conectabd.State == System.Data.ConnectionState.Open)
                {
                    conectabd.Close();
                }
            }
        }
    }
}
