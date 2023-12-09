// Part 1
// Configuration -- the cubes the bag is loaded with.
var configuration = new Dictionary<string, int>()
{
    { "red", 12 },
    { "green", 13 },
    { "blue", 14 }
};

int gameIdSum = 0;

using (var filestream = File.OpenRead("input.txt"))
{
    using var reader = new StreamReader(filestream);


    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        // Parse the line
        var firstSplit = line.Split(':');
        var gameId = int.Parse(firstSplit[0][5..]);
        var draws = firstSplit[1].Split(';');

        // Parse the draws
        bool violation = false;
        foreach (var draw in draws)
        {
            var items = draw.Split(',').Select(i => i.Trim());
            violation = items
                .Select(i => i.Split(' '))
                .Select(split => (int.Parse(split[0]), split[1]))
                .Any(d => configuration[d.Item2] < d.Item1);
            if (violation) break;
        }

        if (violation) continue;

        gameIdSum += gameId;
    }
}

Console.WriteLine(gameIdSum);

// Part 2

int powerSum = 0;
using (var filestream = File.OpenRead("input.txt"))
{
    using var reader = new StreamReader(filestream);

    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        // Parse the line
        var firstSplit = line.Split(':');
        var gameId = int.Parse(firstSplit[0][5..]);
        var draws = firstSplit[1].Split(';');

        var power = draws
            .SelectMany(d => d.Split(';')) // flatten all draws
            .SelectMany(d => d.Split(',')) // flatten all items
            .Select(i => i.Trim().Split(' '))
            .Select(split => (int.Parse(split[0]), split[1])) // parse into tuple (#, color)
            .GroupBy(i => i.Item2) // group by color
            .Select(grp => new
            {
                grp.Key,
                Max = grp.Max(m => m.Item1)
            })
            .Aggregate(1, (total, incoming) => total * incoming.Max);

        powerSum += power;
    }
}

Console.WriteLine(powerSum);