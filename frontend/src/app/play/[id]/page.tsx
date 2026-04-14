import { notFound } from 'next/navigation'
import { TrackPlayerScreen } from '@/features/player/ui/TrackPlayerScreen'
import { recently } from '@/features/home/mock'
import { AppShell } from '@/shared/ui/layout/AppShell'


type Props = {
  params: Promise<{ id: string }>
}

export default async function TrackPlayPage({ params }: Props) {
  const { id } = await params

  const track = recently.find((item) => item.id === id)

  if (!track) {
    notFound()
  }

  return (
    <AppShell
      pageMode="screen"
  withDefaultPadding={false}

>
    <TrackPlayerScreen
      }}
    />
    </AppShell>
  )
}