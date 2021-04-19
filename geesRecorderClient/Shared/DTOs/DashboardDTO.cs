using geesRecorderClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record DashboardDTO
    {
        public string UserName { get; set; } //TODO: Add username to sign up

        public List<Project> Projects { get; set; }
    }
}
