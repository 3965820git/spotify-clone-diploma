import Link from 'next/link'
import { MyPlaylistCard } from '@/shared/ui/cards/MyPlaylistCard'
import type { MyPlaylist } from '../model/types'

type Props = {
  title: string
  items: MyPlaylist[]
}

export function MyPlaylistsSection({ title, items }: Props) {
  return (
    <section className="mt-[22px] pb-[12px]">
      <div className="flex items-center justify-between">
        <h2 className="text-[18px] font-semibold leading-[22px] tracking-[-0.02em] text-groov-accent">
          {title}
        </h2>

        <Link
          href="/library/create-playlist"
          className="text-[16px] leading-[19px] text-groov-accent transition-opacity active:opacity-70"
        >
          + Створити
        </Link>
      </div>

      <div className="mt-[14px] grid grid-cols-2 gap-[10px]">
        {items.map((item) => (
          <MyPlaylistCard
            key={item.id}
            title={item.title}
            author={item.author}
            tracksCount={item.tracksCount}
            cover={item.cover}
            accentColor={item.accentColor}
          />
        ))}
      </div>
    </section>
  )
}