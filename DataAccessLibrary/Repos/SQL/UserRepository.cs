using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Repos.SQL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MovieContext context) : base(context) { }

    }
}
