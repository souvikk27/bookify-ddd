// ReSharper disable MemberCanBePrivate.Global

namespace Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = id;
            ReferenceId = Id.ToString();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        protected Entity() { }

        private readonly List<IDomainEvent> _domainEvents = new();
        public Guid Id { get; set; }
        public string? ReferenceId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        protected void RaiseDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}