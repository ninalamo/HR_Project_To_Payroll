using application.infrastructure.automapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace lib.test.infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
