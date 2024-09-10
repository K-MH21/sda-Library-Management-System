using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem
{
    public class EmailNotificationService : INotificationService
    {
        public bool SendNotificationOnFailure(string content, DataTypeAttribute emailAddress)
        {
            switch (new Random().Next(0, 2))
            {
                case 0:
                    Console.WriteLine($"email sent to {emailAddress.ToString}");
                    Console.WriteLine($"email Content:\n{content}");
                    return true;
                default:
                    Console.WriteLine($"email failed to reach to {emailAddress.ToString}");
                    return false;
            }
        }

        public bool SendNotificationOnSuccess(string content, DataTypeAttribute emailAddress)
        {
            switch (new Random().Next(0, 2))
            {
                case 0:
                    Console.WriteLine($"email sent to {emailAddress.ToString}");
                    Console.WriteLine($"email Content:\n{content}");
                    return true;
                default:
                    Console.WriteLine($"email failed to reach to {emailAddress.ToString}");
                    return false;
            }
        }
    }
}
