using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DesafioIntelltech.Models
{
	public class Atividade
	{
		[Key]
		public long Id { get; set; }
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		[DataType(DataType.DateTime)]
		public DateTimeOffset DataHora { get; set; }
		public bool Concluida { get; set; }
		public string GUID { get; set; }

		public Atividade()
		{
			GUID = Guid.NewGuid().ToString();
		}

		//private int PosSituacao = 0;
		//public static string[] Situacao = { "Não iniciada", "Em andamento", "Concluída" };
		
		//public string getSituacao()
		//{
		//	return Situacao[PosSituacao];
		//}

		//public void setSituacao(int PosSituacao)
		//{
		//	this.PosSituacao = PosSituacao;
		//}
	}
}