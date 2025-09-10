using Junior_Student_Portal.Models;
namespace Junior_Student_Portal.Repository
{
    public interface IStudentRepository
    {
        Task<bool> AddAsync(Student student);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
    }
}
