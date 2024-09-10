using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LibraryManagementSystem
{
    public class SMSNotificationService : INotificationService
    {
        public bool SendNotificationOnFailure(string str, DataTypeAttribute phoneNumber)
        {
            switch (new Random().Next(0, 2))
            {
                case 0:
                    string content = $"Book {str} added to Library. Thank you!";
                    Console.WriteLine($"SMS sent to {phoneNumber.ToString}");
                    Console.WriteLine($"SMS Content:\n{content}");
                    return true;
                default:
                    Console.WriteLine($"SMS failed to reach to {phoneNumber.ToString}");
                    return false;
            }
        }

        public bool SendNotificationOnSuccess(string str, DataTypeAttribute phoneNumber)
        {
            switch (new Random().Next(0, 2))
            {
                case 0:
                    string content = $"Error adding User {str}. Please email support@library.com.";
                    Console.WriteLine($"SMS sent to {phoneNumber.ToString}");
                    Console.WriteLine($"SMS Content:\n{content}");
                    return true;
                default:
                    Console.WriteLine($"SMS failed to reach to {phoneNumber.ToString}");
                    return false;
            }
        }
    }
}
