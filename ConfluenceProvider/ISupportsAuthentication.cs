namespace PurplePete.ConfluenceProvider
{
    public interface ISupportsAuthentication
    {
        string? BearerToken { get; set; }
    }
}
