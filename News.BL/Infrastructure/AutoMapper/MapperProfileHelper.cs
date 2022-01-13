using News.BL.Interfaces.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace News.BL.Infrastructure.AutoMapper
{
    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }

    public static class MapperProfileHelper
    {
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = types.Where(t => t.GetInterfaces().Any(i => i.IsGenericType &&
                                                                       i.GetGenericTypeDefinition() ==
                                                                       typeof(IMapFrom<>)) &&
                                            !t.IsAbstract &&
                                            !t.IsInterface).Select(s => new Map
                                            {
                                                Source = s.GetInterfaces().First().GetGenericArguments().First(),
                                                Destination = s
                                            }).ToList();

            return mapsFrom;
        }

        public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = types.Where(t => typeof(IHaveCustomMapping).IsAssignableFrom(t) &&
                                            !t.IsAbstract &&
                                            !t.IsInterface)
                .Select(s => (IHaveCustomMapping)Activator.CreateInstance(s)).ToList();

            return mapsFrom;
        }
    }
}
