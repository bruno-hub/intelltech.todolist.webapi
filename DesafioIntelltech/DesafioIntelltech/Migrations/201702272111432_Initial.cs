namespace DesafioIntelltech.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Atividades",
				c => new
				{
					Id = c.Long(nullable: false, identity: true),
					Titulo = c.String(),
					Descricao = c.String(),
					DataHora = c.DateTimeOffset(nullable: false, precision: 7),
				})
				.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropTable("dbo.Atividades");
		}
	}
}
