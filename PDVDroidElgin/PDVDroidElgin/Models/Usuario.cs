using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public Usuario(string nome, string senha)
        {
            if (string.IsNullOrEmpty(nome)) throw new Exception("O nome é obrigatório");
            if (string.IsNullOrEmpty(senha)) throw new Exception("A senha é obrigatória");

            Nome = nome;
            Senha = senha;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
