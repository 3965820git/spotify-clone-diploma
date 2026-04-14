'use client'

import { useCreatePlaylistStore } from '@/features/library/model/create-playlist.store'
import { CreatePlaylistBottomAction } from './CreatePlaylistBottomAction'
import { CreatePlaylistNextButton } from './CreatePlaylistNextButton'

export function CreatePlaylistForm() {
  const { name, description, setName, setDescription } =
    useCreatePlaylistStore()

  const isNextDisabled = name.trim().length === 0

  return (
    <>
      <div className="mt-[38px]">
        <h1 className="text-[20px] font-semibold leading-[24px] text-groov-accent">
          Назвіть ваш плейліст
        </h1>

        <p className="mt-[10px] max-w-[370px] text-[15px] leading-[18px] text-groov-accent/50">
          Дайте унікальну назву вашому плейлісту, щоб легко його знайти пізніше.
        </p>
      </div>

      <div className="mt-[30px]">
        <label
          htmlFor="playlist-name"
          className="text-[16px] font-medium leading-[19px] text-groov-accent"
        >
          Назва плейлісту *
        </label>

        <div className="mt-[10px]">
          <input
            id="playlist-name"
            type="text"
            maxLength={50}
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Наприклад: Моя улюблена музика"
            className="h-[50px] w-full rounded-[12px] bg-groov-surface px-[14px] text-[14px] leading-[17px] text-groov-accent outline-none placeholder:text-groov-accent/50"
          />

          <div className="mt-[8px] text-right text-[12px] leading-[14px] text-groov-accent/50">
            {name.length}/50
          </div>
        </div>
      </div>

      <div className="mt-[22px]">
        <label
          htmlFor="playlist-description"
          className="text-[16px] font-medium leading-[19px] text-groov-accent"
        >
          Опис (необов&apos;язково)
        </label>

        <div className="mt-[10px]">
          <textarea
            id="playlist-description"
            maxLength={200}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Опишіть, для чого цей плейліст або який настрій він створює..."
            className="h-[138px] w-full resize-none rounded-[12px] bg-groov-surface px-[14px] py-[14px] text-[14px] leading-[17px] text-groov-accent outline-none placeholder:text-groov-accent/50"
          />

          <div className="mt-[8px] text-right text-[12px] leading-[14px] text-groov-accent/50">
            {description.length}/200
          </div>
        </div>
      </div>

      <CreatePlaylistBottomAction className="pt-[200px]">
        <CreatePlaylistNextButton
          href="/library/create-playlist/cover"
          disabled={isNextDisabled}
        />
      </CreatePlaylistBottomAction>
    </>
  )
}