using System.Runtime.Serialization;

namespace shaker.crosscutting.Messages
{
    public enum ErrorPresentationMessages
    {
        [EnumMember(Value = "Oops, failed signin !")]
        FailedSignInErrorMessage,

        [EnumMember(Value = "Oops, you lock your account please contact admin !")]
        LockoutErrorMessage,

        [EnumMember(Value = "Oops, you lock your account please contact admin !")]
        NotAllowedErrorMessage,

        [EnumMember(Value = "Oops, you require two factor authentication !")]
        RequiresTwoFactorErrorMessage,

        [EnumMember(Value = "Oops, we encountered an error. Please try again !")]
        DefaultErrorMessage
}
}
