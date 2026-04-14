export type PremiumPlan = {
  id: string
  badge: string
  title: string
  price: string
  oldPrice?: string
  features: string[]
  buttonText: string
  description: string
}

export const premiumPlans: PremiumPlan[] = [
  {
    id: 'standard',
    badge: 'Premium',
    title: 'Standart',
    price: '0₴ за 1 місяць',
    oldPrice: 'Потім 199,99₴ на місяць',
    features: [
      '1 акаунт Premium',
      'Можна скасувати будь-коли',
    ],
    buttonText: 'Спробувати 1 місяць за 0₴',
    description:
      '0₴ за 1 місяць, потім 199,99₴ на місяць. Пропозиція доступна лише для тих, хто не користувався підпискою Premium і оформили її через GROOV. Пропозиції в App Store можуть відрізнятися.',
  },
  {
    id: 'student',
    badge: 'Premium',
    title: 'Student',
    price: '0₴ за 1 місяць',
    oldPrice: 'Потім 99,99₴ на місяць',
    features: [
      'Для студентів які відповідають умовам',
      'Можна скасувати будь-коли',
    ],
    buttonText: 'Спробувати 1 місяць за 0₴',
    description:
      '0₴ за 1 місяць, потім 99,99₴ на місяць. Пропозиція доступна для студентів, що навчаються в акредитованих закладах вищої освіти, які відповідають умовам.',
  },
  {
    id: 'duo',
    badge: 'Premium',
    title: 'Duo',
    price: '279,99₴ за 1 місяць',
    features: [
      '2 акаунт Premium',
      'Можна скасувати будь-коли',
    ],
    buttonText: 'Отримуйте Premium Duo',
    description:
      'Для двох користувачів, які проживають за однією адресою.',
  },
  {
    id: 'family',
    badge: 'Premium',
    title: 'Family',
    price: '338,99₴ за 1 місяць',
    features: [
      'До 6 акаунтів Premium',
      'Батьківський контроль для менеджера підписки',
      'Можна скасувати будь-коли',
    ],
    buttonText: 'Отримуйте Premium Family',
    description:
      'Для 6 членів сім’ї, які проживають за однією адресою.',
  },
]