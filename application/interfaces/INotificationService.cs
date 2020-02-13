using application.notifications.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace application.interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
