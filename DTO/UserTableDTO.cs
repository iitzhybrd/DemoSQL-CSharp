namespace Demo_SQL.DTO
{
    public class UserTableDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreateTime { get; set; }
    }
}