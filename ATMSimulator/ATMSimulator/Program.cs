using System;
using System.Reflection.Metadata.Ecma335;


namespace ATMSimulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Type 1 to Login, Type 2 to Create an Account or Type 3 to exit");
            int menuInput = Convert.ToInt32(Console.ReadLine());
            switch (menuInput)
            {
                case 1:
                    LoginMenu login = new LoginMenu();
                    login.displayMenu();
                    break;
                case 2:
                    RegistrationMenu registration = new RegistrationMenu();
                    registration.displayMenu();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("error Invalid choice");
                    break;
            }
        }
    }


}