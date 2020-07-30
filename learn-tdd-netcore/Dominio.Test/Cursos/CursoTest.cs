using System;
using Bogus;
using Xunit;
using ExpectedObjects;
using Dominio.Cursos;
using Dominio.Test._Builder;
using Dominio.Test._Util;
using Dominio._Base;
using Dominio.PublicoAlvo;

namespace Dominio.Test.Cursos
{
    public class CursoTest
    {
        private Faker _faker;
        private string _nome;
        private string _descricao;
        private double _cargaHoraria;
        private ePublicoAlvo _publicoAlvo;
        private double _valor;
        public CursoTest()
        {
            _faker = new Faker();
            _nome = _faker.Random.Word();
            _descricao = _faker.Lorem.Paragraph();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = ePublicoAlvo.Estudante;
            _valor = _faker.Random.Double(50, 1000);
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao = _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor, cursoEsperado.Descricao);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void NaoDeveTerUmaCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo()
                        .ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build()).ComMensagem("Valor inválido");
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = "José";
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido))
            .ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = 20.5;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void NaoDeveAlterarCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida))
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = _faker.Random.Double(1, 2000.99);

            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void NaoDeveAlterarComValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarValor(valorInvalido))
                .ComMensagem(Resource.ValorInvalido);
        }
    }
}
