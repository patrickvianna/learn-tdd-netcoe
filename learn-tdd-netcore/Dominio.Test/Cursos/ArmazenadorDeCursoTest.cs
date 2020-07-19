using System;
using Bogus;
using Dominio.Cursos;
using Dominio.Test._Builder;
using Dominio.Test._Util;
using Moq;
using Xunit;

namespace Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private CursoDto _cursoDto;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            Faker faker = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = faker.Random.Word(),
                Descricao = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Double(600, 3000)
            };
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);

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
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(x => x.ObterPeloNome(cursoJaSalvo.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar((_cursoDto)))
                .ComMensagem("Nome do curso já consta no banco de dados");
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Público alvo inválido");
        }
    }
}
