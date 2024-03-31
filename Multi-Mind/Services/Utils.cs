using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Multi_Mind.Services
{
    public class Utils
    {
        public async Task AlertDialog(string title, string message, string? acceptText, string? cancelText, Func<dynamic> onAccept)
        {
            if (Application.Current is null || Application.Current.MainPage is null)
            {
                return;
            }
            bool isAccept = await Application.Current.MainPage.DisplayAlert(title, message, acceptText, cancelText);
            if (isAccept && onAccept != null)
            {
                onAccept()?.Invoke();
            }
        }
    }
}
