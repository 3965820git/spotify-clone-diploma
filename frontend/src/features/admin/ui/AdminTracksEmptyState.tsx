'use client'

import { AdminMusicNoteIcon } from '@/shared/ui/icons/settings/AdminMusicNoteIcon'

type Props = {
  onUploadClick: () => void
}

export function AdminTracksEmptyState({ onUploadClick }: Props) {
  return (
    <div className="mt-[16px] rounded-[14px] bg-groov-surface px-[20px] py-[30px] text-center">
      <div className="flex justify-center text-groov-accent">
        <AdminMusicNoteIcon className="h-[58px] w-[58px]" />
      </div>

      <h3 className="mt-[20px] text-[16px] font-medium leading-[19px] text-groov-accent">
        Ще немає опублікованих треків
      </h3>

      <p className="mx-auto mt-[12px] max-w-[330px] text-[14px] leading-[17px] text-groov-accent/90">
        Ви ще не опублікували жодного треку. Завантажте свою першу композицію,
        щоб поділитися нею зі світом!
      </p>

      <button
        type="button"
        onClick={onUploadClick}
        className="mt-[22px] flex h-[50px] w-full items-center justify-center rounded-[18px] bg-[#8DA2BE] text-[16px] font-medium leading-[19px] text-groov-accent"
      >
        Завантажити трек
      </button>
    </div>
  )
}