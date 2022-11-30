using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;

namespace RSACrackstation.Controllers;

public class HomeController : Controller{
    private readonly ILogger<HomeController> _logger;

    public Dictionary<string, string> Description = new ()
    {
        { "prime", "Prime factorization page that bla bla " },
        { "crack", "The cracking page that yeehaw" }, 
        { "decrypt", "Decrypt pageingen yeash"}, 
        { "encrypt", "Encrypt pageing yeehaw" },
        { "keygen", "Keygen pageing yeehaw" }
    };
    public HomeController(ILogger<HomeController> logger){
        _logger = logger;
    }

    public IActionResult Index(){
        ViewData["Description"] = Description;
        return View();
    }
    
    public IActionResult Prime(){
        ViewData["Description"] = Description["prime"];
        return View();
    }

    public IActionResult RsaCrack(){
        return View();
    }
    
    public IActionResult RSAEncrypt(){
        return View();
    }
    
    public IActionResult RSADecrypt(){
        return View();
    }

    public IActionResult RSAKeygen(){
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}