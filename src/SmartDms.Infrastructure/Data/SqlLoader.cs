using System.Collections.Concurrent;
using System.Reflection;

namespace JuJuBis.Infrastructure.Data;

/// <summary>
/// អាន SQL script ពី folder Sql/ (cache ក្នុង memory)
/// SQL ទាំងអស់ជា script - មិនសរសេរ query វែងៗក្នុង C# ទេ
/// </summary>
public sealed class SqlLoader
{
    private readonly string _basePath;
    private readonly ConcurrentDictionary<string, string> _cache = new();

    public SqlLoader()
    {
        var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        _basePath = Path.Combine(assemblyDir, "Sql");
    }

    public string Load(string name)
        => _cache.GetOrAdd(name, n =>
        {
            var path = Path.Combine(_basePath, $"{n}.sql");
            if (!File.Exists(path))
                throw new FileNotFoundException($"SQL script not found: {n}.sql");
            return File.ReadAllText(path);
        });
}
