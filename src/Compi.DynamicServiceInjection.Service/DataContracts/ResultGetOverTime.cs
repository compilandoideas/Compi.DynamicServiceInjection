using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.Service.DataContracts
{
	public class ResultGetOverTime
	{
		public List<GetOverTimeDto> Response { get; set; }


		public ResultGetOverTime()
		{

		}
	}


	public class GetOverTimeDto
	{


		public decimal? PersonId { get; set; }
		public string? Name { get; set; }
		public string? LastName { get; set; }
		public decimal? RecordId { get; set; }
		public string? Code { get; set; }
		public string? Rut { get; set; }

		public string? RuleName { get; set; }
		public string? ApplicantComments { get; set; }
		public string? ApproverComments { get; set; }


		public string? Comments { get; set; }



		public bool? OverTimeOverload { get; set; }

		public int? OverTime { get; set; }
		public int? NotWorkedHours { get; set; }
		public int? AbsenceHours { get; set; }
		public int? CustodyHours { get; set; }
		public int? OpeningHours { get; set; }

		public GetOverTimeDto()
		{

		}


	}
}
