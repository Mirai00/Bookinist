using System;
using System.ComponentModel;
using System.Text;

namespace Bookinist.DAL.Entities
{
    public class Book : NamedEntity
    {
        public virtual Category Category { get; set; }
    }
}
