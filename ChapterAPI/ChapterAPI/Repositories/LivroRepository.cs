using ChapterAPI.Contexts;
using ChapterAPI.Interfaces;
using ChapterAPI.Models;

namespace ChapterAPI.Repositories
{
    public class LivroRepository : ILivroRepository
    {
       
        private readonly ChapterContext _chapterContext;

        public LivroRepository(ChapterContext context)
        {
            _chapterContext = context;
        }
        
        
        
        
        public List<Livro> Ler()
        {
            return _chapterContext.Livros.ToList();
        }
    }
}
