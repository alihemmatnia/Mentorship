using System.Security;

namespace Mentorship.FileManager.Configs
{
    public class MentorEnvironment
    {
        public const string S3_ENDPOINT = "S3_ENDPOINT";
        public const string S3_ACCESSKEY = "S3_ACCESSKEY";
        public const string S3_SECRETKEY = "S3_SECRETKEY";
        public const string S3_FILEBUCKET = "S3_FILEBUCKET";

        public static string ReadVariable(string name)
        {
            try
            {
                return Environment.GetEnvironmentVariable(name);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (SecurityException)
            {
                return null;
            }
        }

        public static T? ReadVariable<T>(string name)
        {
            var value = ReadVariable(name);
            if (value == null) return default;

            return ChangeType<T>(value);
        }

        public static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }

        public static string ReadRequiredVariable(string name)
        {
            try
            {
                var value = Environment.GetEnvironmentVariable(name);
                if (value == null) throw new ArgumentNullException();
                return value;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException($"Environment Variable {name} Not Provided");
            }
            catch (SecurityException)
            {
                throw new SecurityException($"SecurityException - Environment Variable {name} Not Found");
            }
        }

        public static T ReadRequiredVariable<T>(string name)
        {
            return (T)Convert.ChangeType(ReadRequiredVariable(name), typeof(T));
        }

        public static bool IsFromSettingFile()
        {
            try
            {
                return Environment.GetEnvironmentVariable("FromFile") == "true";
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsTestEnvironment()
        {
            try
            {
                return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
