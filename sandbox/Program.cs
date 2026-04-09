using Malachite.Game;

public class Sandbox
{
    public static void Main()
    {
        Game game = new("Sandbox");
        Console.WriteLine($"Game '{game.Name}' created with state {game.State}.");
    }
}