using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace MoHEmotionPilot
{
    public class DAC
    {
        public static EFFDEXDEMOEntities DBManager = new EFFDEXDEMOEntities();
        public static EffectivaTestEntities DBDemoManager = new EffectivaTestEntities();

        public static int EmployeeId = 0;

        public static Shot CurrentEmotion = new Shot();

        public static int Visit_ID = 1;

        public static Guid User_ID;

        public static int Shot_ID = 1;

        public static string VisitReason = "";

        public static void AddEmotion(Emotion emotion)
        {
            if (Double.Parse(emotion.Emo_Score) >= 0.1)
                DBManager.Emotions.Add(emotion);
        }

        public static void SaveChanges()
        {
            try
            {
                DBDemoManager.SaveChanges();
            }
            catch
            {
                ///TODO handel exception
            }
        }

        public async static Task<bool> CloseVisitAndSaveChanges()
        {
            try
            {
                await DBDemoManager.SaveChangesAsync();

                if (!String.IsNullOrEmpty(VisitReason))
                {
                    DBDemoManager.Visits.Where(vs => vs.Visit_ID == Visit_ID).FirstOrDefault().Visit_Reason = VisitReason;
                }
                List<Shot> SummaryShots = new List<Shot>();

                //Property "Shot_Seq_Num" is used to contain the "Title" of the emotion as string
                SummaryShots.Add(new Shot() { Score_Value = DBDemoManager.Shots.Where(x => x.Score_Title.Equals("Anger")).ToList().Sum(y => y.Score_Value), Shot_Seq_Num = "Anger" });
                SummaryShots.Add(new Shot() { Score_Value = DBDemoManager.Shots.Where(x => x.Score_Title.Equals("Disgust")).ToList().Sum(y => y.Score_Value), Shot_Seq_Num = "Disgust" });
                SummaryShots.Add(new Shot() { Score_Value = DBDemoManager.Shots.Where(x => x.Score_Title.Equals("Joy")).ToList().Sum(y => y.Score_Value), Shot_Seq_Num = "Joy" });
                SummaryShots.Add(new Shot() { Score_Value = DBDemoManager.Shots.Where(x => x.Score_Title.Equals("Sadness")).ToList().Sum(y => y.Score_Value), Shot_Seq_Num = "Sadness" });
                SummaryShots.Add(new Shot() { Score_Value = DBDemoManager.Shots.Where(x => x.Score_Title.Equals("Surprise")).ToList().Sum(y => y.Score_Value), Shot_Seq_Num = "Surprise" });
                SummaryShots.Add(new Shot() { Score_Value = DBDemoManager.Shots.Where(x => x.Score_Title.Equals("Fear")).ToList().Sum(y => y.Score_Value), Shot_Seq_Num = "Fear" });

                if (SummaryShots.Max(v => v.Score_Value) > 0)
                {
                    string EmotionSummary = SummaryShots.Where(c => c.Score_Value == SummaryShots.Max(v => v.Score_Value)).FirstOrDefault().Shot_Seq_Num;

                    DBDemoManager.Visits.Where(vs => vs.Visit_ID == Visit_ID).FirstOrDefault().Emotion_Summary = EmotionSummary;

                    await DBDemoManager.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                try
                {
                    await DBDemoManager.SaveChangesAsync();
                }
                catch
                {
                    
                }
            }
            return false;
        }


        public static Employee GetLoggedInEmployee()
        {
            //if (DBDemoManager.Employees.ToList().Count < 1)
            //    DBDemoManager.Employees.Add(new Employee() { Emp_ID = 1, Emp_Name = "Mostafa Osama", Emp_Email = "m@osama.com", Emp_Contact_Num = "123", Emp_Position_Title = "Lead Consultant" });

            return DBDemoManager.Employees.ToList()[EmployeeId];

        }

        public static void GetShotID()
        {
            if (DBDemoManager.Shots.ToList().Count > 0)
                Shot_ID = DBDemoManager.Shots.ToList().Last().Shot_ID;
        }

        public static void AddUser(User user)
        {
            User_ID = user.User_ID;
            DBDemoManager.Users.Add(user);
        }

        public static void AddVisit(Visit visit)
        {
            try
            {
                if (DBDemoManager.Visits.ToList().Count > 0)
                    Visit_ID = DBDemoManager.Visits.ToList().Last().Visit_ID + 1;

                visit.Visit_ID = Visit_ID;
                visit.User_ID = User_ID;
                DBDemoManager.Visits.Add(visit);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }



        public static Shot GetCurrentEmotion()
        {
            return CurrentEmotion;
        }

        public static void SetCurrentEmotion(Shot emotion)
        {
            CurrentEmotion = emotion;
        }


        public static void AddShot(Shot shot)
        {
            if (shot.Score_Value > 0)
            {
                shot.User_ID = User_ID;
                shot.Visit_ID = Visit_ID;

                //shot.Shot_ID = DBDemoManager.Shots.ToList().Count()> 0 ?  DBDemoManager.Shots.ToList().Last().Shot_ID+1 : 1;

                shot.Shot_ID = ++Shot_ID;
                //shot.Shot_Seq_Num = "1";

                DBDemoManager.Shots.Add(shot);
            }
        }

        public static void AddEmployee(Employee emp)
        {
            if (DBDemoManager.Employees.ToList().Count >= 1)
                emp.Emp_ID = EmployeeId = DBDemoManager.Employees.ToList().Last().Emp_ID + 1;
            else
                emp.Emp_ID = EmployeeId = 0;
            DBDemoManager.Employees.Add(emp);
            DBDemoManager.SaveChanges();

        }

        public static bool Login(Employee emp)
        {
            try
            {
                Employee loggedinEmp = DBDemoManager.Employees.Where(em => em.Emp_Name == emp.Emp_Name && em.Emp_Password == emp.Emp_Password).FirstOrDefault();
                if (loggedinEmp != null)
                {
                    EmployeeId = loggedinEmp.Emp_ID;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
