using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Models;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Services
{
    public class ContactRepository
    {
        private const string CacheKey = "ContactStore";

        public ContactRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Contact[]
                    {
                new Contact
                {
                    Id = 1, Name = "Glenn Block"
                },
                new Contact
                {
                    Id = 2, Name = "Dan Roth"
                }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }
        public Contact[] GetAllContacts()
        {
            return new Contact[]
            {
                new Contact
                {
                    Id = 1,
                    Name = "Glenn Block"
                },
                new Contact
                {
                    Id = 2,
                    Name = "Dan Roth"
                }
            };
        }
    }

    public class MyComponent : IMyComponent
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public MyComponent(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetDataFromSession()
        {
            return _contextAccessor.HttpContext.Session.GetString(*KEY *);
        }
    }
}
