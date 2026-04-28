using Agenda_BSS.Configurations;
using Agenda_BSS.Interfaces;
using Agenda_BSS.Mappings;
using Agenda_Data.Models;
using Agenda_Model;
using Agenda_Model.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Services
{
    public class RedSocialService : BaseService, IRedSocial
    {
        public RedSocialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<DetalleDTO>>> GetRedSociales()
        {
            Result<List<DetalleDTO>> result = new();
            try
            {
                var red = await _context.RedSocials.ToListAsync();
                result.Data = red.Select(c => c.ToDTO()).ToList();
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
