import type { SettingsItem, ThemeOption } from './settings.types'

export const settingsItems: SettingsItem[] = [
  {
    id: 'edit-profile',
    title: 'Редагувати профіль',
    subtitle: "Змінити ім'я, біографію та іншу інформацію",
    icon: 'edit-profile',
  },
  {
    id: 'appearance',
    title: 'Зовнішній вигляд',
    subtitle: 'Тема, кольори',
    icon: 'appearance',
  },
  {
    id: 'notifications',
    title: 'Сповіщення',
    subtitle: 'Керування сповіщеннями',
    icon: 'notifications',
  },
  {
    id: 'privacy',
    title: 'Приватність',
    subtitle: 'Налаштування конфіденційності',
    icon: 'privacy',
  },
  {
    id: 'admin-panel',
    title: 'Адмін-панель',
    subtitle: 'Керування користувачами та контентом',
    icon: 'admin-panel',
    href: '/admin',
  },
  {
    id: 'account',
    title: 'Обліковий запис',
    subtitle: 'Пароль, безпека, підписка',
    icon: 'profile',
  },
  {
    id: 'help',
    title: 'Допомога',
    subtitle: 'FAQ, підтримка, контакти',
    icon: 'help',
  },
]

export const themeOptions: ThemeOption[] = [
  {
    id: 'dark',
    label: 'Темна',
    active: true,
    icon: 'theme-dark',
  },
  {
    id: 'light',
    label: 'Світла',
    icon: 'theme-light',
  },
  {
    id: 'system',
    label: 'Як у системі',
    icon: 'theme-system',
  },
]

export const dangerItems: SettingsItem[] = [
  {
    id: 'logout',
    title: 'Вихід з аккаунту',
    subtitle: 'Вихід з поточного аккаунту',
    icon: 'logout',
    variant: 'danger',
  },
  {
    id: 'delete-account',
    title: 'Видалення аккаунту',
    subtitle: 'Остаточне видалення аккаунту та всіх даних',
    icon: 'delete-account',
    variant: 'danger',
  },
]