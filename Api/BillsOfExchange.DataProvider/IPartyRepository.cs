﻿using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.DataProvider
{
    public interface IPartyRepository
    {
        IEnumerable<Party> Get(int take, int skip);

        IReadOnlyList<Party> GetByIds(IReadOnlyList<int> ids);
    }
}