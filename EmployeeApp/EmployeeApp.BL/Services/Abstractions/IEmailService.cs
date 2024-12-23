namespace EmployeeApp.BL.Services.Abstractions;

public interface IEmailService
{
    void SendWelcomeEmail(string toUser);
    void SendConfirmEmail(string toUser, string confirmUrl);
    void SendForgotPassword(string toUser, string resetUrl);
}