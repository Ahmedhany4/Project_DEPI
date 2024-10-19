using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using CoffeeManagementSystem.Repositories.Interfaces;
using CoffeeManagementSystem.Repositories.Emplimintations;
using Microsoft.AspNetCore.Authorization;
namespace CoffeeManagementSystem.Controllers
{
    [Authorize]
    public class FeedbacksController : Controller
    {

        private IBaseRepository<Feedback> _FeedbackRepository;


        public FeedbacksController(IBaseRepository<Feedback> FeedbackRepository)
        {
            _FeedbackRepository = FeedbackRepository;

        }

        public async Task<IActionResult> Index()
        {
            var Feedbacks = await _FeedbackRepository.GetAll();
            ViewBag.FeedbacksCount = Feedbacks.Count();
            return View(Feedbacks);
        }


    }
}
