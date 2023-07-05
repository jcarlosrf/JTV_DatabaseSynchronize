using System;
using FirebirdSql.Data.FirebirdClient;

namespace Scire.JTV.Infra.Data.Firebird
{
    public class FirebirdContext 
    {
        public readonly string ConnectionString; 

        public FirebirdContext (string stringConnection )
        {
            ConnectionString = string.Format(
                "{0};" +
                "Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;" 
                , stringConnection);

        }

        public FbConnection GetConnection()
        {
            return new FbConnection(ConnectionString);
        }

        public bool TestConnection()
        {
            var MyConnection = GetConnection();
            try
            {
                if (MyConnection.State == System.Data.ConnectionState.Open)
                    MyConnection.Close();

                MyConnection.Open();

                if (MyConnection.State == System.Data.ConnectionState.Open)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (MyConnection.State == System.Data.ConnectionState.Open)
                    MyConnection.Close();
            }
        }

    }
}
