namespace Malachite.Modules.Scheduler;

using Malachite.Modules.Module;
using Malachite.Modules.Scheduler.Other;
using System.Diagnostics;

public class Scheduler : Module
{
    // Lifecycle
    public List<Channel> Channels { get; private set; } = new();
    private readonly Dictionary<Channel, double> accumulators = new();
    private readonly Stopwatch stopwatch = new();
    private double lasttime;

    public Scheduler(Module parent) : base(
        parent: parent,
        name: "Scheduler",
        description: "Manages the execution of updatable entries across multiple channels.",
        priority: 100
    )
    {
        OnStart.Connect(() =>
        {
            lasttime = 0;
            stopwatch.Restart();
        });

        OnStop.Connect(() =>
        {
            stopwatch.Stop();
        });
    }

    public void AddChannel(Channel channel)
    {
        if (!Channels.Contains(channel))
        {
            Channels.Add(channel);
            accumulators[channel] = 0;
            Channels.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }
    }

    public void RemoveChannel(Channel channel)
    {
        Channels.Remove(channel);
        accumulators.Remove(channel);
    }

    public void Dispatch()
    {
        while (State == ModuleState.Running)
        {
            double currenttime = stopwatch.Elapsed.TotalSeconds;
            double deltatime = currenttime - lasttime;
            lasttime = currenttime;

            for (int i = 0; i < Channels.Count; i++)
            {
                var channel = Channels[i];
                accumulators[channel] += deltatime;

                double targetinterval = 1.0 / channel.TargetFrameRate;

                if (accumulators[channel] >= targetinterval)
                {
                    channel.Update(accumulators[channel]);
                    accumulators[channel] = 0;
                }
            }
        }
    }
}