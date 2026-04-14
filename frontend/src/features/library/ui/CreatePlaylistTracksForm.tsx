'use client'

import { useEffect, useMemo, useState } from 'react'
import {
  useCreatePlaylistStore,
  type PlaylistTrack,
} from '@/features/library/model/create-playlist.store'
import { CreatePlaylistBottomAction } from './CreatePlaylistBottomAction'
import { CreatePlaylistNextButton } from './CreatePlaylistNextButton'

type TrackItem = PlaylistTrack

const tracksMock: TrackItem[] = [
  {
    id: 'motion',
    title: 'MOTION',
    artist: 'Nemzzz',
    duration: '02:22',
    cover: 'https://i.scdn.co/image/ab67616d00001e02e3b0d4e4c8ad7e6d7f4f5c28',
  },
  {
    id: 'sprinter',
    title: 'Sprinter',
    artist: 'Dave i Central Cee',
    duration: '03:50',
    cover: 'https://i.scdn.co/image/ab67616d00001e028d7b3b6b5df4d2f6f7c84d8b',
  },
  {
    id: 'batman',
    title: 'Batman',
    artist: 'mikeyeysmind i dadanny',
    duration: '02:15',
    cover: 'https://i.scdn.co/image/ab67616d00001e025dba2f90ec7c4f6ffc0dbe22',
  },
  {
    id: 'did-it-first',
    title: 'Did It First',
    artist: 'Ice Spice i Central Cee',
    duration: '02:22',
    cover: 'https://i.scdn.co/image/ab67616d00001e02820b8f2d5f2c858447f0c4e5',
  },
  {
    id: 'which-one',
    title: 'Which One',
    artist: 'Central Cee i Drake',
    duration: '03:50',
    cover: 'https://i.scdn.co/image/ab67616d00001e029d3f0b0fd3c1bd5f0fdb59bd',
  },
  {
    id: 'mm3',
    title: 'MM3',
    artist: 'SoFaygo',
    duration: '03:50',
    cover: 'https://i.scdn.co/image/ab67616d00001e02ef4e4dc6d8cb12c325a4e497',
  },
  {
    id: 'only-you',
    title: 'only you (slowed + reverb)',
    artist: 'bluent i bre.beats',
    duration: '02:22',
    cover: 'https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?auto=format&fit=crop&w=600&q=80',
  },
  {
    id: 'glow',
    title: 'glow',
    artist: 'bluent',
    duration: '03:50',
    cover: 'https://dummyimage.com/300x300/ff2222/ff2222',
  },
  {
    id: 'motion23',
    title: 'MOTION',
    artist: 'Nemzzz',
    duration: '02:22',
    cover: 'https://i.scdn.co/image/ab67616d00001e02e3b0d4e4c8ad7e6d7f4f5c28',
  },
  {
    id: 'sprinter3',
    title: 'Sprinter',
    artist: 'Dave i Central Cee',
    duration: '03:50',
    cover: 'https://i.scdn.co/image/ab67616d00001e028d7b3b6b5df4d2f6f7c84d8b',
  },
  {
    id: 'batman2',
    title: 'Batman',
    artist: 'mikeyeysmind i dadanny',
    duration: '02:15',
    cover: 'https://i.scdn.co/image/ab67616d00001e025dba2f90ec7c4f6ffc0dbe22',
  },
  {
    id: 'did-it-first2',
    title: 'Did It First',
    artist: 'Ice Spice i Central Cee',
    duration: '02:22',
    cover: 'https://i.scdn.co/image/ab67616d00001e02820b8f2d5f2c858447f0c4e5',
  },
  {
    id: 'which-one2',
    title: 'Which One',
    artist: 'Central Cee i Drake',
    duration: '03:50',
    cover: 'https://i.scdn.co/image/ab67616d00001e029d3f0b0fd3c1bd5f0fdb59bd',
  },
  {
    id: 'mm32',
    title: 'MM3',
    artist: 'SoFaygo',
    duration: '03:50',
    cover: 'https://i.scdn.co/image/ab67616d00001e02ef4e4dc6d8cb12c325a4e497',
  },
  {
    id: 'glow2',
    title: 'glow',
    artist: 'bluent',
    duration: '03:50',
    cover: 'https://dummyimage.com/300x300/ff2222/ff2222',
  },
]

function SearchIcon() {
  return (
    <svg
      viewBox="0 0 24 24"
      fill="none"
      className="h-[18px] w-[18px]"
      aria-hidden="true"
    >
      <path
        d="M11 19C15.4183 19 19 15.4183 19 11C19 6.58172 15.4183 3 11 3C6.58172 3 3 6.58172 3 11C3 15.4183 6.58172 19 11 19Z"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M21 21L16.65 16.65"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}

type TrackRowProps = {
  track: TrackItem
  checked: boolean
  onToggle: () => void
}

function TrackRow({ track, checked, onToggle }: TrackRowProps) {
  return (
    <button
      type="button"
      onClick={onToggle}
      className={`flex h-[70px] w-full items-center rounded-[14px] px-[12px] text-left transition-all duration-200 ${
        checked
          ? 'border border-groov-accent bg-groov-surface'
          : 'border border-transparent bg-groov-surface'
      }`}
    >
      <div className="flex h-[16px] w-[16px] shrink-0 items-center justify-center rounded-[5px] bg-groov-bg transition-colors">
        {checked ? (
          <svg
            viewBox="0 0 24 24"
            fill="none"
            className="h-[12px] w-[12px] text-groov-accent"
            aria-hidden="true"
          >
            <path
              d="M5 12.5L9.5 17L19 7.5"
              stroke="currentColor"
              strokeWidth="2.2"
              strokeLinecap="round"
              strokeLinejoin="round"
            />
          </svg>
        ) : null}
      </div>

      <div className="ml-[12px] h-[50px] w-[50px] shrink-0 overflow-hidden rounded-[8px]">
        <img
          src={track.cover}
          alt={track.title}
          className="h-full w-full object-cover"
        />
      </div>

      <div className="ml-[12px] min-w-0 flex-1">
        <div className="truncate text-[14px] leading-[17px] text-groov-accent">
          {track.title}
        </div>

        <div className="mt-[6px] truncate text-[12px] leading-[14px] text-groov-accent/80">
          {track.artist}
        </div>
      </div>

      <div className="ml-[10px] shrink-0 text-[14px] leading-[17px] text-groov-accent">
        {track.duration}
      </div>
    </button>
  )
}

export function CreatePlaylistTracksForm() {
  const { tracks, setTracks } = useCreatePlaylistStore()

  const [query, setQuery] = useState('')
  const [visibleCount, setVisibleCount] = useState(8)
  const [selectedIds, setSelectedIds] = useState<string[]>(
    tracks.map((track) => track.id)
  )

  const filteredTracks = useMemo(() => {
    const normalizedQuery = query.trim().toLowerCase()

    if (!normalizedQuery) return tracksMock

    return tracksMock.filter(
      (track) =>
        track.title.toLowerCase().includes(normalizedQuery) ||
        track.artist.toLowerCase().includes(normalizedQuery)
    )
  }, [query])

  const visibleTracks = filteredTracks.slice(0, visibleCount)

  useEffect(() => {
    setVisibleCount(8)
  }, [query])

  useEffect(() => {
    const selectedTracks = tracksMock.filter((track) =>
      selectedIds.includes(track.id)
    )
    setTracks(selectedTracks)
  }, [selectedIds, setTracks])

  const toggleTrack = (trackId: string) => {
    setSelectedIds((prev) =>
      prev.includes(trackId)
        ? prev.filter((id) => id !== trackId)
        : [...prev, trackId]
    )
  }

  const hasSelectedTracks = selectedIds.length > 0

  return (
    <>
      <div className="mt-[25px]">
        <h1 className="text-[24px] font-semibold leading-[24px] text-groov-accent">
          Додайте треки
        </h1>

        <p className="mt-[10px] max-w-[360px] text-[16px] leading-[18px] text-groov-accent">
          Виберіть треки, які будуть у вашому плейлісті. Можна додати будь-яку кількість.
        </p>
      </div>

      <div className="mt-[18px]">
        <label className="relative block">
          <span className="pointer-events-none absolute left-[14px] top-1/2 -translate-y-1/2 text-groov-accent">
            <SearchIcon />
          </span>

          <input
            type="text"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            placeholder="Пошук треків за назвою, артистом..."
            className="h-[40px] w-full rounded-[12px] bg-groov-surface pl-[42px] pr-[14px] text-[16px] leading-[17px] text-groov-accent outline-none placeholder:text-groov-accent"
          />
        </label>
      </div>

      <div className="mt-[14px] space-y-[8px]">
        {visibleTracks.map((track) => (
          <TrackRow
            key={track.id}
            track={track}
            checked={selectedIds.includes(track.id)}
            onToggle={() => toggleTrack(track.id)}
          />
        ))}
      </div>

      <div className="h-[24px]" />

      {visibleCount < filteredTracks.length && (
        <button
          type="button"
          onClick={() => setVisibleCount((prev) => prev + 8)}
          className="w-full text-center text-[16px] leading-[19px] text-groov-accent transition-opacity active:opacity-70"
        >
          Завантажити ще
        </button>
      )}

      <CreatePlaylistBottomAction
        mode="sticky"
        className="pt-[24px]"
        bottomOffset={40}
      >
        <CreatePlaylistNextButton
          href="/library/create-playlist/visibility"
          disabled={!hasSelectedTracks}
        />
      </CreatePlaylistBottomAction>
    </>
  )
}