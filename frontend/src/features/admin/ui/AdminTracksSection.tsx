'use client'

import { AdminMusicNoteIcon } from '@/shared/ui/icons/settings/AdminMusicNoteIcon'
import { useAdminTracks } from '@/features/admin/api/useAdminTracks'
import { AdminTracksEmptyState } from './AdminTracksEmptyState'
import { AdminTracksList } from './AdminTracksList'

type Props = {
  onUploadClick: () => void
}

export function AdminTracksSection({ onUploadClick }: Props) {
  const { data: tracks = [], isLoading, isError } = useAdminTracks()

  return (
    <>
      <div className="mt-[18px] flex items-center gap-[12px]">
        <div className="flex h-[34px] w-[34px] shrink-0 items-center justify-center rounded-[10px] bg-groov-surface text-groov-accent">
          <AdminMusicNoteIcon className="h-[24px] w-[24px]" />
        </div>

        <h2 className="text-[18px] font-medium leading-[22px] text-groov-accent">
          Мої треки
        </h2>
      </div>

      {isLoading ? (
        <div className="mt-[16px] rounded-[14px] bg-groov-surface px-[14px] py-[14px] text-[14px] leading-[17px] text-groov-accent">
          Завантаження треків...
        </div>
      ) : isError ? (
        <div className="mt-[16px] rounded-[14px] bg-groov-surface px-[14px] py-[14px] text-[14px] leading-[17px] text-groov-accent">
          Не вдалося завантажити треки
        </div>
      ) : tracks.length === 0 ? (
        <AdminTracksEmptyState onUploadClick={onUploadClick} />
      ) : (
        <AdminTracksList tracks={tracks} />
      )}
    </>
  )
}