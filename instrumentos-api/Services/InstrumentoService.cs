using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using instrumentos_api.Data;
using instrumentos_api.Models;
using Microsoft.EntityFrameworkCore;

namespace instrumentos_api.Services
{
    public class InstrumentoService : IInstrumentoService

    {
        private readonly InstrumentosApiContext _context;

        public InstrumentoService(InstrumentosApiContext context) => _context = context;

        public async Task<List<InstrumentoEntity>> GetInstrumentos()
        {
            return await _context.Instrumento.ToListAsync();
        }

        public async Task<InstrumentoEntity> GetInstrumentoById(int id)
        {
            return await _context.Instrumento.FindAsync(id);
        }

        public async Task SaveInstrumento(InstrumentoEntity instrumentoEntity)
        {
            _context.Add(instrumentoEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInstrumento(InstrumentoEntity instrumentoEntity, int id)
        {
            _context.Entry(instrumentoEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteInstrumento(int id)
        {
            var instrumento = await _context.Instrumento.FindAsync(id);
            _context.Instrumento.Remove(instrumento);
            await _context.SaveChangesAsync();
        }

        public bool InstrumentoExist(int id)
        {
            return _context.Instrumento.Any(e => e.Id == id);
        }
    }
}
