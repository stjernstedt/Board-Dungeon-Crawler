using System.Collections.Generic;

public class PriorityQueue<T>
{
    List<(T item, int priority)> queue;
    public PriorityQueue()
    {
        queue = new List<(T item, int priority)>();
    }
    public void Enqueue(T item, int priority)
    {
        queue.Add((item, priority));
    }
    public int Count { get { return queue.Count; } }
    public T Dequeue()
    {
        if (queue.Count == 0) throw new System.InvalidOperationException("The queue is empty.");
        var bestCandidate = queue[0];
        foreach (var item in queue)
        {
            if (item.priority < bestCandidate.priority) bestCandidate = item;
        }
        queue.Remove(bestCandidate);
        return bestCandidate.item;
    }

    public (T, int) Peek()
    {
        if (queue.Count == 0) throw new System.InvalidOperationException("The queue is empty.");
        var bestCandidate = queue[0];
        foreach (var item in queue)
        {
            if (item.priority < bestCandidate.priority) bestCandidate = item;
        }
        return bestCandidate;
    }

    public bool Contains(T item)
    {
        foreach (var element in queue)
        {
            if (EqualityComparer<T>.Default.Equals(element.item, item)) return true;
        }
        return false;
    }

    public List<T> ToList()
    {
        List<T> list = new List<T>();
        foreach (var element in queue)
        {
            list.Add(element.item);
        }

        return list;
    }

    internal void UpdatePriority(T item, int priority)
    {
        queue[queue.FindIndex(x => EqualityComparer<T>.Default.Equals(x.item, item))] = (item, priority);
    }
}