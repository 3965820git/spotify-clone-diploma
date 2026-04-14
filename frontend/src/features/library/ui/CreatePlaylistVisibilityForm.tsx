'use client'

import { useCreatePlaylistStore } from '@/features/library/model/create-playlist.store'
import type { PlaylistVisibility } from '@/features/library/model/create-playlist.types'
import { visibilityOptions } from '@/features/library/model/create-playlist.constants'
import { VisibilityOptionCard } from './VisibilityOptionCard'
import { CreatePlaylistBottomAction } from './CreatePlaylistBottomAction'
import { CreatePlaylistNextButton } from './CreatePlaylistNextButton'

export function CreatePlaylistVisibilityForm() {
  const { visibility, setVisibility } = useCreatePlaylistStore()

  return (
    <>
      <div className="mt-[25px]">
        <h1 className="text-[24px] font-semibold text-groov-accent">
          Налаштування доступу
        </h1>

        <p className="mt-[10px] text-[16px] text-groov-accent">
          Виберіть, хто зможе бачити ваш плейліст.
        </p>
      </div>

      <div className="mt-[20px] space-y-[10px]">
        {visibilityOptions.map((option) => (
          <VisibilityOptionCard
            key={option.id}
            title={option.title}
            description={option.description}
            icon={option.icon}
            selected={visibility === option.id}
            onSelect={() => setVisibility(option.id as PlaylistVisibility)}
          />
        ))}
      </div>

      <CreatePlaylistBottomAction className="pt-[220px]">
        <CreatePlaylistNextButton
          href="/library/create-playlist/review"
          disabled={!visibility}
        />
      </CreatePlaylistBottomAction>
    </>
  )
}