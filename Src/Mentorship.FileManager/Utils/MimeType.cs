namespace Mentorship.FileManager.Utils
{
    public static class MimeType
    {
        public static string GetContentTypeFromExtension(string extension)
        {
            var mimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
    {
        { ".txt", "text/plain" },
        { ".pdf", "application/pdf" },
        { ".doc", "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".png", "image/png" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif", "image/gif" },
        { ".csv", "text/csv" },
        { ".mp4", "video/mp4" },
        { ".avi", "video/x-msvideo" },
        { ".mov", "video/quicktime" },
        { ".wmv", "video/x-ms-wmv" },
        { ".flv", "video/x-flv" },
        { ".mkv", "video/x-matroska" },
        { ".webm", "video/webm" },
        { ".m4v", "video/x-m4v" },
        { ".3gp", "video/3gpp" },
        { ".3g2", "video/3gpp2" }
    };

            return mimeTypes.TryGetValue(extension.ToLower(), out string contentType)
                ? contentType
                : "application/octet-stream"; // Default for unknown types
        }

    }
}