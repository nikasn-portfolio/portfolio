namespace DAL.FileSystemRepository;

public class BaseFileSystemRepository : IBaseRepository
{
    
    
    public string? Name { get; } = "FS";
    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
    
    
}