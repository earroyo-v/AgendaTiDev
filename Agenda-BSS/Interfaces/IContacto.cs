using Agenda_Model;
using Agenda_Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_BSS.Interfaces
{
    public interface IContacto
    {
        Task<Result<List<ContactoDTO>>> GetContactos(int idUsuario);
        Task<Result<bool>> CreateContacto(ContactoDTO conatcto);
    }
}
