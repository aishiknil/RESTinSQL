using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOpeartionsInDotnetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOpeartionsInDotnetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;
        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("AllBrands")]
        public async Task<ActionResult<IEnumerable<Brand>>> AllBrands()
        {
            if (_dbContext.Brands == null)
                return NotFound("Empty");
            return await _dbContext.Brands.ToListAsync();
        }
        [HttpGet("BrandsByID")]
        public async Task<ActionResult<Brand>> BrandsByID(int id)
        {
            if (_dbContext.Brands == null)
                return NotFound("Empty");
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
                return NotFound("Item Not Found");
            return brand;
        }

        [HttpPost("AddBrands")]
        public async Task<ActionResult<Brand>> AddBrand(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AllBrands), new { id = brand.ID }, brand);
        }

        [HttpPut("UpdateBrand")]
        public async Task<ActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(brand).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }
        private bool BrandAvailable(int id)
        {
            return (_dbContext.Brands?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteBrand")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}