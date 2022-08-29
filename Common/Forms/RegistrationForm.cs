namespace Common.Forms
{
    public class RegistrationForm
    {
        // Validation: Only letters
        public string Name { get; set; } = string.Empty;
        // Validation: Only letters
        public string SurName { get; set; } = string.Empty;
        // Validation: 1900 < date < currentYear - 16
        //public DateTime BirthDate { get; set; }
        // Validation: RegExp
        public string Email { get; set; } = string.Empty;
        // Validation: RegExp
        public string MobileNumber { get; set; } = string.Empty;
        // Validation: Actual country from DB
        //public string Country { get; set; }
        // Validation: RegExp
        public string Password { get; set; } = string.Empty;
        // Validation: Equal to Password
        public string ConfirmPassword { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsCompleated { get; set; }
    }
}
