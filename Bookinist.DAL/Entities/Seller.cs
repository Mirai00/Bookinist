namespace Bookinist.DAL.Entities
{
    public class Seller : Person
    {

        public override string ToString() => $"Продавец {Surname} {Name} {Patronymic}";
    }
}