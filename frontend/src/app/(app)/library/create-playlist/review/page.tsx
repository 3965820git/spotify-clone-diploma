import { AppShell } from '@/shared/ui/layout/AppShell'
import { CreatePlaylistStepHeader } from '@/features/library/ui/CreatePlaylistStepHeader'
import { CreatePlaylistReviewForm } from '@/features/library/ui/CreatePlaylistReviewForm'

export default function CreatePlaylistReviewPage() {
  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      withBottomNavSpacing={false}
      contentClassName="pb-0"
    >
      <div className="min-h-dvh bg-groov-bg px-[16px] pt-[10px] text-groov-accent">
        <CreatePlaylistStepHeader
          backHref="/library/create-playlist/visibility"
          stepLabel="Крок 5 з 5"
          currentStep={5}
          totalSteps={5}
        />

        <CreatePlaylistReviewForm />
      </div>
    </AppShell>
  )
}