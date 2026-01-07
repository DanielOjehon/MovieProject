namespace MovieProject.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.UserAppTheme =
                e.Value ? AppTheme.Dark : AppTheme.Light;
        }

        private void OnFontSizeChanged(object sender, ValueChangedEventArgs e)
        {
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Shell.Current.GoToAsync("//Login");
        }


    }
}
