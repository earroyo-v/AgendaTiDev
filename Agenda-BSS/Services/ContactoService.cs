using Agenda_BSS.Interfaces;
using Agenda_BSS.Mappings;
using Agenda_Data.Models;
using Agenda_Model;
using Agenda_Model.General;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Services
{
    public class ContactoService : IContacto
    {
        private readonly AppDbContext _context;
        public ContactoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ContactoDTO>>> GetContactos(int idUsuario)
        {
            Result<List<ContactoDTO>> result = new();
            try
            {
                var contactos = await _context.Contactos.
                    Include(c => c.ContactoRedSocials).
                    ThenInclude(crs => crs.IdRedSocialNavigation).
                    Where(x => x.IdUsuario == idUsuario).
                    ToListAsync();
                result.Data = contactos.Select(c => c.ToDTO()).ToList();
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
