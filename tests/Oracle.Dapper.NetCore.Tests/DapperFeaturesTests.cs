using Oracle.Dapper.NetCore.Dapper;
using Oracle.Dapper.NetCore.Tests.Fakes;
using System.Data;

namespace Oracle.Dapper.NetCore.Tests
{
    public class DapperFeaturesTests
    {
        private static FakeDbConnection CreateConnection(params string[] days)
        {
            var table = new DataTable();
            table.Columns.Add("DIA", typeof(string));
            foreach (var day in days)
            {
                table.Rows.Add(day);
            }

            var connection = new FakeDbConnection { Result = table };
            connection.Open();
            return connection;
        }

        [Fact]
        public void QueryFirstOrDefault_Generic_MapsToRequestedType()
        {
            var features = new DapperFeatures(CreateConnection("SEGUNDA", "TERCA"));

            var result = features.QueryFirstOrDefault<string>("SELECT DIA FROM TESTE");

            Assert.Equal("SEGUNDA", result);
        }

        [Fact]
        public void QueryFirstOrDefault_Generic_ReturnsDefaultWhenEmpty()
        {
            var features = new DapperFeatures(CreateConnection());

            Assert.Null(features.QueryFirstOrDefault<string>("SELECT DIA FROM TESTE"));
        }

        [Fact]
        public void Query_Generic_MapsAllRows()
        {
            var features = new DapperFeatures(CreateConnection("SEGUNDA", "TERCA"));

            var result = features.Query<string>("SELECT DIA FROM TESTE");

            Assert.Equal(new[] { "SEGUNDA", "TERCA" }, result);
        }

        [Fact]
        public async Task QueryAsync_Generic_MapsAllRows()
        {
            var features = new DapperFeatures(CreateConnection("SEGUNDA", "TERCA"));

            var result = await features.QueryAsync<string>("SELECT DIA FROM TESTE");

            Assert.Equal(new[] { "SEGUNDA", "TERCA" }, result);
        }
    }
}
