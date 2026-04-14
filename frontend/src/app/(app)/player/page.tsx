import { TrackPlayerScreen } from '@/features/player/ui/TrackPlayerScreen'
import { AppShell } from '@/shared/ui/layout/AppShell'

export default function PlayerPage() {
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
        <TrackPlayerScreen/>
        </AppShell>
  
  )}