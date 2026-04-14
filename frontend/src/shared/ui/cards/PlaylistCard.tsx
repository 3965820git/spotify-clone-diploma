import Link from 'next/link'
import { ArrowIcon } from '@/shared/ui/icons/ArrowIcon'

type Props = {
  href?: string
  title: string
  author: string
  tracksCount: number
  cover?: string
  isMoreCard?: boolean
}


function CardContent({
  title,
  author,
  tracksCount,
  cover,
  isMoreCard,
}: Omit<Props, 'href'>) {
  if (isMoreCard) {
    return (
      <div className="group flex h-full w-full flex-col items-center justify-center rounded-[14px] bg-groov-surface transition-all duration-300 active:scale-[0.96]">

<ArrowIcon className="h-[54px] w-[54px] text-groov-accent transition-transform duration-300 group-active:translate-x-[4px]" />        <span className="mt-[20px] text-[16px] leading-[19px] text-groov-accent">
          Більше
        </span>
      </div>
    )
  }

  return (
    <div className="flex h-full w-full flex-col overflow-hidden rounded-[14px] bg-groov-surface transition-all duration-300 group-active:scale-[0.98]">
      <div className="h-[130px] w-full shrink-0 overflow-hidden">
        <img
          src={cover}
          alt={title}
          className="h-full w-full object-cover"
        />
      </div>

      <div className="flex h-[70px] flex-col justify-start px-[12px] pt-[10px]">
        <div className="truncate text-[14px] leading-[17px] text-groov-accent">
          {title}
        </div>

        <div className="mt-[6px] text-[12px] uppercase leading-[14px] text-groov-primary">
          {author}
        </div>

        <div className="mt-[2px] text-[12px] leading-[14px] text-groov-primary">
          {tracksCount} треків
        </div>
      </div>
    </div>
  )
}

export function PlaylistCard(props: Props) {
  const { href } = props

  if (!href || props.isMoreCard) {
    return (
      <button
        type="button"
        className="group block h-[200px] w-full text-left"
      >
        <CardContent {...props} />
      </button>
    )
  }

  return (
    <Link href={href} className="group block h-[200px] w-full">
      <CardContent {...props} />
    </Link>
  )
}