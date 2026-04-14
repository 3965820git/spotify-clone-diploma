import { AppShell } from '@/shared/ui/layout/AppShell'
import { CreatePlaylistStepHeader } from '@/features/library/ui/CreatePlaylistStepHeader'
import { CreatePlaylistVisibilityForm } from '@/features/library/ui/CreatePlaylistVisibilityForm'

export default function CreatePlaylistVisibilityPage() {
  return (
    <AppShell
     withBottomNavSpacing={false}
      withDefaultPadding={false}
      pageMode="screen"  >
      <div className="min-h-dvh bg-groov-bg px-[16px] pt-[10px] text-groov-accent">
        <CreatePlaylistStepHeader
          backHref="/library/create-playlist/tracks"
          stepLabel="Крок 4 з 5"
          currentStep={4}
          totalSteps={5}
        />

        <CreatePlaylistVisibilityForm />
      </div>
    </AppShell>
  )
}