using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem
{
    public class EmailNotificationService : INotificationService
    {
        public bool SendNotificationOnFailure(string content, DataTypeAttribute emailAddress)
        {
            switch (new Random().Next(0, 5))
            {
                case 0: // 25% chance of failure
                    Console.WriteLine($"email failed to reach to {emailAddress.ToString}");
                    return false;
                default:
                    Console.WriteLine($"email sent to {emailAddress.ToString}");
                    Console.WriteLine($"email Content:\n{content}");
                    return true;
            }
        }

        public bool SendNotificationOnSuccess(string content, DataTypeAttribute emailAddress)
        {
            switch (new Random().Next(0, 5))
            {
                case 0:
                    Console.WriteLine($"email failed to reach to {emailAddress.ToString}");
                    return false;
                default:
                    Console.WriteLine($"email sent to {emailAddress.ToString}");
                    Console.WriteLine($"email Content:\n{content}");
                    return true;
            }
        }
    }
}
