using Infrastructure.Services.Auth;

namespace Infrastructure.Helper
{
    public static class PolicyHelper
    {
        public const string PolicyPrefix = "PERMISSION";
        public const string Separator = "_";
        public static PermissionOperator GetOperatorFromPolicy(string policyName)
        {
            var @operator = int.Parse(policyName.AsSpan(PolicyPrefix.Length+1, 1));
            return (PermissionOperator)@operator;
        }
        public static string[] GetPermissionsFromPolicy(string policyName)
        {
            return policyName.Substring(PolicyPrefix.Length + 2)
                .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
