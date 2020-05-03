using System.Security.Principal;

namespace shaker.crosscutting.Accessors
{
    public class ConnectedUserAccessor : IConnectedUserAccessor
    {
        private readonly IPrincipal _principal;

        public ConnectedUserAccessor(IPrincipal principal)
        {
            _principal = principal;
        }

        public string GetId()
        {
            return _principal.Identity.Name;
        }
    }
}
