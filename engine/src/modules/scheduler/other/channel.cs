namespace Malachite.Modules.Scheduler.Other;

using Malachite.Common.Interfaces;

public class Channel
{
    // Metadata
    public string Name { get; } = "Unnamed Channel";
    public string Description { get; } = "No description provided.";
    public int Priority { get; } = 0;
    public int TargetFrameRate { get; } = 60;

    // Lifecycle
    public List<IUpdatable> Entries { get; private set; } = new();

    public Channel(string name, int priority = 0, int targetFrameRate = 60)
    {
        Name = name;
        Description = "No description provided.";
        Priority = priority;
        TargetFrameRate = targetFrameRate;
    }

    public void AddEntry(IUpdatable entry)
    {
        Entries.Add(entry);
    }

    public void RemoveEntry(IUpdatable entry)
    {
        Entries.Remove(entry);
    }

    public void ClearEntries()
    {
        Entries.Clear();
    }

    public void Update(double deltaTime)
    {
        for (int i = Entries.Count - 1; i >= 0; i--)
        {
            Entries[i].Update(deltaTime);
        }
    }
}