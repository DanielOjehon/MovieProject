using Microsoft.Maui.Storage;

namespace MovieProject.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            return;

        Preferences.Set("username", UsernameEntry.Text);
        Preferences.Set("isLoggedIn", true);

        await Shell.Current.GoToAsync("//MainPage");
    }
}
