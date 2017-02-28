namespace DesafioIntelltech.Migrations
{
	using Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<DesafioIntelltech.App_Start.DatabaseContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(DesafioIntelltech.App_Start.DatabaseContext context)
		{
			context.Atividade.AddOrUpdate(new Atividade() { Id = 1, Titulo = "Estudar C#", Descricao = "Aprender a linguagem", DataHora = DateTimeOffset.UtcNow });
			context.Atividade.AddOrUpdate(new Atividade() { Id = 2, Titulo = "Pensar em C#", Descricao = "Parar de confundir com Java", DataHora = DateTimeOffset.UtcNow });
			context.Atividade.AddOrUpdate(new Atividade() { Id = 3, Titulo = "Ir almo�ar", Descricao = "Comer um croasonho", DataHora = DateTimeOffset.UtcNow });

		}
	}
}
