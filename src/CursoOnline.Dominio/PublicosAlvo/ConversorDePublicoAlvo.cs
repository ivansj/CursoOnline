using CursoOnline.Dominio.Base;
using System;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public ConversorDePublicoAlvo()
        {
        }

        public PublicoAlvo Converter(string publicoAlvo)
        {
            PublicoAlvo publicoAlvoConvertido;
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse(publicoAlvo, out publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DisperarExcecaoseExistir();
            
            return publicoAlvoConvertido;
        }
    }
}