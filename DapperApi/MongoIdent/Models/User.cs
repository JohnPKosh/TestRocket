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

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string OrgName { get; set; }

		public Guid? TeamKey { get; set; }

		public string TeamName { get; set; }
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
