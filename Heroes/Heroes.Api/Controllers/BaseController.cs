﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
