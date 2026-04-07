using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Model.General
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public bool Error { get; set; } = false;
        public string? Message { get; set; }
    }
}
