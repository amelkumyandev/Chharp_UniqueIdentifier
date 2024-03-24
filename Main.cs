using System;
using System.Collections.Concurrent;
using System.Threading;

public class UniqueIdentifierGenerator
{
    // Method to generate unique identifier
    public static Guid GenerateUniqueIdentifier()
    {
        return Guid.NewGuid();
    }
}

class Program
{
    private static ConcurrentDictionary<Guid, object> generatedIdentifiers = new ConcurrentDictionary<Guid, object>();

    static void GenerateAndStoreUniqueIdentifier()
    {
        Guid id = UniqueIdentifierGenerator.GenerateUniqueIdentifier();
        // Assuming null as placeholder data for demonstration
        bool added = generatedIdentifiers.TryAdd(id, null);
        if (added)
        {
            Console.WriteLine($"Generated Unique Identifier: {id}");
        }
    }

    static void Main(string[] args)
    {
        const int numberOfThreads = 10;
        var threads = new Thread[numberOfThreads];

        for (int i = 0; i < numberOfThreads; i++)
        {
            threads[i] = new Thread(GenerateAndStoreUniqueIdentifier);
            threads[i].Start();
        }

        // Wait for all threads to complete
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"Total Unique Identifiers Generated: {generatedIdentifiers.Count}");
    }
}
