'use client'

import { useRef, useState } from 'react'
import { AdminUploadIcon } from '@/shared/ui/icons/settings/AdminUploadIcon'
import { AdminMusicNoteIcon } from '@/shared/ui/icons/settings/AdminMusicNoteIcon'
import { AdminUploadForm } from './AdminUploadForm'

const MAX_FILE_SIZE_MB = 50
const MAX_FILE_SIZE_BYTES = MAX_FILE_SIZE_MB * 1024 * 1024

export function AdminUploadSection() {
  const fileInputRef = useRef<HTMLInputElement | null>(null)

  const [audioFile, setAudioFile] = useState<File | null>(null)
  const [isDragOver, setIsDragOver] = useState(false)
  const [fileError, setFileError] = useState('')

  const validateAudioFile = (file: File) => {
    const isAudio =
      file.type.startsWith('audio/') ||
      /\.(mp3|wav|flac|m4a|aac)$/i.test(file.name)

    if (!isAudio) {
      throw new Error('Оберіть аудіофайл у форматі MP3, WAV, FLAC, M4A або AAC')
    }

    if (file.size > MAX_FILE_SIZE_BYTES) {
      throw new Error(`Файл має бути менше ${MAX_FILE_SIZE_MB}MB`)
    }
  }

  const handleSelectFile = (file: File | null) => {
    if (!file) return

    try {
      validateAudioFile(file)
      setAudioFile(file)
      setFileError('')
    } catch (error) {
      const message =
        error instanceof Error ? error.message : 'Не вдалося обрати файл'
      setFileError(message)
      setAudioFile(null)

      if (fileInputRef.current) {
        fileInputRef.current.value = ''
      }
    }
  }

  const handleClearFile = () => {
    setAudioFile(null)
    setFileError('')

    if (fileInputRef.current) {
      fileInputRef.current.value = ''
    }
  }

  return (
    <>
      <input
        ref={fileInputRef}
        type="file"
        accept=".mp3,.wav,.flac,.m4a,.aac,audio/*"
        className="hidden"
        onChange={(e) => {
          const file = e.target.files?.[0] ?? null
          handleSelectFile(file)
        }}
      />

      <div className="mt-[18px] flex items-center gap-[12px]">
        <div className="flex h-[34px] w-[34px] shrink-0 items-center justify-center rounded-[10px] bg-groov-surface text-groov-accent">
          <AdminUploadIcon className="h-[24px] w-[24px]" />
        </div>

        <h2 className="text-[18px] font-medium leading-[22px] text-groov-accent">
          Завантажте свій перший трек
        </h2>
      </div>

      <button
        type="button"
        onClick={() => fileInputRef.current?.click()}
        onDragOver={(e) => {
          e.preventDefault()
          setIsDragOver(true)
        }}
        onDragLeave={(e) => {
          e.preventDefault()
          setIsDragOver(false)
        }}
        onDrop={(e) => {
          e.preventDefault()
          setIsDragOver(false)

          const file = e.dataTransfer.files?.[0] ?? null
          handleSelectFile(file)
        }}
        className={[
          'mt-[16px] w-full rounded-[14px] bg-groov-surface px-[16px] py-[34px] text-center transition-all',
          isDragOver ? 'ring-2 ring-groov-accent/40' : '',
        ].join(' ')}
      >
        <div className="flex justify-center text-groov-accent">
          <AdminMusicNoteIcon className="h-[58px] w-[58px]" />
        </div>

        <p className="mt-[22px] text-[16px] leading-[19px] text-groov-accent">
          {audioFile ? 'Аудіофайл обрано' : 'Перетягніть аудіофайл сюди'}
        </p>

        <p className="mt-[8px] text-[14px] leading-[17px] text-groov-accent/90">
          {audioFile ? audioFile.name : 'MP3, WAV, FLAC до 50MB'}
        </p>
      </button>

      {fileError ? (
        <p className="mt-[8px] text-center text-[12px] leading-[14px] text-red-400">
          {fileError}
        </p>
      ) : null}

      <AdminUploadForm
        audioFile={audioFile}
        onClearAudioFile={handleClearFile}
      />
    </>
  )
}