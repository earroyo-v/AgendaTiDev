using Agenda_Data.Models;
using Agenda_Model;
using Agenda_Model.General;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Interfaces
{
    public interface IUsuario
    {
        Task<Result<UsuarioDTO>> ValidateUser(string email, string password);
        Task<Result<bool>> CreateUser(UsuarioDTO user);   
        Task<Result<bool>> UpdateUser(UsuarioDTO user);
        Task<Result<bool>> ChangePassword(string email, string NewPssw);
        Task<Result<UsuarioDTO>> GetUser(int id);
        Task<bool> ValidateEmail(string email);
    }
}
