/// <summary>
/// A basic implementation of a FIFO Queue
/// </summary>
public class PersonQueue
{
    private readonly List<Person> _queue = new();

    /// <summary>
    /// Gets the number of people currently in the queue
    /// </summary>
    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the back of the queue (FIFO)
    /// </summary>
    /// <param name="person">The person to add</param>
    public void Enqueue(Person person)
    {
        _queue.Add(person); // add to the end
    }

    /// <summary>
    /// Remove and return the person at the front of the queue
    /// </summary>
    public Person Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        var person = _queue[0]; // front of the queue
        _queue.RemoveAt(0);
        return person;
    }

    /// <summary>
    /// Check if the queue is empty
    /// </summary>
    public bool IsEmpty()
    {
        return _queue.Count == 0;
    }

    /// <summary>
    /// Return a string representation of the queue
    /// </summary>
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}
