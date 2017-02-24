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
			Database.SetInitializer<DatabaseContext>(new DatabaseContextInitializer());
		}

		public DbSet<Atividade> Atividade { get; set; }
	}

	public class DatabaseContextInitializer : DropCreateDatabaseAlways<DatabaseContext>
	{
		protected override void Seed(DatabaseContext context)
		{
			context.Atividade.Add(new Atividade(1, "Aprender C#", "Estudar a sintaxe", "16/02/2017", "09:00", 1));
			context.Atividade.Add(new Atividade(2, "Pensar em C#", "Aprender datas em C#", "16/02/2017", "10:00", 0));
			context.Atividade.Add(new Atividade(3, "Almoçar", "Ir almoçar em algum lugar", "16/02/2017", "12:00", 2));
			context.SaveChanges();

			base.Seed(context);
		}
	}
}