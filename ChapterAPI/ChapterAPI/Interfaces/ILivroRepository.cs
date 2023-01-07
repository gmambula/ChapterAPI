using ChapterAPI.Models;

namespace ChapterAPI.Interfaces
{
    public interface ILivroRepository
    {
        List<Livro> Ler();

        void Cadastrar(Livro livro);

        void Atualizar(int id, Livro livro);

        void Deletar(int id);

        Livro BuscarPorId(int id);
    }
}
