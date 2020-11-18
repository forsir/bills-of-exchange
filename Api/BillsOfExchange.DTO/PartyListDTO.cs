using System;

namespace BillsOfExchange.DTO
{
	public class PartyListDTO
	{
		public int Id { get; set; }

		/// <summary>
		/// Full name of the person
		/// </summary>
		public string Name { get; set; }

		public PartyListDTO(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
