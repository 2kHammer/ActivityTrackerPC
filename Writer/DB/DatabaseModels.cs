using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityTrackerPC.Writer.DB
{

    public class Session
    {
        public int SessionId { get; set; }
        public DateTime? StartingTime;
        public DateTime? EndingTime;
        
        public int UserId { get; set; }
        
        public virtual ICollection<ApplicationDuration> ApplicationDurations { get; set; }
        
        public virtual User User { get; set; }


        public Session(){}
    }

    public class Application
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }

        public virtual ICollection<ApplicationDuration> ApplicationDurations { get; set; }

        public Application(){}

        public Application(string name)
        {
            ApplicationName = name;
        }
    }

    public class ApplicationDuration
    {
        public int ApplicationDurationId { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan? Duration { get; set; }
        
        public int SessionId { get; set; }
        public int ApplicationId { get; set; }
        
        public virtual Session Session { get; set; }
        public virtual Application Application { get; set; }
        
        public ApplicationDuration(){}
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        
        public virtual ICollection<Session> Sessions { get; set; }

        public User(string name)
        {
            UserName = name;
        }
        
        public User(){}
    }
}