public interface IRepositoryReference
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    void Dispose();
    
}