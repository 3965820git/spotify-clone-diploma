import type { SavedCollection } from '../model/types'
import { PlaylistIcon } from '@/shared/ui/icons/PlaylistIcon'
import { AlbumIcon } from '@/shared/ui/icons/AlbumIcon'
import { PodcastIcon } from '@/shared/ui/icons/PodcastIcon'
import { DownloadLibIcon } from '@/shared/ui/icons/DownloadLibIcon'

type Props = {
  items: SavedCollection[]
}

const collectionIcons = {
  playlist: PlaylistIcon,
  album: AlbumIcon,
  podcast: PodcastIcon,
  download: DownloadLibIcon,
} as const

function CollectionIcon({ icon }: { icon: SavedCollection['icon'] }) {
  const Icon = collectionIcons[icon] ?? PlaylistIcon

  return (
    <div className="flex h-[44px] w-[44px] items-center justify-center">
      <Icon className="h-[44px] w-[44px] transition-colors duration-300 group-active:text-groov-textDark" />
    </div>
  )
}

export function SavedCollections({ items }: Props) {
  return (
    <section className="mt-[22px]">
      <h2 className="text-[24px] leading-[22px] tracking-[-0.02em]">
        Збережена музика
      </h2>

      <div className="mt-[14px] flex gap-[8px] overflow-x-auto pb-[2px] pr-[10px] scrollbar-none">
        {items.map((item) => (
          <button
            key={item.id}
            type="button"
            className="
              group
              flex h-[140px] min-w-[136px] shrink-0 flex-col justify-between
              rounded-[10px] bg-groov-primary px-[10px] py-[14px]
              transition-[background-color,transform,box-shadow] duration-300 ease-in-out
              active:scale-[0.97] active:bg-groov-accent
            "
          >
            <div className="mt-[6px] flex justify-center">
              <CollectionIcon icon={item.icon} />
            </div>

            <div className="flex flex-col items-center pb-[2px]">
              <div className="text-[20px] leading-[24px] transition-colors duration-300 group-active:text-groov-textDark">
                {item.title}
              </div>

              <div className="mt-[4px] text-[14px] leading-[17px]/85 transition-colors duration-300 group-active:text-groov-textDark/80">
                {item.subtitle}
              </div>
            </div>
          </button>
        ))}
      </div>
    </section>
  )
}