using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    // Problem 1: FindPairs
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var result = new List<string>();

        foreach (var word in words)
        {
            char[] chars = word.ToCharArray();
            Array.Reverse(chars);
            string reversed = new string(chars);

            if (seen.Contains(reversed) && word != reversed)
            {
                result.Add($"{word} & {reversed}");
            }

            seen.Add(word);
        }

        return result.ToArray();
    }

    // Problem 2: SummarizeDegrees
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            string degree = fields[3].Trim(); // 4th column

            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    // Problem 3: IsAnagram
    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length)
            return false;

        var counts = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (!counts.ContainsKey(c))
                counts[c] = 0;
            counts[c]++;
        }

        foreach (char c in word2)
        {
            if (!counts.ContainsKey(c))
                return false;
            counts[c]--;
            if (counts[c] < 0)
                return false;
        }

        foreach (var kvp in counts)
        {
            if (kvp.Value != 0)
                return false;
        }

        return true;
    }

    // Problem 5: EarthquakeDailySummary
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        string json = client.GetStringAsync(uri).Result;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();
        foreach (var feature in featureCollection.features)
        {
            results.Add($"{feature.properties.place} - Mag {feature.properties.mag}");
        }

        return results.ToArray();
    }
}
