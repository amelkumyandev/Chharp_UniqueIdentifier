using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class UniqueIdentifierGenerator
{
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
        const int numberOfTasks = 10;
        var tasks = new Task[numberOfTasks];

        for (int i = 0; i < numberOfTasks; i++)
        {
            tasks[i] = Task.Run(() => GenerateAndStoreUniqueIdentifier());
        }

        // Wait for all tasks to complete
        Task.WaitAll(tasks);

        Console.WriteLine($"Total Unique Identifiers Generated: {generatedIdentifiers.Count}");
    }
}
