export type LibraryTabId =
  | 'saved'
  | 'liked'
  | 'playlists'
  | 'artists'

export type LibraryTab = {
  id: LibraryTabId
  label: string
}

export type LibraryStat = {
  id: string
  value: string
  label: string
}

export type SavedCollection = {
  id: string
  title: string
  subtitle: string
  icon: 'playlist' | 'album' | 'podcast' | 'download'
}

export type RecentPlaylist = {
  id: string
  title: string
  author: string
  tracksCount: number
  cover?: string
  isMoreCard?: boolean
}

export type MyPlaylist = {
  id: string
  title: string
  author: string
  tracksCount: number
  cover?: string
  accentColor?: string
}

export type LikedTrack = {
  id: string
  title: string
  artist: string
  duration: string
  cover: string
  isLiked?: boolean
}

export type FollowedArtist = {
  id: string
  name: string
  followers: string
  image: string
  isFollowing?: boolean
}

export type LibrarySavedContent = {
  sectionTitle: string
  savedCollections: SavedCollection[]
  recentPlaylists: RecentPlaylist[]
}

export type LibraryLikedContent = {
  sectionTitle: string
  tracks: LikedTrack[]
}

export type LibraryArtistsContent = {
  title: string
  items: FollowedArtist[]
}

export type LibraryPlaylistsContent = {
  title: string
  items: MyPlaylist[]
}

export type LibraryPageData = {
  tabs: LibraryTab[]
  stats: LibraryStat[]
  saved: LibrarySavedContent
  liked: LibraryLikedContent
  artists: LibraryArtistsContent
  playlists: LibraryPlaylistsContent
}