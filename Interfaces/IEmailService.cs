using NewsletterSubscription.Model;

namespace NewsletterSubscription.Interfaces
{
    public interface IEmailService
    {
        void Send(Email email);
    }
}
