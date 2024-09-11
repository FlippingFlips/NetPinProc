namespace NetPinProc.Game.Manager.Shared.Responses
{
    public class Media
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? MimeType { get; set; }
        public byte[]? Data { get; set; }
        public long Size { get; set; }
        public string? Tags { get; set; }

        public string? Base64Url()
        {
            if(Data?.Length > 0)
            {
                return $"data:{MimeType};base64,{Convert.ToBase64String(Data)}";
            }

            return null;
        }            
    }
}
