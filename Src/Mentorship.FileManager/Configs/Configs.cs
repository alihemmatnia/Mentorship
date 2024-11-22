namespace Mentorship.FileManager.Configs
{
    public class Configs
    {
        public static string S3_ACCESSKEY;
        public static string S3_SECRETKEY;
        public static string S3_ENDPOINT;

        public static string S3_FILEBUCKET;


        public static void Init(IConfiguration configuration)
        {

            S3_ENDPOINT = MentorEnvironment.ReadVariable<string>(MentorEnvironment.S3_ENDPOINT)
                 ?? configuration.GetRequiredSection("S3_ENDPOINT").Value.ToString();
            S3_ACCESSKEY = MentorEnvironment.ReadVariable<string>(MentorEnvironment.S3_ACCESSKEY)
                 ?? configuration.GetRequiredSection("S3_ACCESSKEY").Value.ToString();
            S3_SECRETKEY = MentorEnvironment.ReadVariable<string>(MentorEnvironment.S3_SECRETKEY)
                 ?? configuration.GetRequiredSection("S3_SECRETKEY").Value.ToString();

            S3_FILEBUCKET = MentorEnvironment.ReadVariable<string>(MentorEnvironment.S3_FILEBUCKET)
                 ?? configuration.GetRequiredSection("S3_FILEBUCKET").Value.ToString();
        }
    }
}
