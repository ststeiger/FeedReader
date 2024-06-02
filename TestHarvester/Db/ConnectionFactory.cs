namespace TestHarvester.Db
{

    public delegate string GetConnectionString_t(System.Data.Common.DbProviderFactory factory);


    public class ConnectionFactory
    {
        protected System.Data.Common.DbProviderFactory m_factory;

        protected GetConnectionString_t m_getConnectionString;


        public static System.Data.Common.DbProviderFactory GetFactory(System.Type type)
        {
            if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
            {
                // Provider factories are singletons with the instance field having the sole instance 
                System.Reflection.FieldInfo field = type.GetField("Instance",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                );

                if (field != null)
                {
                    // return field.GetValue(null) as System.Data.Common.DbProviderFactory;
                    // We actually want GetFactory to throw if the type is not a subtype of DbProviderFactory
                    return (System.Data.Common.DbProviderFactory)field.GetValue(null);
                } // End if (field != null)

            } // End if (type != null && type.IsSubclassOf(typeof(DbProviderFactory)))

            throw new System.Exception("DataProvider is missing!");
        } // End Function GetFactory


        public static ConnectionFactory CreateInstance(System.Type t, GetConnectionString_t getConnectionString)
        {
            if (!t.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
                throw new System.ArgumentException("Type \"" + t.FullName +
                                                   "\" is not of type System.Data.Common.DbProviderFactory.");

            System.Data.Common.DbProviderFactory factory = GetFactory(t);
            return new ConnectionFactory(factory, getConnectionString);
        } // End Function CreateInstance 


        public static ConnectionFactory CreateInstance<T>(GetConnectionString_t getConnectionString)
            where T : System.Data.Common.DbProviderFactory
        {
            return CreateInstance(typeof(T), getConnectionString);
        } // End Function CreateInstance 


        public static ConnectionFactory CreateInstance(
            string providerAssembly,
            GetConnectionString_t getConnectionString)
        {
            System.Type type = null;

            try
            {
                if (!string.IsNullOrEmpty(providerAssembly))
                {
                    int commaPos = providerAssembly.IndexOf(",");
                    if (commaPos != -1)
                    {
                        providerAssembly = providerAssembly.Substring(0, commaPos);
                    }
                }


                System.Reflection.Assembly asm = System.Linq.Enumerable.FirstOrDefault(
                    System.AppDomain.CurrentDomain.GetAssemblies(), a => a.GetName().Name == providerAssembly);

                if (asm == null)
                    asm = System.Reflection.Assembly.Load(providerAssembly);

                if (asm != null)
                {
                    System.Console.WriteLine("Assembly: {0}", asm.FullName);

                    type = System.Linq.Enumerable.First(
                        System.Linq.Enumerable.Where(
                            asm.GetTypes(), t => t.BaseType == typeof(System.Data.Common.DbProviderFactory)
                        ));
                } // End if (asm != null) 
                else
                    System.Console.WriteLine("asm IS NULL !");
            } // End Try 
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error fetching DbProviderFactory from providerAssembly \"" + providerAssembly + "\".");
                System.Console.WriteLine(System.Environment.NewLine);

                System.Exception innerException = ex;
                int lvl = 0;
                do
                {
                    string indent = new string(' ', lvl * 2);

                    System.Console.WriteLine(System.Environment.NewLine);
                    System.Console.Write(indent);
                    System.Console.WriteLine("Message: ");
                    System.Console.Write(indent);
                    System.Console.WriteLine(innerException.Message);
                    System.Console.WriteLine();
                    System.Console.Write(indent);
                    System.Console.WriteLine("Stacktrace: ");
                    System.Console.Write(indent);
                    System.Console.WriteLine(innerException.StackTrace);
                    innerException = innerException.InnerException;
                    lvl++;
                } while (innerException != null);

            } // End Catch 

            if (type == null)
            {
                System.Console.WriteLine("Warning ! Undefined factory type in ConnectionFactory.CreateInstance.");
                type = typeof(System.Data.SqlClient.SqlClientFactory);
                System.Console.WriteLine("Assuming ConnectionFactory.CreateInstance-default of \"" + type.FullName + "\".");
            } // End if (type == null) 

            return CreateInstance(type, getConnectionString);
        } // End Function CreateInstance 


        public ConnectionFactory(System.Data.Common.DbProviderFactory factory,
            GetConnectionString_t getConnectionString)
        {
            m_factory = factory;
            m_getConnectionString = getConnectionString;
        } // End Constructor 


        public string ConnectionString
        {
            get { return m_getConnectionString(m_factory); }
        } // End Property ConnectionString 


        public System.Data.Common.DbConnection Connection
        {
            get
            {
                System.Data.Common.DbConnection cnn = m_factory.CreateConnection();
                cnn.ConnectionString = ConnectionString;

                return cnn;
            }
        } // End Property Connection 


    } // End Class ConnectionFactory 


} // End Namespace SSRS_Manager 
