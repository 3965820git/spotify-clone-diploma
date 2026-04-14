'use client'

import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import { ChevronDownIcon } from '@/shared/ui/icons/ChevronDownIcon'
import {
  createAlbum,
  createTrack,
  getArtists,
  getGenres,
  getMoods,
  getMyTracks,
  getTracks,
  uploadTrackAudio,
} from '@/features/admin/api/admin.api'

type Props = {
  audioFile: File | null
  onClearAudioFile: () => void
}

export function AdminUploadForm({
  audioFile,
  onClearAudioFile,
}: Props) {
  const queryClient = useQueryClient()

  const [trackTitle, setTrackTitle] = useState('')
  const [selectedArtistId, setSelectedArtistId] = useState('')
  const [selectedGenreId, setSelectedGenreId] = useState('')
  const [selectedMoodId, setSelectedMoodId] = useState('')
  const [isArtistOpen, setIsArtistOpen] = useState(false)
  const [isGenreOpen, setIsGenreOpen] = useState(false)
  const [isMoodOpen, setIsMoodOpen] = useState(false)
  const [submitMessage, setSubmitMessage] = useState('')

  const { data: artistsData } = useQuery({
    queryKey: ['admin-artists'],
    queryFn: getArtists,
  })

  const { data: genresData } = useQuery({
    queryKey: ['admin-genres'],
    queryFn: getGenres,
  })

  const { data: moodsData } = useQuery({
    queryKey: ['admin-moods'],
    queryFn: getMoods,
  })

  useQuery({
    queryKey: ['admin-tracks'],
    queryFn: getTracks,
  })

  useQuery({
    queryKey: ['admin-my-tracks'],
    queryFn: getMyTracks,
  })

  const artists = artistsData ?? []
  const genres = genresData ?? []
  const moods = moodsData ?? []

  const selectedArtist =
    artists.find((artist) => artist.id === selectedArtistId) ?? null
  const selectedGenre =
    genres.find((genre) => genre.id === selectedGenreId) ?? null
  const selectedMood =
    moods.find((mood) => mood.id === selectedMoodId) ?? null

  const resetForm = () => {
    setTrackTitle('')
    setSelectedArtistId('')
    setSelectedGenreId('')
    setSelectedMoodId('')
    setIsArtistOpen(false)
    setIsGenreOpen(false)
    setIsMoodOpen(false)
    onClearAudioFile()
  }

  const handleSelectArtist = (artistId: string) => {
    setSelectedArtistId(artistId)
    setIsArtistOpen(false)
  }

  const handleSelectGenre = (genreId: string) => {
    setSelectedGenreId(genreId)
    setIsGenreOpen(false)
  }

  const handleSelectMood = (moodId: string) => {
    setSelectedMoodId(moodId)
    setIsMoodOpen(false)
  }

  const uploadMutation = useMutation({
    mutationFn: async () => {
      if (!selectedArtist) {
        throw new Error('Оберіть виконавця')
      }

      if (!trackTitle.trim()) {
        throw new Error('Введіть назву треку')
      }

      if (!selectedGenreId) {
        throw new Error('Оберіть жанр')
      }

      if (!selectedMoodId) {
        throw new Error('Оберіть настрій')
      }

      if (!audioFile) {
        throw new Error('Оберіть аудіофайл')
      }

      const album = await createAlbum({
        title: `${trackTitle.trim()} Album`,
        mainArtistIds: [selectedArtist.id],
      })

      const track = await createTrack({
        title: trackTitle.trim(),
        containsExplicitContent: false,
        albumId: album.albumId,
        mainArtists: [selectedArtist.id],
        featuredArtists: [],
        genres: [selectedGenreId],
        moods: [selectedMoodId],
      })

      await uploadTrackAudio(track.trackId, audioFile)

      return track.trackId
    },
    onSuccess: async () => {
      setSubmitMessage('Трек успішно створено і аудіо завантажено')
      resetForm()

      await Promise.all([
        queryClient.invalidateQueries({ queryKey: ['admin-tracks'] }),
        queryClient.invalidateQueries({ queryKey: ['admin-my-tracks'] }),
      ])
    },
    onError: (error) => {
      const message =
        error instanceof Error ? error.message : 'Не вдалося завантажити трек'
      setSubmitMessage(message)
    },
  })

  const handleSubmit = async () => {
    setSubmitMessage('')
    await uploadMutation.mutateAsync()
  }

  return (
    <>
      <div className="mt-[20px]">
        <label className="block">
          <span className="text-[14px] font-medium leading-[17px] text-groov-accent">
            Назва треку
          </span>

          <input
            type="text"
            value={trackTitle}
            onChange={(e) => setTrackTitle(e.target.value)}
            placeholder="Введіть назву треку"
            className="mt-[10px] h-[40px] w-full rounded-[10px] border-0 bg-groov-accent px-[14px] text-[14px] leading-[14px] text-groov-textDark outline-none placeholder:text-groov-textDark/60"
          />
        </label>
      </div>

      <div className="mt-[16px]">
        <span className="text-[14px] font-medium leading-[17px] text-groov-accent">
          Виконавець
        </span>

        <div className="relative mt-[10px]">
          <button
            type="button"
            onClick={() => setIsArtistOpen((prev) => !prev)}
            className="flex h-[40px] w-full items-center justify-between rounded-[10px] bg-groov-accent px-[14px] text-groov-textDark"
          >
            <span
              className={[
                'text-[14px] leading-[14px]',
                selectedArtist ? 'text-groov-textDark' : 'text-groov-textDark/60',
              ].join(' ')}
            >
              {selectedArtist?.name ?? 'Оберіть виконавця'}
            </span>

            <ChevronDownIcon
              className={[
                'h-[24px] w-[24px] transition-transform',
                isArtistOpen ? 'rotate-180' : '',
              ].join(' ')}
            />
          </button>

          {isArtistOpen ? (
            <div className="absolute left-0 right-0 top-[46px] z-20 max-h-[220px] overflow-auto rounded-[12px] bg-groov-accent shadow-lg">
              {artists.map((artist) => (
                <button
                  key={artist.id}
                  type="button"
                  onClick={() => handleSelectArtist(artist.id)}
                  className="flex h-[40px] w-full items-center px-[14px] text-left text-[14px] leading-[14px] text-groov-textDark transition-colors hover:bg-black/5"
                >
                  {artist.name}
                </button>
              ))}
            </div>
          ) : null}
        </div>
      </div>

      <div className="mt-[16px]">
        <span className="text-[14px] font-medium leading-[17px] text-groov-accent">
          Жанр
        </span>

        <div className="relative mt-[10px]">
          <button
            type="button"
            onClick={() => setIsGenreOpen((prev) => !prev)}
            className="flex h-[40px] w-full items-center justify-between rounded-[10px] bg-groov-accent px-[14px] text-groov-textDark"
          >
            <span
              className={[
                'text-[14px] leading-[14px]',
                selectedGenre ? 'text-groov-textDark' : 'text-groov-textDark/60',
              ].join(' ')}
            >
              {selectedGenre?.name ?? 'Оберіть жанр'}
            </span>

            <ChevronDownIcon
              className={[
                'h-[24px] w-[24px] transition-transform',
                isGenreOpen ? 'rotate-180' : '',
              ].join(' ')}
            />
          </button>

          {isGenreOpen ? (
            <div className="absolute left-0 right-0 top-[46px] z-20 max-h-[220px] overflow-auto rounded-[12px] bg-groov-accent shadow-lg">
              {genres.map((genre) => (
                <button
                  key={genre.id}
                  type="button"
                  onClick={() => handleSelectGenre(genre.id)}
                  className="flex h-[40px] w-full items-center px-[14px] text-left text-[14px] leading-[14px] text-groov-textDark transition-colors hover:bg-black/5"
                >
                  {genre.name}
                </button>
              ))}
            </div>
          ) : null}
        </div>
      </div>

      <div className="mt-[16px]">
        <span className="text-[14px] font-medium leading-[17px] text-groov-accent">
          Настрій
        </span>

        <div className="relative mt-[10px]">
          <button
            type="button"
            onClick={() => setIsMoodOpen((prev) => !prev)}
            className="flex h-[40px] w-full items-center justify-between rounded-[10px] bg-groov-accent px-[14px] text-groov-textDark"
          >
            <span
              className={[
                'text-[14px] leading-[14px]',
                selectedMood ? 'text-groov-textDark' : 'text-groov-textDark/60',
              ].join(' ')}
            >
              {selectedMood?.name ?? 'Оберіть настрій'}
            </span>

            <ChevronDownIcon
              className={[
                'h-[24px] w-[24px] transition-transform',
                isMoodOpen ? 'rotate-180' : '',
              ].join(' ')}
            />
          </button>

          {isMoodOpen ? (
            <div className="absolute left-0 right-0 top-[46px] z-20 max-h-[220px] overflow-auto rounded-[12px] bg-groov-accent shadow-lg">
              {moods.map((mood) => (
                <button
                  key={mood.id}
                  type="button"
                  onClick={() => handleSelectMood(mood.id)}
                  className="flex h-[40px] w-full items-center px-[14px] text-left text-[14px] leading-[14px] text-groov-textDark transition-colors hover:bg-black/5"
                >
                  {mood.name}
                </button>
              ))}
            </div>
          ) : null}
        </div>
      </div>

      <button
        type="button"
        onClick={() => {
          void handleSubmit()
        }}
        disabled={uploadMutation.isPending}
        className="mt-[16px] flex h-[40px] w-full items-center justify-center rounded-[12px] bg-[#8DA2BE] text-[16px] font-medium leading-[19px] text-groov-accent disabled:opacity-60"
      >
        {uploadMutation.isPending ? 'Завантаження...' : 'Завантажити трек'}
      </button>

      {submitMessage ? (
        <p className="mt-[10px] text-center text-[11px] leading-[14px] text-groov-accent/90">
          {submitMessage}
        </p>
      ) : (
        <p className="mt-[10px] text-center text-[11px] leading-[14px] text-groov-accent/90">
          Ваш трек з’явиться у вашому профілі після модерації
        </p>
      )}
    </>
  )
}