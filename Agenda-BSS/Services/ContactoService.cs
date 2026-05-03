using Agenda_BSS.Configurations;
using Agenda_BSS.Interfaces;
using Agenda_BSS.Mappings;
using Agenda_Data.Models;
using Agenda_Model;
using Agenda_Model.General;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Services
{
    public class ContactoService : BaseService , IContacto
    {
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
        public async Task<Result<bool>> CreateContacto(ContactoDTO contacto)
        {
            Result<bool> response = new();
            try
            {
                //valida null
                if (contacto == null)
                {
                    response.Error = true;
                    response.Message = "Favor de enviar la informacion";
                    return response;
                }
                //fluent validation para reglas de negocio                 
                _context.Contactos.Add(contacto.ToEntity());
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Result<bool>> EditContacto(ContactoDTO contacto)
        {
            Result<bool> response = new();
            try
            {
                //valida null
                if (contacto == null)
                {
                    response.Error = true;
                    response.Message = "Favor de enviar la informacion";
                    return response;
                }
                //fluent validation para reglas de negocio                 
                _context.Contactos.Add(contacto.ToEntity());
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
