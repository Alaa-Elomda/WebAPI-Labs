using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registeration.DAL;

public class Employee : IdentityUser
{
    public int PerformanceRate { get; set; }
}
