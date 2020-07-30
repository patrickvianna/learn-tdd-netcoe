using Dominio.Cursos;
using Dominio.PublicoAlvo;
using System;

namespace Dominio.Test._Builder
{
    public class CursoBuilder
    {
        private string _nome = "Informática básica";
        private string _descricao = "Descrição do curso";
        private double _cargaHoraria = 80;
        private ePublicoAlvo _publicoAlvo = ePublicoAlvo.Estudante;
        private double _valor = 950;
        private int _id;

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComId(int id)
        {
            _id = id;

            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(_nome, _cargaHoraria, _publicoAlvo,_valor, _descricao);
            var propertyInfo = curso.GetType().GetProperty("Id");
            propertyInfo.SetValue(curso, Convert.ChangeType(_id, propertyInfo.PropertyType, null));

            return curso;
        }
    }
}
