﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Settings")]
    public class SettingsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public SettingsController(IBlogService blogService, UserManager<ApplicationUser> userManager, 
            IEmailService emailService)
        {
            _blogService = blogService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [Route("Get")]
        [HttpGet]
        public SettingsViewModel Get()
        {
            return _blogService.GetBlogSettings();
        }

        [Route("Save")]
        [HttpPost]
        public SettingsViewModel Save([FromBody]SettingsViewModel settings)
        {
            return _blogService.SaveBlogSettings(settings);
        }

        [Route("SendTestEmail")]
        [HttpPost]
        public async Task<bool> SendTestEmail([FromBody]SettingsViewModel settings)
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return await _emailService.SendTestEmail(settings, user.Id);
        }
    }
}