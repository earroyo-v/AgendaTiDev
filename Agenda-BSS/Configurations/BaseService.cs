using Agenda_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Configurations
{
    public class BaseService
    {
        protected AppDbContext _context { get; set; }
    }
}
