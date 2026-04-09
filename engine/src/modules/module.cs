namespace Malachite.Modules.Module;

using Malachite.Common.Interfaces;
using Malachite.Common.Events;

public enum ModuleState
{
    Idle,
    Starting,
    Running,
    Stopping,
    Failed,
}

public abstract class Module : IUpdatable
{
    // Metadata
    public Module Parent { get; }
    public string Name { get; } = "Unnamed Module";
    public string Description { get; } = "No description provided.";
    public int Priority { get; } = 0;

    // Lifecycle
    public ModuleState State { get; private set; } = ModuleState.Idle;

    // Signals
    public Signal OnStart { get; } = new();
    public Signal OnStop { get; } = new();

    protected Module(Module parent, string name, string description, int priority = 0)
    {
        Parent = parent;
        Name = name;
        Description = description;
        Priority = priority;
    }

    public virtual void Update(double deltaTime) {}

    public void Start()
    {
        if (State != ModuleState.Idle)
        {
            throw new InvalidOperationException($"Cannot start module '{Name}' because it is not idle.");
        }

        State = ModuleState.Starting;
        try
        {
            OnStart.Fire();
            State = ModuleState.Running;
        }
        catch
        {
            State = ModuleState.Failed;
            throw;
        }
    }

    public void Stop()
    {
        if (State != ModuleState.Running)
        {
            throw new InvalidOperationException($"Cannot stop module '{Name}' because it is not running.");
        }

        State = ModuleState.Stopping;
        try
        {
            OnStop.Fire();
            State = ModuleState.Idle;
        }
        catch
        {
            State = ModuleState.Failed;
            throw;
        }
    }
}