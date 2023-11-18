﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BowlingLeague.Models;

namespace BowlingLeague.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly WeatherContext _context;

        public WeatherForecastsController(WeatherContext context)
        {
            _context = context;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetForecasts()
        {
          if (_context.Forecasts == null)
          {
              return NotFound();
          }
            return await _context.Forecasts.ToListAsync();
        }

        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(long id)
        {
          if (_context.Forecasts == null)
          {
              return NotFound();
          }
            var weatherForecast = await _context.Forecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        // PUT: api/WeatherForecasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(long id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherForecasts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
          if (_context.Forecasts == null)
          {
              return Problem("Entity set 'WeatherContext.Forecasts'  is null.");
          }
            _context.Forecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetWeatherForecast", new { id = weatherForecast.Id }, weatherForecast);
            return CreatedAtAction(nameof(GetWeatherForecast), new {id = weatherForecast.Id}, weatherForecast);
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(long id)
        {
            if (_context.Forecasts == null)
            {
                return NotFound();
            }
            var weatherForecast = await _context.Forecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.Forecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherForecastExists(long id)
        {
            return (_context.Forecasts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
