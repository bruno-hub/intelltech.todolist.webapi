using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DesafioIntelltech.Models;

namespace DesafioIntelltech.Controllers
{
	public class AtividadeController : ApiController
	{
		Atividade[] atividades = new Atividade[]
		{
			new Atividade(1, "Aprender C#", "Estudar a sintaxe", "16/02/2017", "09:00", 1),
			new Atividade(2, "Pensar em C#", "Aprender datas em C#", "16/02/2017", "10:00", 0),
			new Atividade(3, "Almoçar", "Ir almoçar em algum lugar", "16/02/2017", "12:00", 2)
		};
		
		// /api/atividade
		public IEnumerable<Atividade> GetAllAtividades()
		{
			return atividades;
		}

		// /api/atividade/?id
		public IHttpActionResult GetAtividade(int id)
		{
			var atividade = atividades.FirstOrDefault((a) => a.id == id);
			Console.WriteLine(atividade);
			if (atividade == null)
				return NotFound();

			return Ok(atividade);
		}

		public IEnumerable<Atividade> GetAtividadesPorSituacao(string situacao)
		{
			return atividades.Where(
				(p) => string.Equals(p.getSituacao(), situacao,
					StringComparison.OrdinalIgnoreCase));
		}
	}
}
