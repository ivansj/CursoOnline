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
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse(publicoAlvo, out PublicoAlvo publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DisperarExcecaoseExistir();

            return publicoAlvoConvertido;
        }
    }
}