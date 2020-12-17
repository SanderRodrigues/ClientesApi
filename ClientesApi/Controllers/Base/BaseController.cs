﻿using CrossCutting.Command;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApi.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : Controller
    {
        protected IActionResult TratarRetorno(CommandResult result)
        {
            if (result.Success)
            {
                return Created(result.ResourceId, null);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }
    }
}
