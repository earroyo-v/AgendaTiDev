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
    public class ContactoService : BaseService, IContacto
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
                if (contacto == null)
                {
                    response.Error = true;
                    response.Message = "Favor de enviar la informacion";
                    return response;
                }

                var hashRed = new HashSet<int?>(contacto.ContactoRedSocials.Select(x => x.IdContactoRedSocial));

                var redesExistentes = await _context.ContactoRedSocials
                        .AsNoTracking()
                        .Where(x => x.IdContacto == contacto.IdContacto)
                        .ToListAsync();

                await using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    if (redesExistentes.Any())
                    {
                        foreach (var redSocial in redesExistentes)
                        {
                            if (!hashRed.Contains(redSocial.IdContactoRedSocial))
                            {
                                _context.ContactoRedSocials.Remove(redSocial);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }

                    //fluent validation para reglas de negocio
                    _context.Contactos.Update(contacto.ToEntity());
                    await _context.SaveChangesAsync();
                    response.Data = true;

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Result<bool>> DeleteContacto(int idContacto)
        {
            Result<bool> response = new();
            try
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var contacto = await _context.Contactos.AsNoTracking().FirstOrDefaultAsync(x => x.IdContacto == idContacto);

                    if (contacto == null)
                    {
                        response.Error = true;
                        response.Message = "Contacto no encontrado";
                        return response;
                    }

                    var redes = await _context.ContactoRedSocials.AsNoTracking().Where(x => x.IdContacto == idContacto)
                        .ToListAsync();

                    if (redes.Any())
                    {
                        _context.ContactoRedSocials.RemoveRange(redes);
                        await _context.SaveChangesAsync();
                    }
                    
                    _context.Contactos.Remove(contacto);
                    await _context.SaveChangesAsync();

                    response.Data = true;

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
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
