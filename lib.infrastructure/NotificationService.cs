using application.interfaces;
using application.notifications.models;
using System.Threading.Tasks;

namespace lib.infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
