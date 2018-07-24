using System.Collections.Generic;
using System.Web.Http;
using Chpr.DataLayer;
using Chpr.DataLayer.Entities;

namespace Chpr.Controllers
{
  public class PostsController : ApiController
  {
    // GET: api/Posts
    public IEnumerable<Post> Get()
    {
      PostsService postsService = new PostsService();
      IEnumerable<Post> posts = postsService.GetAllPosts();
      return posts;
    }

    // POST: api/Posts
    [Authorize]
    public void Post([FromBody]Post post)
    {
      PostsService postsService = new PostsService();

      // If the post exists, submit the post
      if (post != null)
      {
        // If the Post is longer than 140 characters, truncate to the first 140
        if (post.Text.Length > 140)
        {
          post.Text = post.Text.Substring(0, 140);
        }

        postsService.AddPost(post);
      }
    }
  }
}
