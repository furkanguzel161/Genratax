

using System.Security.Claims;
using Genratax.Data.Abstrack;
using Genratax.Data.Concrete.EfCore;
using Genratax.Entity;
using Genratax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Genratax.Controllers
{
    public class PostsController:Controller
    {
   
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;



        public PostsController(IPostRepository postRepository,ICommentRepository commentRepository){
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public IActionResult IndexAsync(){
            var claims = User.Claims;
            return View( 
            new PostsViewModel{
              Posts =  _postRepository.Posts.ToList(),
            }
            );
             
        }
 

        public async Task<IActionResult>Thread(string url){
            return View(await _postRepository
            .Posts
            .Include(x=>x.Comments)
            .ThenInclude(x=>x.User)
            .Include(x=>x.User)
            .FirstOrDefaultAsync(p=>p.Url == url));
        }
        [HttpPost]
        [Authorize]
        public JsonResult AddComment(int PostId,string Text){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var entity = new Comment{
                Text = Text,
                PostId = PostId,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(entity);

            return Json(new{
                   username,
                    Text,
                 
                    
            });

        }
        [Authorize]
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreatePostViewModel model){
            var postsconfirm = _postRepository.Posts.FirstOrDefault(x=>x.Title == model.Title);
            
            if(ModelState.IsValid && postsconfirm == null){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _postRepository.CreatePost(
                    new Post{
                        Title = model.Title,
                        Content = model.Content,
                        Url = model.Title,
                        UserId = int.Parse(userId ?? ""),
                        PublisedOn = DateTime.Now,
                        Image = model.Image,
                        IsActive = true
                    }
                
                );

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Delete(int? id){
            var post = _postRepository.Posts.FirstOrDefault(i=> i.PostId == id);
                        if(id == null){
                return NotFound();

            }
            if(post == null){
                return NotFound();
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(post.UserId != int.Parse(userId ?? "")){
                return NotFound();
            }
        if(ModelState.IsValid){

                _postRepository.DeletePost(post!);
                }
                
            return RedirectToAction("Profile","User");
        
        }

        [Authorize]
        public IActionResult DeleteComment(int? id){
            var comments = _commentRepository.Comments.FirstOrDefault(i=> i.CommentId == id);
            if(id == null){
                return NotFound();

            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(comments!.UserId != int.Parse(userId ?? "")){
                return NotFound();
            }
        if(ModelState.IsValid){

                _commentRepository.DeleteComment(comments!);
                }
                
            return RedirectToAction("Profile","User");
        
        }

        [Authorize]
        public IActionResult Edit(int? id){
            
            if(id == null){
                return NotFound();

            }
            var post = _postRepository.Posts.FirstOrDefault(i=> i.PostId == id);
            if(post == null){
                return NotFound();
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(post.UserId != int.Parse(userId ?? "")){
                return NotFound();
            }

            return View(new CreatePostViewModel{
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Image = post.Image,
                Url = post.Url
            
            });
        }

        [Authorize]
        public IActionResult EditComment(int? id){
            
            if(id == null){
                return NotFound();

            }
            var comment = _commentRepository.Comments.FirstOrDefault(i=> i.CommentId == id);
            if(comment == null){
                return NotFound();
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(comment.UserId != int.Parse(userId ?? "")){
                return NotFound();
            }

            return View(new CreateCommentsViewModel{
                CommentId = comment.CommentId,
                Text = comment.Text,
            
            });
        } 
        [Authorize]
        [HttpPost]
        public IActionResult EditComment(CreateCommentsViewModel model){
            if(ModelState.IsValid){
                var entityUpdate = new Comment{
                CommentId = model.CommentId,
                Text = model.Text

                };
                _commentRepository.EditComment(entityUpdate);
               
            } 
            return RedirectToAction("profilecommented","User");
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(CreatePostViewModel model){
            if(ModelState.IsValid){
                var entityUpdate = new Post{
                PostId = model.PostId,
                Title = model.Title,
                Content = model.Content,
                Image = model.Image,
                Url = model.Url

                };
                _postRepository.EditPost(entityUpdate);
               
            } 
            return RedirectToAction("Profile","User");
        }
    }

}