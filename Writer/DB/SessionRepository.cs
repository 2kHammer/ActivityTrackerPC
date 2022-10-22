using ActivityTrackerPC.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivityTrackerPC.Writer.DB
{

    public class SessionRepository
    {
        private SessionModelFile _smFile;

        private User _user;
        private List<Application> _applications;
        private Session _session;
        private List<ApplicationDuration> _applicationDurations;
        
        
        
        public SessionRepository()
        {
        }

        public void addSession(SessionModelFile smFile)
        {
            _smFile = smFile;

            _user = AddUserIfNotExists(smFile.User);

            _session = AddSession(_user, smFile.StartingTime, smFile.EndingTime);
            
            _applications = AddApplicationsIfNotExists(smFile.ApplicationsFile);

            _applicationDurations = AddApplicationDurations(_session, _applications, smFile.ApplicationsFile);

        }
        
        //Session
        public Session AddSession(User usr, DateTime startTime, DateTime endingTime)
        {
            Session actSession = new Session();
            actSession.User = usr;
            actSession.EndingTime = startTime;
            actSession.StartingTime = endingTime;
            
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                
                ctxt.Attach(usr);
                ctxt.sessions.Add(actSession);
                ctxt.SaveChanges();
                
                return actSession;
            }

            
        }
        
        //Application Durations
        private List<ApplicationDuration> AddApplicationDurations(Session se, List<Application> appList, List<ApplicationModelFile> appFileList)
        {
            List<ApplicationDuration> durationList = new List<ApplicationDuration>();
            foreach (Application a in appList)
            {
                ApplicationDuration appDuration = new ApplicationDuration();
                appDuration.Session = _session;
                appDuration.Application = a;
                ApplicationModelFile matchingAMF = appFileList.Where(aMF => aMF.ApplicationName == a.ApplicationName)
                    .FirstOrDefault((ApplicationModelFile)null);
                appDuration.Duration = matchingAMF?.Duration;
                
                durationList.Add(appDuration);
            }

            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                ctxt.AttachRange(appList);
                ctxt.Attach(_session);
                //foreach (Application a in appList) ctxt.Attach(a);
                ctxt.applicationDurations.AddRange(durationList);
                ctxt.SaveChanges();
            }

            
            return durationList;
        }
        
        //User
        private User AddUserIfNotExists(string username)
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
        
        
        //Application
        private List<Application> AddApplicationsIfNotExists(List<ApplicationModelFile> applist)
        {
            using (ActivityTrackerContext ctxt = new ActivityTrackerContext())
            {
                List<Application> appDBList = ctxt.application.ToList<Application>();

                List<Application> completeApplications = new List<Application>();

                foreach (ApplicationModelFile actApp in applist)
                {
                    completeApplications.Add(new Application(actApp.ApplicationName));
                    
                    Application newapp = appDBList.Where(app => app.ApplicationName == actApp.ApplicationName).FirstOrDefault((Application)null);

                    if (newapp == null)
                    {
                        ctxt.application.Add(completeApplications.Last());
                    }
                    
                }

                ctxt.SaveChanges();
                return completeApplications;
            }

        }
    }

}