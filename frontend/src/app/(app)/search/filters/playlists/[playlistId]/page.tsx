import { notFound } from 'next/navigation'
import { AppShell } from '@/shared/ui/layout/AppShell'
import { IconButton } from '@/shared/ui/buttons/IconButton'
import { BackButton } from '@/shared/ui/buttons/BackButton'
import { MoreIcon } from '@/shared/ui/icons/MoreIcon'
import { ShareIcon } from '@/shared/ui/icons/ShareIcon'
import { DownloadIcon } from '@/shared/ui/icons/DownloadIcon'
import { ShuffleToggle } from '@/features/player/ui/ShuffleToggle'
import { TrackPlayToggle } from '@/features/player/ui/TrackPlayToggle'
import { getPlaylistWithTracks } from '@/features/playlist/api/getPlaylistWithTracks'

type Props = {
  params: Promise<{ playlistId: string }>
}

function formatTracksCount(count: number) {
  return `${count} пісень`
}

export default async function PlaylistDetailsPage({ params }: Props) {
  const { playlistId } = await params

  let playlist: Awaited<ReturnType<typeof getPlaylistWithTracks>> | null = null

  try {
    playlist = await getPlaylistWithTracks(playlistId)
  } catch {
    playlist = null
  }

  if (!playlist) {
    notFound()
  }

  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      contentClassName="pb-0"
    >
      <div className="flex items-center gap-3 text-groov-accent">
        <BackButton className="flex h-[26px] w-[26px] items-center justify-center text-groov-accent" />

        <h1 className="line-clamp-1 text-[20px] font-semibold tracking-[-0.01em] text-groov-accent">
          {playlist.title}
        </h1>
      </div>

      <div className="px-4 pb-6">
        <div className="flex flex-col items-center pt-5 text-center">
          <div className="flex h-[230px] w-[230px] items-center justify-center overflow-hidden rounded-[18px] bg-groov-surface">
            {playlist.cover ? (
              <img
                src={playlist.cover}
                alt={playlist.title}
                className="h-full w-full object-cover"
              />
            ) : (
              <div className="flex h-full w-full items-center justify-center text-[14px] text-groov-muted">
                No cover
              </div>
            )}
          </div>

          <h2 className="mt-4 text-[20px] font-semibold text-groov-accent">
            {playlist.title}
          </h2>

          <p className="mt-[8px] w-[340px] text-[15px] leading-[20px] text-groov-accent">
            {playlist.description || 'Без опису'}
          </p>

          <p className="mt-[10px] text-[15px] leading-[21px] text-groov-accent">
            Плейлист • {playlist.author}
            <br />
            {playlist.year ? `${playlist.year} • ` : ''}
            {formatTracksCount(playlist.tracksCount)}
          </p>
        </div>

        <div className="mt-[22px] flex items-center justify-between text-groov-accent">
          <div className="flex items-center gap-[10px]">
            <IconButton className="h-[28px] w-[28px]">
              <MoreIcon className="h-[20px] w-[20px]" />
            </IconButton>

            <IconButton className="h-[28px] w-[28px]">
              <ShareIcon className="h-[24px] w-[24px]" />
            </IconButton>

            <IconButton className="h-[28px] w-[28px]">
              <DownloadIcon className="h-[24px] w-[24px]" />
            </IconButton>
          </div>

          <div className="flex items-center gap-[10px]">
            <ShuffleToggle />

            <TrackPlayToggle
              trackId={playlist.tracks[0]?.id ?? ''}
              source="playlist"
              contextExternalId={playlist.id}
              variant="round"
              size="md"
              disabled={!playlist.tracks.length}
            />
          </div>
        </div>

        <div className="mt-[20px] space-y-[12px]">
          {playlist.tracks.map((track) => (
            <div key={track.id} className="flex min-h-[64px] items-center gap-3">
              <div className="flex h-[48px] w-[48px] shrink-0 items-center justify-center overflow-hidden rounded-[8px] bg-groov-surface">
                {track.image ? (
                  <img
                    src={track.image}
                    alt={track.title}
                    className="h-full w-full object-cover"
                  />
                ) : (
                  <div className="text-[11px] text-groov-muted">No image</div>
                )}
              </div>

              <div className="min-w-0 flex-1">
                <p className="truncate text-[17px] text-groov-accent">
                  {track.title}
                </p>
                <p className="truncate text-[14px] text-groov-accent">
                  {track.artist}
                </p>
              </div>

              <TrackPlayToggle
                trackId={track.id}
                source="playlist"
                contextExternalId={playlist.id}
                size="sm"
              />

              <IconButton className="h-[28px] w-[28px]">
                <MoreIcon className="h-[20px] w-[20px]" />
              </IconButton>
            </div>
          ))}
        </div>
      </div>
    </AppShell>
  )
}