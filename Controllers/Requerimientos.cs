using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PruebaTecnica.Services;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;
using PruebaTecnica.Data;

namespace PruebaTecnica.Controllers
{
    public class Requerimientos : Controller
    {
        private const string _docLimitException = "No se permiten más de 6 URLs";
        private const string _noUrlException = "No hay urls para generar PDF";
        private const string _conversionException = "Problemas al hacer la conversión";

        private readonly IConverter _converter;
        private readonly AppDbContext _context;
        public Requerimientos(IConverter converter, AppDbContext dbContext) {
            _converter = converter;
            _context = dbContext;
        }

        [HttpPost("urlToPdf")]
        public IActionResult SingleUrlToPdf([FromQuery] string url)
        {
            if (string.IsNullOrEmpty(url)) {
                return BadRequest("La URL no puede estar vacía.");
            }

            List<string> urls = new List<string> { url };

            byte[] pdf = Services.Services.GetPdf(urls, _converter);
            return (pdf == null) ? BadRequest(_noUrlException) : File(pdf, "application/pdf", "output.pdf");
        }



        [HttpPost("multipleUrlToPdf")]
        public IActionResult MultipleUrlsToPdf([FromBody] List<string> urls)
        {
            if (urls.Count > 6) {
                return BadRequest(_docLimitException);
            }
            if (urls.Count == 0) { return BadRequest(_noUrlException);  }

            byte[] pdf = Services.Services.GetPdf(urls, _converter);
            return (pdf == null) ? BadRequest(_noUrlException) : File(pdf, "application/pdf", "output.pdf");
        }

        [HttpPost("urlToZip")]
        public async Task<IActionResult> getZipAsync([FromBody] List<string> urls)
        {

            if (urls.Count > 6) { return BadRequest(_docLimitException); }
            if (urls.Count == 0) { return BadRequest(_noUrlException); }

            byte[] pdf = Services.Services.GetPdf(urls, _converter);
            var zip = Services.Services.CreateZipFromPdf(pdf);
            var base64Zip = Convert.ToBase64String(zip);

            var zipRecord = new ZipFileRecord
            {
                Archivo = base64Zip,
                CreatedAt = DateTime.UtcNow
            };

            _context.ZipFileRecords.Add(zipRecord);
            await _context.SaveChangesAsync();

            return File(zip, "application/zip", "Zip ID N° -" + zipRecord.Id + ".zip");
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadZip(int id)
        {
            var zipRecord = await _context.ZipFileRecords.FindAsync(id);
            if (zipRecord == null)
                return NotFound();

            var zipData = Convert.FromBase64String(zipRecord.Archivo);
            return File(zipData, "application/zip", "output.zip");
        }
    }
}

