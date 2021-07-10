using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using instrumentos_api.Data;
using instrumentos_api.Models;
using System.Threading;
using System.IO;
using instrumentos_api.Services;

namespace instrumentos_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentosController : ControllerBase
    {
        private readonly IInstrumentoService _instrumentoService;

        public InstrumentosController(IInstrumentoService instrumentoService)
        {
            _instrumentoService = instrumentoService;
        }

        // GET: api/Instrumentos
        [HttpGet]
        public async Task<IActionResult> GetInstrumento()
        {
            var instrumentos = await _instrumentoService.GetInstrumentos();
            return Ok(instrumentos);
        }

        // GET: api/Instrumentos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstrumento(int id)
        {
            var instrumento = await _instrumentoService.GetInstrumentoById(id);

            if (instrumento == null)
            {
                return NotFound();
            }

            return Ok(instrumento);
        }

        // PUT: api/Instrumentos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrumento(int id, InstrumentoEntity instrumento)
        {
            if (id != instrumento.Id)
            {
                return BadRequest();
            }

            try
            {
               await _instrumentoService.UpdateInstrumento(instrumento, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentoExists(id))
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

        // POST: api/Instrumentos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostInstrumento([FromBody] InstrumentoEntity instrumento)
        {
            if (instrumento == null)
            {
                return BadRequest();
            }
   
            await _instrumentoService.SaveInstrumento(instrumento);

            return Created("Instrumento creado", instrumento);
        }

        // DELETE: api/Instrumentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrumento(int id)
        {
            var instrumento = await _instrumentoService.GetInstrumentoById(id);

            if (instrumento == null)
            {
                return NotFound();
            }

            await _instrumentoService.DeleteInstrumento(id);
            return Ok("Instrumento eliminado");
        }
        // UPLOAD: api/Instrumentos/upload
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var res = await WriteImage(file);
            return res;
        }

        // GET FILE: api/Instrumentos/image/fileName
        [HttpGet]
        [Route("image/{fileName}")]
        public IActionResult ReadImage(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Src\\Images\\" + fileName);
            var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "Src\\Images\\default.jpg");

            try
            {
                var image = System.IO.File.OpenRead(path);
                return File(image, "image/jpeg");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            var imageDefault = System.IO.File.OpenRead(defaultPath);
            return File(imageDefault, "image/jpeg");
        }

        private bool InstrumentoExists(int id)
        {
            return _instrumentoService.InstrumentoExist(id);
        }

        private async Task<string> WriteImage(IFormFile file)
        {
            string isSaveSuccess = "";
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = file.FileName;
                var pathRoot = Path.Combine(Directory.GetCurrentDirectory(), "Src\\Images");
                if (!Directory.Exists(pathRoot))
                {
                    Directory.CreateDirectory(pathRoot);
                }
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Src\\Images", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    isSaveSuccess = "La imagen " + fileName + " ha sido subida con éxito";

                }
                else
                {
                    isSaveSuccess = "No es una imagen";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return isSaveSuccess;
        }
    }
}
