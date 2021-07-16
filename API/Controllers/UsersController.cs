using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using API.Data;
using API.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System;

namespace API.Controllers
{

    public class UsersController : BaseAPI
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auth>>> GetUsers(){
            return await _context.Users.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("file")]
        public async Task<IActionResult> Download()  
        {  
            string filename = "text.html";
            if (filename == null)  
                return Content("filename not present");  
        
            var path = Path.Combine(  
                            "D:\\Projects\\dotnet\\Example\\API\\Data", filename);  
        
            var memory = new MemoryStream();  
            using (var stream = new FileStream(path, FileMode.Open))  
            {  
                await stream.CopyToAsync(memory);  
            }  
            memory.Position = 0;  
            return File(memory, "text/plain", "file.doc");  
        } 
        private string GetContentType(string path)  
        {  
            var types = GetMimeTypes();  
            var ext = Path.GetExtension(path).ToLowerInvariant();  
            return types[ext];  
        }  
   
        private Dictionary<string, string> GetMimeTypes()  
        {  
            return new Dictionary<string, string>  
            {  
                {".txt", "text/plain"},  
                {".pdf", "application/pdf"},  
                {".doc", "application/vnd.ms-word"},  
                {".docx", "application/vnd.ms-word"},  
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},  
                {".jpg", "image/jpeg"},  
                {".jpeg", "image/jpeg"},  
                {".gif", "image/gif"},  
                {".csv", "text/csv"}  
            };  
        } 

    }
}