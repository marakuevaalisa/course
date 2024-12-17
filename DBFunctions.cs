using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    internal static class DBFunctions
    {
        public static async Task addUser(string name, string password)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                User user = new User
                {
                    Username = name,
                    Password = password,
                    CreatedAt = DateTime.Now
                };
                db.Users.AddRange(user);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                }

            }
            
        }
        public static async Task SaveLog(string log)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                Log log1 = new Log
                {
                    History= log
                };
                db.Logs.AddRange(log1);
                await db.SaveChangesAsync();

            }

        }
        public static async Task UpdateLog(string log)
        {
            using (var context = new russiancheckersContext())
            {
                var lastRecord = context.Logs
                    .OrderByDescending(x => x.IdGame) 
                    .FirstOrDefault();
                
                if (lastRecord != null)
                {
                    // Доступ к полям последней записи
                    var fieldValue = lastRecord.History = log;
                }
                await context.SaveChangesAsync();
            }

        }
        public static bool IsUserExists(string username)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                if (db != null && db.Users != null)
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    return user != null;
                }
                return false;
            }
                
        }

        public static async Task SaveEnd(string username, string winner, DateTime start)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {

                HistoryOfGame history = new HistoryOfGame
                {
                    Player = username,
                    Winner = winner,
                    StartDateTime = start,
                    EndDateTime = DateTime.Now
                };
                db.HistoyOfGames.AddRange(history);
                await db.SaveChangesAsync();

            }
        }
        public static string GetHashedPassword(string username)
        {
            using (russiancheckersContext db = new russiancheckersContext())
            {
                if (db != null && db.Users != null)
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    if (user != null)
                    {
                        return user.Password;
                    }
                }
                return null;
            }
        }
        
        public static async Task SaveGameState(string username, string gameState)
        {
            using (var db = new russiancheckersContext())
            {
                var log = new Log
                {
                    History = gameState
                };
                db.Logs.Add(log);
                await db.SaveChangesAsync();
            }
        }
        
        public static async Task<string> GetLastGameState(string username)
        {
            using (var db = new russiancheckersContext())
            {
                var log = await db.Logs
                    .OrderByDescending(l => l.IdGame)
                    .FirstOrDefaultAsync();
        
                return log?.History;
            }
        }
        
            }
   


}
