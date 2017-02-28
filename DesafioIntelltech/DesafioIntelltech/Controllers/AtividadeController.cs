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
	public class AtividadeController : ApiController
	{
		private DatabaseContext db = new DatabaseContext();

		// GET: api/Atividade
		public IQueryable<Atividade> GetAtividade()
		{
			return db.Atividade;
		}

		// GET: api/Atividade/5
		[ResponseType(typeof(Atividade))]
		public IHttpActionResult GetAtividade(long id)
		{
			Atividade atividade = db.Atividade.Find(id);
			if (atividade == null)
			{
				return NotFound();
			}

			return Ok(atividade);
		}

		// PUT: api/Atividade/5
		[ResponseType(typeof(void))]
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

		// POST: api/Atividade
		[ResponseType(typeof(Atividade))]
		public IHttpActionResult PostAtividade(Atividade atividade)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Atividade.Add(atividade);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = atividade.Id }, atividade);
		}

		// DELETE: api/Atividade/5
		[ResponseType(typeof(Atividade))]
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