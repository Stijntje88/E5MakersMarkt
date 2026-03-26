using E5MakersMarkt.Data.Models;

namespace E5MakersMarkt.Data.Session;

internal static class CurrentSession
{
    public static User? LoggedInUser { get; set; }
}
