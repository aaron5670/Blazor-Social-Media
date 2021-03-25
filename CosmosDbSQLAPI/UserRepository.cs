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
    private readonly CommunityDbContext communityDbContext;

    public UserRepository(CommunityDbContext communityDbContext)
    {
      this.communityDbContext = communityDbContext;
    }

    public void Add(User user)
    {
      communityDbContext.Users.Add(user);
    }

    public void Delete(User user)
    {
      communityDbContext.Users.Remove(user);
    }

    public IQueryable<User> Find(Expression<Func<User, bool>> expression)
    {
      return communityDbContext.Users.Where(expression);
    }

    public ICollection<User> GetAll()
    {
      return communityDbContext.Users.ToList();
    }

    public void Commit()
    {
      communityDbContext.SaveChanges();
    }

    public void Include(string navigationPropertyPath)
    {
      _ = communityDbContext.Users.Include(navigationPropertyPath);
    }

    public void GenerateDatabase()
    {
      this.communityDbContext.Database.EnsureDeleted();
      this.communityDbContext.Database.EnsureCreated();
    }


    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          communityDbContext.Dispose();
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~UserRepository()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}