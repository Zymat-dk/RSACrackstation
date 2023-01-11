using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;

namespace RSACrackstation.Controllers;

public class HomeController : Controller{
    private readonly ILogger<HomeController> _logger;

    public Dictionary<string, string> Description = new()
    {
        {
            "prime",
            "A page for factorizing prime numbers to the corresponding p and q. Very useful for known weak RSA keys."
        },
        { "crack", "Simply input known variables about RSA and use this tool for RSA and decrypting the message." },
        {
            "decrypt",
            "Quickly decrypt RSA encrypted text with this simple module. Output text can either be ASCII, Decimal or HEX."
        },
        {
            "encrypt",
            "Page for encrypting an input with RSA. Very useful if you quickly need to encrypt text with a known key."
        },
        { "keygen", "Generate RSA keys quickly with this module. It can generate from 2 bit to 2048 bit keys." },
        {
            "smalle",
            "Crack RSA encryption if you know it's vulnerable to small e attacks or simple use the module to check for small e vulnerabilities."
        }
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
        ViewData["Description"] = Description["crack"];
        return View();
    }

    public IActionResult RSAEncrypt(){
        ViewData["Description"] = Description["encrypt"];
        return View();
    }

    public IActionResult RSADecrypt(){
        ViewData["Description"] = Description["decrypt"];
        return View();
    }

    public IActionResult RSAKeygen(){
        ViewData["Description"] = Description["keygen"];
        return View();
    }

    public IActionResult SmallE(){
        ViewData["Description"] = Description["smalle"];
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}