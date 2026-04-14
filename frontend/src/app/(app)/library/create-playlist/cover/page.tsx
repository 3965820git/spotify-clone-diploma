import { AppShell } from '@/shared/ui/layout/AppShell'
import { CreatePlaylistCoverForm } from '@/features/library/ui/CreatePlaylistCoverForm'
import { CreatePlaylistStepHeader } from '@/features/library/ui/CreatePlaylistStepHeader'

export default function CreatePlaylistCoverPage() {
  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      withBottomNavSpacing={false}
      contentClassName="pb-0"
    >
      <div className="min-h-dvh bg-groov-bg px-[16px] pt-[10px] text-groov-accent">
        <CreatePlaylistStepHeader
          backHref="/library/create-playlist"
          stepLabel="Крок 2 з 5"
          currentStep={2}
          totalSteps={5}
        />

        <CreatePlaylistCoverForm />
      </div>
    </AppShell>
  )
}