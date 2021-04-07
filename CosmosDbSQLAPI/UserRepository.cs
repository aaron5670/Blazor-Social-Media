using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CosmosDbSQLAPI
{
    public class UserRepository : IDisposable
    {
        private readonly CommunityDbContext _communityDbContext;

        public UserRepository(CommunityDbContext communityDbContext)
        {
            _communityDbContext = communityDbContext;
        }

        public void Add(User user)
        {
            _communityDbContext.Users.Add(user);
        }

        public void Delete(User user)
        {
            _communityDbContext.Users.Remove(user);
        }

        public IQueryable<User> Find(Expression<Func<User, bool>> expression)
        {
            return _communityDbContext.Users.Where(expression);
        }

        public List<User> GetAll()
        {
            return _communityDbContext.Users.ToList();
        }

        public void Commit()
        {
            _communityDbContext.SaveChanges();
        }

        public void Include(string navigationPropertyPath)
        {
            _ = _communityDbContext.Users.Include(navigationPropertyPath);
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _communityDbContext.Dispose();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}