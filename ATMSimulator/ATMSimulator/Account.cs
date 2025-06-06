using System.Reflection.PortableExecutable;
using MySql.Data.MySqlClient;

class Account
{
    public int AccountId { get; set; }
    public int UserID { get; set; }
    public decimal Balance { get; set; }
    public decimal Withdraw(int userId)
    {
        string connectionString = "server=localhost;user=root;password=1234;database=ATMSimulator;";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string accountSelectionQuery = "SELECT AccountID, Balance FROM Accounts WHERE UserID = @userId";
            int accountId = -1;
            decimal currentBalance = 0;

            using (MySqlCommand cmd = new MySqlCommand(accountSelectionQuery, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        accountId = reader.GetInt32("AccountID");
                        currentBalance = reader.GetDecimal("Balance");
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                        return -1;
                    }
                }
            }

            Console.Write("Enter amount to withdraw: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return currentBalance;
            }

            if (amount > currentBalance)
            {
                Console.WriteLine("Insufficient funds.");
                return currentBalance;
            }

            decimal newBalance = currentBalance - amount;
            string updateQuery = "UPDATE Accounts SET Balance = @newBalance WHERE AccountID = @accountId";
            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
            {
                updateCmd.Parameters.AddWithValue("@newBalance", newBalance);
                updateCmd.Parameters.AddWithValue("@accountId", accountId);
                updateCmd.ExecuteNonQuery();
            }

            string insertTransaction = @"
                INSERT INTO Transactions (TransactionType, Amount, TransactionDate, UserID, AccountID)
                VALUES ('Withdrawal', @amount, NOW(), @userId, @accountId)";
            using (MySqlCommand transCmd = new MySqlCommand(insertTransaction, conn))
            {
                transCmd.Parameters.AddWithValue("@amount", amount);
                transCmd.Parameters.AddWithValue("@userId", userId);
                transCmd.Parameters.AddWithValue("@accountId", accountId);
                transCmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Withdrawal successful. New balance: {newBalance:C}");
            return newBalance;
        }
    }

    public decimal Deposit(int userId)
    {
        string connectionString = "server=localhost;user=root;password=1234;database=ATMSimulator;";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string accountSelectionQuery = "SELECT AccountID, Balance FROM Accounts WHERE UserID = @userId";
            int accountId = -1;
            decimal currentBalance = 0;

            using (MySqlCommand cmd = new MySqlCommand(accountSelectionQuery, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        accountId = reader.GetInt32("AccountID");
                        currentBalance = reader.GetDecimal("Balance");
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                        return -1;
                    }
                }
            }

            Console.Write("Enter amount to deposit: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return currentBalance;
            }

            decimal newBalance = currentBalance + amount;
            string updateQuery = "UPDATE Accounts SET Balance = @newBalance WHERE AccountID = @accountId";
            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
            {
                updateCmd.Parameters.AddWithValue("@newBalance", newBalance);
                updateCmd.Parameters.AddWithValue("@accountId", accountId);
                updateCmd.ExecuteNonQuery();
            }

            string insertTransaction = @"
            INSERT INTO Transactions (TransactionType, Amount, TransactionDate, UserID, AccountID)
            VALUES ('Deposit', @amount, NOW(), @userId, @accountId)";
            using (MySqlCommand transCmd = new MySqlCommand(insertTransaction, conn))
            {
                transCmd.Parameters.AddWithValue("@amount", amount);
                transCmd.Parameters.AddWithValue("@userId", userId);
                transCmd.Parameters.AddWithValue("@accountId", accountId);
                transCmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Deposit successful. New balance: {newBalance:C}");
            return newBalance;
        }
    }


}