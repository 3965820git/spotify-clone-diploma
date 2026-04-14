'use client'

import { useState } from 'react'
import { AppShell } from '@/shared/ui/layout/AppShell'
import { libraryData } from '@/features/library/model/library.mock'
import { useLibraryPlaylistsStore } from '@/features/library/model/useLibraryPlaylistsStore'
import type { LibraryTabId } from '@/features/library/model/types'
import { LibraryHeader } from '@/features/library/ui/LibraryHeader'
import { LibraryFilters } from '@/features/library/ui/LibraryFilters'
import { LibraryStats } from '@/features/library/ui/LibraryStats'
import { SavedCollections } from '@/features/library/ui/SavedCollections'
import { RecentPlaylists } from '@/features/library/ui/RecentPlaylists'
import { LikedTracksList } from '@/features/library/ui/LikedTracksList'
import { FollowedArtists } from '@/features/library/ui/FollowedArtists'
import { MyPlaylistsSection } from '@/features/library/ui/MyPlaylistsSection'

export default function LibraryPage() {
  const [activeTab, setActiveTab] = useState<LibraryTabId>('saved')
  const createdPlaylists = useLibraryPlaylistsStore((s) => s.playlists)

  const playlistItems = [
    ...createdPlaylists.map((p) => ({
      id: p.id,
      title: p.title,
      author: p.subtitle,
      tracksCount: p.tracksCount,
      cover: p.image ?? undefined,
      accentColor: p.color,
    })),
    ...libraryData.playlists.items,
  ]

  const stats = libraryData.stats.map((stat) =>
    stat.id === 'playlists'
      ? {
          ...stat,
          value: String(playlistItems.length),
        }
      : stat
  )

  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      withBottomNavSpacing
      contentClassName="pb-0"
    >
      <div className="min-h-full bg-groov-bg px-[10px] pt-[10px]">
        <LibraryHeader />

        <LibraryFilters
          tabs={libraryData.tabs}
          activeTab={activeTab}
          onChange={setActiveTab}
        />

        <LibraryStats stats={stats} />

        {activeTab === 'saved' && (
          <>
            <SavedCollections items={libraryData.saved.savedCollections} />
            <RecentPlaylists items={libraryData.saved.recentPlaylists} />
          </>
        )}

        {activeTab === 'liked' && (
          <LikedTracksList
            title={libraryData.liked.sectionTitle}
            tracks={libraryData.liked.tracks}
          />
        )}

        {activeTab === 'artists' && (
          <FollowedArtists
            title={libraryData.artists.title}
            artists={libraryData.artists.items}
          />
        )}

        {activeTab === 'playlists' && (
          <MyPlaylistsSection
            title={libraryData.playlists.title}
            items={playlistItems}
          />
        )}
      </div>
    </AppShell>
  )
}