using CursoOnline.Dominio.Base;
using System;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public interface IConversorDePublicoAlvo
    {
        PublicoAlvo Converter(string publicoAlvo);        
    }
}