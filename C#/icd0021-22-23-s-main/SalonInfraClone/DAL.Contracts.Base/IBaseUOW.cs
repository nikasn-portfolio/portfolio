namespace DAL.Contracts.Base;

public interface IBaseUOW
{
    Task<int> SaveChangesAsync();
}