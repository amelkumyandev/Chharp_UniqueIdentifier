using System;
using System.Collections.Concurrent;
using System.Threading;

public class UniqueIdentifierGenerator
{
    public static Guid GenerateUniqueIdentifier()
    {
        return Guid.NewGuid();
    }
}

class Program
{
    private static ConcurrentBag<Guid> generatedIdentifiers = new ConcurrentBag<Guid>();

    static void GenerateAndStoreUniqueIdentifier()
    {
        Guid id = UniqueIdentifierGenerator.GenerateUniqueIdentifier();
        generatedIdentifiers.Add(id);
        Console.WriteLine($"Generated Unique Identifier: {id}");
    }

    static void Main(string[] args)
    {
        const int numberOfThreads = 100;
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
