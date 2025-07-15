using System.Text.Json;
using MauiPasswordManager.Models;

namespace MauiPasswordManager.Services;

public class PasswordService
{
    private const string FileName = "passwords.json";
    private readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, FileName);

    public async Task<List<PasswordEntry>> LoadPasswordsAsync()
    {
        if (!File.Exists(FilePath))
            return new List<PasswordEntry>();

        string json = await File.ReadAllTextAsync(FilePath);
        return JsonSerializer.Deserialize<List<PasswordEntry>>(json) ?? new List<PasswordEntry>();
    }

    public async Task SavePasswordsAsync(List<PasswordEntry> passwords)
    {
        string json = JsonSerializer.Serialize(passwords, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(FilePath, json);
    }

    public string GeneratePassword(int length = 16)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
