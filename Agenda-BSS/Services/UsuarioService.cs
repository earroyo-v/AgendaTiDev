using Agenda_BSS.Interfaces;
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
    public class UsuarioService : IUsuario
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<UsuarioDTO>> GetUser(string email)
        {
            Result<UsuarioDTO> response = new();
            try
            {
                response.Data = await _context.Usuarios.AsNoTracking().Where(x => x.Nombre.Contains(email)).FirstOrDefaultAsync() ?? new UsuarioDTO();
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
