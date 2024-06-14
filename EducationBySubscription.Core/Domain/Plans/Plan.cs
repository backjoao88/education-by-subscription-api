using EducationSubscription.Core.Primitives;

namespace EducationSubscription.Core.Domain.Plans;

public class Plan : Entity
{
    public Plan(string title, string description, decimal basePrice)
    {
        Title = title;
        Description = description;
        BasePrice = basePrice;
    }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal BasePrice { get; private set; }

    public void Update(string title, string description, decimal basePrice)
    {
        Title = title;
        Description = description;
        BasePrice = basePrice;
    }
    
}