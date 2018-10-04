using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio.Base
{
    public class ValidadorDeRegra
    {
        private readonly List<string> _mensagensDeErro;

        private ValidadorDeRegra()
        {
            _mensagensDeErro = new List<string>();
        }

        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }

        public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
        {
            if (temErro)
                _mensagensDeErro.Add(mensagemDeErro);
            return this;
        }

        public void DisperarExcecaoseExistir()
        {
            if (_mensagensDeErro.Any())
                throw new ExcecaoDeDominio(_mensagensDeErro);
        }

        
    }
        
    public class ExcecaoDeDominio : ArgumentException
    {
        public List<string> MensagensDeErro { get; set; }

        public ExcecaoDeDominio(List<string> mensagensDeErro)
        {
            MensagensDeErro = mensagensDeErro;
        }    
    }
}
