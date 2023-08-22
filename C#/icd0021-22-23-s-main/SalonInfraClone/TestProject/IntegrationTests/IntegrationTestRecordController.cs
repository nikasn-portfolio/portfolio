using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AngleSharp.Common;
using DAL;
using Domain.App.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol;
using Public.DTO.v1;
using Xunit.Abstractions;
using Record = Domain.App.Record;

namespace TestProject.IntegrationTests;

public class IntegrationTestRecordController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _context;


    public IntegrationTestRecordController(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>()!;
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }


    [Fact]
    public async Task Get_Records()
    {
        // Arrange
        var listOfTitles = new List<string>()
        {
            "test0",
            "test1",
            "test2",
            "test3",
            "test4",
        };
        // Act
        var response = await _client.GetAsync("api/v1.0/Records");
        var jsonStream = await response.Content.ReadAsStreamAsync();
        var jsonDocument = await JsonDocument.ParseAsync(jsonStream);

        // Assert
        response.EnsureSuccessStatusCode();
        _testOutputHelper.WriteLine(jsonDocument.RootElement.ToString());
        foreach (var record in jsonDocument.RootElement.EnumerateArray())
        {
            // Access properties of each record
            var title = record.GetProperty("title").GetString();

            // Do something with the record data
            Assert.Contains(title, listOfTitles);
        }
        
    }

    [Fact]
    public async Task Get_RecordById()
    {
        // Arrange
        var response = await _client.GetAsync("api/v1.0/Records");
        var jsonStream = await response.Content.ReadAsStreamAsync();
        var jsonDocument = await JsonDocument.ParseAsync(jsonStream);
        var recordId = jsonDocument.RootElement[0].GetProperty("id").GetString();
        // Act
        var response2 = await _client.GetAsync($"api/v1.0/Records/{recordId}");
        //var jsonStream2 = await response2.Content.ReadAsStreamAsync();
        //var jsonDocument2 = await JsonDocument.ParseAsync(jsonStream2);
        //var res = jsonDocument2.RootElement.GetProperty("id").GetString();
        // Assert
        response2.EnsureSuccessStatusCode();
        //Assert.Equal(recordId, res);
    }
    
    [Fact]
    public async Task Get_RecordByIdNotFound()
    {
        // Arrange
        
        var recordId = Guid.NewGuid().ToString();
        // Act
        var response = await _client.GetAsync($"api/v1.0/Records/{recordId}");

        // Assert
        response.StatusCode.Equals(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Get_PutRecordWithOutServices()
    {
        // Arrange
        var recordId = _context.Records.First().Id;
        var record = new RecordDTO()
        {
            Id = recordId.ToString(),
            Title = "test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Comment = "test",
            IsHidden = "false",
            AppUserId = _userManager.Users.First().Id.ToString(),
            ClientId = _context.Clients.First().Id.ToString()

        };
        // Act
        var response = await _client.PutAsJsonAsync($"api/v1.0/Records/{recordId}", record);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Get_PutRecordWithServices()
    {
        // Arrange
        
        var recordFromDb = _context.Records.Include(r => r.RecordServices).First();
        var record = new RecordDTO()
        {
            Id = recordFromDb.Id.ToString(),
            Title = "test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Comment = "test",
            IsHidden = "false",
            AppUserId = _userManager.Users.First().Id.ToString(),
            ClientId = _context.Clients.First().Id.ToString()


        };
        var listOfRecordServices = new List<RecordServiceDTO>();
        if (recordFromDb.RecordServices != null && recordFromDb.RecordServices!.Count == 1)
        {
            
            foreach (var recordService in recordFromDb.RecordServices)
            {
                listOfRecordServices.Add(new RecordServiceDTO()
                {
                    RecordId = recordService.RecordId,
                    ServiceId = recordService.ServiceId
                });
            }

            var newServiceId = _context.Services.FirstOrDefault(s => s.Id != listOfRecordServices.First().ServiceId)!.Id;
            listOfRecordServices.Add(new RecordServiceDTO()
            {
                RecordId = Guid.Parse(record.Id),
                ServiceId = newServiceId
            });
        }
        else
        {
            if (recordFromDb.RecordServices is { Count: > 1 })
            {
                record.RecordServices.Remove(record.RecordServices.GetItemByIndex(record.RecordServices.Count - 1));
                foreach (var recordService in recordFromDb.RecordServices)
                {
                    listOfRecordServices.Add(new RecordServiceDTO()
                    {
                        RecordId = recordService.RecordId,
                        ServiceId = recordService.ServiceId
                    });
                }
            }
            else
            {
                var serviceId = _context.Services.First().Id;
                listOfRecordServices.Add(new RecordServiceDTO()
                {
                    RecordId = Guid.Parse(record.Id),
                    ServiceId = serviceId
                });
            }
            
        }
        record.RecordServices = listOfRecordServices;
        // Act
        var response = await _client.PutAsJsonAsync($"api/v1.0/Records/{record.Id}", record);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Put_RecordWithDifferentIds()
    {
        // Arrange
        
        var recordId = Guid.NewGuid().ToString();
        var record = new RecordDTO()
        {
            Id = Guid.NewGuid().ToString(),
            Title = "test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Comment = "test",
            IsHidden = "false",
            AppUserId = _userManager.Users.First().Id.ToString(),
            ClientId = _context.Clients.First().Id.ToString()


        };
        // Act
        var response = await _client.PutAsJsonAsync($"api/v1.0/Records/{recordId}", record);


        // Assert
        response.StatusCode.Equals(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task Post_RecordWithOutServices()
    {
        // Arrange
        var record = new RecordDTO()
        {
            Id = "",
            Title = "test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Comment = "test",
            IsHidden = "false",
            AppUserId = _userManager.Users.First().Id.ToString(),
            ClientId = _context.Clients.First().Id.ToString()


        };
        // Act
        var response = await _client.PostAsync("api/v1.0/Records", new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json"));


        // Assert
        response.Equals(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Post_RecordWithServices()
    {
        // Arrange
        var record = new RecordDTO()
        {
            Id = "",
            Title = "test",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Comment = "test",
            IsHidden = "false",
            AppUserId = _userManager.Users.First().Id.ToString(),
            ClientId = _context.Clients.First().Id.ToString(),
            RecordServices = new List<RecordServiceDTO>()
            {
                new RecordServiceDTO()
                {
                    RecordId = Guid.NewGuid(),
                    ServiceId = _context.Services.First().Id
                }
            }


        };
        // Act
        var response = await _client.PostAsync("api/v1.0/Records", new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json"));


        // Assert
        response.Equals(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Delete_Record()
    {
        // Arrange
        var recordId = _context.Records.First().Id;
        // Act
        var response = await _client.DeleteAsync($"api/v1.0/Records/{recordId}");


        // Assert
        response.Equals(StatusCodes.Status200OK);
    }

}