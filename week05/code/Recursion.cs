using System.Collections;

public static class Recursion
{
    /// <summary>
    /// Problem 1: Recursive Squares Sum
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case
        if (n <= 0)
            return 0;

        // Recursive case
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// Problem 2: Permutations Choose
    /// </summary>
    public static void PermutationsChoose(
        List<string> results,
        string letters,
        int size,
        string word = "")
    {
        // Base case: We have selected enough letters.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try each available letter.
        for (int i = 0; i < letters.Length; i++)
        {
            char letter = letters[i];

            // Remove the selected letter.
            string remainingLetters = letters.Remove(i, 1);

            // Recursively build the word.
            PermutationsChoose(
                results,
                remainingLetters,
                size,
                word + letter
            );
        }
    }

    /// <summary>
    /// Problem 3: Climbing Stairs
    /// </summary>
    public static decimal CountWaysToClimb(
        int s,
        Dictionary<int, decimal>? remember = null)
    {
        // Base cases
        if (s == 0)
            return 0;

        if (s == 1)
            return 1;

        if (s == 2)
            return 2;

        if (s == 3)
            return 4;

        // Initialize the dictionary.
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Check whether the result is already remembered.
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Recursive calculation with memoization.
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Store the calculated result.
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// Problem 4: Wildcard Binary Patterns
    /// </summary>
    public static void WildcardBinary(
        string pattern,
        List<string> results)
    {
        // Find the first wildcard.
        int index = pattern.IndexOf('*');

        // Base case: No wildcard remains.
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace the wildcard with 0.
        string patternZero =
            pattern[..index] + "0" + pattern[(index + 1)..];

        WildcardBinary(patternZero, results);

        // Replace the wildcard with 1.
        string patternOne =
            pattern[..index] + "1" + pattern[(index + 1)..];

        WildcardBinary(patternOne, results);
    }

    /// <summary>
    /// Problem 5: Maze
    /// </summary>
    public static void SolveMaze(
        List<string> results,
        Maze maze,
        int x = 0,
        int y = 0,
        List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize the path on the first call.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Add the current position to the path.
        currPath.Add((x, y));

        // Check whether we reached the end.
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            // Try moving right.
            if (maze.IsValidMove(currPath, x + 1, y))
            {
                SolveMaze(results, maze, x + 1, y, currPath);
            }

            // Try moving down.
            if (maze.IsValidMove(currPath, x, y + 1))
            {
                SolveMaze(results, maze, x, y + 1, currPath);
            }

            // Try moving left.
            if (maze.IsValidMove(currPath, x - 1, y))
            {
                SolveMaze(results, maze, x - 1, y, currPath);
            }

            // Try moving up.
            if (maze.IsValidMove(currPath, x, y - 1))
            {
                SolveMaze(results, maze, x, y - 1, currPath);
            }
        }

        // Backtrack: remove the current position.
        currPath.RemoveAt(currPath.Count - 1);
    }
}