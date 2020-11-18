using System;

namespace BillsOfExchange.BusinessLayer.Dto
{
    public class PartyListDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Full name of the person
        /// </summary>
        public string Name { get; set; }

        public PartyListDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
