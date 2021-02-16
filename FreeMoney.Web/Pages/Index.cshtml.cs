using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using FreeMoney.Web.Models;
using FreeMoney.Web.Data;

namespace FreeMoney.Web.Pages
{
    public class IndexModel : PageModel
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [BindProperty]
        public List<UserRecord> UserRecords { get; private set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly FreeMoneyDbContext _freeMoneyDbContext;

        public IndexModel(ILogger<IndexModel> logger, FreeMoneyDbContext freeMoneyDbContext)
        {
            _logger = logger;
            _freeMoneyDbContext = freeMoneyDbContext;
        }

        public void OnGet()
        {
            UserRecords = _freeMoneyDbContext.UserRecords.ToList();
            // return Page();
        }

        public async Task<IActionResult> OnPost([FromForm] string name, [FromForm] string email)
        {
            Console.WriteLine(name);
            if (!isEmailUnique(email))
            {
                return RedirectToPage("Index");
            }
            UserRecord userRecord = new UserRecord()
            {
                Name = name,
                Email = email,
            };
            await _freeMoneyDbContext.UserRecords.AddAsync(userRecord);
            await _freeMoneyDbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        private bool isEmailUnique(string email)
        {
            var recordsWithMatchingEmail = _freeMoneyDbContext.UserRecords.Where(ur => ur.Email == email).ToList();
            return recordsWithMatchingEmail.Count == 0;
        }
    }
}
