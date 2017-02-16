using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIntelltech.Models
{
    public class Atividade
    {
        public int id { get; set; }
        private string titulo { get; set; }
        private string descricao { get; set; }
        private string data { get; set; }
        private string horario { get; set; }

        private int posSituacao = 0;
        private string[] situacao = { "Não iniciada", "Em andamento", "Concluída" };

        public Atividade(int id, string titulo, string descricao, string data, string horario, int posSituacao)
        {
            this.id = id;
            this.titulo = titulo;
            this.descricao = descricao;
            this.data = data;
            this.horario = horario;
            this.posSituacao = posSituacao;
        }

        public string getSituacao()
        {
            return this.situacao[posSituacao];
        }
        
        public void setSituacao(int posSituacao)
        {
            this.posSituacao = posSituacao;
        }
    }
}