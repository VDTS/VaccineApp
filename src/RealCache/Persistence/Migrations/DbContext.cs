using Core.Models;
using SQLite;

namespace RealCache.Persistence.Migrations;

public class DbContext<T> where T : class
{
    private SQLiteConnection _database;
    public void CreateDB(string role, string user = "user")
    {
        if (_database == null)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{role}-{user}-cache.db3");
            _database = new SQLiteConnection(dbPath);
        }
    }

    public void CreateTable()
    {
        _database.CreateTable(typeof(T));
    }

    public void Insert(T data)
    {
        _database.Insert(data);
    }

    public IEnumerable<PeriodModel> Get()
    {
        return _database.Table<PeriodModel>().ToList();
    }
}
