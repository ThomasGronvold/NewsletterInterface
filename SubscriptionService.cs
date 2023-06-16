using NewsletterSubscription.Implementations;
using NewsletterSubscription.Interfaces;
using NewsletterSubscription.Model;

namespace NewsletterSubscription
{
    public class SubscriptionService
    {
        private readonly IEmailService _emailService;
        private ISubscriptionRepository _subscriptionRepository;
        public string FIrstname;

        public SubscriptionService(
            IEmailService emailService,
            ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _emailService = emailService;
        }

        public void Subscribe(string emailAddress)
        {
            var newUser = new Subscription("timmy", emailAddress);
            _subscriptionRepository.Save(newUser);
            var email = new Email(
                emailAddress,
                emailAddress,
                "Din verifiseringskode.",
                newUser.VerificationCode
                );
            _emailService.Send(email);
            
        }

        public void Verify(string emailAddress, string verificationCode)
        {
            var user = _subscriptionRepository.Load(emailAddress);
            if (user.IsVerified)
            {
                var email = new Email(
                    "admin@admin.com",
                    emailAddress,
                    "Subscription Already Exist",
                    "The email you tried to subscribe with is already subscribed with us."
                );
                _emailService.Send(email);
            }

            user.IsVerified = user.VerificationCode == verificationCode;

            if (user.IsVerified)
            {
                _subscriptionRepository.Save(user);
                var email = new Email(
                    emailAddress,
                    emailAddress,
                    "Spam mail Success",
                    "You have successfully signed up for spam-mail!"
                );
                _emailService.Send(email);
            }


        }
    }
}
