using NewsletterSubscription.Model;

namespace NewsletterSubscription.Interfaces
{
    public interface ISubscriptionRepository
    {
        void Save(Subscription subscription);
        Subscription Load(string email);
    }
}
