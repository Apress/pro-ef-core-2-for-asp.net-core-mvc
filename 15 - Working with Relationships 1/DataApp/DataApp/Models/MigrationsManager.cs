using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataApp.Models {

    public class MigrationsManager {
        private IEnumerable<Type> ContextTypes;
        private IServiceProvider provider;
        public IEnumerable<string> ContextNames;

        public MigrationsManager(IServiceProvider prov) {
            provider = prov;

            ContextTypes = provider.GetServices<DbContextOptions>()
                .Select(o => o.ContextType);
            ContextNames = ContextTypes.Select(t => t.FullName);
            ContextName = ContextNames.First();
        }

        public string ContextName { get; set; }

        public IEnumerable<string> AppliedMigrations
            => Context.Database.GetAppliedMigrations();

        public IEnumerable<string> PendingMigrations
            => Context.Database.GetPendingMigrations();

        public IEnumerable<string> AllMigrations
            => Context.Database.GetMigrations();

        public void Migrate(string contextName, string target = null) {
            Context.GetService<IMigrator>().Migrate(target);
        }

        public DbContext Context => 
            provider.GetRequiredService(Type.GetType(ContextName)) as DbContext;
    }
}
