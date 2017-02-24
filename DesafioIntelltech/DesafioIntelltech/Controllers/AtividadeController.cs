using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DesafioIntelltech.Models;
using System.IO;
using DesafioIntelltech.App_Start;

namespace DesafioIntelltech.Controllers
{
	public class AtividadeController : ApiController
	{
		private DatabaseContext db = new DatabaseContext();

		Atividade[] Atividades = new Atividade[]
		{
			new Atividade(1, "Aprender C#", "Estudar a sintaxe", "16/02/2017", "09:00", 1),
			new Atividade(2, "Pensar em C#", "Aprender datas em C#", "16/02/2017", "10:00", 0),
			new Atividade(3, "Almoçar", "Ir almoçar em algum lugar", "16/02/2017", "12:00", 2)
		};
		
		// /api/atividade
		public IEnumerable<Atividade> GetAllAtividades()
		{
			return db.Atividade;
		}
		

		// /api/atividade/?id
		public IHttpActionResult GetAtividade(int Id)
		{
			var atividade = Atividades.FirstOrDefault((a) => a.Id == Id);
			
			if (atividade == null)
				return NotFound();

			return Ok(atividade);
		}

		public IEnumerable<Atividade> GetAtividadesPorSituacao(string Situacao)
		{
			return Atividades.Where(
				(p) => string.Equals(p.getSituacao(), Situacao,
					StringComparison.OrdinalIgnoreCase));
		}
	}
}
