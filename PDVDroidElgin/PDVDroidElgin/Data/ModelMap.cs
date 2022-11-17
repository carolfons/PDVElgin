using AutoMapper;
using PDVDroidElgin.Models;
using PDVDroidElgin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Data
{
    public class ModelMap
    {
        private static MapperConfiguration cfg;

        private static void Configure()
        {
            cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, ProdutoViewModel>();
            });
        }

        public static TDestination Map<TDestination>(object source) where TDestination : class
        {
            if (cfg == null) Configure();
            IMapper map = cfg.CreateMapper();
            return map.Map<TDestination>(source);
        }
    }
}
