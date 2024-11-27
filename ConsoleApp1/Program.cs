using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static Dictionary<string, string> passwordVault = new Dictionary<string, string>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nPassword Manager");
            Console.WriteLine("1. Add Password");
            Console.WriteLine("2. View Passwords");
            Console.WriteLine("3. Delete Password");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            switch (Console.ReadLine())
            {
                case "1":
                    AddPassword();
                    break;
                case "2":
                    ViewPasswords();
                    break;
                case "3":
                    DeletePassword();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
    static void AddPassword()
{
    Console.Write("Enter account name: ");
    string account = Console.ReadLine();

    Console.Write("Enter password: ");
    string password = Console.ReadLine();

    if (!IsValidPassword(password))
    {
        Console.WriteLine("Password must contain at least one uppercase letter and one digit.");
        return;
    }

    string encryptedPassword = EncryptPassword(password);
    passwordVault[account] = encryptedPassword;

    Console.WriteLine($"Password for '{account}' added.");
}
static bool IsValidPassword(string password)
{
    bool hasUpperCase = false;
    bool hasDigit = false;

    foreach (char c in password)
    {
        if (char.IsUpper(c)) hasUpperCase = true;
        if (char.IsDigit(c)) hasDigit = true;

        if (hasUpperCase && hasDigit)
            return true;
    }
    
    return false;
}

   static void ViewPasswords()
    {
        if (passwordVault.Count == 0)
        {
            Console.WriteLine("No passwords saved.");
            return;
        }

        foreach (var entry in passwordVault)
        {
            string decryptedPassword = DecryptPassword(entry.Value);
            Console.WriteLine($"Account: {entry.Key}, Password: {decryptedPassword}");
        }
    }
    static void DeletePassword()
    {
        Console.Write("Enter account name to delete: ");
        string account = Console.ReadLine();

        if (passwordVault.Remove(account))
            Console.WriteLine($"Password for '{account}' deleted.");
        else
            Console.WriteLine("Account not found.");
    }
    static string EncryptPassword(string password)
    {
        byte[] data = Encoding.UTF8.GetBytes(password);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("a16byteslongkey!"); // Example key
            aes.IV = Encoding.UTF8.GetBytes("a16byteslongiv!!");  // Example IV
            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(encrypted);
            }
        }
    }
    static string DecryptPassword(string encryptedPassword)
    {
        byte[] data = Convert.FromBase64String(encryptedPassword);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("a16byteslongkey!");
            aes.IV = Encoding.UTF8.GetBytes("a16byteslongiv!!");
            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] decrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}

