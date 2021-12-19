using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating.Compilation;

using MOTI.Data;
using MOTI.Models;
using MOTI.Utils;

using NuGet.Protocol;

namespace MOTI.Controllers {
    public class GreedyController : Controller {
        private readonly ApplicationDbContext _context;

        public GreedyController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET
        public IActionResult Index() {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Title");
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId");
            return View();
        }
        
        
        public async Task<IActionResult> Proceed(int roomId=1, int requestId=1) {
            var devicesResult = await GreedyAlgorythm.OptimizeDevices(_context,roomId,requestId);
            return Ok(devicesResult);
            return View( devicesResult);
        }
    }
}