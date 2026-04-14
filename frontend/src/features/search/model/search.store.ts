'use client'

import { create } from 'zustand'
import { mockSearchResults } from '../mock/search.mock'
import type { FilterType, SearchItem } from './search.types'

type SearchState = {
  query: string
  activeFilters: FilterType[]
  advancedFilters: string[]

  allItems: SearchItem[]
  recentItems: SearchItem[]

  setQuery: (value: string) => void
  setActiveFilters: (filters: FilterType[]) => void
  toggleFilter: (filter: FilterType) => void
  toggleAdvancedFilter: (filter: string) => void
  clearRecent: () => void
  removeRecentItem: (id: string) => void
  resetFilters: () => void
}

const allSearchItems: SearchItem[] = [
  ...mockSearchResults,
  {
    id: '4',
    title: 'Starboy',
    subtitle: 'Альбом · The Weeknd',
    type: 'album',
    image: '/Weeknd.png',
  },
  {
    id: '5',
    title: 'Blinding Lights',
    subtitle: 'Пісня · The Weeknd',
    type: 'track',
    image: '/Weeknd.png',
  },
  {
    id: '6',
    title: 'After Hours',
    subtitle: 'Альбом · The Weeknd',
    type: 'album',
    image: '/Weeknd.png',
  },
  {
    id: '7',
    title: 'Playboi Carti',
    subtitle: 'Виконавець',
    type: 'artist',
    image: '/Weeknd.png',
  },
  {
    id: '8',
    title: 'Die Lit',
    subtitle: 'Альбом · Playboi Carti',
    type: 'album',
    image: '/Weeknd.png',
  },
]

export const useSearchStore = create<SearchState>((set) => ({
  query: '',
  activeFilters: [],
  advancedFilters: [],

  allItems: allSearchItems,
  recentItems: mockSearchResults,

  setQuery: (value) => set({ query: value }),

  setActiveFilters: (filters) => set({ activeFilters: filters }),

  toggleFilter: (filter) =>
    set((state) => ({
      activeFilters: state.activeFilters.includes(filter)
        ? state.activeFilters.filter((item) => item !== filter)
        : [...state.activeFilters, filter],
    })),

  toggleAdvancedFilter: (filter) =>
    set((state) => ({
      advancedFilters: state.advancedFilters.includes(filter)
        ? state.advancedFilters.filter((item) => item !== filter)
        : [...state.advancedFilters, filter],
    })),

  clearRecent: () => set({ recentItems: [] }),

  removeRecentItem: (id) =>
    set((state) => ({
      recentItems: state.recentItems.filter((item) => item.id !== id),
    })),

  resetFilters: () =>
    set({
      activeFilters: [],
      advancedFilters: [],
    }),
}))