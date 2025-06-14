using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UDAS.Entities;

public class SeatingPlan
{
    public int Id { get; set; }
    public string PlanName { get; set; }

    // Navigation Property
    public ICollection<SeatAssignment> SeatAssignments { get; set; }
    public ICollection<ExamSchedule> ExamSchedules { get; set; }
}
