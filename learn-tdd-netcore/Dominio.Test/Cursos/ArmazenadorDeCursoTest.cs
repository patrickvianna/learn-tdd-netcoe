using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Cursos;
using Moq;
using Xunit;

namespace Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                Name = "Curso A",
                Descricao = "Descrição",
                CargaHoraria = 80,
                PublicoAlvo = 1,
                Valor = 850.00
            };
            var cursoRepositorioMock = new Mock<ICursoRepositorio>();
            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);
            armazenadorDeCurso.Armazenar(cursoDto);

            cursoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Curso>()));
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
        public int CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}
