'use client'

import type { SearchItem } from '../model/search.types'
import { SearchResultItem } from './SearchResultItem'

type Props = {
  items: SearchItem[]
  onClear: () => void
  onRemove: (id: string) => void
  isSearching: boolean
}

export function SearchResults({
  items,
  onClear,
  onRemove,
  isSearching,
}: Props) {
  return (
    <>
      <div className="mt-[14px] flex items-center justify-between">
        <h2 className="text-[20px] font-semibold leading-[24px] text-groov-accent">
          {isSearching ? 'Результати пошуку' : 'Нещодавно переглянуте'}
        </h2>

        {!isSearching ? (
          <button
            type="button"
            onClick={onClear}
            className="text-[14px] leading-[18px] text-groov-accent/85"
          >
            Очистити
          </button>
        ) : null}
      </div>

      <div className="mt-[14px] space-y-3 pb-[100px]">
        {items.map((item) => (
          <SearchResultItem key={item.id} item={item} onRemove={onRemove} />
        ))}

        {items.length === 0 ? (
          <div className="rounded-[16px] bg-groov-surface px-4 py-5 text-[14px] text-groov-muted">
            Нічого не знайдено
          </div>
        ) : null}
      </div>
    </>
  )
}