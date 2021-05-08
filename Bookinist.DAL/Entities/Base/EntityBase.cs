using System;
using System.Collections.Generic;
using System.Text;
using Bookinist.Interfaces;

namespace Bookinist.DAL.Entities
{
    public abstract class EntityBase : IEntity
    {
        public  int Id { get; set; }
    }
}
