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
      track={{
        id: track.id,
        title: track.title,
        artistName: track.subtitle ?? 'Unknown artist',
        durationSeconds: 160,
        containsExplicitContent: false,
        coverUrl: track.image ?? null,
        audioUrl: null,
        albumId: null,
        audioFileId: null,
      }}
    />
    </AppShell>
  )
}