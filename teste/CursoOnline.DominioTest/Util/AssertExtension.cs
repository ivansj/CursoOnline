using System;
using Xunit;

namespace CursoOnline.DominioTest.Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if (string.Equals(exception?.Message, mensagem))
                Assert.True(true);
            else
                Assert.True(false, $"Esperava a mensagem '{mensagem}'");
        }
    }
}
