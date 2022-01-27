using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnnoTest3.Models;
using EnnoTest3.Areas.Manage.ViewModels;
using Microsoft.AspNetCore.Hosting;
using EnnoTest3.Helper;

namespace EnnoTest3.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeamController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var teams = await _context.Teams.ToListAsync();
            ViewBag.PageSize = 2;
            TeamViewModel teamVM = new TeamViewModel
            {
                PagenatedTeams = PagenatedList<Team>.Create(teams, page, ViewBag.PageSize)
            };
            return View(teamVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (team.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is Required");
                return View();
            }
            else if (team.ImageFile.ContentType != "image/jpeg" && team.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "Image type only png and jpeg");
                return View();
            }
            else if (team.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size only 2mb under!");
                return View();
            }
            team.Image = FileManager.Save(_env.WebRootPath, "uploads/teams", team.ImageFile);

            _context.Teams.Add(team);
            _context.SaveChanges();
            return RedirectToAction("index", "team");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Team existTeam = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (existTeam == null)
            {
                return NotFound();
            }
            return View(existTeam);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Team team)
        {
            Team existTeam = _context.Teams.FirstOrDefault(x => x.Id == team.Id);
            if (existTeam == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(team);
            }
            
            existTeam.Name = team.Name;
            existTeam.Desc = team.Desc;
            existTeam.Level = team.Level;

            if (team.ImageFile != null)
            {
                
                if (team.ImageFile.ContentType != "image/jpeg" && team.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image type only png and jpeg");
                    return View(team);
                }
                else if (team.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size only 2mb under!");
                    return View(team);
                }
                FileManager.Delete(_env.WebRootPath, "uploads/teams", existTeam.Image);
                existTeam.Image = FileManager.Save(_env.WebRootPath, "uploads/teams", team.ImageFile);
            }
            _context.SaveChanges();
            return RedirectToAction("index", "team");
        }


       
        public IActionResult Delete(int id)
        {
            Team existTeam = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (existTeam == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(existTeam.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/teams", existTeam.Image);
            }
            _context.Teams.Remove(existTeam);
            _context.SaveChanges();
            return Ok();
        }


    }
}
