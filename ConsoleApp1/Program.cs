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

