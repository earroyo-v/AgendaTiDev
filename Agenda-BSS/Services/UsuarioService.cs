using Agenda_BSS.Interfaces;
using Agenda_Data.Models;
using Agenda_Model;
using Agenda_Model.General;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Agenda_BSS.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<UsuarioDTO>> ValidateUser(string email, string password)
        {
            Result<UsuarioDTO> response = new();
            try
            {
                if (email.IsNullOrEmpty() || password.IsNullOrEmpty())
                {
                    response.Error = true;
                    response.Message = "Falta Email o Password";
                    return response;
                }

                var hasher = new PasswordHasher<object>();

                string hash = hasher.HashPassword(null, password);

                //var resultado = hasher.VerifyHashedPassword(null, hash, password);

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Password == hash);

                if (usuario == null)
                {
                    response.Error = true;
                    response.Message = "Email o Password incorrecto";
                    return response;
                }

                response.Data = usuario;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
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
