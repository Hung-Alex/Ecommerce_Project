namespace Application.Common.Interface
{
    public record GoogleSettings
    {
        public string ClientId {  get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri {  get; set; }
    }
}
