using shaker.crosscutting.Utils;

namespace shaker.crosscutting.Messages
{
    public static class MessagesGetter
    {
        public static string Get(ErrorLogMessages code, params string[] values)
        {
            return string.Format(EnumUtils.GetEnumMemberValue(code), values);
        }

        public static string Get(ErrorPresentationMessages code)
        {
            return EnumUtils.GetEnumMemberValue(code);
        }
    }
}
