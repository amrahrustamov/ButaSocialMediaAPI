using ButaAPI.Database;
using ButaAPI.Database.Model;
using ButaAPI.Database.ViewModel;
using ButaAPI.Exceptions;
using ButaAPI.Services.Abstracts;
using ButaAPI.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using static System.Net.Mime.MediaTypeNames;

namespace ButaAPI.Controllers.Client
{
   
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ButaDbContext _butaDbContext;

        public HomeController(IUserService userService, ButaDbContext butaDbContext)
        {
            _userService = userService;
            _butaDbContext = butaDbContext;
        }

        #region Blogs
        [HttpGet]
        [Route("user_id")]
        public IActionResult GetUserId()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            return Ok(user.Id);
        }

        [HttpPost]
        [Route("all_blogs")]
        public IActionResult GetAllBlogs()
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            var allBlog = _butaDbContext.Blogs.OrderByDescending(b => b).ToList();
            var data = new List<BlogViewModel>();

            foreach (var blog in allBlog)
            {
                BlogViewModel blogViewModel = new BlogViewModel
                {
                    OwnerId = blog.OwnerId,
                    Owner = _butaDbContext.Users.FirstOrDefault(u => u.Id == blog.OwnerId),
                    OwnerFullName = user.FirstName + " " + user.LastName,
                    Content = blog.Content,
                    DateTime = blog.DateTime,
                    Image = blog.Image,
                    Tags = blog.Tags,
                    IsPublic = blog.IsPublic,
                    Id = blog.Id,
                    Commets = blog.Commets,
                    Likes = blog.Likes,
                    Location = blog.Location
                };
                data.Add(blogViewModel);
            }

            return Ok(data);
        }
        [HttpGet]
        [Route("images/{name}")]
        public async Task<IActionResult> Images(string name)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", name);

            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.ReadAllBytes(path), "image/jpeg");
            }
            var defaultImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Default", name);
            if (System.IO.File.Exists(defaultImage))
            {
                return File(System.IO.File.ReadAllBytes(defaultImage), "image/jpeg");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add_blog")]
        public async Task<IActionResult> AddBlog()
        {
            var files = Request.Form.Files;
            var form = Request.Form.ToList();
            StringValues stringListStringValues = form.FirstOrDefault(f=>f.Key == "tags").Value.ToString();
            string stringListString = stringListStringValues.FirstOrDefault();
            List<string> stringList = stringListString?.Split(',').ToList() ?? new List<string>();


            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            var visibility = form.FirstOrDefault(v => v.Key == "isPublic").Value;

            Blog blog = new Blog
            {
                OwnerId = user.Id,
                Content = form.FirstOrDefault(d => d.Key == "text").Value,
                DateTime = DateTime.UtcNow.Date,
                Image = new List<string>(),
                Tags = stringList,
                IsPublic = visibility.ToString() == "public",
            };

            if (files != null || blog.Content != null)
            {
                if (files != null)
                {
                    foreach (var item in files)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", fileName);
                        using var fileStream = new FileStream(path, FileMode.Create);
                        item.CopyTo(fileStream);
                        blog.Image.Add(fileName);
                    }
                }
                _butaDbContext.Add(blog);
                _butaDbContext.SaveChanges();
                return Ok();
            }
            if (null == files && null == blog.Content) { ModelState.AddModelError("Empty", "Content or Image can not be empty"); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return BadRequest();
       }

        [HttpDelete]
        [Route("delete_blog/{id}")]
        public IActionResult DeleteBlog(int id)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();
            var item = _butaDbContext.Blogs.FirstOrDefault(b => b.Id == id && b.OwnerId == user.Id);

            if (item != null)
            {
                foreach(var i in item.Image)
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Images", i);
                    System.IO.File.Delete(oldPath);
                }

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
        [Route("add_like")]
        public IActionResult AddLike([FromBody] int blogId)
        {
            if (!_userService.IsCurrentUserAuthenticated()) return NotFound();
            var user = _userService.GetCurrentUser();

            Like liked = new Like
            {
                OwnerId = user.Id,
                BlogId=blogId,
                DateTime=DateTime.UtcNow
            };
            _butaDbContext.Likes.Add(liked);
            _butaDbContext.SaveChanges();

            return Ok();
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

            if (userId > 0)
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