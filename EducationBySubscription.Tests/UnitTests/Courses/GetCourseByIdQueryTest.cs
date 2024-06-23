using EducationBySubscription.Application.Core.Courses.Queries.GetCourseById;
using EducationBySubscription.Application.Core.Courses.Views;
using EducationSubscription.Core.Domain.Courses;
using EducationSubscription.Core.Domain.Courses.Errors;
using EducationSubscription.Core.Domain.Members;
using EducationSubscription.Core.Domain.Members.Errors;
using EducationSubscription.Core.Domain.Users;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Primitives.Errors;
using EducationSubscription.Core.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace EducationBySubscription.Tests.UnitTests.Courses;

public class GetCourseByIdQueryTest
{

    [Fact]
    public async Task ShouldReturnSuccessIfMemberHasActiveSubscriptionWithCourse()
    {
        // Setup Fixture
        var exampleUser = new User("joao@gmail.com", "123", EUserRole.Student);
        var exampleMember = new Member("João", "Back", "11154990800", "joao@gmail.com", true);
        var exampleCourse = Course.Create("Compiladores", "Curso de teste");
        exampleCourse.AddLesson(Lesson.Create(exampleCourse.Id, "Teste", "Teste"));

        var allowedCourse = exampleCourse.Id;
        var requestedCourse = exampleCourse.Id;
        
        var userRepositoryMock = Substitute.For<IUserRepository>();
        userRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .Returns(exampleUser);

        var memberRepositoryMock = Substitute.For<IMemberRepository>();
        memberRepositoryMock
            .ReadByEmail(Arg.Any<string>())
            .Returns(exampleMember);
        
        var subscriptionRepositoryMock = Substitute.For<ISubscriptionRepository>();
        
        subscriptionRepositoryMock
            .ReadCoursesByActiveMember(exampleMember.Id)
            .Returns(new List<Guid> { allowedCourse });
        
        var courseRepositoryMock = Substitute.For<ICourseRepository>();
        courseRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .Returns(exampleCourse);

        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.UserRepository = userRepositoryMock;
        unitOfWorkMock.MemberRepository = memberRepositoryMock;
        unitOfWorkMock.SubscriptionRepository = subscriptionRepositoryMock;
        unitOfWorkMock.CourseRepository = courseRepositoryMock;
        
        var loggerMock = Substitute.For<ILogger<GetCourseByIdQueryHandler>>();

        // Arrange
        var command = new GetCourseByIdQuery(requestedCourse, new Guid());
        var commandHandler = new GetCourseByIdQueryHandler(unitOfWorkMock, loggerMock);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Error, Error.None);
        Assert.IsType<Result<CourseDetailedViewModel>>(result);
    }
    
    [Fact]
    public async Task ShouldReturnErrorIfCourseIsNotFound()
    {
        // Setup Fixture
        var exampleUser = new User("joao@gmail.com", "123", EUserRole.Student);
        var exampleMember = new Member("João", "Back", "11154990800", "joao@gmail.com", true);

        var userRepositoryMock = Substitute.For<IUserRepository>();
        userRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .Returns(exampleUser);

        var memberRepositoryMock = Substitute.For<IMemberRepository>();
        memberRepositoryMock
            .ReadByEmail(Arg.Any<string>())
            .Returns(exampleMember);

        var subscriptionRepositoryMock = Substitute.For<ISubscriptionRepository>();
        var allowedCourse = Guid.NewGuid();
        subscriptionRepositoryMock
            .ReadCoursesByActiveMember(exampleMember.Id)
            .Returns(new List<Guid> { allowedCourse });

        var courseRepositoryMock = Substitute.For<ICourseRepository>();
        courseRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .ReturnsNull();
        
        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.UserRepository = userRepositoryMock;
        unitOfWorkMock.MemberRepository = memberRepositoryMock;
        unitOfWorkMock.SubscriptionRepository = subscriptionRepositoryMock;
        unitOfWorkMock.CourseRepository = courseRepositoryMock;

        var loggerMock = Substitute.For<ILogger<GetCourseByIdQueryHandler>>();

        // Arrange
        var command = new GetCourseByIdQuery(allowedCourse, new Guid());
        var commandHandler = new GetCourseByIdQueryHandler(unitOfWorkMock, loggerMock);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Error, CourseErrors.Course.CourseNotFound);
    }

    [Fact]
    public async Task ShouldReturnErrorNotContainAllowedCourseInSubscription()
    {
        // Setup Fixture

        var allowedCourse = Guid.Parse("1016d7d7-1fe4-4cac-a13d-87c24c3be92b");
        var requestedCourse = Guid.Parse("d5d70f09-3e2a-42a1-9878-818296062f76");
        
        var exampleUser = new User("joao@gmail.com", "123", EUserRole.Student);
        var exampleMember = new Member("João", "Back", "11154990800", "joao@gmail.com", true);

        var userRepositoryMock = Substitute.For<IUserRepository>();
        userRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .Returns(exampleUser);

        var memberRepositoryMock = Substitute.For<IMemberRepository>();
        memberRepositoryMock
            .ReadByEmail(Arg.Any<string>())
            .Returns(exampleMember);
        
        var subscriptionRepositoryMock = Substitute.For<ISubscriptionRepository>();
        
        subscriptionRepositoryMock
            .ReadCoursesByActiveMember(exampleMember.Id)
            .Returns(new List<Guid> { allowedCourse });

        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.UserRepository = userRepositoryMock;
        unitOfWorkMock.MemberRepository = memberRepositoryMock;
        unitOfWorkMock.SubscriptionRepository = subscriptionRepositoryMock;

        var loggerMock = Substitute.For<ILogger<GetCourseByIdQueryHandler>>();

        // Arrange
        var command = new GetCourseByIdQuery(requestedCourse, new Guid());
        var commandHandler = new GetCourseByIdQueryHandler(unitOfWorkMock, loggerMock);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Error, MemberErrors.MemberNotGranted);
    }

    [Fact]
    public async Task ShouldReturnErrorWhenMemberHasNoActiveSubscription()
    {
        // Setup Fixture
        var exampleUser = new User("joao@gmail.com", "123", EUserRole.Student);
        var exampleMember = new Member("João", "Back", "11154990800", "joao@gmail.com", true);

        var userRepositoryMock = Substitute.For<IUserRepository>();
        userRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .Returns(exampleUser);

        var memberRepositoryMock = Substitute.For<IMemberRepository>();
        memberRepositoryMock
            .ReadByEmail(Arg.Any<string>())
            .Returns(exampleMember);

        var subscriptionRepositoryMock = Substitute.For<ISubscriptionRepository>();
        subscriptionRepositoryMock
            .ReadCoursesByActiveMember(exampleMember.Id)
            .Returns(new List<Guid>());

        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.UserRepository = userRepositoryMock;
        unitOfWorkMock.MemberRepository = memberRepositoryMock;
        unitOfWorkMock.SubscriptionRepository = subscriptionRepositoryMock;

        var loggerMock = Substitute.For<ILogger<GetCourseByIdQueryHandler>>();

        // Arrange
        var command = new GetCourseByIdQuery(new Guid(), new Guid());
        var commandHandler = new GetCourseByIdQueryHandler(unitOfWorkMock, loggerMock);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Error, MemberErrors.MemberNotGranted);
    }

    [Fact]
    public async Task ShouldReturnErrorWhenMemberNotFound()
    {
        // Setup Fixture
        var exampleUser = new User("joao@gmail.com", "123", EUserRole.Student);
        var userRepositoryMock = Substitute.For<IUserRepository>();

        userRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .Returns(exampleUser);

        var memberRepositoryMock = Substitute.For<IMemberRepository>();
        memberRepositoryMock
            .ReadByEmail(Arg.Any<string>())
            .ReturnsNull();

        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.UserRepository = userRepositoryMock;
        unitOfWorkMock.MemberRepository = memberRepositoryMock;
        var loggerMock = Substitute.For<ILogger<GetCourseByIdQueryHandler>>();

        // Arrange
        var command = new GetCourseByIdQuery(new Guid(), new Guid());
        var commandHandler = new GetCourseByIdQueryHandler(unitOfWorkMock, loggerMock);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Error, MemberErrors.MemberNotFound);
    }

    [Fact]
    public async Task ShouldReturnErrorWhenUserNotFound()
    {
        // Setup Fixture        
        var userRepositoryMock = Substitute.For<IUserRepository>();
        userRepositoryMock
            .ReadById(Arg.Any<Guid>())
            .ReturnsNull();
        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.UserRepository = userRepositoryMock;
        var loggerMock = Substitute.For<ILogger<GetCourseByIdQueryHandler>>();

        // Arrange
        var command = new GetCourseByIdQuery(new Guid(), new Guid());
        var commandHandler = new GetCourseByIdQueryHandler(unitOfWorkMock, loggerMock);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Error, UserErrors.UserNotFound);
    }
}