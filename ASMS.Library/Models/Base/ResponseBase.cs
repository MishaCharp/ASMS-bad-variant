namespace ASMS.Library.Models.Base
{
    public abstract class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string? ResponseText { get; set; }
        public string? ErrorText { get; set; }
    }
}
