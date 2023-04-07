namespace Register.ViewModels
{
    public class UserModel
    {
        int Id { get; set; }
	    public string Username{get;set;}=string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
