using Infrastructure.Context;
using System.Linq;

namespace Infrastructure.Data
{
    public static class DbSeed
    {
        public static void Initialize(SurveyContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}
