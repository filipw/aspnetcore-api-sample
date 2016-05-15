using System;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    public class ItemsController
    {
        [HttpGet("items")]
        public void Get()
        {
            throw new Exception("something dramatic happened!");
        }
    }
}