using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Malachite.Common.Events;

public class Signal<T>
{
    private readonly List<Action<T>> listeners = new();

    public void Connect(Action<T> listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void Disconnect(Action<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Fire(T args)
    {
        ReadOnlySpan<Action<T>> span = CollectionsMarshal.AsSpan(listeners);
        for (int i = span.Length - 1; i >= 0; i--)
        {
            span[i](args);
        }
    }

    public void Clear() => listeners.Clear();
}

public class Signal
{
    private readonly List<Action> listeners = new();

    public void Connect(Action listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void Disconnect(Action listener)
    {
        listeners.Remove(listener);
    }

    public void Fire()
    {
        ReadOnlySpan<Action> span = CollectionsMarshal.AsSpan(listeners);
        for (int i = span.Length - 1; i >= 0; i--)
        {
            span[i]();
        }
    }

    public void Clear() => listeners.Clear();
}