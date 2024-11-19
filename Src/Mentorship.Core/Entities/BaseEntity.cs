namespace Mentorship.Domain.Entities
{
    public class BaseEntity<TID>
    {
        public TID Id { get; private set; }
    }
}