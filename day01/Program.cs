var tokens = new Dictionary<string, int>()
{
    { "1", 1 },
    { "2", 2 },
    { "3", 3 },
    { "4", 4 },
    { "5", 5 },
    { "6", 6 },
    { "7", 7 },
    { "8", 8 },
    { "9", 9 },
    { "0", 0 },
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

int calibrationSum = 0;

using (var filestream = File.OpenRead("input.txt"))
{
    using var reader = new StreamReader(filestream);

    var foundTokenValues = new List<int>();
    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        foundTokenValues.Clear();

        for (int i = 0; i < line.Length; i++)
        {
            // Determine if the substring starting at this position matches any token
            var match = tokens.Keys.FirstOrDefault(t => line[i..].StartsWith(t));
            if (match != null)
            {
                foundTokenValues.Add(tokens[match]);
            }
        }

        var calibrationValue = foundTokenValues.First() * 10 + foundTokenValues.Last();
        calibrationSum += calibrationValue;
    }
}

Console.WriteLine(calibrationSum.ToString());