namespace Tinderro.API.Helpers
{

    // klasa slzy do przekazywania parametrow do pobierania uzytkownikow

    public class UserParams
    {
        public const int MaxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 24;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        // id i gender sa potrzebne do tego zeby na liscie uzytkownikow nie pojawial sie zalogowany user i zeby np kobiecie pokazywali sie faceci
        public int UserId { get; set; }
        public string Gender { get; set; }

        // filterki
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public string ZodiacSign { get; set; } = "wszystkie";

        // sortowanie
        public string OrderBy { get; set; } // za to mozna bedzie podstawic co sie chce, np po cenie albo jaki piernik
    }
}