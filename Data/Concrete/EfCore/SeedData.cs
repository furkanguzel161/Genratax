using Microsoft.EntityFrameworkCore;
using Genratax.Entity;
namespace Genratax.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void FillTestData(IApplicationBuilder app){
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ForumContext>();
            if (context != null){
                if (context.Database.GetPendingMigrations().Any()){
                    context.Database.Migrate();
                }
            }
            if (!context.Users.Any()){
                context.Users.AddRange(
                    new User {UserName = "User1",Email = "user1@gmail.com",Password = "user1user1"},
                    new User {UserName = "User2",Email = "user2@gmail.com",Password = "user2user2"}
                );
                context.SaveChanges();
            }
            if (!context.Posts.Any()){
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "figgit",
                        Content = "Yeni tur figgit!",
                        Url = "figgit",
                        Image = "ddd.png",
                        IsActive = true,
                        PublisedOn= DateTime.Now.AddDays(-5),
                        UserId = 1,
                        Comments = new List<Comment>{
                            new Comment{Text = "test 1343423",PublishedOn = new DateTime(),UserId=1},
                            new Comment{Text = "dummytest 33333",PublishedOn = new DateTime(),UserId=2}
                        }
                    },
                    new Post
                    {
                        Title = "dddddd",
                        Content = "Yeni tur figgitdddddddddddddddff fffffffffffffffffff ffffffffffff ffffffffffffff ddddddddddddd ddddddddddddddd ddddd ",
                        Url = "ddddd",
                        Image = "ddd.png",
                        IsActive = true,
                        PublisedOn= DateTime.Now.AddDays(-5),
                        UserId = 1,
                        Comments = new List<Comment>{
                            new Comment{Text = "test 1343423",PublishedOn = new DateTime(),UserId=1},
                            new Comment{Text = "dummytest 33333",PublishedOn = new DateTime(),UserId=2}
                        }
                    },
                    new Post
                    {
                        Title = "aaaaaa",
                        Content = "Yeni tur figaaaaaaaaaaaaaaaaaa aaaaaaaaaaaaaa aaaaaaaaaaaaaaa aaaaaaaaaaaaaaaa aaaaaaaaaaa aaaaaaaaaaaaaa aaaaaaaaaaa aaaaaaaaaaa aaaaaaaa ddddddddddddd ddddddddddddddd ddddd ",
                        Url = "aaaaaa",
                        Image = "ddd.png",
                        IsActive = true,
                        PublisedOn= DateTime.Now.AddDays(-5),
                        UserId = 1,
                        Comments = new List<Comment>{
                            new Comment{Text = "test 1343423",PublishedOn = new DateTime(),UserId=1},
                            new Comment{Text = "dummytest 33333",PublishedOn = new DateTime(),UserId=2}
                        }
                    }
                );
                context.SaveChanges();
            }
            
        }
    }
}