using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfExchange.BusinessLayer.Dto
{
    public class PartyDetailDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Full name of the person
        /// </summary>
        public string Name { get; set; }

        public PartyDetailDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
