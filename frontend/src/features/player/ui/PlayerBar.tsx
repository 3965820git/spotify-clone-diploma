'use client'

import { useRouter } from 'next/navigation'
import { PauseIcon } from '@/shared/ui/icons/PauseIcon'
import { PlayIcon } from '@/shared/ui/icons/PlayIcon'
import { NextIcon } from '@/shared/ui/icons/NextIcon'
import { usePlayerControls } from '@/features/player/lib/usePlayerControls'
import { useCurrentTrackDetails } from '@/features/player/api/useCurrentTrackDetails'

export function PlayerBar() {
  const router = useRouter()

  const {
    currentTrack,
    isPlaying,
    isDisabled,
    canGoNext,
    handleTogglePlay,
    handleNext,
  } = usePlayerControls()

  const { data: currentTrackDetails } = useCurrentTrackDetails(
    currentTrack?.id ?? null
  )

  if (!currentTrack) return null

  const artistLabel =
    currentTrackDetails?.mainArtists?.length
      ? currentTrackDetails.mainArtists.map((a) => a.name).join(', ')
      : 'Unknown artist'

  return (
    <div className="fixed bottom-[75px] left-0 right-0 z-40">
      <div className="flex w-full items-center bg-groov-surface px-[16px] py-[12px]">
        <button
          type="button"
          onClick={() => router.push('/player')}
          className="flex min-w-0 flex-1 items-center gap-[12px] text-left"
        >
          <div className="h-[50px] w-[50px] shrink-0 overflow-hidden rounded-[10px] bg-groov-primary">
            <img
              src="/Weeknd.png"
              alt={currentTrack.title}
              className="h-full w-full object-cover"
            />
          </div>

          <div className="min-w-0">
            <div className="truncate text-[16px] font-medium text-groov-accent">
              {currentTrack.title}
            </div>

            <div className="mt-[4px] truncate text-[14px] text-groov-accent/80">
              {artistLabel}
            </div>
          </div>
        </button>

        {/* CONTROLS */}
        <div className="ml-[12px] flex items-center gap-[18px]">
          <button
            type="button"
            onClick={() => void handleTogglePlay()}
            disabled={isDisabled}
            className={[
              'flex h-[28px] w-[28px] items-center justify-center text-groov-accent',
              isDisabled ? 'opacity-40' : '',
            ].join(' ')}
          >
            {isPlaying ? (
              <PauseIcon className="h-[28px] w-[28px]" />
            ) : (
              <PlayIcon className="h-[28px] w-[28px]" />
            )}
          </button>

          <button
            type="button"
            onClick={() => void handleNext()}
            disabled={isDisabled || !canGoNext}
            className={[
              'flex h-[28px] w-[28px] items-center justify-center text-groov-accent',
              isDisabled || !canGoNext ? 'opacity-40' : '',
            ].join(' ')}
          >
            <NextIcon className="h-[28px] w-[28px]" />
          </button>
        </div>
      </div>
    </div>
  )
}