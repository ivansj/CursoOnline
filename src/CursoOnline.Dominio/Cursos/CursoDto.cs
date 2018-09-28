namespace CursoOnline.Dominio.Cursos
{

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
        public int Id { get; set; }
    }
}
