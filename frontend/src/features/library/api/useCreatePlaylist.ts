import { useMutation } from '@tanstack/react-query'
import { createPlaylist, updatePlaylist } from '@/shared/api/playlists'
import { useLibraryPlaylistsStore } from '@/features/library/model/useLibraryPlaylistsStore'

type CreatePlaylistInput = {
  name: string
  description: string | null
  isPublic: boolean
  trackIds: string[]
  coverFile?: File | null
  coverColor?: string
}

export function useCreatePlaylist() {
  return useMutation({
    mutationFn: async ({
      name,
      description,
      isPublic,
      trackIds,
      coverColor,
    }: CreatePlaylistInput) => {
      const created = await createPlaylist()
      const playlistId = created.playlistId

      await updatePlaylist(playlistId, {
        name,
        description,
        isPublic,
      })

      return {
        playlistId,
        playlist: {
          id: playlistId,
          title: name,
          subtitle: '@andrii_koval',
          tracksCount: trackIds.length,
          image: null,
          color: coverColor ?? '#A78BCE',
        },
      }
    },
    onSuccess: ({ playlist }) => {
      useLibraryPlaylistsStore.getState().prependPlaylist(playlist)
    },
  })
}