namespace EducationBySubscription.Application.Core.Plans.Views;

public class PlanViewModel
{
    public PlanViewModel(Guid id, string title, string description, decimal basePrice)
    {
        Id = id;
        Title = title;
        Description = description;
        BasePrice = basePrice;
    }
    
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal BasePrice { get; private set; }
}