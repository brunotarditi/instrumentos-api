using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using instrumentos_api.Models;

namespace instrumentos_api.Services
{
    public interface IInstrumentoService
    {
        Task<List<InstrumentoEntity>> GetInstrumentos();
        Task<InstrumentoEntity> GetInstrumentoById(int id);

        Task SaveInstrumento(InstrumentoEntity instrumentoEntity);

        Task UpdateInstrumento(InstrumentoEntity instrumentoEntity, int id);

        Task DeleteInstrumento(int id);

        bool InstrumentoExist(int id);
    }
}
