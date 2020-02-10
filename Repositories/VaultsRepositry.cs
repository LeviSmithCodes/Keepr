using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string userId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @userId;";
      return _db.Query<Vault>(sql, new { userId });
    }
    internal int Create(Vault newVault)
    {
      string sql = @"
            INSERT INTO vaults (name, description, userId) 
            VALUES (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      return id;
    }

    internal Vault GetById(int id, string userId)
    {
      string sql = "SELECT * FROM vaults WHERE id = @id AND userId = @userId";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id, userId });
    }



    internal void Delete(int id, string userId)
    {
      string sql = "DELETE FROM vaults WHERE id = @id AND userId = @userId";
      _db.Execute(sql, new { id, userId });
    }
  }
}