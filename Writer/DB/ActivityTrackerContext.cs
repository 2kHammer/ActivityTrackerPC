using Microsoft.EntityFrameworkCore;


namespace ActivityTrackerPC.Writer.DB
{
    public class ActivityTrackerContext : DbContext
    {
        
        private const string ConnectionString = "Server=localhost;User=alex;Password=dbtest;Database=PCActivity";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        } 
        
        //Gateways to the tables
        public DbSet<Session> sessions { get; set; } = null!;
        public DbSet<Application> application { get; set; } = null!;
        public DbSet<ApplicationDuration>  applicationDurations { get; set; } = null!;
        public DbSet<User> users { get; set; } = null!;


        //For the database migration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Session
            builder.Entity<Session>()
                .ToTable("Session")
                .HasKey(p => p.SessionId);

            builder.Entity<Session>()
                .Property(x => x.StartingTime)
                .IsRequired();
            
            builder.Entity<Session>()
                .Property(x => x.EndingTime)
                .IsRequired();

            builder.Entity<Session>()
                .HasOne<User>(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId);
            
            //User
            builder.Entity<User>()
                .ToTable("User");
            
            builder.Entity<User>()
                .Property(a => a.UserName)
                .HasColumnType("varchar(30)");
            
            //ApplicationDuration
            builder.Entity<ApplicationDuration>()
                .ToTable("ApplicationDuration");

            builder.Entity<ApplicationDuration>()
                .HasOne<Session>(aD => aD.Session)
                .WithMany(s => s.ApplicationDurations)
                .HasForeignKey(aD => aD.SessionId);
            
            builder.Entity<ApplicationDuration>()
                .HasOne<Application>(aD => aD.Application)
                .WithMany(a => a.ApplicationDurations)
                .HasForeignKey(aD => aD.ApplicationId);

            builder.Entity<ApplicationDuration>()
                .Property(aD => aD.ApplicationId)
                .IsRequired();
            
            builder.Entity<ApplicationDuration>()
                .Property(aD => aD.SessionId)
                .IsRequired();
                
            
            //Application
            builder.Entity<Application>()
                .ToTable("Application");

            builder.Entity<Application>()
                .Property(a => a.ApplicationName)
                .HasColumnType("varchar(30)");



        }
    }
}