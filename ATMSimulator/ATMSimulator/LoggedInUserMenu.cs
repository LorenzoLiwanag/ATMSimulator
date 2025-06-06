class LoggedInMenu : IMenu
{
    private readonly int userId;
    public LoggedInMenu(int userId)
    {
        this.userId = userId;
    }
    public void displayMenu()
    {
        Account userAcc = new Account();
        Console.WriteLine("Type 1 to make a withdrawal, Type 2 to make a deposit, Type 3 to logout");
        int userInput = Convert.ToInt32(Console.ReadLine());

        switch (userInput)
        {
            case 1:
                userAcc.Withdraw(userId);
                break;
            case 2:
                Console.WriteLine("Making Deposit");
                userAcc.Deposit(userId);
                break;
            case 3:
                return;
            default:
                Console.WriteLine("Invalid selection");
                break;
        }
    }
}