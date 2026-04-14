import { create } from 'zustand'

export type LibraryPlaylistItem = {
  id: string
  title: string
  subtitle: string
  tracksCount: number
  image: string | null
  color?: string
}

type State = {
  playlists: LibraryPlaylistItem[]
  setPlaylists: (playlists: LibraryPlaylistItem[]) => void
  prependPlaylist: (playlist: LibraryPlaylistItem) => void
}

export const useLibraryPlaylistsStore = create<State>((set) => ({
  playlists: [],
  setPlaylists: (playlists) => set({ playlists }),
  prependPlaylist: (playlist) =>
    set((state) => ({
      playlists: [playlist, ...state.playlists],
    })),
}))