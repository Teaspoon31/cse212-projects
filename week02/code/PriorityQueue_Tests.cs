using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities and dequeue them.
    // Expected Result: Highest priority item is removed first.
    // Defect(s) Found: Code originally skipped last element in loop.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("Low", 1);
        pq.Enqueue("Medium", 5);
        pq.Enqueue("High", 10);

        var result = pq.Dequeue();
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue items with equal priority.
    // Expected Result: The first item enqueued with that priority is removed first (FIFO).
    // Defect(s) Found: Code originally overwrote index on equal priority, breaking FIFO.
    public void TestPriorityQueue_EqualPriorityFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("First", 5);
        pq.Enqueue("Second", 5);

        var result = pq.Dequeue();
        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None after fix.
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var pq = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with mixed priorities and dequeue repeatedly.
    // Expected Result: Items come out in order of highest priority first, respecting FIFO for ties.
    // Defect(s) Found: Code did not remove item after returning value.
    public void TestPriorityQueue_MixedPriorities()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 2);
        pq.Enqueue("B", 7);
        pq.Enqueue("C", 7);
        pq.Enqueue("D", 10);

        Assert.AreEqual("D", pq.Dequeue()); // highest priority
        Assert.AreEqual("B", pq.Dequeue()); // next highest, FIFO among equal
        Assert.AreEqual("C", pq.Dequeue()); // next equal priority
        Assert.AreEqual("A", pq.Dequeue()); // lowest priority
    }
}
