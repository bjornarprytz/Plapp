﻿using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCLStorage;
using Plapp.Core;

namespace Plapp.Persist
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction LoadConfiguration(this FrameworkConstruction construction)
        {
            construction.Configuration["ConnectionString"] = $"Data Source={FileSystem.Current.LocalStorage.Path}/Plapp.db";
            return construction;
        }

        public static FrameworkConstruction UsePlappDataStore(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton(provider =>
            {
                return FileSystem.Current;
            });

            construction.Services.AddDbContext<PlappDbContext>(options =>
            {
                options.UseSqlite(construction.Configuration["ConnectionString"]);
            }, contextLifetime: ServiceLifetime.Transient);

            construction.Services.AddTransient<IPlappDataStore>(
                provider => new PlappDataStore(
                    provider.GetService<PlappDbContext>(), 
                    provider.GetService<IFileSystem>()));

            return construction;
        }
    }
}
