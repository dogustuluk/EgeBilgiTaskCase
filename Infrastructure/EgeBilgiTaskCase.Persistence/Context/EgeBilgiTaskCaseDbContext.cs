using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgeBilgiTaskCase.Persistence.Context
{
    public class EgeBilgiTaskCaseDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EgeBilgiTaskCaseDbContext(DbContextOptions options) : base(options)
        {
        }
        #region Character
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterDetail> CharacterDetails { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        #endregion
        #region Common

        public DbSet<DbParameter> DbParameters { get; set; }
        public DbSet<DbParameterType> DbParameterTypes { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Status> Statuses { get; set; }
        #endregion

        #region Management
        public DbSet<ItemChange> ItemChanges { get; set; }
        public DbSet<OperationLog> OperationLogs { get; set; }
        public DbSet<PdfTemplate> PdfTemplates { get; set; }
        public DbSet<ServiceLog> ServiceLogs { get; set; }
        #endregion



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            });

            builder.Entity<Character>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id)
                    .ValueGeneratedOnAdd();

            });
            builder.Entity<CharacterDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(a => a.Id)
                .ValueGeneratedOnAdd();

                entity.Property(c => c.EpisodeIds)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());

            });

            builder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(a => a.Id)
                .ValueGeneratedOnAdd();

                entity.Property(c => c.Residents)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());

            });

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            //   var guid = Guid.NewGuid();
            foreach (var data in datas)
            {
                //if (data.Entity is BaseEntity entity)
                //{
                //    Action action = data.State switch
                //    {
                //        EntityState.Added => () =>
                //        {
                //            entity.CreatedDate = DateTime.UtcNow;
                //            entity.CreatedUser = guid; //test
                //            entity.Guid = Guid.NewGuid();
                //            entity.UpdatedDate = DateTime.UtcNow;
                //            entity.UpdatedUser = guid;
                //        }
                //        ,
                //        EntityState.Modified => () =>
                //        {
                //            entity.UpdatedDate = DateTime.UtcNow;
                //            entity.UpdatedUser = Guid.NewGuid();
                //        }
                //        ,
                //        _ => () => {  }
                //    };

                //    // Action'ı çağır
                //    action();
                //}
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}