using System;
using Dominio._Base;

namespace Dominio.Cursos
{
    public class Curso : Entidade
    {
        private double _valor;
        private string _descricao;
        private double _cargaHoraria;
        private string _nome;
        private PublicoAlvo _publicoAlvo;

        public PublicoAlvo PublicoAlvo { get => _publicoAlvo; set => _publicoAlvo = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public double CargaHoraria { get => _cargaHoraria; set => _cargaHoraria = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }

        public Curso() { }
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            _nome = nome;
            _descricao = descricao;
            _cargaHoraria = cargaHoraria;
            _publicoAlvo = publicoAlvo;
            _valor = valor;
        }
    }
}