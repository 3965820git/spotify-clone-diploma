import { create } from 'zustand'

export type PlaylistVisibility = 'public' | 'friends' | 'private'

export type PlaylistTrack = {
  id: string
  title: string
  artist: string
  duration: string
  cover: string
}

type State = {
  name: string
  description: string
  coverColor: string
  coverFile: File | null
  visibility: PlaylistVisibility
  tracks: PlaylistTrack[]
}

type Actions = {
  setName: (name: string) => void
  setDescription: (description: string) => void
  setCoverColor: (color: string) => void
  setCoverFile: (file: File | null) => void
  setVisibility: (visibility: PlaylistVisibility) => void
  setTracks: (tracks: PlaylistTrack[]) => void
  reset: () => void
}

const initialState: State = {
  name: '',
  description: '',
  coverColor: '#E5B19C',
  coverFile: null,
  visibility: 'public',
  tracks: [],
}

export const useCreatePlaylistStore = create<State & Actions>((set) => ({
  ...initialState,

  setName: (name) => set({ name }),
  setDescription: (description) => set({ description }),
  setCoverColor: (coverColor) => set({ coverColor }),
  setCoverFile: (coverFile) => set({ coverFile }),
  setVisibility: (visibility) => set({ visibility }),
  setTracks: (tracks) => set({ tracks }),

  reset: () => set(initialState),
}))