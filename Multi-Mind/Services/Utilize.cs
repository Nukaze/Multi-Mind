using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Mind.Services
{
    public class Utilize
    {
        public static async Task AlertDialog(
            string title, string message,
            string? acceptText = null, string? cancelText = null,
            Delegate? onAccept = null
            )
        {
            if (Application.Current is null || Application.Current.MainPage is null)
            {
                return;
            }

            acceptText ??= "OK";

            if (cancelText is not null)
            {
                bool isAccept = await Application.Current.MainPage.DisplayAlert(title, message, acceptText, cancelText);
                if (isAccept && onAccept is not null)
                {
                    onAccept.DynamicInvoke();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(title, message, acceptText);
            }
        }
    }
}
