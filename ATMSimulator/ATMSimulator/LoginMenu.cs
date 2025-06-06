using MySql.Data;
using MySql.Data.MySqlClient;
class LoginMenu : IMenu
{
    public void displayMenu()
    {
        Console.WriteLine("Enter your Card Number");
        int cardNumber = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter your PIN");
        int pin = Convert.ToInt32(Console.ReadLine());

        login(cardNumber, pin);

    }

    public void login(int cardNumber, int pin)
    {

        string connectionString = "server=localhost;user=root;password=1234;database=ATMSimulator;";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            User? user = null;
            string loginQuery = "SELECT * FROM Users WHERE CardNumber = @cardNumber AND Pin = @pin";

            using (MySqlCommand cmd = new MySqlCommand(loginQuery, conn))
            {
                cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
                cmd.Parameters.AddWithValue("@pin", pin);

                using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = reader.GetInt32("UserID"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        CardNumber = reader.GetInt32("CardNumber"),
                        Pin = reader.GetInt32("Pin")
                    };

                    Console.Clear();
                    user.displayUserInformation();
                    LoggedInMenu userMenu = new LoggedInMenu(user.UserID);
                    userMenu.displayMenu();
                }
                else
                {
                    Console.WriteLine("Invalid Credentials");
                }

            }
        }
    }
}
