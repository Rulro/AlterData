using ControleDev.Models;
using ControleDev.Models.ViewModels;
using ControleDev.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Collections.Generic;
using System.Diagnostics;

namespace ControleDev.Controllers
{
    public class DevelopersController : Controller
    {
        public async Task<IActionResult> Index(string? searchString, DateTime? minDate, DateTime? maxDate)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                int foundId = int.Parse(searchString);
                DeveloperAll returnDevAll = new DeveloperAll();
                Developer devFound = await returnDevAll.ReturnDev(foundId);

                if (devFound == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
                }

                List<Developer> list = new List<Developer>();
                list.Add(new Developer
                {
                    CreatedAt = devFound.CreatedAt
                                        ,
                    id = devFound.id
                                        ,
                    name = devFound.name
                                        ,
                    Avatar = devFound.Avatar
                                        ,
                    Squad = devFound.Squad
                                        ,
                    Login = devFound.Login
                                        ,
                    Email = devFound.Email
                });

                ViewData["searchString"] = searchString;

                return View(list);
                
            }
            else
            {
                DeveloperAll returnDevAll = new DeveloperAll();
                Developer[] devAllFound = await returnDevAll.ReturnAll();

                if (devAllFound == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Nenhum registro retornado" });
                }

                if (minDate.HasValue) 
                {
                    ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
                    ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

                    var filter = devAllFound.Where(x => x.CreatedAt.Date >= minDate).Where(x => x.CreatedAt.Date <= maxDate); 
                    return View(filter);
                }
                
                return View(devAllFound);
            }


        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Developer developer)
        {
            DeveloperAll developerAll = new DeveloperAll();
            developerAll.Insert(developer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado." });
            }

            DeveloperAll developer = new DeveloperAll();
            var devResult = await developer.ReturnDev(id.Value);

            if (devResult == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            return View(devResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Developer developer)
        {
            if (id != developer.id) {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondem." });
            }

            try
            {
                DeveloperAll developerAll = new DeveloperAll();
                developerAll.Update(id, developer);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            DeveloperAll developer = new DeveloperAll();
            var devResult = await developer.ReturnDev(id);

            if (devResult == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            return View(devResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado."});
            }

            DeveloperAll developerAll = new DeveloperAll();
            developerAll.Remove(id.Value);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
