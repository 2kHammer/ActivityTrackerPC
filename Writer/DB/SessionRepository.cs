using ActivityTrackerPC.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivityTrackerPC.Writer.DB
{
    //Store the data from one session to the database
    public class SessionRepository
    {
        //contains the data for the database
        private SessionModelFile? _smFile;
        
        private User _user;
        private List<Application> _usedApplications;
        private Session _session;
        private List<ApplicationDuration> _applicationDurations;
        
        
        
        public SessionRepository()
        {
        }

        //Stores the complete session data to the database
        public void addSession(SessionModelFile? smFile)
        {
            _smFile = smFile;

            _user = AddUserIfNotExists(smFile.User);

            _session = AddSession(_user, smFile.StartingTime, smFile.EndingTime);
            
            _usedApplications = AddApplicationsIfNotExists(smFile.ApplicationsFile);

            _applicationDurations = AddApplicationDurations(_session, _usedApplications, smFile.ApplicationsFile);

        }
        
        
        /*
         * Session
         * Creates a session from the parameters and stores them in the database (uses the already saved User: _user)
         */
        public Session AddSession(User usr, DateTime startTime, DateTime endingTime)
        {
            Session actSession = new Session();
            actSession.User = usr;
            actSession.EndingTime = endingTime;
            actSession.StartingTime = startTime;
            
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                
                ctxt.Attach(usr);
                ctxt.sessions.Add(actSession);
                ctxt.SaveChanges();
                
                return actSession;
            }

            
        }
        
        /*
         * ApplicationDuration
         * Creates a list of ApplcationDurations from the parameters and stores them in the database
         * (uses the already saved Session: _session and the List of Applications: _usedApplications)
         */
        private List<ApplicationDuration> AddApplicationDurations(Session se, List<Application> appList, List<ApplicationModelFile>? appFileList)
        {
            List<ApplicationDuration> durationList = new List<ApplicationDuration>();
            foreach (Application a in appList)
            {
                ApplicationDuration appDuration = new ApplicationDuration();
                appDuration.Session = _session;
                appDuration.Application = a;
                ApplicationModelFile matchingAMF = appFileList.Where(aMF => aMF.ApplicationName == a.ApplicationName)
                    .FirstOrDefault((ApplicationModelFile)null);
                appDuration.Duration = Convert.ToInt64(matchingAMF?.Duration.TotalSeconds);
                
                
                durationList.Add(appDuration);
            }

            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                
                ctxt.AttachRange(appList);
                ctxt.Attach(_session);
                ctxt.applicationDurations.AddRange(durationList);
                ctxt.SaveChanges();
            }

            
            return durationList;
        }
        
        /*
         * Checks if the user with this username already exists, saves it in the database
         * if not.
         */
        private User AddUserIfNotExists(string? username)
        {
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                List<User> userlist = ctxt.users.ToList<User>();
                User existingUser = userlist.Where(a => a.UserName == username).FirstOrDefault((User)null);
                
                if (existingUser != null)
                {
                    return existingUser;
                }
                else
                {
                    User newUser = new User(username);
                    ctxt.users.Add(newUser);
                    ctxt.SaveChanges();

                    return newUser;
                }
            }
        }
        
        
        /*
         * Checks which of the used applications are already stored in the database and saves the rest,
         * returns the list of used applications in this session.
         */
        private List<Application> AddApplicationsIfNotExists(List<ApplicationModelFile>? applist)
        {
            List<Application> appDBList = null;
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                appDBList = ctxt.application.ToList<Application>();
            }

            List<Application> newApplications = new List<Application>();
            List<Application> usedInSessionApplications = new List<Application>();

            foreach (ApplicationModelFile actApp in applist)
            {
                    
                    
                    Application newapp = appDBList?.Where(app => app.ApplicationName == actApp.ApplicationName).FirstOrDefault((Application)null);

                    if (newapp == null)
                    {
                        newapp = new Application(actApp.ApplicationName);
                        newApplications.Add(newapp);
                       
                    }
                    
                    usedInSessionApplications.Add(newapp);
            }
                
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                ctxt.application.AddRange(newApplications);
                ctxt.SaveChanges();
            }
            
            return usedInSessionApplications;
        }


        private List<Application> getAllApplication()
        {
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                return ctxt.application.ToList();
            }
        }
    }

}