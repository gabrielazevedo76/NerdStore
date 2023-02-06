using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    public class Dimensoes
    {
        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DescricaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        public void Validar()
        {
            Validacoes.ValidarSeMenorQue(Altura, 1, "O campo Altura não pode ser menor que 1");
            Validacoes.ValidarSeMenorQue(Largura, 1, "O campo Largura não pode ser menor que 1");
            Validacoes.ValidarSeMenorQue(Profundidade, 1, "O campo Profundidade não pode ser menor que 1");
        }
    }
}
