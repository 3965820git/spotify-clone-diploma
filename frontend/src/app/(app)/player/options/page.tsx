import { TrackOptionsScreen } from '@/features/player/ui/TrackOptionsScreen'
import { AppShell } from '@/shared/ui/layout/AppShell'

export default function PlayerOptionsPage() {
  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      withSafeAreaTop
      withSafeAreaBottom
      withBottomNavSpacing={false}
      pageMode="screen"
      mobileTopOffset={50}
    >
      <TrackOptionsScreen />
    </AppShell>
  )
}