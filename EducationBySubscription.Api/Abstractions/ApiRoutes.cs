namespace EducationBySubscription.Api.Abstractions;

internal static class ApiRoutes
{
    internal static class Subscription
    {
        public const string BaseSubscription = "subscriptions";
        public const string BaseSubscriptionWithId = "subscriptions/{id:guid}";
    }

    internal static class Payment
    {
        public const string BasePayment = "payments";
        public const string BasePaymentConfirm = "payments/confirm";
    }
    
    internal static class User
    {
        public const string BaseUser = "users";
        public const string BaseUserLogin = "users/login";
        public const string BaseUserWithId = "users/{id:guid}";
    }
    
    internal static class Member
    {
        public const string BaseMember = "members";
        public const string BaseMemberWithId = "members/{id:guid}";
    }
    
    internal static class Course
    {
        public const string BaseCourse = "courses";
        public const string BaseCourseLessonWithId = "courses/{id:guid}/lessons";
        public const string BaseCourseWithId = "courses/{id:guid}";
        public const string BaseCourseWithIdCover = "courses/{id:guid}/cover";
    }

    internal static class Plan
    {
        public const string BasePlan = "plans";
        public const string BasePlanWithId = "plans/{id:guid}";
    }
}