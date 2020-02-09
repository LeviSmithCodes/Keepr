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
      // I don't know why this threw an error when the delete didn't (maybe it created it's own?)... oh wait, I don't want thsi logic. Duh. Ppl should be able to see the public posts of others by Id. 
      // if (exists.UserId != userId) { throw new Exception("Cannot get things you did not create"); }
      return exists;
    }

    public Keep Create(Keep newKeep)
    {
      newKeep.Id = _repo.Create(newKeep);
      return newKeep;
    }

    public Keep Edit(Keep editedKeep, int id)
    {
      Keep exists = _repo.GetById(id, editedKeep.UserId);
      if (exists == null) { throw new Exception("Invalid Id"); }
      if (exists.UserId != editedKeep.UserId) { throw new Exception("Cannot edit things you did not create"); }
      _repo.Edit(editedKeep, id);
      return editedKeep;


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