'use client'

import { AppShell } from '@/shared/ui/layout/AppShell'
import { BottomNav } from '@/shared/ui/BottomNav'
import { SearchBar } from '@/features/search/ui/SearchBar'
import { SearchFilters } from '@/features/search/ui/SearchFilters'
import { SearchResults } from '@/features/search/ui/SearchResults'
import { useSearch } from '@/features/search/model/use-search'

export default function SearchPage() {
  const {
    query,
    setQuery,
    activeFilters,
    toggleFilter,
    filteredResults,
    clearResults,
    removeItem,
    resetFilters,
    isSearching,
  } = useSearch()

  return (
    <AppShell>
      <div className="pt-[10px]">
        <SearchBar query={query} onChange={setQuery} />

        <SearchFilters
          activeFilters={activeFilters}
          onToggleFilter={toggleFilter}
          onReset={resetFilters}
        />

        <SearchResults
          items={filteredResults}
          onClear={clearResults}
          onRemove={removeItem}
          isSearching={isSearching}
        />
      </div>

      <BottomNav />
    </AppShell>
  )
}