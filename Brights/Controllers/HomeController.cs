using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Brights.Models;
using System.Net.Http;
using HtmlAgilityPack;
using Brights.BLL.DTO;
using Brights.BLL.Abstract;

namespace Brights.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceHttpRequest _requestService;

        public HomeController(ILogger<HomeController> logger, IServiceHttpRequest requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ResponseModel> Check([FromBody] UrlModel data)
        {
            try
            {
                var result = await _requestService.RequestToUrl(data.Url);
                await Task.Delay(5000);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }         
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
