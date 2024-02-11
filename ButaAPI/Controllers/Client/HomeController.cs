using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions;
using ButaAPI.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ButaAPI.Controllers.Client
{
    [Authorize]
    [Route("homepage")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        ButaDbContext _butaDbContext;

        public HomeController(IUserService userService, ButaDbContext butaDbContext)
        {
            _userService = userService;
            _butaDbContext = butaDbContext;
        }

        #region Current User Info
        [HttpGet]
        [Route("user")]
        public IActionResult UserInfo()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var item = _userService.GetCurrentUser();

            return Ok();
        }
        #endregion

        #region Blogs
        [HttpGet]
        [Route("blogs")]
        public IActionResult GetBlogs()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            return Ok();
        }

        [HttpPost]
        [Route("add_blog")]
        public IActionResult AddBlog([FromBody] AddBlogViewModel addBlogViewModel)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            if (addBlogViewModel.Image != null || addBlogViewModel.Content != null)
            {
                Blog blog = new Blog
                {
                    OwnerId = user.Id,
                    Content = addBlogViewModel.Content,
                    Location = addBlogViewModel.Location,
                    Tags = addBlogViewModel.Tags,
                    Image = addBlogViewModel.Image,
                    DateTime = addBlogViewModel.DateTime
                };
                _butaDbContext.Add(blog);
                _butaDbContext.SaveChanges();
                return Ok();
            }
            if (null == addBlogViewModel.Image && null == addBlogViewModel.Content) { ModelState.AddModelError("Empty", "Content or Image can not be empty"); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return BadRequest();
        }

        [HttpPost]
        [Route("delete_blog")]
        public IActionResult DeleteBlog([FromBody] int blogId)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            var item = _butaDbContext.Blogs.FirstOrDefault(b => b.Id == blogId && b.OwnerId == user.Id);

            if (item != null)
            {
                _butaDbContext.Remove(item);
                _butaDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("user/blogs")]
        public IActionResult CurrentUserBlogs()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            var blogs = _butaDbContext.Blogs.Where(blog => blog.OwnerId == user.Id);

            return Ok(blogs);
        }

        [HttpPost]
        [Route("blogs/{tags}")]
        public IActionResult GetBlogsWithTags(string[] tags)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            if (tags != null && tags.Length > 0)
            {
                var blogs = _butaDbContext.Blogs.Where(blog => blog.Tags.Any(tag => tags.Contains(tag)) && blog.OwnerId != user.Id && blog.IsPublic == true);
                return Ok(blogs);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add_comment")]
        public IActionResult AddComment([FromBody] int blogId, string content)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            if (blogId > 0 && content != null)
            {
                Comment comment = new Comment
                {
                    Content = content,
                    BlogId = blogId,
                    OwnerId = user.Id,
                    DateTime = DateTime.UtcNow
                };
                _butaDbContext.Add(comment);
                _butaDbContext.SaveChanges();
                return Ok();
            }
            ModelState.AddModelError("Empty", "Content can not be empty");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("blogs/comment")]
        public IActionResult GetBlogComment(int blogId)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            var comments = _butaDbContext.Comments.Where(comment => comment.BlogId == blogId).ToList();

            if (comments.Count > 0) return Ok(comments);
            return NotFound();
        }

        #endregion

        #region Send Message

        [HttpPost]
        [Route("send_message")]
        public IActionResult SendMessage([FromBody] MessageViewModel messageViewModel)
        {
            if (null == messageViewModel.Message) { ModelState.AddModelError("Message", "Message field cannot be empty. Please enter a message."); }
            if (messageViewModel.Id <= 0) { ModelState.AddModelError("Receiver", "Recipient not selected."); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

                 Message message = new Message
                 {
                     Content = messageViewModel.Message,
                     SendingTime = DateTime.UtcNow,
                     SenderId = user.Id,
                     ReceiverId = messageViewModel.Id
                 };
                 _butaDbContext.Add(message);
                 _butaDbContext.SaveChanges();
                 return Ok();
        }

        [HttpGet]
        [Route("messages")]
        public IActionResult Messages()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var messages = _butaDbContext.Messages.Where(m => m.ReceiverId == user.Id).ToList();

            return Ok(messages);
        }

        #endregion

        #region Send Friendship Request

        [HttpPost]
        [Route("send_friendship_request")]
        public IActionResult SendFriendshipRequest([FromBody] int userId)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            if (userId != 0)
            {
                FriendshipRequest request = new FriendshipRequest
                {
                    UserId = user.Id,
                    FriendsId = userId,
                    DateTime = DateTime.UtcNow
                };
                _butaDbContext.Add(request);
                _butaDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("show_friendship_request")]
        public IActionResult ShowFriendshipRequest()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var requests = _butaDbContext.FriendshipsRequests.Where(r => r.FriendsId == user.Id).ToList();

            return Ok(requests);
        }

        [HttpPost]
        [Route("response_friendship_request")]
        public IActionResult ResponseFriendshipRequest([FromBody] ResponseViewModel responseViewModel)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var requests = _butaDbContext.FriendshipsRequests.Where(r => r.FriendsId == user.Id && responseViewModel.Id == r.UserId).ToList();

            if (responseViewModel.Response)
            {
                    Friendships friendship = new Friendships
                    {
                       User1Id = user.Id,
                       User2Id = requests.Single().UserId
                    };

                    _butaDbContext.Friendships.Add(friendship);
                    _butaDbContext.SaveChanges();

                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("show_friends")]
        public IActionResult ShowFriends()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var friends = _butaDbContext.Friendships.Where(f => f.User1Id == user.Id).ToList(); 
           
            return NotFound(friends);
        }
        #endregion
    }
}