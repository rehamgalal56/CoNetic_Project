using CoNetic.Core.Models;
using CoNetic.Core.ServicesInterfaces;
using CoNetic.DTOs;
using CoNetic.Core.ReposInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Mapster;
using CoNetic.Repository.Identity;
using System.Drawing;

namespace CoNetic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IGenericRepo<User> ProfileRepo;
        private readonly IFileService FileService;
        private readonly AppIdentityDbContext Context;

        public ProfileController(UserManager<User> userManager, IGenericRepo<User> ProfileRepo,
        IFileService FileService,AppIdentityDbContext Context)
        {
            _userManager = userManager;
            this.ProfileRepo = ProfileRepo;
            this.FileService = FileService;
            this.Context = Context;
        }
        [HttpGet("GetProfile")]
        [Authorize]
        public async Task<ActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var list = ProfileRepo.Get(userId);

            return Ok(list.Adapt<ProfileDTO>());

        }
        [HttpPost("AddInformation")]
        [Authorize]
        public async Task<ActionResult> AddInformation(AddInformationDTO model)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }
            user!.JobRole = model.JobRole;
            user.Gender = model.Gender;
            user.BirthDate = model.BirthDate;
            user.PhoneNumber = model.PhoneNumber;
            user.AboutMe = model.AboutMe;
            var experiences = model.Experiences.Adapt<List<Experience>>();
            var skills = model.Skills.Adapt<List<Skill>>();

            user.experiences = experiences;
            user.skills = skills;
            ProfileRepo.update(user);
            ProfileRepo.save();
            return Ok("ok");

            

        }
        [HttpPost("EditProfile")]
        [Authorize]
        public async Task<ActionResult> EditProfile([FromForm] EditProfileDTO model)
        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user!.JobRole = model.JobRole;
            user.Gender = model.Gender;
            user.BirthDate = model.BirthDate;
            user.AboutMe = model.AboutMe;
            user.Country = model.Country;
            user.PhoneNumber = model.PhoneNumber;
            user.City = model.City;
            var experiences = model.Experiences.Adapt<List<Experience>>();
            var skills = model.Skills.Adapt<List<Skill>>();
            user.experiences = experiences;
            user.skills = skills;
            // image upload 
            user.Image=await FileService.UploadImage(model.Image);
            //cv upload
            user.CVFile=await FileService.UploadFile(model.CVFile);


            ProfileRepo.update(user);
            ProfileRepo.save();
            return Ok("ok");



        }


    }
}
