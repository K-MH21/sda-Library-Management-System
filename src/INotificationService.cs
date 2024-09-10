using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem
{
    public interface INotificationService
    {
        // DataTypeAttribute is Parent of Email and Phone classes
        bool SendNotificationOnSuccess(string content, DataTypeAttribute destination);
        bool SendNotificationOnFailure(string content, DataTypeAttribute destination);
    }
}
