using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi.DynamicServiceInjection.Service.DataContracts
{
	public class DataResult
	{
		public List<DataEvent> data { get; set; }

		public DataResult()
		{

		}
	}


	public class DataEvent
	{
		public int Id { get; set; }
		public string TypeEvent { get; set; }


		public DataEvent(int id, string typeEvent)
		{
			Id = id;
			TypeEvent = typeEvent;

		}
	}
}
