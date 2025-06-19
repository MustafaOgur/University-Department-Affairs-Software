using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDAS.Entities;

namespace UDAS.ViewModels
{
    public class ExamDisplayViewModel
    {
        public List<ExamSchedule>? ExamSchedules { get; set; }

        public List<Note>? ExamNotes { get; set; }
    }
}