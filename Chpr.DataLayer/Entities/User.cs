using Amazon.DynamoDBv2.DataModel;

namespace Chpr.DataLayer.Entities
{
    [DynamoDBTable("ChprUser")]
    public class User
    {
        [DynamoDBHashKey]
        public string UserName { get; set; }
        [DynamoDBProperty]
        public System.DateTime JoinDate { get; set; }
        [DynamoDBProperty]
        public string Password { get; set; }
    }
}