
using System;

namespace Tim.Domain.Entities
{
  public abstract class Entity : IEquatable<Entity>
  {
 
    public Entity(int id)
    {
      Id = id;
    }

    public int Id { get; private set; }
    
    public bool Equals(Entity other)
    {
      return Id == other.Id;
    }


  }
}