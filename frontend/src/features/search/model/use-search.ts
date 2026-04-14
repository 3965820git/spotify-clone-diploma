'use client'

import { useEffect, useMemo, useRef } from 'react'
import { usePathname, useRouter, useSearchParams } from 'next/navigation'
import { useSearchStore } from './search.store'
import type { FilterType, SearchItem } from './search.types'
import { useDebouncedValue } from '@/shared/lib/hooks/use-debounced-value'

const FILTER_TO_ITEM_TYPE: Record<FilterType, SearchItem['type']> = {
  tracks: 'track',
  artists: 'artist',
  albums: 'album',
}

const ALLOWED_FILTERS: FilterType[] = ['tracks', 'artists', 'albums']

function isFilterType(value: string): value is FilterType {
  return ALLOWED_FILTERS.includes(value as FilterType)
}

export function useSearch() {
  const router = useRouter()
  const pathname = usePathname()
  const searchParams = useSearchParams()

  const isInitializedRef = useRef(false)

  const query = useSearchStore((s) => s.query)
  const activeFilters = useSearchStore((s) => s.activeFilters)
  const advancedFilters = useSearchStore((s) => s.advancedFilters)
  const allItems = useSearchStore((s) => s.allItems)
  const recentItems = useSearchStore((s) => s.recentItems)

  const setQuery = useSearchStore((s) => s.setQuery)
  const setActiveFilters = useSearchStore((s) => s.setActiveFilters)
  const toggleFilter = useSearchStore((s) => s.toggleFilter)
  const toggleAdvancedFilter = useSearchStore((s) => s.toggleAdvancedFilter)
  const clearRecent = useSearchStore((s) => s.clearRecent)
  const removeRecentItem = useSearchStore((s) => s.removeRecentItem)
  const resetFilters = useSearchStore((s) => s.resetFilters)

  const debouncedQuery = useDebouncedValue(query, 400)

  // 1. Один раз читаємо URL -> store
  useEffect(() => {
    if (isInitializedRef.current) return

    const urlQuery = searchParams.get('q') ?? ''
    const rawTypes = searchParams.get('types') ?? ''

    const urlFilters = rawTypes
      .split(',')
      .map((item) => item.trim())
      .filter(isFilterType)

    setQuery(urlQuery)
    setActiveFilters(urlFilters)

    isInitializedRef.current = true
  }, [searchParams, setQuery, setActiveFilters])

  // 2. Після ініціалізації store -> URL
  useEffect(() => {
    if (!isInitializedRef.current) return

    const params = new URLSearchParams()

    const normalizedQuery = debouncedQuery.trim()
    if (normalizedQuery) {
      params.set('q', normalizedQuery)
    }

    if (activeFilters.length > 0) {
      params.set('types', activeFilters.join(','))
    }

    const nextUrl = params.toString()
      ? `${pathname}?${params.toString()}`
      : pathname

    const currentUrl = searchParams.toString()
      ? `${pathname}?${searchParams.toString()}`
      : pathname

    if (nextUrl !== currentUrl) {
      router.replace(nextUrl, { scroll: false })
    }
  }, [debouncedQuery, activeFilters, pathname, router, searchParams])

  const isSearching = debouncedQuery.trim().length > 0 || activeFilters.length > 0

  const filteredResults = useMemo(() => {
    let items = isSearching ? allItems : recentItems

    const normalizedQuery = debouncedQuery.trim().toLowerCase()

    if (normalizedQuery) {
      items = items.filter((item) => {
        return (
          item.title.toLowerCase().includes(normalizedQuery) ||
          item.subtitle.toLowerCase().includes(normalizedQuery) ||
          item.type.toLowerCase().includes(normalizedQuery)
        )
      })
    }

    if (activeFilters.length > 0) {
      const allowedTypes = activeFilters.map(
        (filter) => FILTER_TO_ITEM_TYPE[filter]
      )

      items = items.filter((item) => allowedTypes.includes(item.type))
    }

    return items
  }, [debouncedQuery, activeFilters, allItems, recentItems, isSearching])

  return {
    query,
    debouncedQuery,
    activeFilters,
    advancedFilters,
    filteredResults,
    isSearching,
    setQuery,
    toggleFilter,
    toggleAdvancedFilter,
    clearResults: clearRecent,
    removeItem: removeRecentItem,
    resetFilters,
  }
}