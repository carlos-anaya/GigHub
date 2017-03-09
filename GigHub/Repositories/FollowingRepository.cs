﻿using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followeeId, string followerId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId
                    && f.FollowerId == followerId);
        }

        public bool IsFollowing(string followeeId, string followerId)
        {
            return GetFollowing(followeeId, followerId) != null;
        }
    }
}