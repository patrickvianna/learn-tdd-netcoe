using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Test._Util;
using Xunit;

namespace Dominio.Test.Cursos
{
    public class CursoTest
    {
        private string _nome;
        private string _descricao;
        private double _cargaHoraria;
        private PublicoAlvo _publicoAlvo;
        private double _valor;
        public CursoTest()
        {
            _nome = "Informática básica";
            _descricao = "Descrição do curso";
            _cargaHoraria = (double)80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = (double)950;
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

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido, _descricao, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQueUm(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                new Curso(_nome, _descricao, cargaHorariaInvalida, _publicoAlvo, _valor)).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQueUm(double valorInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, valorInvalida)).ComMensagem("Valor inválido");
        }
    }


    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }
    public class Curso
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
            if (cargaHoraria < 1) throw new ArgumentException("Carga horária inválida");
            if (valor < 1) throw new ArgumentException("Valor inválido");

            this.Nome = nome;
            this.Descricao = descricao;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;
        }
    }
}
