'use client'

import Link from 'next/link'

type GridCardItem = {
  id: string | number
  title: string
  subtitle?: string
  image?: string
}

type Props = {
  items: GridCardItem[]
  hrefPrefix: string
}

export function GridCards({ items, hrefPrefix }: Props) {
  return (
    <div className="grid grid-cols-2 gap-3">
      {items.map((item) => (
        <Link
          key={item.id}
          href={`${hrefPrefix}/${item.id}`}
          className="overflow-hidden rounded-[16px] bg-groov-surface"
        >
          <div className="h-[92px] w-full bg-groov-primary">
            {item.image ? (
              <img
                src={item.image}
                alt={item.title}
                className="h-full w-full object-cover"
              />
            ) : null}
          </div>

          <div className="bg-groov-surface px-3 pb-3 pt-2">
            <div className="line-clamp-1 text-[13px] font-medium leading-[16px] text-groov-accent">
              {item.title}
            </div>

            {item.subtitle ? (
              <div className="mt-[3px] line-clamp-2 text-[11px] leading-[14px] text-groov-accent/85">
                {item.subtitle}
              </div>
            ) : null}
          </div>
        </Link>
      ))}
    </div>
  )
}