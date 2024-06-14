namespace EducationBySubscription.Application.Core.Users.Views;

public class UserCreatedViewModel
{
    public UserCreatedViewModel(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}