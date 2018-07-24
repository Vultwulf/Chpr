using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Chpr.DataLayer
{
    public class ServiceBase
    {
        public static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        public static DynamoDBContext Context = new DynamoDBContext(client);
    }
}