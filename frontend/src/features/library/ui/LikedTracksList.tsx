import type { LikedTrack } from '../model/types'
import { ShuffleIcon } from '@/shared/ui/icons/ShuffleIcon'
import { HeartIcon } from '@/shared/ui/icons/HeartIcon'
import { TrackPlayToggle } from '@/features/player/ui/TrackPlayToggle'

type Props = {
  title: string
  tracks: LikedTrack[]
}

export function LikedTracksList({ title, tracks }: Props) {
  return (
    <section className="mt-[22px]">
      <div className="flex items-center justify-between">
        <h2 className="text-[24px] leading-[22px] tracking-[-0.02em] text-groov-accent">
          {title}
        </h2>

        <div className="flex items-center gap-[8px]">
          <button
            type="button"
            aria-label="Перемішати"
            className="flex h-[24px] w-[24px] items-center justify-center text-groov-accent transition-transform duration-200 active:scale-[0.92]"
          >
            <ShuffleIcon className="h-[24px] w-[24px]" />
          </button>

         <TrackPlayToggle
  trackId="liked-tracks"
  source={"library-liked" as any}
  variant="circle"
  size="xs"
/>
        </div>
      </div>

      <div className="mt-[12px] space-y-[8px]">
        {tracks.map((track) => (
          <button
            key={track.id}
            type="button"
            className="flex h-[58px] w-full items-center rounded-[12px] bg-groov-surface px-[10px] text-left transition-colors duration-200 active:bg-groov-primary"
          >
            <div className="h-[42px] w-[42px] shrink-0 overflow-hidden rounded-[10px]">
              <img
                src={track.cover}
                alt={track.title}
                className="h-full w-full object-cover"
              />
            </div>

            <div className="ml-[10px] min-w-0 flex-1">
              <div className="truncate text-[14px] leading-[17px]">
                {track.title}
              </div>

              <div className="mt-[2px] truncate text-[12px] leading-[14px]/80">
                {track.artist}
              </div>
            </div>

            <div className="ml-[10px] flex items-center gap-[10px]">
              <span className="text-[15px] leading-[14px]">
                {track.duration}
              </span>

              <span className="flex h-[24px] w-[24px] items-center justify-center">
                <HeartIcon filled className="h-[24px] w-[24px]" />
              </span>
            </div>
          </button>
        ))}
      </div>
    </section>
  )
}