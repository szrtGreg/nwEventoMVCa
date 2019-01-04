using nwEventoMVCa.Core.Domain;
using nwEventoMVCa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwEventoMVCa.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static Event GetOrFailEvent(this IEventRepository repo, Guid id)
        {
            var @event = repo.Get(id);
            if (@event == null)
            {
                throw new Exception($"Event was not found, id: '{id}'.");
            }

            return @event;
        }
    }
}
