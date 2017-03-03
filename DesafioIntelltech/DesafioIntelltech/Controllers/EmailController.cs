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
using System.Net.Mail;

namespace DesafioIntelltech.Controllers
{
	[RoutePrefix("api/email")]
	public class EmailController : ApiController
	{
		private DatabaseContext db = new DatabaseContext();

		[HttpPost]
		[Route("enviar")]
		// POST: api/Email
		[ResponseType(typeof(Email))]
		public IHttpActionResult EnviarEmail(Email email)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			//db.Email.Add(email);
			//db.SaveChanges();

			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

			smtpClient.Credentials = new System.Net.NetworkCredential("estacaoiffoz@gmail.com", "sokkojou");
			//smtpClient.UseDefaultCredentials = true;
			//smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.EnableSsl = true;
			MailMessage mail = new MailMessage();

			mail.IsBodyHtml = true;
			mail.Subject = "Lista de atividades de hoje";
			mail.BodyEncoding = System.Text.Encoding.UTF8;

			string mensagem = "";
			foreach (Atividade a in email.Atividades){
				mensagem += "Atividade: " + a.Titulo + ": " + a.Descricao + "<br/>";
			}

			mail.Body = mensagem;

			//Setting From , To and CC
			mail.From = new MailAddress("estacaoiffoz@gmail.com", "Desafio IntellTech");
			mail.To.Add(new MailAddress("matheusmarquesm31@gmail.com"));
			//mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));

			smtpClient.Send(mail);

			return Ok(email);
		}

		[HttpGet]
		[Route("")]
		// GET: api/Email
		public IQueryable<Email> GetEmail()
		{
			return db.Email;
		}

		[HttpGet]
		[Route("{id}")]
		// GET: api/Email/5
		[ResponseType(typeof(Email))]
		public IHttpActionResult GetEmail(long id)
		{
			Email email = db.Email.Find(id);
			if (email == null)
			{
				return NotFound();
			}

			return Ok(email);
		}

		[HttpPut]
		[Route("{id}")]
		// PUT: api/Email/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutEmail(long id, Email email)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != email.Id)
			{
				return BadRequest();
			}

			db.Entry(email).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EmailExists(id))
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
		// POST: api/Email
		[ResponseType(typeof(Email))]
		public IHttpActionResult PostEmail(Email email)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Email.Add(email);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = email.Id }, email);
		}

		[HttpDelete]
		[Route("{id}")]
		// DELETE: api/Email/5
		[ResponseType(typeof(Email))]
		public IHttpActionResult DeleteEmail(long id)
		{
			Email email = db.Email.Find(id);
			if (email == null)
			{
				return NotFound();
			}

			db.Email.Remove(email);
			db.SaveChanges();

			return Ok(email);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool EmailExists(long id)
		{
			return db.Email.Count(e => e.Id == id) > 0;
		}
	}
}