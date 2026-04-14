'use client'

import { useQuery } from '@tanstack/react-query'
import { useRouter } from 'next/navigation'
import { getTrackById } from '@/features/player/api/getTrackById'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import { MoreIcon } from '@/shared/ui/icons/MoreIcon'
import type { AdminTrackItem } from '@/features/admin/api/admin.types'

type Props = {
  tracks: AdminTrackItem[]
}

type TrackRowProps = {
  track: AdminTrackItem
  currentTrackId: string | null
  isStartingPlayback: boolean
  onOpenTrack: (track: AdminTrackItem) => Promise<void>
}

function formatDuration(value?: string | null) {
  if (!value) {
    return '00:00'
  }

  const parts = value.split(':')

  if (parts.length === 3) {
    const hours = Number(parts[0])
    const minutes = Number(parts[1])
    const seconds = Number(parts[2].split('.')[0])

    if (hours > 0) {
      return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`
    }

    return `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`
  }

  return value
}

function AdminTrackRow({
  track,
  currentTrackId,
  isStartingPlayback,
  onOpenTrack,
}: TrackRowProps) {
  const router = useRouter()
  const isPublished = Boolean(track.audioFileId)
  const isCurrent = currentTrackId === track.id

  const { data } = useQuery({
    queryKey: ['admin-track-details', track.id],
    queryFn: () => getTrackById(track.id),
    staleTime: 60_000,
  })

  const artistName = data?.artistName?.trim() || 'Unknown artist'

  return (
    <div
      role={isPublished ? 'button' : undefined}
      tabIndex={isPublished ? 0 : -1}
      onClick={() => {
        void onOpenTrack(track)
      }}
      onKeyDown={(e) => {
        if (!isPublished) return

        if (e.key === 'Enter' || e.key === ' ') {
          e.preventDefault()
          void onOpenTrack(track)
        }
      }}
      className={[
        'flex min-h-[98px] items-center rounded-[24px] bg-groov-surface px-[14px] py-[14px]',
        isPublished ? 'cursor-pointer transition-opacity hover:opacity-90' : '',
      ].join(' ')}
    >
      <div className="h-[60px] w-[60px] shrink-0 overflow-hidden rounded-[18px] bg-groov-primary/30">
        <div className="flex h-full w-full items-center justify-center text-[11px] text-groov-accent/55">
          ♪
        </div>
      </div>

      <div className="ml-[14px] min-w-0 flex-1">
        <p className="truncate text-[17px] font-medium leading-[21px] text-groov-accent">
          {track.title}
        </p>

        <p className="mt-[4px] truncate text-[14px] leading-[17px] text-groov-accent/90">
          {artistName}
        </p>

        <p
          className={[
            'mt-[6px] text-[13px] leading-[16px]',
            isPublished ? 'text-green-400' : 'text-groov-accent/65',
          ].join(' ')}
        >
          {isPublished ? 'Опубліковано' : 'На модерації'}
        </p>

        {isCurrent && isPublished ? (
          <p className="mt-[4px] text-[11px] leading-[13px] text-groov-accent/55">
            Зараз грає
          </p>
        ) : null}
      </div>

      <div className="ml-[12px] flex shrink-0 items-center gap-[14px]">
        <span className="text-[15px] font-medium leading-[21px] text-groov-accent">
          {formatDuration(track.duration)}
        </span>

        <button
          type="button"
          onClick={(e) => {
            e.stopPropagation()
            router.push(`/player/options?trackId=${track.id}`)
          }}
          className="flex h-[30px] w-[30px] items-center justify-center text-groov-accent/90"
          aria-label="Опції треку"
          disabled={isStartingPlayback}
        >
          <MoreIcon />
        </button>
      </div>
    </div>
  )
}

export function AdminTracksList({ tracks }: Props) {
  const router = useRouter()
  const { startPlaybackMutation } = usePlaybackActions()
  const currentTrackId = usePlayerStore((state) => state.currentTrackId)

  const handleOpenTrack = async (track: AdminTrackItem) => {
    const isPublished = Boolean(track.audioFileId)
    if (!isPublished) return

    if (currentTrackId !== track.id) {
      await startPlaybackMutation.mutateAsync({
        contextType: track.albumId ? 'album' : 'search',
        contextExternalId: track.albumId,
        startTrackId: track.id,
      })
    }

    router.push('/player')
  }

  return (
    <div className="mt-[16px] space-y-[10px]">
      {tracks.map((track) => (
        <AdminTrackRow
          key={track.id}
          track={track}
          currentTrackId={currentTrackId}
          isStartingPlayback={startPlaybackMutation.isPending}
          onOpenTrack={handleOpenTrack}
        />
      ))}
    </div>
  )
}