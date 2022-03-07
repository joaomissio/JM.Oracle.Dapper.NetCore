namespace Oracle.Dapper.NetCore.Context
{
    internal class ProviderContext
    {
        public string Provider { get; internal set; }

        public string ConnectionString { get; internal set; }

        public ProviderContext(string provider, string connectionString)
        {
            this.Provider = provider;
            this.ConnectionString = connectionString;
        }
    }
}
