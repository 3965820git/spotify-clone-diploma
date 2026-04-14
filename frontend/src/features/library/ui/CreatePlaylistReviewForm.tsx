'use client'

import { useMemo } from 'react'
import { useRouter } from 'next/navigation'
import { useCreatePlaylist } from '@/features/library/api/useCreatePlaylist'
import { useCreatePlaylistStore } from '@/features/library/model/create-playlist.store'
import { CreatePlaylistBottomAction } from './CreatePlaylistBottomAction'
import { CreatePlaylistNextButton } from './CreatePlaylistNextButton'
import { ReviewStatCard } from './ReviewStatCard'
import { ReviewTrackRow } from './ReviewTrackRow'

const visibilityLabelMap = {
  public: 'Публічний',
  friends: 'Спільний',
  private: 'Приватний',
} as const

export function CreatePlaylistReviewForm() {
  const {
    name,
    description,
    coverColor,
    coverFile,
    visibility,
    tracks,
    reset,
  } = useCreatePlaylistStore()

  const router = useRouter()
  const createPlaylistMutation = useCreatePlaylist()

  const totalMinutes = useMemo(() => {
    const totalSeconds = tracks.reduce((acc, track) => {
      const [minutes, seconds] = track.duration.split(':').map(Number)
      return acc + minutes * 60 + seconds
    }, 0)

    return Math.max(1, Math.round(totalSeconds / 60))
  }, [tracks])

  const handleCreatePlaylist = async () => {
    try {
      const result = await createPlaylistMutation.mutateAsync({
  name: name || 'Без назви',
  description: description || null,
  isPublic: visibility === 'public',
  trackIds: tracks.map((track) => track.id),
  coverFile,
  coverColor,
})

      reset()
      router.push(`/playlist/${result.playlistId}`)
    } catch (error) {
      console.error('Failed to create playlist', error)
    }
  }

  return (
    <>
      <div className="mt-[25px]">
        <h1 className="text-[24px] font-semibold leading-[29px] text-groov-accent">
          Готово!
        </h1>

        <p className="mt-[10px] max-w-[360px] text-[16px] leading-[18px] text-groov-accent/80">
          Ось як виглядатиме ваш новий плейліст. Якщо все правильно,
          натисніть &quot;Створити плейліст&quot;.
        </p>
      </div>

      <div className="mt-[18px] rounded-[16px] bg-groov-surface px-[16px] pb-[12px] pt-[16px]">
        <div className="flex justify-center">
          {coverFile ? (
            <img
              src={URL.createObjectURL(coverFile)}
              alt="Playlist cover"
              className="h-[120px] w-[120px] rounded-[20px] object-cover"
            />
          ) : (
            <div
              className="h-[120px] w-[120px] rounded-[20px]"
              style={{ backgroundColor: coverColor }}
            />
          )}
        </div>

        <div className="mt-[13px] text-center">
          <div className="text-[20px] font-semibold leading-[30px] text-groov-accent">
            {name || 'Без назви'}
          </div>

          <div className="mt-[8px] text-[16px] leading-[19px] text-groov-accent/80">
            {description || 'Без опису'}
          </div>
        </div>

        <div className="mt-[15px] flex gap-[8px]">
          <ReviewStatCard value={String(tracks.length)} label="Треків" />
          <ReviewStatCard
            value={visibilityLabelMap[visibility]}
            label="Доступ"
          />
          <ReviewStatCard value={String(totalMinutes)} label="хвилин" />
        </div>

        <div className="mt-[16px] space-y-0">
          {tracks.map((track) => (
            <ReviewTrackRow
              key={track.id}
              cover={track.cover}
              title={track.title}
              artist={track.artist}
              duration={track.duration}
            />
          ))}
        </div>
      </div>

      <CreatePlaylistBottomAction className="pt-[40px]" bottomOffset={34}>
        <CreatePlaylistNextButton
          href="#"
          label={
            createPlaylistMutation.isPending
              ? 'Створення...'
              : 'Створити плейліст'
          }
          onClick={handleCreatePlaylist}
        />
      </CreatePlaylistBottomAction>
    </>
  )
}