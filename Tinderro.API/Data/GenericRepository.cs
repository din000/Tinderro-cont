using System.Threading.Tasks;

namespace Tinderro.API.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class   // dodaje uzytkownia lub inne obiekty
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class  // usuwa uzytkownika lub inne obiekty
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; // zwroci true jezeli zapis bedzie poprawny
        }
    }
}