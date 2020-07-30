using Moq;
using Xunit;
using Bogus;
using Dominio._Base;
using Dominio.Cursos;
using Dominio.PublicoAlvo;
using Dominio.Test._Builder;
using Dominio.Test._Util;

namespace Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private Faker _faker;
        private CursoDto _cursoDto;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private ArmazenadorDeCurso _armazenadorDeCurso;
        private Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvoMock;

        public ArmazenadorDeCursoTest()
        {
            _faker = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = _faker.Random.Word(),
                Descricao = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = _faker.Random.Double(600, 3000)
            };
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _conversorDePublicoAlvoMock = new Mock<IConversorDePublicoAlvo>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object, _conversorDePublicoAlvoMock.Object);

        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(x => x.Adicionar(
            It.Is<Curso>(
                c => c.Nome.Equals(_cursoDto.Nome) &&
                     c.Descricao.Equals(c.Descricao)
                )
            ));
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComId(_faker.Random.Int(1, 99999999)).ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(c => c.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar((_cursoDto)))
                .ComMensagem(Resource.NomeCursoJaExiste);
        }
    }
}
