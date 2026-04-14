'use client'

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import type { FilterType } from '../model/search.types'

const filters = [
  { key: 'tracks', label: 'Треки' },
  { key: 'artists', label: 'Виконавці' },
  { key: 'albums', label: 'Альбоми' },
] as const

type Props = {
  activeFilters: FilterType[]
  onToggleFilter: (filter: FilterType) => void
  onReset: () => void
}

export function SearchFilters({
  activeFilters,
  onToggleFilter,
  onReset,
}: Props) {
  const pathname = usePathname()
  const isAdvancedActive = pathname === '/search/filters'

  return (
    <>
      <div className="mt-[22px] flex items-center justify-between">
        <h2 className="text-[20px] font-semibold leading-[24px] text-groov-accent">
          Фільтри
        </h2>

        <button
          type="button"
          onClick={onReset}
          className="text-[14px] leading-[18px] text-groov-accent/85"
        >
          Скинути всі
        </button>
      </div>

      <div className="hide-scrollbar mt-[14px] flex gap-2 overflow-x-auto pb-1">
        <Link
          href="/search/filters"
          className={`shrink-0 rounded-[10px] px-4 py-[7px] text-[14px] leading-[18px] ${
            isAdvancedActive
              ? 'bg-groov-primary text-groov-accent'
              : 'bg-groov-secondary text-groov-accent'
          }`}
        >
          Розширені фільтри
        </Link>

        <button
          type="button"
          onClick={onReset}
          className={`shrink-0 rounded-[10px] px-4 py-[7px] text-[14px] leading-[18px] ${
            activeFilters.length === 0
              ? 'bg-groov-accent text-groov-textDark'
              : 'bg-groov-primary text-groov-accent'
          }`}
        >
          Всі
        </button>

        {filters.map((filter) => {
          const isActive = activeFilters.includes(filter.key)

          return (
            <button
              key={filter.key}
              type="button"
              onClick={() => onToggleFilter(filter.key)}
              className={`shrink-0 rounded-[10px] px-4 py-[7px] text-[14px] leading-[18px] ${
                isActive
                  ? 'bg-groov-accent text-groov-textDark'
                  : 'bg-groov-primary text-groov-accent'
              }`}
            >
              {filter.label}
            </button>
          )
        })}
      </div>

      <div className="mt-[10px] h-px w-full bg-groov-muted/40" />
    </>
  )
}