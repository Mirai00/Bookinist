namespace Bookinist.DAL.Entities
{
    public abstract class Person : NamedEntity
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}