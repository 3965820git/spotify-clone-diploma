import { AppShell } from '@/shared/ui/layout/AppShell'
import { CreatePlaylistStepHeader } from '@/features/library/ui/CreatePlaylistStepHeader'
import { CreatePlaylistTracksForm } from '@/features/library/ui/CreatePlaylistTracksForm'

export default function CreatePlaylistTracksPage() {
  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      withBottomNavSpacing={false}
      contentClassName="pb-0"
    >
      <div className="min-h-dvh bg-groov-bg px-[16px] pt-[10px] text-groov-accent">
        <CreatePlaylistStepHeader
          backHref="/library/create-playlist/cover"
          stepLabel="Крок 3 з 5"
          currentStep={3}
          totalSteps={5}
        />

        <CreatePlaylistTracksForm />
      </div>
    </AppShell>
  )
}