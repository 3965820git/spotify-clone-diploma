'use client'

import { useMutation, useQueryClient } from '@tanstack/react-query'
import {
  createTrack,
  uploadTrackAudio,
  type CreateTrackRequest,
} from './admin.api'

type CreateTrackWithAudioInput = CreateTrackRequest & {
  file: File | null
}

export function useCreateTrackWithAudio() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: async ({ file, ...trackData }: CreateTrackWithAudioInput) => {
      const created = await createTrack(trackData)

      if (file) {
        await uploadTrackAudio(created.trackId, file)
      }

      return created
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['admin-my-tracks'] })
      await queryClient.invalidateQueries({ queryKey: ['admin-tracks'] })
    },
  })
}