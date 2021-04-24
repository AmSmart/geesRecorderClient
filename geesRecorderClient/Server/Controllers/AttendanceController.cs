using geesRecorderClient.Server.Data;
using geesRecorderClient.Shared.DTOs;
using geesRecorderClient.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Controllers
{
    [ApiController]
    [Route("attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendanceController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewAttendance(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("add-event")]
        public async Task<IActionResult> AddNewEvent(AddNewEventDTO dto)
        {            
            _dbContext.Events.Add(new Event
            {
                AttendanceProject = new AttendanceProject
                {
                    Id = dto.ProjectId
                },
                Name = dto.EventName,
                Start = dto.Start,
                End = dto.End
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("mark-in")]
        public async Task<IActionResult> MarkAttendanceIn(MarkAttendanceDTO dto)
        {
            var personEvent = new PersonEvent
            {
                Event = new Event { Id = dto.EventId },
                Person = new Person { Id = dto.PersonId },
                TimeIn = dto.AttendanceTimeStamp,                
            };            

            _dbContext.PersonEvents.Add(personEvent);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("mark-out")]
        public async Task<IActionResult> MarkAttendanceOut(MarkAttendanceDTO dto)
        {
            var personEvent = _dbContext.PersonEvents
                .FirstOrDefault(x => x.Person.Id == dto.PersonId && x.Event.Id == dto.EventId);

            personEvent.TimeOut = dto.AttendanceTimeStamp;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("enrol-new")]
        public async Task<IActionResult> EnrolAttendant(EnrolPersonDTO dto)
        {
            _dbContext.Persons.Add(new Person
            {
                AttendanceProjects = new List<AttendanceProject>
                {
                    new AttendanceProject{ Id = dto.ProjectId }
                },
                CustomId = dto.Person.CustomId,
                FingerprintIds = dto.Person.FingerprintIds,
                FirstName = dto.Person.FirstName,
                LastName = dto.Person.LastName
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("enrol-new")]
        public async Task<IActionResult> EnrolExisting(EnrolPersonDTO dto)
        {
            var person = _dbContext.Persons
                .FirstOrDefault(x => x.FingerprintIds.Contains(dto.FingerPrintId));
            var project = await _dbContext.AttendanceProjects.FindAsync(dto.ProjectId);
            project.Persons.Add(person);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
