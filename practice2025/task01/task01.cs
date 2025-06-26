namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        var marked = new string(input
            .ToLower()
            .Where(c => !char.IsWhiteSpace(c) && !char.IsPunctuation(c))
            .ToArray());

        if (marked == "") return false;

        var reversed = new string(marked.Reverse().ToArray());
        
        return marked == reversed;
    }
}
