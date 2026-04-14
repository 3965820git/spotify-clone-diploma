'use client'

import { useMemo } from 'react'
import { useQuery } from '@tanstack/react-query'
import { useRouter, useSearchParams } from 'next/navigation'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { getTrackById } from '@/features/player/api/getTrackById'
import { PlayNextIcon } from '@/shared/ui/icons/options/PlayNextIcon'
import { AddToQueueIcon } from '@/shared/ui/icons/options/AddToQueueIcon'
import { DownloadTrackIcon } from '@/shared/ui/icons/options/DownloadTrackIcon'
import { SaveToPlaylistIcon } from '@/shared/ui/icons/options/SaveToPlaylistIcon'
import { SaveToLibraryIcon } from '@/shared/ui/icons/options/SaveToLibraryIcon'
import { RemoveFromQueueIcon } from '@/shared/ui/icons/options/RemoveFromQueueIcon'
import { ReportTrackIcon } from '@/shared/ui/icons/options/ReportTrackIcon'
import { ClearQueueIcon } from '@/shared/ui/icons/options/ClearQueueIcon'
import { UserCircleIcon } from '@/shared/ui/icons/options/UserCircleIcon'

type MenuItem = {
  key: string
  label: string
  icon: React.ReactNode
  onClick: () => void | Promise<void>
  danger?: boolean
}

export function TrackOptionsScreen() {
  const router = useRouter()
  const searchParams = useSearchParams()

  const trackIdFromQuery = searchParams.get('trackId')

  const queue = usePlayerStore((state) => state.queue)
  const currentTrackFromPlayer = queue?.currentTrack ?? null
  const nextTracks = queue?.tracksInQueue ?? []

  const { addToQueueMutation, removeFromQueueMutation } = usePlaybackActions()

  const { data: trackFromQuery, isLoading } = useQuery({
    queryKey: ['track-options', trackIdFromQuery],
    queryFn: () => getTrackById(trackIdFromQuery!),
    enabled: Boolean(trackIdFromQuery),
    staleTime: 60_000,
  })

  const currentTrack = trackFromQuery
    ? {
        id: trackFromQuery.id,
        title: trackFromQuery.title,
        mainArtists: [trackFromQuery.artistName],
      }
    : currentTrackFromPlayer

  const artistLabel = useMemo(() => {
    if (trackFromQuery?.artistName?.trim()) {
      return trackFromQuery.artistName
    }

    if (!currentTrack?.mainArtists?.length) {
      return 'Unknown artist'
    }

    return currentTrack.mainArtists.join(', ')
  }, [currentTrack, trackFromQuery])

  const handleDismiss = () => {
    router.back()
  }

  const handlePlayNext = async () => {
    if (!currentTrack?.id) return
    await addToQueueMutation.mutateAsync(currentTrack.id)
  }

  const handleAddToQueue = async () => {
    if (!currentTrack?.id) return
    await addToQueueMutation.mutateAsync(currentTrack.id)
  }

  const handleRemoveFromQueue = async () => {
    if (!currentTrack?.id) return
    await removeFromQueueMutation.mutateAsync(currentTrack.id)
  }

  const handleClearQueue = async () => {
    await Promise.all(
      nextTracks.map((track) => removeFromQueueMutation.mutateAsync(track.id))
    )
  }

  const noop = () => {}

  const menuItems: MenuItem[] = [
    {
      key: 'play-next',
      label: 'Відтворити наступним',
      icon: <PlayNextIcon className="h-[24px] w-[24px]" />,
      onClick: handlePlayNext,
    },
    {
      key: 'go-artist',
      label: 'Перейти на сторінку автора',
      icon: <UserCircleIcon className="h-[24px] w-[24px]" />,
      onClick: noop,
    },
    {
      key: 'add-to-queue',
      label: 'Додати в чергу',
      icon: <AddToQueueIcon className="h-[24px] w-[24px]" />,
      onClick: handleAddToQueue,
    },
    {
      key: 'download',
      label: 'Завантажити',
      icon: <DownloadTrackIcon className="h-[24px] w-[24px]" />,
      onClick: noop,
    },
    {
      key: 'save-to-playlist',
      label: 'Зберегти до плейліста',
      icon: <SaveToPlaylistIcon className="h-[24px] w-[24px]" />,
      onClick: noop,
    },
    {
      key: 'save-to-library',
      label: 'Зберегти до бібліотеки',
      icon: <SaveToLibraryIcon className="h-[24px] w-[24px]" />,
      onClick: noop,
    },
    {
      key: 'remove-from-queue',
      label: 'Вилучити з черги',
      icon: <RemoveFromQueueIcon className="h-[24px] w-[24px]" />,
      onClick: handleRemoveFromQueue,
    },
    {
      key: 'share',
      label: 'Поділитися',
      icon: <ShareSimpleIcon className="h-[24px] w-[24px]" />,
      onClick: noop,
    },
    {
      key: 'report',
      label: 'Поскаржитися',
      icon: <ReportTrackIcon className="h-[24px] w-[24px]" />,
      onClick: noop,
    },
    {
      key: 'clear-queue',
      label: 'Відхилити чергу',
      icon: <ClearQueueIcon className="h-[24px] w-[24px]" />,
      onClick: handleClearQueue,
      danger: true,
    },
  ]

  if (isLoading) {
    return (
      <div className="flex h-full items-center justify-center bg-groov-bg px-[24px] text-center text-groov-accent">
        Завантаження...
      </div>
    )
  }

  if (!currentTrack) {
    return (
      <div className="flex h-full items-center justify-center bg-groov-bg px-[24px] text-center text-groov-accent">
        Нічого не відтворюється
      </div>
    )
  }

  return (
    <div className="flex h-full flex-col overflow-hidden bg-groov-bg text-groov-accent">
      <div className="flex items-center justify-between px-[24px] pb-[18px] pt-[8px]">
        <div className="w-[56px]" />
        <button
          type="button"
          onClick={handleDismiss}
          className="text-[16px] font-semibold leading-none text-groov-accent"
        >
          Готово
        </button>
      </div>

      <div className="flex-1 overflow-y-auto px-[24px]">
        <div className="flex flex-col items-center">
          <div className="h-[96px] w-[96px] overflow-hidden rounded-[22px] bg-groov-surface">
            {trackFromQuery?.coverUrl ? (
              <img
                src={trackFromQuery.coverUrl}
                alt={currentTrack.title}
                className="h-full w-full object-cover"
              />
            ) : (
              <div className="flex h-full w-full items-center justify-center text-[13px] text-groov-muted">
                No cover
              </div>
            )}
          </div>

          <div className="mt-[10px] text-center text-[18px] font-medium leading-[120%] text-groov-accent">
            {currentTrack.title}
          </div>

          <div className="mt-[6px] text-center text-[14px] font-normal leading-[120%] text-groov-muted">
            {artistLabel}
          </div>
        </div>

        <div className="mt-[20px]">
          {menuItems.map((item, index) => (
            <div key={item.key}>
              <button
                type="button"
                onClick={() => {
                  void item.onClick()
                }}
                className="flex w-full items-center gap-[14px] py-[14px] text-left text-groov-accent"
              >
                <span className="flex h-[24px] w-[24px] shrink-0 items-center justify-center">
                  {item.icon}
                </span>

                <span className="text-[16px] font-normal leading-[120%]">
                  {item.label}
                </span>
              </button>

              {index !== menuItems.length - 1 ? (
                <div className="h-px w-full bg-groov-accent/55" />
              ) : null}
            </div>
          ))}
        </div>
      </div>

      <div className="shrink-0 px-[24px] pb-[20px] pt-[16px]">
        <button
          type="button"
          onClick={handleDismiss}
          className="h-[40px] w-full rounded-[14px] bg-groov-primary text-[16px] font-semibold leading-none text-groov-accent"
        >
          Відхилити
        </button>
      </div>
    </div>
  )
}

function ShareSimpleIcon({ className = '' }: { className?: string }) {
  return (
    <svg
      className={className}
      viewBox="0 0 24 24"
      fill="none"
      aria-hidden="true"
    >
      <path
        d="M8.59 13.51L15.42 17.49"
        stroke="currentColor"
        strokeWidth="1.8"
        strokeLinecap="round"
      />
      <path
        d="M15.41 6.51L8.59 10.49"
        stroke="currentColor"
        strokeWidth="1.8"
        strokeLinecap="round"
      />
      <path
        d="M18 8.75C19.5188 8.75 20.75 7.51878 20.75 6C20.75 4.48122 19.5188 3.25 18 3.25C16.4812 3.25 15.25 4.48122 15.25 6C15.25 7.51878 16.4812 8.75 18 8.75Z"
        stroke="currentColor"
        strokeWidth="1.8"
      />
      <path
        d="M6 13.75C7.51878 13.75 8.75 12.5188 8.75 11C8.75 9.48122 7.51878 8.25 6 8.25C4.48122 8.25 3.25 9.48122 3.25 11C3.25 12.5188 4.48122 13.75 6 13.75Z"
        stroke="currentColor"
        strokeWidth="1.8"
      />
      <path
        d="M18 20.75C19.5188 20.75 20.75 19.5188 20.75 18C20.75 16.4812 19.5188 15.25 18 15.25C16.4812 15.25 15.25 16.4812 15.25 18C15.25 19.5188 19.5188 20.75 18 20.75Z"
        stroke="currentColor"
        strokeWidth="1.8"
      />
    </svg>
  )
}