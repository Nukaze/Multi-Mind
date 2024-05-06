using Multi_Mind.Models;
using SQLite;
using static Multi_Mind.Services.Utilize;

namespace Multi_Mind.Services
{
    public class DatabaseService
    {
        
        public const SQLite.SQLiteOpenFlags flags = (SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache);


        private const string DB_PATH = "multimind_db.db3";

        public static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DB_PATH);
        
        public SQLiteAsyncConnection _conn;


        public async Task InitializeConnection()
        {
            if (_conn is not null)
            {
                return;
            }

            _conn = new SQLiteAsyncConnection(DatabasePath, flags);
            await _conn.CreateTableAsync<User>();

        }


        public void Dispose()
        { 
            _conn.CloseAsync().Wait();
        }



        public async Task<List<User>> GetUsersAsync()
        {
            await InitializeConnection();
            return await _conn.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            await InitializeConnection();
            return await (_conn.Table<User>().Where(u => u.Uid == id).FirstOrDefaultAsync());
        }

        public async Task<int> Create(User user)
        {
            await InitializeConnection();
            string uid = GenerateUniqueId();

            while (await IsUidExisted(uid))
            {
                uid = GenerateUniqueId();
            }

            user.Uid = uid;

            return await _conn.InsertAsync(user);
        }

        public async Task<int> Update(User user)
        {
            await InitializeConnection();
            return await _conn.UpdateAsync(user);
        }

        public async Task<int> Delete(User user)
        {
            await InitializeConnection();
            return await _conn.DeleteAsync(user);
        }



        public async Task<bool> IsEmailExisted(string email)
        {
            await InitializeConnection();
            return await _conn.Table<User>().Where(record => record.Email == email).FirstOrDefaultAsync() is not null;
        }

        public async Task<bool> IsUidExisted(string uid)
        {
            await InitializeConnection();
            return await _conn.Table<User>().Where(record => record.Uid == uid).FirstOrDefaultAsync() is not null;
        }


    }
}
