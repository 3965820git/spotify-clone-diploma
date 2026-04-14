import { AppShell } from '@/shared/ui/layout/AppShell'
import { CreatePlaylistStepHeader } from '@/features/library/ui/CreatePlaylistStepHeader'
import { CreatePlaylistForm } from '@/features/library/ui/CreatePlaylistForm'


export default function CreatePlaylistPage() {
  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      withBottomNavSpacing={false}
      contentClassName="pb-0"
      pageMode="screen"
    >
      <div className="min-h-dvh bg-groov-bg px-[16px] pt-[10px] text-groov-accent">
<CreatePlaylistStepHeader
  backHref="/library"
  stepLabel="Крок 1 з 5"
  currentStep={1}
  totalSteps={5}
/>        <CreatePlaylistForm />
      </div>
    </AppShell>
  )
}