namespace UserService.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.student;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>(); 
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }

    public enum UserRole
    {
        student,
        teacher,
        staff,
        admin
    }
}
