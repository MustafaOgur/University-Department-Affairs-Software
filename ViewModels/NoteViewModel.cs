using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UDAS.ViewModels
{
    public class NoteViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string NoteType { get; set; }  // "Sınav" veya "Ders Programı"

        public string Content { get; set; }
    }
}