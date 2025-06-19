using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UDAS.Entities
{
    public class Note
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string NoteType { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}