namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Enums;

public enum SubscriptionStatus
{
    Pending = 0,            // Створена, але ще не оплачена
    Active = 1,             // Оплачена і діє
    PastDue = 2,            // Проблема з оплатою (наприклад, немає грошей на картці)
    Canceled = 3,           // Користувач скасував, але вона ще може діяти до кінця оплаченого періоду
    Expired = 4             // Час вийшов, підписка неактивна
}
