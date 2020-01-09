using CursoOnline.Dominio.Base;
using Xunit;

namespace CursoOnline.DominioTest.Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ExcecaoDeDominio exception, string mensagem)
        {
            //if (string.Equals(exception?.Message, mensagem))
            if (exception.MensagensDeErro.Contains(mensagem))
                Assert.True(true);
            else
                Assert.True(false, $"Esperava a mensagem '{mensagem}'");
        }
    }
}
