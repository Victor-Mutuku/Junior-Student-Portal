using Junior_Student_Portal.Data;
using Junior_Student_Portal.Models;
using Microsoft.EntityFrameworkCore;
namespace Junior_Student_Portal.Repository
{
    public class StudentRepository : IStudentRepository //This a Declared  class that implements the interface for all methods declared in it.
    {
        private readonly ApplicationDbContext _context;//private field that holds a reference to the database context.

        public StudentRepository(ApplicationDbContext context)//Is a constructor that receives ApplicationDbContext via dependecy injection.
        {
            _context = context;
        }
        //handles all the data operations(add,update,delete) also calls _context.SaveChanges() internally since contoller does not touch the DbContext directly
        public async Task<bool> AddAsync(Student student) //Used to insert New student ,track the object(student),excutes the SQL insert in the Database & confirms the operation was successful.
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()//Fetches all the students from the Database.
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)//Searches the Database for a Student with specified Id.
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);//Finds a student in the Database using the Id.
            if (existingStudent == null) return false;
            _context.Entry(existingStudent).CurrentValues.SetValues(student);//This replaces the values effectively without re-adding the object(student)
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)//Looks for the student by Id.
        {
            var existingstudent = await _context.Students.FindAsync(id);
            if (existingstudent == null) return false;
            _context.Students.Remove(existingstudent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}