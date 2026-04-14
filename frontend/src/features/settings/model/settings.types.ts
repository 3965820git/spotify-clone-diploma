export type SettingsIconKey =
  | 'edit-profile'
  | 'appearance'
  | 'notifications'
  | 'privacy'
  | 'admin-panel'
  | 'profile'
  | 'help'
  | 'logout'
  | 'delete-account'
  | 'theme-dark'
  | 'theme-light'
  | 'theme-system'

export type SettingsItem = {
  id: string
  title: string
  subtitle: string
  icon: SettingsIconKey
  href?: string
  variant?: 'default' | 'danger'
}

export type ThemeOption = {
  id: 'dark' | 'light' | 'system'
  label: string
  active?: boolean
  icon: SettingsIconKey
}