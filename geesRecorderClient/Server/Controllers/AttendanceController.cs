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
        
        [HttpPost("event")]
        public async Task<IActionResult> AddNewEvent(AddNewEventDTO dto)
        {
            var attendanceProject = await _dbContext.AttendanceProjects.FindAsync(dto.ProjectId);

            attendanceProject.Events.Add(new Event
            {                
                Name = dto.EventName,
                Start = dto.Start,
                End = dto.End
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("event")]
        public async Task<IActionResult> GetEventDetails(int eventId)
        {
            var attendanceEvent = await _dbContext.Events.FindAsync(eventId);
            var personEvents = _dbContext.PersonEvents.Where(x => x.Event.Id == eventId).ToList();
            var timeIns = personEvents.Select(x => x.TimeIn).OrderByDescending(x => x);
            var timeOuts = personEvents.Select(x => x.TimeOut).OrderByDescending(x => x);

            DateTime? averageTimeIn = null, averageTimeOut = null;

            if(timeIns.Count() > 0)
            {
                var timeInFirst = timeIns.First();
                averageTimeIn = timeInFirst.AddSeconds(timeIns.Average(d => (d - timeInFirst).TotalSeconds));
            }

            if(timeOuts.Count() > 0)
            {
                var timeOutFirst = timeOuts.First();
                averageTimeOut = timeOutFirst.AddSeconds(timeOuts.Average(d => (d - timeOutFirst).TotalSeconds));
            }                

            var eventDetails = new EventDetailsDTO
            {
                EventName = attendanceEvent.Name,
                StartTime = attendanceEvent.Start,
                EndTime = attendanceEvent.End,
                MeanArrivalTime = averageTimeIn,
                MeanDepartureTime = averageTimeOut,
                TotalAttendeesCount = personEvents.Count
            };
            return Ok(eventDetails);
        }

        [HttpGet("person")]
        public async Task<IActionResult> GetPersonDetails(int personId, int projectId)
        {
            Console.WriteLine($"{personId} and {projectId}");
            var person = await _dbContext.Persons.FindAsync(personId);
            var attendanceProject = await _dbContext.AttendanceProjects.FindAsync(projectId);

            int totalEvents = attendanceProject.Events.Count;
            int attendedEvents = _dbContext.PersonEvents.Where(x => x.Person.Id == personId 
                && x.Event.AttendanceProject.Id == projectId).Count();

            var personDetails = new PersonDetailsDTO
            {
                Name = $"{person.FirstName} {person.LastName}",
                TotalEventsAttended = attendedEvents,
                TotalEventsMissed = totalEvents - attendedEvents
            };
            return Ok(personDetails);
        }
        
        [HttpPost("mark-in")]
        public async Task<IActionResult> MarkAttendanceIn(MarkAttendanceDTO dto)
        {
            Person person = null;
            var persons = _dbContext.Persons.ToList();
            foreach (var p in persons)
            {
                if (p.FingerprintIds.Contains(dto.FingerprintId))
                {
                    person = p;
                    break;
                }
            }

            if(person is null)
            {
                return BadRequest("Person not enrolled");
            }

            var attendanceEvent = await _dbContext.Events.FindAsync(dto.EventId);
            var personEvent = new PersonEvent
            {
                Event = attendanceEvent,
                Person = person,
                TimeIn = dto.AttendanceTimeStamp,
                TimeOut = attendanceEvent.End
            };            

            _dbContext.PersonEvents.Add(personEvent);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("mark-out")]
        public async Task<IActionResult> MarkAttendanceOut(MarkAttendanceDTO dto)
        {
            Person person = null;
            var persons = _dbContext.Persons.ToList();
            foreach (var p in persons)
            {
                if (p.FingerprintIds.Contains(dto.FingerprintId))
                {
                    person = p;
                    break;
                }
            }

            if (person is null)
            {
                return BadRequest("Person not enrolled");
            }

            var personEvent = _dbContext.PersonEvents
                .FirstOrDefault(x => x.Person.Id == person.Id && x.Event.Id == dto.EventId);

            personEvent.TimeOut = dto.AttendanceTimeStamp;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("enrol")]
        public async Task<IActionResult> EnrolAttendant(EnrolPersonDTO dto)
        {
            Person person = null;
            var attendanceProject = await _dbContext.AttendanceProjects.FindAsync(dto.ProjectId);
            Console.WriteLine(dto.PersonAlreadyExists);
            if (dto.PersonAlreadyExists)
            {
                bool personExistsInProject = attendanceProject.Persons
                    .SelectMany(x => x.FingerprintIds)
                    .Contains(dto.FingerprintId);
                if (personExistsInProject)
                {
                    return BadRequest("This person has already been enrolled to this project");
                }

                var persons = _dbContext.Persons.ToList();
                foreach (var p in persons)
                {
                    if (p.FingerprintIds.Contains(dto.FingerprintId))
                    {
                        person = p;
                        break;
                    }
                }

                if (person is null)
                {
                    return BadRequest("Person not enrolled");
                }


                attendanceProject.Persons.Add(person);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                person = new Person
                {
                    CustomId = dto.CustomId,
                    FingerprintIds = new List<int> { dto.FingerprintId },
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
                attendanceProject.Persons.Add(person);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            
        }
        
        [HttpPost("enrol-new")]
        public async Task<IActionResult> EnrolExisting(EnrolPersonDTO dto)
        {
            var person = _dbContext.Persons
                .FirstOrDefault(x => x.FingerprintIds.Contains(dto.FingerprintId));
            var project = await _dbContext.AttendanceProjects.FindAsync(dto.ProjectId);
            project.Persons.Add(person);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
