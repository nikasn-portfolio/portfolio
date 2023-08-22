using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.Api.v1
{
    /// <summary>
    /// Record REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecordsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RecordMapper _mapper;
        //private readonly RecordServiceMapper _recordServiceMapper;
        private readonly IAppBLL _bll;
        //private readonly ApplicationDbContext _context;

        /// <summary>
        /// Record controller constructor
        /// </summary>
        /// <param name="userManager">instance of userManager from dependency injection</param>
        /// <param name="autoMapper">instance of automapper from dependency injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public RecordsController(UserManager<AppUser> userManager, IMapper autoMapper, IAppBLL bll)
        {
            _userManager = userManager;
            _bll = bll;
            _mapper = new RecordMapper(autoMapper);
        }

        // GET: api/Records
        /// <summary>
        /// Extract via GET request all records with service ids
        /// </summary>
        /// <returns>RecordDTO object</returns>
        [HttpGet]
        public async Task<List<RecordDTO>> GetRecords()
        {
            var data = await _bll.RecordService.AllAsyncWithServices();
            var res = data.Select(e => _mapper.Map(e)).ToList();
            return res;
        }

        // GET: api/v1/Records/5
        /// <summary>
        /// Returns record by id
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns>RecordDTO? and status code</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordDTO?>> GetRecord(Guid id)
        {
          
            var record = await _bll.RecordService.FindAsync(id);
            if (record == null) return NotFound();
            var res = _mapper.Map(record);

            return Ok(res);
        }

        // PUT: api/Records/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Updates record to database
        /// </summary>
        /// <param name="id">id of record</param>
        /// <param name="record">RecordDTO object</param>
        /// <returns>Status code</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(Guid id, RecordDTO record)
        {
            if (id != Guid.Parse(record.Id!))
            {
                return BadRequest();
            }
            var recordUpdated = (await _bll.RecordService.GetRecordsWithServices()).FirstOrDefault(s => s.Id == id);

            recordUpdated!.AppUserId = Guid.Parse(record.AppUserId);
            recordUpdated.AppUser = _userManager.Users.FirstOrDefault(u => u.Id == recordUpdated.AppUserId);
            recordUpdated.ClientId = Guid.Parse(record.ClientId);
            recordUpdated.Client = await _bll.ClientService.GetClientById(Guid.Parse(record.ClientId));
            recordUpdated.Comment = record.Comment;
            recordUpdated.Title = record.Title;
            recordUpdated.IsHidden = Boolean.Parse(record.IsHidden);
            var listOfRecordServicesResult = new List<RecordService>();
            if (record.RecordServices != null)
            {
                foreach (var item1 in record.RecordServices!)
                {
                    var savedRecordService =
                        _bll.RecordServiceService.FindRecordServiceByChild(item1.RecordId, item1.ServiceId);
                    if (savedRecordService == null!)
                    {
                        var newRecordService = new RecordService
                        {
                            RecordId = item1.RecordId,
                            ServiceId = item1.RecordId,
                            Record = await _bll.RecordService.GetRecordById(item1.RecordId),
                            Service = await _bll.Service.GetServiceById(item1.ServiceId)
                        };
                        listOfRecordServicesResult.Add(newRecordService);
                    }
                    else
                    {
                        listOfRecordServicesResult.Add(savedRecordService);
                    }
                }
            
                _bll.RecordServiceService.RemoveListOfRecordServicesAsync(recordUpdated.RecordServices!.ToList());
                foreach (var recordService in listOfRecordServicesResult)
                {
                    _bll.RecordServiceService.Add(recordService);
                } 
            }
            
            recordUpdated.RecordServices = listOfRecordServicesResult;


            recordUpdated.StartTime = record.StartTime.DateTime; //TimeZoneInfo.ConvertTime(record.StartTime, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time")).DateTime;
            recordUpdated.EndTime = record.EndTime.DateTime; //TimeZoneInfo.ConvertTime(record.EndTime, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time")).DateTime;

            _bll.RecordService.SetEntry(recordUpdated).State = EntityState.Modified;
            
            
            await _bll.SaveChangesAsync();
            
            

            return Ok();
        }

        // POST: api/Records
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*{
            "id": "",
            "recordServices": [
            {
                "serviceId": "b124cea8-bb68-4037-9462-07de4816154e",
                "recordId": ""
            }
            ],
            "serviceId": "b124cea8-bb68-4037-9462-07de4816154e",
            "title": "testPost",
            "startTime": "2023-04-18T15:00:08.703Z",
            "endTime": "2023-04-18T17:00:08.707Z",
            "comment": "testPost",
            "isHidden": "false",
            "appUserId": "2ce65d98-f7e7-4ad9-89f0-e66dcc3f8029",
            "clientId": "1e3c9ca1-88d6-4bc8-9af0-39a5e72724f1"
        }*/
        /// <summary>
        /// Add record to database
        /// </summary>
        /// <param name="recordData">RecordDTO object</param>
        /// <returns>Status code and RecordDTO object</returns>
        [HttpPost]
        public async Task<ActionResult<RecordDTO>> PostRecord([FromBody] RecordDTO recordData)
        {
            var record = _bll.RecordService.Add(new Record()
            {
                Comment = recordData.Comment,
                EndTime = recordData.EndTime.DateTime, //TimeZoneInfo.ConvertTime(recordData.EndTime, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time")).DateTime,
                IsHidden = Boolean.Parse(recordData.IsHidden),
                StartTime = recordData.StartTime.DateTime, //TimeZoneInfo.ConvertTime(recordData.StartTime, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time")).DateTime,
                Title = recordData.Title,
                AppUserId = Guid.Parse(recordData.AppUserId),
                ClientId = Guid.Parse(recordData.ClientId),
                Client = _bll.ClientService.GetClientById(Guid.Parse(recordData.ClientId)).Result,
                AppUser = _userManager.Users.FirstOrDefault(s => s.Id == Guid.Parse(recordData.AppUserId))

            });
            var listOfRecordServices = new List<RecordService>();
            if (recordData.RecordServices != null)
            {
                foreach (var recordService in recordData.RecordServices!)
                {
                    var newRecordService = new RecordService
                    {
                        RecordId = record.Id,
                        ServiceId = recordService.ServiceId,
                        Record = record,
                        Service = _bll.Service.GetServiceById(recordService.ServiceId).Result
                    };
                    listOfRecordServices.Add(newRecordService);
                    _bll.RecordServiceService.Add(newRecordService);
                }
            }
            
            record.RecordServices = listOfRecordServices;

            await _bll.SaveChangesAsync();


            return Ok(recordData);
        }

        // DELETE: api/Records/5
        /// <summary>
        /// Delete record from database
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns>IEnumerable[Record]</returns>
        [HttpDelete("{id}")]
        public async Task<IEnumerable<Record>> DeleteRecord(Guid id)
        {
            _bll.RecordService.Remove(_bll.RecordService.GetRecordById(id).Result);
            await _bll.SaveChangesAsync();
            return await _bll.RecordService.GetRecordsWithServices(); //All
        }

        /*private bool RecordExists(Guid id)
        {
            var res = _bll.RecordService.GetRecordById(id).Result;
            return res == null!;
        }*/
    }
}
