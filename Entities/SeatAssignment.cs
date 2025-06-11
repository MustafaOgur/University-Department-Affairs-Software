using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UDAS.Entities;
public class SeatAssignment
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public string Owner { get; set; } // Örn: öğrenci adı ya da "(boş)"

    // Foreign Key
    public int SeatingPlanId { get; set; }
    public SeatingPlan SeatingPlan { get; set; }
}
