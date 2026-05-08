namespace models.Dtos.GitHubDtos
{
    public class GitHubConfigDto
    {
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string AuthUrl { get; set; }
    }
}