class Menu {
    private Breathing breathing = new();
    private Reflection reflection = new();
    private Listing listing = new();

    public void Run() {
        Console.WriteLine("Here are the available Activities:");
        breathing.MenuDisplay(1);
        reflection.MenuDisplay(2);
        listing.MenuDisplay(3);
    }
}