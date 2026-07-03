namespace ScreenStats.App.Errors;

public static class ErrorManager
{
    public static List<Error> Errors { get; } = [];

    public static void Add(string message)
    {
        Errors.Add(new Error(message));
    }

    public static void Clear()
    {
        Errors.Clear();
    }

    public static bool HasErrors()
    {
        return Errors.Count > 0;
    }
}