using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;

namespace RSACrackstation.Controllers;

public class ApiController : Controller{
    public string GetPrimes(string inputNum){
        return "yeehasdfsdfw" + inputNum;
    }
}