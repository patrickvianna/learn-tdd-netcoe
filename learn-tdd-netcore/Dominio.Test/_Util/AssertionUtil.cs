using System;
using Xunit;

namespace Dominio.Test._Util
{
    public static class AssertionUtil
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if (exception.Message.Equals(mensagem))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem {exception.Message}");
        }
    }
}
