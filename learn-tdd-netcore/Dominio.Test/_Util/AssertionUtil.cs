using Dominio._Base;
using System;
using Xunit;

namespace Dominio.Test._Util
{
    public static class AssertionUtil
    {
        public static void ComMensagem(this ExcecaoDeDominio exception, string mensagem)
        {
            if (exception.MensagensDeErro.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem {exception.Message}");
        }
    }
}
