using MentalHealthApp.BusinessLogic.Implementation;
using MentalHealthApp.BusinessLogic.Implementation.Admin;
using MentalHealthApp.BusinessLogic.Implementation.Forum.ViewModels;
using MentalHealthApp.Entities.Enums;
using MentalHealthApp.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace MentalHealthApp.WebApp.Controllers
{
    public class ForumController : BaseController
    {
        private readonly ForumService _forumService;
        private readonly HomePageService _homeService;
       public ForumController(ControllerDependencies dependencies,
                              ForumService forumService,
                              HomePageService homePageService)
                             : base(dependencies)
        { 
            _forumService = forumService;
            _homeService = homePageService;
        }



        [HttpGet]
        public IActionResult ViewForumFirstPage(int page)
        {
            if(page < 0)
            {
                page = 0;
            }
            int pageSize = 6;
            var conditions = _homeService.GetConditionsName();
            var discussionsList = _forumService.GetAllDiscussions(page*pageSize, pageSize);
            var model = new CreateDiscussionVM()
            {
                DiscussionVMs = discussionsList.Select(app => new DiscussionVM
                {
                    UserId = app.UserId,
                    Username = app.Username,
                    Id = app.Id,
                    Titlu = app.Titlu,
                    Continut = app.Continut,
                    DataCreare = app.DataCreare,
                    UserImage = app.UserImage,

                }).ToList(),
                Conditions = conditions.ToList(),
                Page = page

            };

            return View("ForumFirstPage", model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscussion(CreateDiscussionVM model)
        {

            _forumService.CreateDiscussion(model);
           // return View("ForumFirstPage", model);
            return RedirectToAction("ViewForumFirstPage");
        }

/// 
        [HttpGet]
        public IActionResult ViewDiscussion(Guid id)
        {
            var discussionDetails = _forumService.GetDiscussionById(id);
            var comments = _forumService.ViewDiscussionComments(id);
            var model = new DiscussionVM()
            {
                Id = discussionDetails.Id,
                UserId = discussionDetails.UserId,
                Username = discussionDetails.Username,
                Titlu = discussionDetails.Titlu,
                Continut = discussionDetails.Continut,
                DataCreare = discussionDetails.DataCreare,
                UserImage = discussionDetails.UserImage,
                CommentReaction = discussionDetails.CommentReaction,
                createDiscussionCommentsVMs = comments.Select(app => new CreateDiscussionCommentsVM
                {
                    Id = app.Id,
                    UserId = app.UserId,
                    DiscutieId = id,
                    DiscussionTitle = app.DiscussionTitle,
                    Username = app.Username,
                    Continut = app.Continut,
                    DataComentariu = app.DataComentariu,
                    CommentReaction = app.CommentReaction,
                    UserImage = app.UserImage
                }).ToList()
            };
            return View("DiscussionPage", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscussionComment(DiscussionVM model)
        {
            if(!ModelState.IsValid)
            {
                return Json("Discutia nu este adecvata");
            }
            _forumService.CreateDiscussionComment(model);
            return RedirectToAction("ViewDiscussion", new {id = model.Id});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscussion(Guid id, Guid user)
        {
            var role =  _forumService.GetRole((Guid)CurrentUser.Id);
            if (user.Equals(CurrentUser.Id) || role.Equals(RoleTypes.Admin.ToString()))
            {
                var model = await _forumService.DeleteDiscussion(id);
                return Ok(model.Id);
            }
            return StatusCode(StatusCodes.Status401Unauthorized, "You can not proceed with the action");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid id, Guid user)
        {
            var role = _forumService.GetRole((Guid)CurrentUser.Id);
            if (user.Equals(CurrentUser.Id) || role.Equals(RoleTypes.Admin.ToString()))
            {
                var model = await _forumService.DeteleComment(id);
                return Ok(model.Id);
            }
            return StatusCode(StatusCodes.Status401Unauthorized);

        }

		[HttpGet]
        public IActionResult NumberOfComments(Guid id)
		{
            var comments =  _forumService.NumberOfComments(id);
            return Ok(comments);
		}
        [HttpPatch]
        public IActionResult EditComment(Guid id, string message, Guid user)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var role = _forumService.GetRole((Guid)CurrentUser.Id);
            if (user.Equals(CurrentUser.Id) || role.Equals(RoleTypes.Admin.ToString()))
            {
                var newMessage = _forumService.EditComment(id, message);
                return Ok(newMessage);
            }
            return Ok();
        }

        [HttpPatch]
        public IActionResult AdDiscussionReaction(Guid id)
        {
            _forumService.AddDiscussionReaction(id);
            return Ok();
        }


        [HttpPatch]
        public IActionResult AddCommentReaction(Guid id)
        {
            _forumService.AddReaction(id);
            return Ok();
        }
        [HttpPatch]
        public IActionResult DeleteReaction(Guid id)
        {

            _forumService.DeleteReaction(id);
            return Ok();
        }

        [HttpPatch]
        public IActionResult EditDiscussionMainComment(Guid id, string message, Guid user)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var role = _forumService.GetRole((Guid)CurrentUser.Id);
            if (user.Equals(CurrentUser.Id) || role.Equals(RoleTypes.Admin.ToString()))
            {
                var newMessage = _forumService.EditDiscussionContent(id, message);
                return Ok(newMessage);
            }
            return Ok();
        }
    }
}
