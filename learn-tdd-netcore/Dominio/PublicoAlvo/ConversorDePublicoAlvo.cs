using System;
using Dominio._Base;

namespace Dominio.PublicoAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
	{
		public ConversorDePublicoAlvo()
		{
		}

		public ePublicoAlvo Converter(string publicoAlvo)
		{
			ValidadorDeRegra.Novo()
				.Quando(!Enum.TryParse<ePublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
				.DispararExcecaoSeExistir();

			return publicoAlvoConvertido;
		}
	}
}
