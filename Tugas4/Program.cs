using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;





namespace BasicAuthentication
{
    public class Program
    {
        public static string[] first_name = new string[0];
        public static string[] last_name = new string[0];
        public static string[] fullname = new string[0];
        public static string[] username = new string[0];
        public static string[] password = new string[0];

        public static void Main(string[] args) => Program.Home();
        
        public static void Home()
        {
            Console.Clear();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("=== HOME ===");
            Console.WriteLine("1. CREATE USER");
            Console.WriteLine("2. SHOW USER");
            Console.WriteLine("3. SEARCH USER");
            Console.WriteLine("4. LOGIN USER");
            Console.Write("\nINPUT : ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Program.CreateUser();
                    break;
                case 2:
                    Program.ShowUser();
                    break;
                case 3:
                    Program.SearchUser();
                    break;
                case 4:
                    Program.Login();
                    break;
                default:
                    Program.WrongOption();
                    break;
            }
        }
        public static void WrongOption()
        {
            Console.Clear();
            Console.WriteLine("Invalid Option" +
                              "\nPlease select any option (1-4)" +
                              "\nPress any key to back HOME");
            Console.ReadKey();
            Program.Home();
        }
        public static void CreateUser()
        {
            Console.Clear();
            Console.Write("First Name\t: ");
            string firstName = Program.CekFirstName(Console.ReadLine());
            Console.Write("Last Name\t: ");
            string lastName = Program.CekLastName(Console.ReadLine());
            Console.Write("Password\t: ");
            string passwords = Program.CekPassword(Console.ReadLine());
            Program.User(firstName, lastName, passwords);
            Console.WriteLine();
            Console.WriteLine("Successed!" +
                              "\nPress any key to HOME");
            Console.ReadKey();
            Program.Home();
        }

        public static void ShowUser()
        {
            Console.Clear();
            int num = 1;
            Console.WriteLine("=== USER LIST ===");
            for (int index = 0; index < Program.username.Length; ++index)
            {
                Console.WriteLine("========================");
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
                interpolatedStringHandler.AppendLiteral("ID\t\t: ");
                interpolatedStringHandler.AppendFormatted<int>(num++);
                Console.WriteLine(interpolatedStringHandler.ToStringAndClear());
                Console.WriteLine("Name\t\t: " + Program.first_name[index] + " " + Program.last_name[index]);
                Console.WriteLine("Username\t: " + Program.username[index]);
                Console.WriteLine("Password\t: " + Program.password[index]);
                Console.WriteLine("========================");
            }
            Console.WriteLine("\n=== USER MENU ===");
            Console.WriteLine("1. EDIT USER");
            Console.WriteLine("2. DELETE USER");
            Console.WriteLine("3. HOME");
            Console.Write("\nINPUT : ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Program.EditUser();
                    break;
                case 2:
                    Program.DeleteUser();
                    break;
                case 3:
                    Program.Home();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid Option" +
                              "\nPlease select an option (1-3)" +
                              "\nPress any key to SHOW USER");
                    Console.ReadKey();
                    Program.ShowUser();
                    break;
            }
        }

        public static void EditUser()
        {
            Console.Write("Select an ID\t: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            if (index >= 0 && index <= Program.first_name.Length)
            {
                Console.Write("First Name\t: ");
                string str1 = Program.CekFirstName(Console.ReadLine());
                Console.Write("Last Name\t: ");
                string str2 = Program.CekLastName(Console.ReadLine());
                Console.Write("Password\t: ");
                string str3 = Program.CekPassword(Console.ReadLine());
                Program.first_name.SetValue((object)str1, index);
                Program.last_name.SetValue((object)str2, index);
                Program.username.SetValue((object)(str1 + str2.Substring(0, 2)), index);
                Program.fullname.SetValue((object)(str1 + str2), index);
                Program.password.SetValue((object)str3, index);
                Console.WriteLine("Editing Successed" +
                                  "\nPress any key to SHOW USER");
                Console.ReadKey();
                Program.ShowUser();
            }
            else if (index > Program.first_name.Length)
            {
                Console.WriteLine("Invalid User");
                Program.EditUser();
            }
            else
            {
                if (index >= 0)
                    return;
                Program.ShowUser();
            }
        }

        public static void DeleteUser()
        {
            Console.Write("Select an ID\t: ");
            int pilih = Convert.ToInt32(Console.ReadLine()) - 1;
            if (pilih >= 0 && pilih <= Program.first_name.Length)
            {
                Program.first_name = ((IEnumerable<string>)Program.first_name).Where<string>((Func<string, int, bool>)((source, index) => index != pilih)).ToArray<string>();
                Program.last_name = ((IEnumerable<string>)Program.last_name).Where<string>((Func<string, int, bool>)((source, index) => index != pilih)).ToArray<string>();
                Program.fullname = ((IEnumerable<string>)Program.fullname).Where<string>((Func<string, int, bool>)((source, index) => index != pilih)).ToArray<string>();
                Program.username = ((IEnumerable<string>)Program.username).Where<string>((Func<string, int, bool>)((source, index) => index != pilih)).ToArray<string>();
                Program.password = ((IEnumerable<string>)Program.password).Where<string>((Func<string, int, bool>)((source, index) => index != pilih)).ToArray<string>();
                Console.WriteLine("Deleting Successed" +
                                  "\nPress any key to SHOW USER");
                Console.ReadKey();
                Program.ShowUser();
            }
            else if (pilih > Program.first_name.Length)
            {
                Console.WriteLine("Uset not found");
                Program.DeleteUser();
            }
            else
            {
                if (pilih >= 0)
                    return;
                Program.ShowUser();
            }
        }

        public static void SearchUser()
        {
            Console.Clear();
            Console.WriteLine("=== FIND USER ===");
            Console.Write("Name\t: ");
            string input = Console.ReadLine();
            int[] source = new int[0];
            int num = 1;
            for (int index = 0; index < Program.fullname.Length; ++index)
            {
                string str = Array.Find<string>(new string[1]
                {
          Program.fullname[index]
                }, (Predicate<string>)(n => n.Contains(input)));
                int element = Array.IndexOf<string>(Program.fullname, str);
                if (element != -1)
                    source = ((IEnumerable<int>)source).Append<int>(element).ToArray<int>();
            }
            if (source.Length != 0)
            {
                for (int index = 0; index < source.Length; ++index)
                {
                    Console.WriteLine("========================");
                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
                    interpolatedStringHandler.AppendLiteral("ID\t\t: ");
                    interpolatedStringHandler.AppendFormatted<int>(num++);
                    Console.WriteLine(interpolatedStringHandler.ToStringAndClear());
                    Console.WriteLine("Name\t\t: " + Program.first_name[source[index]] + " " + Program.last_name[source[index]]);
                    Console.WriteLine("Username\t: " + Program.username[source[index]]);
                    Console.WriteLine("Password\t: " + Program.password[source[index]]);
                    Console.WriteLine("========================");
                }
                Console.ReadKey();
                Program.Home();
            }
            else
            {
                Console.WriteLine("User not found");
                Console.ReadKey();
                Program.Home();
            }
        }

        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("=== LOGIN ===");
            Console.Write("Username\t: ");
            string input_username = Console.ReadLine();
            Console.Write("Password\t: ");
            string input_password = Console.ReadLine();
            string str1 = Array.Find<string>(Program.username, (Predicate<string>)(n => n == input_username));
            int num1 = Array.IndexOf<string>(Program.username, str1);
            string str2 = Array.Find<string>(Program.password, (Predicate<string>)(n => n == input_password));
            int num2 = Array.IndexOf<string>(Program.password, str2);
            if (num1 == -1 || num2 == -1)
            {
                Console.WriteLine("Login Failed");
                Console.ReadKey();
                Program.Home();
            }
            else if (num1 == num2)
            {
                Console.WriteLine("Login Successed");
                Console.ReadKey();
                Program.Home();
            }
            else
            {
                Console.WriteLine("Login Failed");
                Console.ReadKey();
                Program.Home();
            }
        }

        public static void User(string firstName, string lastName, string passwords)
        {
            Program.first_name = ((IEnumerable<string>)Program.first_name).Append<string>(firstName).ToArray<string>();
            Program.last_name = ((IEnumerable<string>)Program.last_name).Append<string>(lastName).ToArray<string>();
            Program.fullname = ((IEnumerable<string>)Program.fullname).Append<string>(firstName + " " + lastName).ToArray<string>();
            Program.username = ((IEnumerable<string>)Program.username).Append<string>(firstName + lastName).ToArray<string>();
            Program.password = ((IEnumerable<string>)Program.password).Append<string>(passwords).ToArray<string>();
        }
        public static string CekFirstName(string firstName)
        {
            bool flag;
            do
            {
                if (firstName.Length > 2 && firstName.Any<char>(new Func<char, bool>(char.IsUpper)) && firstName.Any<char>(new Func<char, bool>(char.IsLower)) && !firstName.Any<char>(new Func<char, bool>(char.IsNumber)))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("\nFirst Name must have at least 3 characters." +
                                      "\nWith one Capital letter at the beginning." +
                                      "\nWithout any number.\n");
                    Console.Write("First Name\t: ");
                    firstName = Console.ReadLine();
                    flag = true;
                }
            }
            while (flag);
            return firstName;
        }
        public static string CekLastName(string lastName)
        {
            bool flag;
            do
            {
                if (lastName.Length > 2 && lastName.Any<char>(new Func<char, bool>(char.IsUpper)) && lastName.Any<char>(new Func<char, bool>(char.IsLower)) && !lastName.Any<char>(new Func<char, bool>(char.IsNumber)))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("\nLast Name must have at least 3 characters." +
                                      "\nWith one Capital letter at the beginning." +
                                      "\nWithout any number.\n");
                    Console.Write("Last Name\t: ");
                    lastName = Console.ReadLine();
                    flag = true;
                }
            }
            while (flag);
            return lastName;
        }
        public static string CekPassword(string passwords)
        {
            bool flag;
            do
            {
                if (passwords.Length > 7 && passwords.Any<char>(new Func<char, bool>(char.IsUpper)) && passwords.Any<char>(new Func<char, bool>(char.IsLower)) && passwords.Any<char>(new Func<char, bool>(char.IsNumber)))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("\nPassword must have at least 8 characters" +
                                      "\nWith at least one Capital letter" +
                                      "\nWith at least one lower case letter and at least one number.\n");
                    Console.Write("Password\t: ");
                    passwords = Console.ReadLine();
                    flag = true;
                }
            }
            while (flag);
            return passwords;
        }
        
    }
}
