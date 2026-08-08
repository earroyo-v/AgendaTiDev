using Agenda_BSS.Configurations;
using Agenda_BSS.Interfaces;
using Agenda_BSS.Mappings;
using Agenda_Data.Models;
using Agenda_Model;
using Agenda_Model.General;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Agenda_BSS.Services
{
    public class UsuarioService : BaseService, IUsuario
    {
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

                //string hash = hasher.HashPassword(null, password); //PBKDF2(password, salt, iteraciones, SHA512)

                //el email tiene que ser unico
                var usuario = await _context.Usuarios.Include(x => x.IdRolNavigation).FirstOrDefaultAsync(x => x.Email == email);

                if (usuario == null)
                {
                    response.Error = true;
                    response.Message = "Email incorrecto";
                    return response;
                }

                //var resultado = hasher.VerifyHashedPassword(null, hash, usuario.Password);
                var resultado = hasher.VerifyHashedPassword(null, usuario.Password, password);

                if (resultado != PasswordVerificationResult.Success)
                {
                    response.Error = true;
                    response.Message = "Password incorrecto";
                    return response;
                }

                response.Data = usuario.ToDTO();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Result<bool>> CreateUser(UsuarioDTO usuario)
        {
            Result<bool> response = new();
            try
            {
                //valida null
                if (usuario == null)
                {
                    response.Error = true;
                    response.Message = "Favor de enviar la informacion";
                    return response;
                }
                //fluent validation para reglas de negocio 
                //validar email
                if (_context.Usuarios.AsNoTracking().Any(x => x.Email == usuario.Email))
                {
                    response.Error = true;
                    response.Message = "El email ya existe";
                    return response;
                }
                //valida passwaord con espacios -> eso se podria validar en controller esta bien
                if (usuario.Password.Contains(" "))
                {
                    response.Error = true;
                    response.Message = "El password no puede contener espacios";
                    return response;
                }
                Usuario user = usuario.ToDTO();
                var hasher = new PasswordHasher<object>();
                user.Password = hasher.HashPassword(null, usuario.Password); //PBKDF2(password, salt, iteraciones, SHA512)                
                _context.Usuarios.Add(user);
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
        public async Task<Result<bool>> UpdateUser(UsuarioDTO usuario)
        {
            Result<bool> response = new();
            try
            {
                //valida null
                if (usuario == null)
                {
                    response.Error = true;
                    response.Message = "Favor de enviar la informacion";
                    return response;
                }
                //fluent validation para reglas de negocio 
                //Obtener usuario
                var userDB = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuario.IdUsuario);
                //validar si es null
                if (userDB == null)
                {
                    response.Error = true;
                    response.Message = "El usuario no existe";
                    return response;
                }
                //actualizar campos con for
                userDB.Nombre = usuario.Nombre;
                userDB.ApellidoPaterno = usuario.ApellidoPaterno;
                userDB.ApellidoMaterno = usuario.ApellidoMaterno;
                userDB.FechaNacimiento = usuario.FechaNacimiento;
                userDB.Email = usuario.Email;
                userDB.NickName = usuario.NickName;
                userDB.Foto = !usuario.Foto.IsNullOrEmpty() ? usuario.Foto : userDB.Foto;
                userDB.UrlPerfil = usuario.UrlPerfil;

                _context.Usuarios.Update(userDB);
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
        public async Task<Result<bool>> ChangePassword(string email, string NewPssw)
        {
            Result<bool> response = new();
            try
            {                
                //fluent validation para reglas de negocio 
                //valida passwaord con espacios -> eso se podria validar en controller esta bien
                if (NewPssw.Contains(" "))
                {
                    response.Error = true;
                    response.Message = "El password no puede contener espacios";
                    return response;
                }
                var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    response.Error = true;
                    response.Message = "El usuario no existe";
                    return response;
                }

                var hasher = new PasswordHasher<object>();
                user.Password = hasher.HashPassword(null, NewPssw);

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

        public async Task<Result<UsuarioDTO>> GetUser(int id)
        {
            Result<UsuarioDTO> response = new();
            try
            {
                var usuario = await _context.Usuarios.AsNoTracking().Where(x => x.IdUsuario == id).FirstOrDefaultAsync();
                response.Data = usuario?.ToDTO();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<bool> ValidateEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().AnyAsync(x => x.Email == email);
        }
    }
}
