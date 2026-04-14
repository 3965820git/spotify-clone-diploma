import { ArtistCard } from '@/shared/ui/cards/ArtistCard'
import type { FollowedArtist } from '../model/types'

type Props = {
  title: string
  artists: FollowedArtist[]
}

export function FollowedArtists({ title, artists }: Props) {
  return (
    <section className="mt-[22px] pb-[12px]">
      <h2 className="text-[24px] leading-[22px] tracking-[-0.02em] text-groov-accent">
        {title}
      </h2>

      <div className="mt-[14px] grid grid-cols-3 gap-x-[9px] gap-y-[16px]">
        {artists.map((artist) => (
          <ArtistCard
            key={artist.id}
            name={artist.name}
            followers={artist.followers}
            image={artist.image}
            isFollowing={artist.isFollowing}
          />
        ))}
      </div>
    </section>
  )
}