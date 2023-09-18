using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
            var platforms = grpcClient?.ReturnAllPlatforms();
            if (platforms == null)
            {
                Console.WriteLine("--> Platforms returned from grpc client was null");
                return;
            }

            var repo = serviceScope.ServiceProvider.GetService<ICommandRepo>();
            if(repo == null)
            {
                Console.WriteLine("--> Command Repo is null");
                return;
            }

            SeedData(repo, platforms);
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine($"--> Seeding data, num of platforms: {platforms.Count()}");

            foreach (var platform in platforms)
            {
                if (!repo.ExternalPlatformExists(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                }

                repo.SaveChanges();
            }
        }
    }
}
