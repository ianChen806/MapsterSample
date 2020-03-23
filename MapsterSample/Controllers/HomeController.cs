using System.Diagnostics;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MapsterSample.Models;
using MapsterSampleTest.Model;
using Newtonsoft.Json;

namespace MapsterSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public string Index()
        {
            var classA = new ClassA()
            {
                Id = 1,
                Name = "Test",
                Price = 12.34m
            };
            var classB = _mapper.Map<ClassB>(classA);

            return JsonConvert.SerializeObject(classB);
        }

        public string Index2()
        {
            var classA = new ClassB()
            {
                Id = 1,
                Name = "Test",
                Price = 12.34m
            };
            var classC = _mapper.Map<ClassC>(classA);

            return JsonConvert.SerializeObject(classC);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}