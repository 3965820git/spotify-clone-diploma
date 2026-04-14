'use client'

import { CloseIcon } from './icons'
import type { SearchItem } from '../model/search.types'

type Props = {
  item: SearchItem
  onRemove: (id: string) => void
}

export function SearchResultItem({ item, onRemove }: Props) {
  return (
    <div className="flex items-center gap-3 rounded-[16px] bg-groov-surface px-3 py-2">
      <div className="h-[44px] w-[44px] shrink-0 overflow-hidden rounded-[12px] bg-groov-primary">
        <img
          src={item.image}
          alt={item.title}
          className="h-full w-full object-cover"
        />
      </div>

      <div className="min-w-0 flex-1">
        <div className="truncate text-[15px] font-medium leading-[18px] text-groov-accent">
          {item.title}
        </div>
        <div className="mt-[2px] truncate text-[12px] leading-[15px] text-groov-muted">
          {item.subtitle}
        </div>
      </div>

      <button
        type="button"
        onClick={() => onRemove(item.id)}
        className="shrink-0 text-groov-accent/90"
      >
        <CloseIcon />
      </button>
    </div>
  )
}