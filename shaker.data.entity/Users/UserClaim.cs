﻿using System.Security.Claims;

namespace shaker.data.entity.Users
{
    public class IdentityUserClaim
    {
		public IdentityUserClaim()
		{
		}

		public IdentityUserClaim(Claim claim)
		{
			Type = claim.Type;
			Value = claim.Value;
		}

		public string Type { get; set; }
		public string Value { get; set; }

		public Claim ToSecurityClaim()
		{
			return new Claim(Type, Value);
		}
	}
}
