﻿namespace ChapterAPI.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int QuantidadeDePaginas { get; set; }    
        public bool Disponivel { get; set; }    
    }
}