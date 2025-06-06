class User
{
    public int UserID { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int CardNumber { get; set; }

    public int Pin { get; set; }

    public void displayUserInformation()
    {
        Console.WriteLine($"Welcome back {FirstName} {LastName} {CardNumber}");
    }
}