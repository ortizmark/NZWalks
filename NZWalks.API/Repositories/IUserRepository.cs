﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(string username, string password);

    }
}
