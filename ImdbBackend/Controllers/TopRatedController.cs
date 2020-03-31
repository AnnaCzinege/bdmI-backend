﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ImdbBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopRatedController : ControllerBase
    {
        private readonly ILogger<TopRatedController> _logger;
        private readonly MovieContext _db;

        public TopRatedController(ILogger<TopRatedController> logger, MovieContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public Movie Get()
        { 
            return null; //_db.Movies.Include(movie => movie.MovieGenres).Include(movie => movie.MovieLanguages).Single(movie => movie.OriginalTitle.Equals("LOL"));
        }
    }
}