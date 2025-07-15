using MauiPasswordManager.Models;
using MauiPasswordManager.Services;

namespace MauiPasswordManager.Pages;

public partial class MainPage : ContentPage
{
    private readonly PasswordService _passwordService = new();
    private List<PasswordEntry> _passwords = new();

    public MainPage()
    {
        InitializeComponent();
        LoadPasswords();
    }

    private async void LoadPasswords()
    {
        _passwords = await _passwordService.LoadPasswordsAsync();
        passwordsList.ItemsSource = null;
        passwordsList.ItemsSource = _passwords;
    }

    private void OnGeneratePasswordClicked(object sender, EventArgs e)
    {
        passwordEntry.Text = _passwordService.GeneratePassword();
    }

    private async void OnSavePasswordClicked(object sender, EventArgs e)
    {
        var entry = new PasswordEntry
        {
            Website = websiteEntry.Text,
            Username = usernameEntry.Text,
            Password = passwordEntry.Text
        };

        if (string.IsNullOrWhiteSpace(entry.Website) || string.IsNullOrWhiteSpace(entry.Username) || string.IsNullOrWhiteSpace(entry.Password))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }

        _passwords.Add(entry);
        await _passwordService.SavePasswordsAsync(_passwords);
        LoadPasswords();

        websiteEntry.Text = string.Empty;
        usernameEntry.Text = string.Empty;
        passwordEntry.Text = string.Empty;
    }
}
