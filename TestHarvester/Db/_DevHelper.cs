namespace TestHarvester.Db
{


    internal class DevHelper
    {

        internal static ConnectionFactory GetTestFactory()
        {
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder csb =
                    new Microsoft.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName;
            csb.InitialCatalog = "abc";

            csb.PersistSecurityInfo = false;
            csb.PacketSize = 4096;
            csb.Encrypt = false;

            csb.ApplicationName = "HubSpotClient";
            csb.Pooling = false;
            csb.IntegratedSecurity = true;

            string cs = csb.ConnectionString;
            System.Console.WriteLine(cs);

            ConnectionFactory factory = ConnectionFactory.CreateInstance(
                typeof(Microsoft.Data.SqlClient.SqlClientFactory),
                delegate
                {
                    return cs;
                }
            );


            //ConnectionFactory factory = new ConnectionFactory(System.Data.SqlClient.SqlClientFactory.Instance, delegate (System.Data.Common.DbProviderFactory factory) 
            //{
            //    return cs;
            //});

            return factory;
        }

    }


}
