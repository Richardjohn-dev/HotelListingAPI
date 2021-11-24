using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.DTOs;
using HotelListingAPI.IRepository;
using HotelListingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Controllers
{
    [ApiVersion("2.0", Deprecated = true) ]
    [Route("api/country")]   
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {
        // just for example that its different.
        private readonly DatabaseContext _context;

        public CountryV2Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountries()
        {
            return Ok(_context.Countries);
        }


    }
}
