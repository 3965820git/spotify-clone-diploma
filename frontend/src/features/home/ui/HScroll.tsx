'use client'

import Link from 'next/link'
import { useRouter } from 'next/navigation'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'

type HScrollItem = {
  id: string | number
  title: string
  subtitle?: string
  image?: string
  contextType?: 'album' | 'playlist'
  contextExternalId?: string | null
}

type Props = {
  items: HScrollItem[]
  hrefPrefix?: string
  variant?: 'default' | 'album'
}

function isUuid(value: string) {
  return /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(
    value
  )
}

export function HScroll({
  items,
  hrefPrefix = '',
  variant = 'default',
}: Props) {
  const router = useRouter()
  const { startPlaybackMutation } = usePlaybackActions()

  const isAlbum = variant === 'album'
  const isTrackList = variant === 'default'

  const handleTrackClick = async (item: HScrollItem) => {
    const trackId = String(item.id)

    if (!isUuid(trackId)) {
      return
    }

    try {
      await startPlaybackMutation.mutateAsync({
        contextType: item.contextType ?? 'album',
        contextExternalId: item.contextExternalId ?? null,
        startTrackId: trackId,
      })

      router.push('/player')
    } catch (error) {
      console.error('Start playback failed', error)
    }
  }

  return (
    <div className="hide-scrollbar -mx-4 flex items-start gap-3 overflow-x-auto px-4 pb-1">
      {items.map((item) => {
        const content = (
          <div className="w-[126px] shrink-0">
            <div className="rounded-[18px] bg-groov-surface p-[10px]">
              <div className="h-[80px] w-full overflow-hidden rounded-[12px] bg-groov-primary">
                {item.image ? (
                  <img
                    src={item.image}
                    alt={item.title}
                    className="h-full w-full object-cover"
                  />
                ) : null}
              </div>

              <div className={isAlbum ? 'h-[52px] pt-[8px]' : 'pt-[8px]'}>
                <div
                  className={
                    isAlbum
                      ? 'line-clamp-2 text-[13px] font-medium leading-[15px] text-groov-accent'
                      : 'line-clamp-1 text-[13px] font-medium leading-[16px] text-groov-accent'
                  }
                >
                  {item.title}
                </div>

                {item.subtitle ? (
                  <div
                    className={
                      isAlbum
                        ? 'mt-[3px] line-clamp-1 text-[11px] leading-[13px] text-groov-muted'
                        : 'mt-[2px] line-clamp-2 text-[11px] leading-[14px] text-groov-muted'
                    }
                  >
                    {item.subtitle}
                  </div>
                ) : null}
              </div>
            </div>
          </div>
        )

        if (isTrackList) {
          return (
            <button
              key={item.id}
              type="button"
              onClick={() => {
                void handleTrackClick(item)
              }}
              className="shrink-0 text-left"
            >
              {content}
            </button>
          )
        }

        return (
          <Link
            key={item.id}
            href={`${hrefPrefix}/${item.id}`}
            className="shrink-0"
          >
            {content}
          </Link>
        )
      })}
    </div>
  )
}