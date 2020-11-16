using System;
using System.Collections.Generic;
using BillsOfExchange.Base;

namespace BillsOfExchange.Extensions
{
    /// <summary>
    /// Mapper Extensions
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Zkrácená varianta mapování IMapper
        /// </summary>
        /// <typeparam name="TSource">Typ zdrojového objektu</typeparam>
        /// <typeparam name="TDestination">Typ cílového objektu</typeparam>
        /// <param name="mapper">IMapper</param>
        /// <param name="source">Zdrojový objekt</param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, TSource source) where TDestination : new()
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            if ((object) source == null)
                throw new ArgumentNullException(nameof(source));
            TDestination instance = Activator.CreateInstance<TDestination>();
            mapper.Map(source, instance);
            return instance;
        }

        /// <summary>
        /// Zkrácená varianta mapování IMapper pro pole
        /// </summary>
        /// <typeparam name="TSource">Typ zdrojového objektu</typeparam>
        /// <typeparam name="TDestination">Typ cílového objektu</typeparam>
        /// <param name="mapper">IMapper</param>
        /// <param name="source">Zdrojový objekt</param>
        /// <returns></returns>
        public static List<TDestination> MapList<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source) where TDestination : new()
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var instance1 = Activator.CreateInstance<List<TDestination>>();
            foreach (TSource source1 in source)
            {
                TDestination instance2 = Activator.CreateInstance<TDestination>();
                mapper.Map(source1, instance2);
                instance1.Add(instance2);
            }

            return instance1;
        }
    }
}