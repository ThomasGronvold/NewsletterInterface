﻿using System.Text.Json;
using NewsletterSubscription.Interfaces;
using NewsletterSubscription.Model;

namespace NewsletterSubscription.Implementations
{
    internal class SubscriptionFileRepository : ISubscriptionRepository
    {
        public void Save(Subscription subscription)
        {
            var json = JsonSerializer.Serialize(subscription);
            var filename = subscription.Email + "_" + Guid.NewGuid().ToString() + ".json";
            File.WriteAllText(filename, json);
        }

        public Subscription Load(string email)
        {
            var filename = email + ".json";
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<Subscription>(json);
        }
    }
}
