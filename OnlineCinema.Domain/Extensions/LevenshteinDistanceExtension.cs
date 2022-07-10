namespace OnlineCinema.Domain.Extensions;

public static class LevenshteinDistanceExtension
{
    public static int LevenshteinDistance(this string value, string other)
    {
        int m = value.Length, n = other.Length;
        var ed = new int[m, n];
	
        for (var i = 0; i < m; ++i)
            ed[i, 0] = i + 1;
	
        for (var j = 0; j < n; ++j)
            ed[0, j] = j + 1;
	
        for (var j = 1; j < n; ++j)
            for (var i = 1; i < m; ++i)
            {
                if (value[i] == other[j])
                    ed[i, j] = ed[i - 1, j - 1];
                else
                    ed[i, j] = Math.Min(ed[i - 1, j] + 1, 
                        Math.Min(ed[i, j - 1] + 1, ed[i - 1, j - 1] + 1));
            }
	
        return ed[m - 1, n - 1];
    }
}