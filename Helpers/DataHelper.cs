using ContactPro.Data;
using Microsoft.EntityFrameworkCore;
namespace ContactPro.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //Get an instance of the db application context
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            //migration: this is equivalent to update-database
            await dbContextSvc.Database.MigrateAsync();
        }
        
    }
}
