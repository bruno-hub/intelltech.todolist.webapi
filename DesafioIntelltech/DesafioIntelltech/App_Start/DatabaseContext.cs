using DesafioIntelltech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DesafioIntelltech.App_Start
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext() : base("ConnDatabase")
		{
			
		}

		public DbSet<Atividade> Atividade { get; set; }
		public DbSet<Email> Email { get; set; }
	}

}