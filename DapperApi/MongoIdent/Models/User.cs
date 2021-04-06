using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace MongoIdent.Models
{
	[CollectionName("users")]
	public class ApplicationUser : MongoIdentityUser<Guid>
	{
		public ApplicationUser() : base()
		{
		}

		public ApplicationUser(string userName, string email) : base(userName, email)
		{
		}
	}

	[CollectionName("roles")]
	public class ApplicationRole : MongoIdentityRole<Guid>
	{
		public ApplicationRole() : base()
		{
		}

		public ApplicationRole(string roleName) : base(roleName)
		{
		}
	}
}
