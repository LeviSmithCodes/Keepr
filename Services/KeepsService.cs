using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    internal object GetById(int id, string userId)
    {
      Keep exists = _repo.GetById(id, userId);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.UserId != userId) { throw new Exception("Cannot delete things you did not create"); }
      return exists;
    }

    public Keep Create(Keep newKeep)
    {
      newKeep.Id = _repo.Create(newKeep);
      return newKeep;
    }

    internal object Delete(int id, string userId)
    {
      Keep exists = _repo.GetById(id, userId);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.UserId != userId) { throw new Exception("Cannot delete things you did not create"); }
      _repo.Delete(id, userId);
      return "Successfully Deleted";
    }
  }
}