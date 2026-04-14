import { AppShell } from '@/shared/ui/layout/AppShell'
import { ProfileScreen } from '@/features/profile/ui/ProfileScreen'

export default function ProfilePage() {
  return (
    <AppShell
      withSafeAreaTop={false}
      withSafeAreaBottom
      withBottomNavSpacing
      withDefaultPadding={false}
    >
      <ProfileScreen />
    </AppShell>
  )
}