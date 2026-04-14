import { PlaylistCard } from '@/shared/ui/cards/PlaylistCard'
import type { RecentPlaylist } from '../model/types'

type Props = {
  items: RecentPlaylist[]
}

export function RecentPlaylists({ items }: Props) {
  return (
    <section className="mt-[24px] pb-[12px]">
      <h2 className="text-[24px] leading-[22px] tracking-[-0.02em] text-groov-accent">
        Недавно додані плейлісти
      </h2>

      <div className="mt-[14px] grid grid-cols-2 gap-[10px]">
        {items.map((item) => (
          <PlaylistCard
            key={item.id}
            href={
              item.isMoreCard
                ? undefined
                : `/search/filters/playlists/${item.id}`
            }
            title={item.title}
            author={item.author}
            tracksCount={item.tracksCount}
            cover={item.cover}
            isMoreCard={item.isMoreCard}
          />
        ))}
      </div>
    </section>
  )
}