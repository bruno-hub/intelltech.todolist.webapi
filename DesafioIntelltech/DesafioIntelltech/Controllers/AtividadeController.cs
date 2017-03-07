using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DesafioIntelltech.App_Start;
using DesafioIntelltech.Models;

namespace DesafioIntelltech.Controllers
{
	[RoutePrefix("api/atividade")]
	public class AtividadeController : ApiController
	{
		private DatabaseContext db = new DatabaseContext();

		[HttpGet]
		[Route("")]
		// GET: api/Atividade
		public IQueryable<Atividade> GetAtividade()
		{
			return db.Atividade;
		}

		[HttpGet]
		[Route("{id}")]
		// GET: api/Atividade/5
		public IHttpActionResult GetAtividade(long id)
		{
			Atividade atividade = db.Atividade.Find(id);
			if (atividade == null)
			{
				return NotFound();
			}
			//new Guid
			return Ok(atividade);
		}

		[HttpPut]
		[Route("{id}")]
		// PUT: api/Atividade/5
		public IHttpActionResult PutAtividade(long id, Atividade atividade)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != atividade.Id)
			{
				return BadRequest();
			}

			db.Entry(atividade).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AtividadeExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		[HttpPost]
		[Route("")]
		//POST: api/Atividade
		public IHttpActionResult PostAtividade(Atividade atividade)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			db.Atividade.Add(atividade);
			db.SaveChanges();

			return Ok(atividade);
		}

		[HttpPost]
		[Route("sync")]
		// POST: api/Atividade/sync
		public IHttpActionResult PostAtividade(List<Atividade> atividades)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			for (int c = 0; c < atividades.Count; c++)
			{
				db.Atividade.Add(atividades[c]);
			}
			db.SaveChanges();

			return Ok(atividades);
		}

		[HttpDelete]
		[Route("{id}")]
		// DELETE: api/Atividade/5
		public IHttpActionResult DeleteAtividade(long id)
		{
			Atividade atividade = db.Atividade.Find(id);
			if (atividade == null)
			{
				return NotFound();
			}

			db.Atividade.Remove(atividade);
			db.SaveChanges();

			return Ok(atividade);
		}

		[HttpDelete]
		[Route("delete/{titulo}")]
		// DELETE: api/Atividade/delete/{titulo}
		public IHttpActionResult DeleteAtividade(string titulo)
		{
			Atividade atividade = db.Atividade.Find(titulo);
			if (atividade == null)
			{
				return NotFound();
			}

			db.Atividade.Remove(atividade);
			db.SaveChanges();

			return Ok(atividade);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool AtividadeExists(long id)
		{
			return db.Atividade.Count(e => e.Id == id) > 0;
		}
	}
}