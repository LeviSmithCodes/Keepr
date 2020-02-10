using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public Vault Create(Vault newVault)
    {
      newVault.Id = _repo.Create(newVault);
      return newVault;
    }

    internal object GetById(int id, string userId)
    {
      Vault exists = _repo.GetById(id, userId);
      if (exists == null) { throw new Exception("Invalid Id or You Did Not Create This Vault"); }
      return exists;
    }

    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }

    internal object Delete(int id, string userId)
    {
      Vault exists = _repo.GetById(id, userId);
      if (exists == null) { throw new Exception("Invalid Id or You Did Not Create This Vault"); }

      _repo.Delete(id, userId);
      return "Successfully Deleted";
    }
  }
}