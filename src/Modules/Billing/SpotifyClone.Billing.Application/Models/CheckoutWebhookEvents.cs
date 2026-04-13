namespace SpotifyClone.Billing.Application.Models;

public static class CheckoutWebhookEvents
{
    public const string CheckoutSessionCompleted = "checkout.session.completed";
    public const string InvoicePaid = "invoice.paid";
    public const string InvoicePaymentFailed = "invoice.payment_failed";
    public const string SubscriptionDeleted = "customer.subscription.deleted";
}
