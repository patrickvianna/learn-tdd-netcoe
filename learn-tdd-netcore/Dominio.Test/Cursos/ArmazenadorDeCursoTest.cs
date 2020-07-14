using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Dominio.Cursos;
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
                Name = faker.Random.Word(),
                Descricao = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Double(50, 1000),
                PublicoAlvo = 1,
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
                c => c.Nome.Equals(_cursoDto.Name) &&
                     c.Descricao.Equals(c.Descricao)
                )
            ));
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private ICursoRepositorio _cursoRepositorio;
        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso(cursoDto.Name, cursoDto.Descricao, cursoDto.CargaHoraria, PublicoAlvo.Estudante, cursoDto.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }
    public class CursoDto
    {
        public string Name { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public double PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}
