using SQLite;

namespace Plapp
{
    public abstract class DataTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
