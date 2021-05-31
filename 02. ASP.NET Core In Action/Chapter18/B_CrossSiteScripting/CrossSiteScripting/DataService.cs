namespace CrossSiteScripting
{
    using System.Collections.Generic;

    public static class DataService
    {
        public static IList<string> Data { get; } = new List<string> { "Dave", "Jim" };

        public static string GetMaliciousValue() => "<script>alert('Oh no! XSS!')</script>";
    }
}
