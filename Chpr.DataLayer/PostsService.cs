using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Chpr.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chpr.DataLayer
{
    public class PostsService : ServiceBase
    {
        /// <summary>
        ///  AddPost will accept a Post object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(Post post)
        {
            // Set the guuid for this post
            post.Guid = Guid.NewGuid().ToString();

            // Set the datePosted to UtcNow
            post.DatePosted = DateTime.UtcNow;

            Context.Save<Post>(post);
        }

        /// <summary>
        /// GetPost will perform a Load for specified uuid
        /// </summary>
        /// <returns>Single of Post</returns>
        public Post GetPost(string uuid)
        {
            Post postRetrieved = Context.Load<Post>(uuid);
            return postRetrieved;
        }

        /// <summary>
        /// GetAllPosts will perform a Table Scan operation to return all the Posts
        /// </summary>
        /// <returns>Collection of Posts</returns>
        public IEnumerable<Post> GetAllPosts()
        {
            IEnumerable<Post> posts = Context.Scan<Post>();

            Collection<Post> orderedPosts = new Collection<Post>(posts
                .OrderByDescending(d => d.DatePosted)
                .ToList());

            return orderedPosts;
        }

        /// <summary>
        /// SearchPostsByUser will perform a Table Scan operation to return all the Posts based on UserName
        /// </summary>
        /// <param name="userName">The Username string</param>
        /// <returns>Collection of Posts</returns>
        public IEnumerable<Post> SearchPostsByUser(string userName)
        {
            IEnumerable<Post> filteredPosts = Context.Scan<Post>(new ScanCondition("UserName", ScanOperator.Equal, userName));

            return filteredPosts;
        }

        /// <summary>
        /// Delete Post will remove an item from DynamoDb
        /// </summary>
        /// <param name="uuid">Post uuid key</param>
        public void DeletePost(string uuid)
        {
            Context.Delete<Post>(uuid);
        }
    }
}