using BLL.Contracts.App;
using DAL;
using DAL.Contracts.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace TestProject.ServicesTests;

public class RecordServiceTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _context;
    private readonly IAppBLL _bll;
    private readonly IAppUOW _uow;


    public RecordServiceTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        _uow = serviceProvider.GetRequiredService<IAppUOW>();
        
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _bll = serviceProvider.GetRequiredService<IAppBLL>();
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    [Fact]
    public async Task CheckDataBaseSeedingWithRecords()
    {
        // Arrange

        // Act
        var recordsCount = _context.Records.Count();


        // Assert
        Assert.True(recordsCount >= 0);
    }
    [Fact]
    public async Task BLL_GetRecordsWithServices()
    {
        // Arrange

        // Act
        var recordService = await _bll.RecordService.GetRecordsWithServices();
        _testOutputHelper.WriteLine(recordService.First().RecordServices.ToString());

        // Assert
        Assert.NotNull(recordService);
        Assert.True(recordService.Count() >= 5);
        Assert.True(recordService.FirstOrDefault(s => s.RecordServices?.Count != 0) != null);
        
    }
    
    [Fact]
    public async Task BLL_GetBllDtoRecordWithServices()
    {
        // Arrange

        // Act
        var recordService = await _bll.RecordService.AllAsyncWithServices();
        _testOutputHelper.WriteLine(recordService.Count().ToString());

        // Assert
        Assert.NotNull(recordService);
        Assert.True(recordService.FirstOrDefault(s => s.RecordServices?.Count != 0) != null);
        
    }
    
    [Fact]
    public async Task BLL_GetRecordsById()
    {
        // Arrange

        // Act
        var recordService = await _bll.RecordService.GetRecordById(_context.Records.First().Id);

        // Assert
        Assert.NotNull(recordService);
        
        
    }
    
    [Fact]
    public async Task BLL_SetEntry()
    {
        // Arrange

        // Act
        var recordService = _bll.RecordService.SetEntry(_context.Records.First());
        recordService.State = EntityState.Unchanged;

        // Assert
        Assert.True(recordService.State == EntityState.Unchanged);
        
        
    }
    
    [Fact]
    public async Task BLL_Add()
    {
        // Arrange
        var record = _context.Records.First();
        record.Id = Guid.NewGuid();
        record.Title = "testBLLAdd";
        // Act
        var recordService = _bll.RecordService.Add(record);
        await _bll.SaveChangesAsync();

        // Assert
        Assert.True(_context.Records.FirstOrDefault(e => e.Title == "testBLLAdd") != null);
        
        
    }
    
    [Fact]
    public async Task BLL_Remove()
    {
        // Arrange
        var record = _context.Records.First();
        var recordId = record.Id;
        // Act
        var recordService = _bll.RecordService.Remove(record);
        await _bll.SaveChangesAsync();

        // Assert
        Assert.True(_context.Records.FirstOrDefault(e => e.Id == recordId) == null);
        
        
    }
    
    [Fact]
    public async Task BLL_Update()
    {
        // Arrange
        var record = _context.Records.First();
        record.Title = "xxx";
        // Act
        var recordService = _bll.RecordService.Update(record);
        await _bll.SaveChangesAsync();

        // Assert
        Assert.True(_context.Records.FirstOrDefault(e => e.Id == record.Id)!.Title == "xxx");
        
        
    }
    
    [Fact]
    public async Task EFBaseRepository_GetAllAsync()
    {
        // Arrange

        // Act
        var recordService = await _uow.RecordRepository.AllAsync();

        // Assert
        Assert.NotNull(recordService);
        Assert.True(recordService.Count() >= 2);
        
        
    }

    [Fact]
    public async Task EFBaseRepository_RemoveAsync()
    {
        // Arrange
        var record = _context.Records.First();
        // Act
        var recordService = await _uow.RecordRepository.RemoveAsync(record.Id);

        // Assert
        Assert.NotNull(recordService);
    }
}