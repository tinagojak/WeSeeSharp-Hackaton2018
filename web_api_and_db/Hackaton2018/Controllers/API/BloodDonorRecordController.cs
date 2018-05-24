using Hackaton2018.Models;
using Hackaton2018.Models.DbModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hackaton2018.Controllers.API
{
    public class BloodDonorRecordController : ApiController
    {
        private ApplicationDbContext _context;

        public BloodDonorRecordController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/blooddonorrecord
        public IHttpActionResult GetBloodDonorRecords()
        {
            var bloodDonorRecordsQuery = _context.BloodDonorRecords.ToList(); ;
            return Ok(bloodDonorRecordsQuery);
        }

        // GET /api/blooddonorrecord/1
        public IHttpActionResult GetBloodDonorRecord(int id)
        {
            var bloodDonorRecord = _context.BloodDonorRecords.SingleOrDefault(c => c.Id == id);

            if (bloodDonorRecord == null)
                return NotFound();

            return Ok(bloodDonorRecord);
        }

        // POST /api/blooddonorrecord
        [HttpPost]
        public IHttpActionResult CreateBloodDonorRecord(BloodDonorRecord bloodDonorRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.BloodDonorRecords.Add(bloodDonorRecord);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + bloodDonorRecord.Id), bloodDonorRecord);
        }

        // PUT /api/blooddonorrecord/1
        [HttpPut]
        public IHttpActionResult UpdateBloodDonorRecord(int id, BloodDonorRecord bloodDonorRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bloodDonorRecordInDb = _context.BloodDonorRecords.SingleOrDefault(c => c.Id == id);

            if (bloodDonorRecordInDb == null)
                return NotFound();

            bloodDonorRecordInDb = bloodDonorRecord;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/blooddonorrecord/1
        [HttpDelete]
        public IHttpActionResult DeleteBloodDonorRecord(int id)
        {
            var bloodDonorRecordInDb = _context.BloodDonorRecords.SingleOrDefault(c => c.Id == id);

            if (bloodDonorRecordInDb == null)
                return NotFound();

            _context.BloodDonorRecords.Remove(bloodDonorRecordInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
