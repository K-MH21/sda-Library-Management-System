using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LibraryManagementSystem
{
    public class SMSNotificationService : INotificationService
    {
        public bool SendNotificationOnFailure(string str, DataTypeAttribute phoneNumber)
        {
            switch (new Random().Next(0, 5))
            {
                case 0: // 25% of failure
                    Console.WriteLine($"SMS failed to reach to {phoneNumber.ToString}");
                    return false;
                default:
                    string content = $"{str}";
                    Console.WriteLine($"SMS sent to {phoneNumber.ToString}");
                    Console.WriteLine($"SMS Content:\n{content}");
                    return true;
            }
        }

        public bool SendNotificationOnSuccess(string str, DataTypeAttribute phoneNumber)
        {
            switch (new Random().Next(0, 5))
            {
                case 0:
                    Console.WriteLine($"SMS failed to reach to {phoneNumber.ToString}");
                    return false;
                default:
                    string content = $"{str}";
                    Console.WriteLine($"SMS sent to {phoneNumber.ToString}");
                    Console.WriteLine($"SMS Content:\n{content}");
                    return true;
            }
        }
    }
}
