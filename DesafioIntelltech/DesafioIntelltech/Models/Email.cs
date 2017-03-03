using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DesafioIntelltech.Models
{
	public class Email
	{
		[Key]
		public long Id { get; set; }
		public string StrEmail { get; set; }
		[DataType(DataType.DateTime)]
		public DateTimeOffset DataHora { get; set; }
		public List<Atividade> Atividades;
	}
}