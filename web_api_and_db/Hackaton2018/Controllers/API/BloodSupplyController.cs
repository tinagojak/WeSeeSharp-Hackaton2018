using Hackaton2018.Models;
using Hackaton2018.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hackaton2018.Controllers.API
{
    public class BloodSupplyController : ApiController
    {
        private ApplicationDbContext _context;

        public BloodSupplyController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/bloodsupply
        public IHttpActionResult GetBloodSupplies()
        {
            var BloodSuppliesQuery = _context.BloodSupplies.ToList(); ;
            return Ok(BloodSuppliesQuery);
        }

        // GET /api/bloodsupply/1
        public IHttpActionResult GetBloodSupply(int id)
        {
            var bloodSupplyRecord = _context.BloodSupplies.SingleOrDefault(c => c.Id == id);

            if (bloodSupplyRecord == null)
                return NotFound();

            return Ok(bloodSupplyRecord);
        }

        // POST /api/bloodsupply
        [HttpPost]
        public IHttpActionResult CreateBloodSupply(BloodSupply bloodSupply)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.BloodSupplies.Add(bloodSupply);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + bloodSupply.Id), bloodSupply);
        }

        // PUT /api/bloodsupply/1
        [HttpPut]
        public IHttpActionResult UpdateBloodSupply(int id, BloodSupply bloodSupply)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bloodSupplyInDb = _context.BloodSupplies.SingleOrDefault(c => c.Id == id);

            if (bloodSupplyInDb == null)
                return NotFound();

            bloodSupplyInDb = bloodSupply;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/bloodsupply/1
        [HttpDelete]
        public IHttpActionResult DeleteBloodSupply(int id)
        {
            var bloodSupplyInDb = _context.BloodSupplies.SingleOrDefault(c => c.Id == id);

            if (bloodSupplyInDb == null)
                return NotFound();

            _context.BloodSupplies.Remove(bloodSupplyInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
