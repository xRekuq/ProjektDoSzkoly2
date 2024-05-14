using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SzafkiSzkolne.Models
{
    public class LocalDbService
    {
        private const string DB_NAME = "LocalDb.db";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            
            /*"C:\\Users\\grzes\\AppData\\Local\\LocalDb.db"*/
            /*Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DB_NAME)*/
            /*string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "DatabasePath.txt");

            File.WriteAllText(filePath, AppDomain.CurrentDomain.BaseDirectory);*/

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string modelsPath = Path.Combine(basePath, "..", "..", "..", "..", "..", "Models");
            string dbPath = Path.Combine(modelsPath, DB_NAME);

            if (!File.Exists(dbPath))
            {
                File.Create(dbPath).Dispose();
            }

            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Locker>();
        }

        public async Task<List<Locker>> GetAllLockers()
        {
            return await _connection.Table<Locker>().ToListAsync();
        }

        public async Task<Locker> GetLockerById(int id)
        {
            return await _connection.Table<Locker>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task CreateLocker(Locker locker)
        {
            await _connection.InsertAsync(locker);
        }

        public async Task UpdateLocker(Locker locker)
        {
            await _connection.UpdateAsync(locker);
        }

        public async Task DeleteLocker(Locker locker)
        {
            await _connection.DeleteAsync(locker);
        }
    }
}
