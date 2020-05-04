using System.Runtime.Serialization;

namespace shaker.crosscutting.Messages
{
    public enum ErrorLogMessages
    {
        [EnumMember(Value = "Critical => User {0} :: {1}")]
        CriticalLogErrorMessage,

        [EnumMember(Value = "FailedSignIn => User {0}")]
        FailedSignInLogErrorMessage,

        [EnumMember(Value = "Lockout => User {0}")]
        LockoutLogErrorMessage,

        [EnumMember(Value = "NotAllowed => User {0}")]
        NotAllowedLogErrorMessage,

        [EnumMember(Value = "RequiresTwoFactor => User {0}")]
        RequiresTwoFactorLogErrorMessage

    }
}
