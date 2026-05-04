using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;

namespace Le2me.ViewModels;

public partial class NotificationsViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<AppNotification> _notifications = new();

    public NotificationsViewModel()
    {
        // Sample notifications for UI demo
        Notifications.Add(new AppNotification { Title = "New Like", Body = "GrillMaster liked your post" });
        Notifications.Add(new AppNotification { Title = "New Comment", Body = "VeganVibes commented: Looks great!" });
        Notifications.Add(new AppNotification { Title = "New Follower", Body = "PastaPrincess is now following you" });
    }

    [RelayCommand]
    private void DismissNotification(AppNotification notif)
    {
        Notifications.Remove(notif);
    }

    [RelayCommand]
    private void ClearAll()
    {
        Notifications.Clear();
    }
}
