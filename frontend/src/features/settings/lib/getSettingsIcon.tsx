import { EditProfileIcon } from '@/shared/ui/icons/settings/EditProfileIcon'
import { AppearanceIcon } from '@/shared/ui/icons/settings/AppearanceIcon'
import { NotificationsIcon } from '@/shared/ui/icons/settings/NotificationsIcon'
import { PrivacyIcon } from '@/shared/ui/icons/settings/PrivacyIcon'
import { AdminPanelIcon } from '@/shared/ui/icons/settings/AdminPanelIcon'
import { ProfileIcon } from '@/shared/ui/icons/settings/ProfileIcon'
import { HelpIcon } from '@/shared/ui/icons/settings/HelpIcon'
import { LogoutIcon } from '@/shared/ui/icons/settings/LogoutIcon'
import { DeleteAccountIcon } from '@/shared/ui/icons/settings/DeleteAccountIcon'
import { ThemeDarkIcon } from '@/shared/ui/icons/settings/ThemeDarkIcon'
import { ThemeLightIcon } from '@/shared/ui/icons/settings/ThemeLightIcon'
import { ThemeSystemIcon } from '@/shared/ui/icons/settings/ThemeSystemIcon'
import type { SettingsIconKey } from '@/features/settings/model/settings.types'

export function getSettingsIcon(icon: SettingsIconKey) {
  switch (icon) {
    case 'edit-profile':
      return EditProfileIcon
    case 'appearance':
      return AppearanceIcon
    case 'notifications':
      return NotificationsIcon
    case 'privacy':
      return PrivacyIcon
    case 'admin-panel':
      return AdminPanelIcon
    case 'profile':
      return ProfileIcon
    case 'help':
      return HelpIcon
    case 'logout':
      return LogoutIcon
    case 'delete-account':
      return DeleteAccountIcon
    case 'theme-dark':
      return ThemeDarkIcon
    case 'theme-light':
      return ThemeLightIcon
    case 'theme-system':
      return ThemeSystemIcon
    default:
      return ProfileIcon
  }
}