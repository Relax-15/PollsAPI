﻿using PollsAPI.Entities;

namespace PollsAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
    // int ExtractUserIdFromToken(string token);

}