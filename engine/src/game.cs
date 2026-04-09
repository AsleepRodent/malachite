namespace Malachite.Game;

using Malachite.Modules.Module;

public enum GameState
{
    Idle,
    Starting,
    Running,
    Stopping,
    Failed,
}

public class Game
{
    // Metadata
    public string Name { get; }

    // Lifecycle
    public GameState State { get; private set; } = GameState.Idle;
    public Dictionary<string, Module> Modules { get; } = new();
    
    public Game(string name)
    {
        Name = name;
    }

    private void Setup()
    {

    }

    public void Start()
    {
        if (State != GameState.Idle)
        {
            throw new InvalidOperationException($"Cannot start game '{Name}' because it is not idle.");
        }

        State = GameState.Starting;
        try
        {
            Setup();
            foreach (var module in Modules.Values)
            {
                module.Start();
            }
            State = GameState.Running;
        }
        catch
        {
            State = GameState.Failed;
            throw;
        }
    }

    public void Stop()
    {
        if (State != GameState.Running)
        {
            throw new InvalidOperationException($"Cannot stop game '{Name}' because it is not running.");
        }

        State = GameState.Stopping;
        try
        {
            foreach (var module in Modules.Values)
            {
                module.Stop();
            }
            State = GameState.Idle;
        }
        catch
        {
            State = GameState.Failed;
            throw;
        }
    }
}