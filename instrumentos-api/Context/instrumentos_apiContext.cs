using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using instrumentos_api.Models;

namespace instrumentos_api.Data
{
    public class instrumentos_apiContext : DbContext
    {
        public instrumentos_apiContext (DbContextOptions<instrumentos_apiContext> options)
            : base(options)
        {
        }

        public DbSet<instrumentos_api.Models.InstrumentoEntity> Instrumento { get; set; }
    }
}
