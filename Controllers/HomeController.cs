using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PalindromeMVC.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PalindromeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TheCode()
        {
            return View();
        }

        // Get the selected View
        [HttpGet]
        public IActionResult Palindrome()
        {
            PalindromeClass model = new();
            return View(model);
        }

        //New Action - get the post info
        [HttpPost]
        // Making sure we are operating wihtin our form
        [ValidateAntiForgeryToken]

        // Create a action with the param class i want to full info from
        public IActionResult Palindrome(PalindromeClass palindrome)
        {
            // Get our input
            string inputWord = palindrome.InputWord;

            // Reverse the word
            string revWord = "";

            // The palindrome algo using a for loop
            for (int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }
            // Setting the Class var RevWord to revWord
            palindrome.RevWord = revWord;

            // Using Regex to clean up the words 
            revWord = Regex.Replace(revWord.ToLower(), "[^a-zAZ0-9]+", "");
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zAZ0-9]+", "");

            //Eval the revWord against inputWord
            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"Success, your word {palindrome.InputWord} is a Palindrome";
            } else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"Sorry, your word {palindrome.InputWord} is Not a Palindrome";
            }
            return View(palindrome);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
