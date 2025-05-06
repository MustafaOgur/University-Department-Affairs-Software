using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDAS.Entities;

namespace UDAS.ViewModels
{
    public class ScheduleDisplayViewModel
    {
        public List<YearlyScheduleGroup>? Groups { get; set; }
        
    }

    public class YearlyScheduleGroup
    {
        public string? Year { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}