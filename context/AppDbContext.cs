
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CharacterModel> Characters { get; set; }
    public DbSet<EpisodeModel> Episodes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
