using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using instrumentos_api.Models;

namespace instrumentos_api.Data
{
    public class InstrumentosApiContext : DbContext
    {
        public InstrumentosApiContext(DbContextOptions<InstrumentosApiContext> options)
            : base(options)
        {
        }

        public DbSet<instrumentos_api.Models.InstrumentoEntity> Instrumento { get; set; }
    }
}
