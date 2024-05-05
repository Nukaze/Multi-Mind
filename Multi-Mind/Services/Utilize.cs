﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bcrypt = BCrypt.Net.BCrypt;


namespace Multi_Mind.Services
{
    public class Utilize
    {
        public static async Task AlertDialogCustom(
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

        public static async Task LoadingDialog(bool active, Task? isTaskFinished = null, Color? circularColor = null, string bgColor = "#80333333", double circularSize = 100)
        {
            if (Application.Current is null || Application.Current.MainPage is null)
            {
                return;
            }

            circularColor ??= Colors.Cyan;

            // Check if the loading dialog is being activated or deactivated
            if (active)
            {
                // Create a new ContentPage to host the loading indicator
                ContentPage loadingPage = new ContentPage
                {
                    BackgroundColor = Color.FromArgb(bgColor),
                    Content = new Grid {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = { new ActivityIndicator {
                                        IsRunning = true,
                                        Color = circularColor,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Center,
                                        WidthRequest = circularSize,
                                        HeightRequest = circularSize
                                    }
                        }
                    }
                };

                // Present the loading page as a modal dialog
                await Application.Current.MainPage.Navigation.PushModalAsync(loadingPage);

                if (isTaskFinished is not null)
                {
                    await isTaskFinished;
                }

                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                // Dismiss the loading page
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }


        public static bool IsUsernameValid(string u)
        {
            string validPattern = @"^[a-zA-Z0-9_]{1,32}$";

            return Regex.IsMatch(u, validPattern);
        }

        public static bool IsEmailValid(string e)
        {
            string validPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{1,}$";
            
            return Regex.IsMatch(e, validPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsPasswordValid(string p)
        {
            string validPattern = @"(?=.*[a-zA-Z])(?=.*\d).{6,32}$";
            
            return Regex.IsMatch(p, validPattern);
        }

        public static bool IsPasswordMatch(string p1, string p2)
        {
            return p1 == p2;
        }

        public static string HashPassword(string p, int saltFactor=10)
        {
            string salt = Bcrypt.GenerateSalt(saltFactor);
            string hashedPassword = Bcrypt.HashPassword(p, salt);
            return hashedPassword;
        }
    }
}
