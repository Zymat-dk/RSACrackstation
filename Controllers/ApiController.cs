using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;
using System.Net;
using System.Text.Json.Nodes;
using RSACrackstation.Backend;

namespace RSACrackstation.Controllers;

public class ApiController : Controller {
    public string[] GetFactors(string inputNum) {
        var cracker = new RSACracker(inputNum);
        return cracker.GetFactors();
    }
}