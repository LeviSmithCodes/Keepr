using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal object Create(VaultKeep newVaultKeep)
    {
      newVaultKeep.Id = _repo.Create(newVaultKeep);
      return newVaultKeep;
    }

    internal object Delete(int vaultId, int keepId, string userId)
    {
      VaultKeep exists = _repo.GetByIds(vaultId, keepId, userId);
      if (exists == null) { throw new Exception("Invalid Id or You Did Not Create This VaultKeep"); }
      _repo.Delete(vaultId, keepId, userId);
      return "Successfully Deleted";
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      // this would only return one
      // VaultKeep exists = _repo.GetById(vaultId, userId);
      // if (exists == null) { throw new Exception("Invalid Id or You Did Not Create This Vault"); }
      return _repo.GetKeepsByVaultId(vaultId, userId);
    }
  }
}