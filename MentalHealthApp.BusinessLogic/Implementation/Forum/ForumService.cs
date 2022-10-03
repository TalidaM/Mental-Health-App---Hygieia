using MentalHealthApp.BusinessLogic.Base;
using MentalHealthApp.BusinessLogic.Implementation.Forum.Validations;
using MentalHealthApp.BusinessLogic.Implementation.Forum.ViewModels;
using MentalHealthApp.Common.DTOs;
using MentalHealthApp.Common.Extensions;
using MentalHealthApp.Entities;
using MentalHealthApp.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation
{
    public class ForumService : BaseService
    {
        private readonly DiscussionValidator _discussionValidator;
        public ForumService(ServiceDependencies dependencies) 
                            : base(dependencies) 
        {
            _discussionValidator = new DiscussionValidator();
        }
        public string GetRole(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.IdentityRoles.Get()
                                        .Include(r => r.Users)
                                        .ThenInclude(ir => ir.Roles)
                                        .Where(r => r.Users.Select(u => u.Id).Contains(id))
                                        .Select(r => r.Name)
                                        .Single();
            });
        }
        public List<DiscussionVM> GetAllDiscussions(int pages, int numberOnPage)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Discutii.Get()
                                    .Select(d => new DiscussionVM
                                    {
                                        Id = d.Id,
                                        UserId = d.UserId,
                                        Username = uow.IdentityUsers.Get()
                                                         .Where(u => u.Id.Equals(d.UserId))
                                                          .Select(u => $"{u.LastName} {u.FirstName}")
                                                          .Single(),
                                        Titlu = d.Titlu,
                                        Continut = d.Continut,
                                        DataCreare = d.DataCreare,
                                        UserImage = uow.IdentityUsers.Get().Where(u => u.Id.Equals(d.UserId)).Select(u => u.UserImage).Single(),
                                    })
                                    .Skip(pages).Take(numberOnPage)
                                    .OrderByDescending(od => od.DataCreare)
                                   .ToList();  
            });
        }

        public DiscussionVM GetDiscussionById(Guid id)
        {
            
                return ExecuteInTransaction(uow =>
                {
                    return uow.Discutii.Get()
                           .Where(d => d.Id.Equals(id))
                           .Select(d => new DiscussionVM
                           {
                               Id = d.Id,
                               UserId = d.UserId,
                               Username = uow.IdentityUsers.Get()
                                                         .Where(u => u.Id.Equals(d.UserId))
                                                          .Select(u => $"{u.LastName} {u.FirstName}")
                                                          .Single(),
                               Titlu = d.Titlu,
                               Continut = d.Continut,
                               DataCreare = d.DataCreare,
                               CommentReaction = d.CommentReaction,
                               UserImage = uow.IdentityUsers.Get().Where(u => u.Id.Equals(d.UserId)).Select(u => u.UserImage).Single(),
                               
                           })
                          .Single();
                });
           
           
        }

        public void CreateDiscussion(CreateDiscussionVM newDiscussion)
        {
           // _discussionValidator.Validate(newDiscussion).ThenThrow(newDiscussion);
             ExecuteInTransaction(uow =>
            {
                var discussion = new Discutie();
                discussion.Id = Guid.NewGuid();
                discussion.UserId = (Guid)CurrentUser.Id;
                discussion.Titlu = newDiscussion.Titlu;
                discussion.Topic = newDiscussion.Topic;
                discussion.CommentReaction = 0;
                discussion.Continut = newDiscussion.Continut;
                discussion.DataCreare = DateTime.UtcNow;
                uow.Discutii.Insert(discussion);
                uow.SaveChanges();

            });
        }

        public void CreateDiscussionComment(DiscussionVM newComment)
        {
            ExecuteInTransaction(uow =>
            {
                var comment = new ComentariiDiscutie();
                comment.Id = Guid.NewGuid();
                comment.UserId = (Guid)CurrentUser.Id;
                comment.DiscutieId = newComment.Id;
                comment.Continut = newComment.Continut;
                comment.CommentReaction = 0;
                comment.DataComentariu = DateTime.UtcNow;
                uow.ComentariiDiscutii.Insert(comment);
                uow.SaveChanges();
            });
        }
        public List<CreateDiscussionCommentsVM> ViewDiscussionComments(Guid discussionId)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.ComentariiDiscutii.Get()
                                             .Include(cd => cd.Discutie)
                                             .Include(cd => cd.User)
                                             .Where(cd => cd.DiscutieId.Equals(discussionId))
                                             .Select(cd => new CreateDiscussionCommentsVM
                                             {
                                                 Id = cd.Id,
                                                 UserId = cd.UserId,
                                                 DiscussionTitle = cd.Discutie.Titlu,
                                                 Username = uow.IdentityUsers.Get()
                                                         .Where(u => u.Id.Equals(cd.UserId))
                                                          .Select(u => $"{u.LastName} {u.FirstName}")
                                                          .Single(),
                                                 Continut = cd.Continut,
                                                 DataComentariu = cd.DataComentariu,
                                                 CommentReaction = cd.CommentReaction,
                                                 UserImage = uow.IdentityUsers.Get().Where(u => u.Id.Equals(cd.UserId)).Select(u => u.UserImage).Single(),
                                             })
                                             .OrderBy(u => u.DataComentariu)
                                             .ToList();
            });
        }
        public int NumberOfComments(Guid discussionId)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.ComentariiDiscutii.Get()
                                             .Include(cd => cd.Discutie)
                                             .Include(cd => cd.User)
                                             .Where(cd => cd.DiscutieId.Equals(discussionId))
                                             .Count();
                                  
            });
        }
        public async Task<ComentariiDiscutie> DeteleComment(Guid id)
        {
            
            return ExecuteInTransaction(uow =>
            {
                
                    var comment = uow.ComentariiDiscutii.Get()
                                                    .Where(cd => cd.Id.Equals(id))
                                                    .Single();
                    uow.ComentariiDiscutii.Delete(comment);
                    uow.SaveChanges();
                return comment;
                    
            });
        }
        public async Task<Discutie> DeleteDiscussion (Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var discussion = uow.Discutii.Get()
                                              .Include(d => d.ComentariiDiscuties)
                                              .Where(uow => uow.Id.Equals(id))
                                              .Single();
                uow.Discutii.Delete(discussion);
                uow.SaveChanges();
                return discussion;
            });
        }
        public string EditComment(Guid id, string message)
        {
            return ExecuteInTransaction(uow =>
            {
                var comment = uow.ComentariiDiscutii.Get()
                                                          .Where(cd => cd.Id.Equals(id))
                                                          .SingleOrDefault();
                comment.Continut = message;
                uow.ComentariiDiscutii.Update(comment);
                uow.SaveChanges();
                return message;
            });
        }
        public int AddDiscussionReaction(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var comment = uow.Discutii.Get()
                                        .Where(cd => cd.Id == id)
                                        .Single();
        
                comment.CommentReaction += 1;
                uow.Discutii.Update(comment);
                uow.SaveChanges();
                return comment.CommentReaction;
            });
        }
        public int AddReaction(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var comment = uow.ComentariiDiscutii.Get()
                                                          .Where(cd => cd.Id.Equals(id))
                                                          .SingleOrDefault();
                comment.CommentReaction += 1;
                uow.ComentariiDiscutii.Update(comment);
                uow.SaveChanges();
                return comment.CommentReaction;
            });
        }
        public int DeleteReaction(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var comment = uow.ComentariiDiscutii.Get()
                                                          .Where(cd => cd.Id.Equals(id))
                                                          .SingleOrDefault();
                comment.CommentReaction -= 1;
                uow.ComentariiDiscutii.Update(comment);
                uow.SaveChanges();
                return comment.CommentReaction;
            });
        }
        public string EditDiscussionContent(Guid id, string message)
        {
            return ExecuteInTransaction(uow =>
            {
                var discussion = uow.Discutii.Get()
                                             .Where(d => d.Id.Equals(id))
                                             .Single();
                discussion.Continut = message;
                uow.Discutii.Update(discussion);
                uow.SaveChanges();
                return message;
            });
        }
    }
}
