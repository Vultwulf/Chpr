using System;
using Amazon.DynamoDBv2.DataModel;

namespace Chpr.DataLayer.Entities
{
    [DynamoDBTable("ChprPost")]
    public class Post
    {
        [DynamoDBHashKey]
        public string Guid { get; set; }
        [DynamoDBProperty]
        public string UserName { get; set; }
        [DynamoDBProperty]
        public string Text { get; set; }
        [DynamoDBProperty]
        public DateTime DatePosted { get; set; }
    }
}