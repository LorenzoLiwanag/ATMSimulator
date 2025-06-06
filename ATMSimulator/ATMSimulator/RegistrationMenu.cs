using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

class RegistrationMenu : IMenu
{
    public void displayMenu()
    {
        Console.WriteLine("Enter your First Name");
        string FirstName = Console.ReadLine();
        Console.WriteLine("Enter your Last Name");
        string LastName = Console.ReadLine();
        Console.WriteLine("Enter your Card Number");
        int cardNumber = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter your PIN");
        int pin = Convert.ToInt32(Console.ReadLine());

        register(FirstName, LastName, cardNumber, pin);
    }

    public void register(string FirstName, string LastName, int cardNumber, int pin)
    {
        string connectionString = "server=localhost;user=root;password=1234;database=ATMSimulator;";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string registrationQuerry = "INSERT INTO Users (FirstName, LastName, CardNumber, Pin) " +
                                        "VALUES (@firstName, @lastName, @cardNumber, @Pin) ";
            using (MySqlCommand cmd = new MySqlCommand(registrationQuerry, conn))
            {
                cmd.Parameters.AddWithValue("@firstName", FirstName);
                cmd.Parameters.AddWithValue("@lastName", LastName);
                cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
                cmd.Parameters.AddWithValue("@pin", pin);

                int rowsInserted = cmd.ExecuteNonQuery();

                if (rowsInserted > 0)
                {
                    Console.WriteLine("Registration successful.");
                }
                else
                {
                    Console.WriteLine("Registration failed.");
                }

            }
        }
    }

}
