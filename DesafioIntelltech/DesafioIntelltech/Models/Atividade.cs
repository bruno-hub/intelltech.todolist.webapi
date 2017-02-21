using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIntelltech.Models
{
	public class Atividade
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		public string Data { get; set; }
		public string Horario { get; set; }

		private int PosSituacao = 0;
		public static string[] Situacao = { "Não iniciada", "Em andamento", "Concluída" };

		public Atividade(int Id, string Titulo, string Descricao, string Data, string Horario, int PosSituacao)
		{
			this.Id = Id;
			this.Titulo = Titulo;
			this.Descricao = Descricao;
			this.Data = Data;
			this.Horario = Horario;
			this.PosSituacao = PosSituacao;
		}

		public string getSituacao()
		{
			return Situacao[PosSituacao];
		}
		
		public void setSituacao(int PosSituacao)
		{
			this.PosSituacao = PosSituacao;
		}
	}
}