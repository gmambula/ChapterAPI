using ChapterAPI.Models;

namespace ChapterAPI.Interfaces
{
    public interface ILivroRepository
    {
        List<Livro> Ler();
    }
}
