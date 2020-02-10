using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(VaultKeep newVaultKeep)
    {
      string sql = @"
            INSERT INTO vaultkeeps (vaultId, keepId, userId) 
            VALUES (@VaultId, @KeepId, @UserId);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return id;
    }

    internal VaultKeep GetByIds(int vaultId, int keepId, string userId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @vaultId AND keepId = @keepId AND userId = @userId";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultId, keepId, userId });
    }

    internal void Delete(int vaultId, int keepId, string userId)
    {
      string sql = "DELETE FROM vaultkeeps WHERE vaultId = @vaultId AND keepId = @keepId AND userId = @userId";
      _db.Execute(sql, new { vaultId, keepId, userId });
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"SELECT k.* FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId
      WHERE (vk.vaultId = @vaultId AND vk.userId = @userId)";
      return _db.Query<Keep>(sql, new { vaultId, userId });
    }
  }
}