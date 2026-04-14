'use client'

import Image from 'next/image'
import { useRouter } from 'next/navigation'
import { useMemo } from 'react'
import { useAuthStore } from '@/shared/stores/auth.store'
import { ProfileCompassIcon } from '@/shared/ui/icons/profile/ProfileCompassIcon'
import { ProfileHeartIcon } from '@/shared/ui/icons/profile/ProfileHeartIcon'
import { ProfileCalendarIcon } from '@/shared/ui/icons/profile/ProfileCalendarIcon'
import { ProfileSettingsIcon } from '@/shared/ui/icons/profile/ProfileSettingsIcon'

import {
  profileDiscoveryOfMonth,
  profileFavoriteArtists,
} from '../model/profile.mock'

function toUsername(displayName: string) {
  return (
    '@' +
    displayName
      .trim()
      .toLowerCase()
      .replace(/\s+/g, '_')
      .replace(/[^a-zа-яіїєґ0-9_]/gi, '')
  )
}

function getInitials(name: string) {
  const parts = name.trim().split(/\s+/)
  const first = parts[0]?.[0] ?? 'U'
  const second = parts[1]?.[0] ?? ''
  return `${first}${second}`.toUpperCase()
}

function ProfileStatCard({
  value,
  label,
}: {
  value: string
  label: string
}) {
  return (
    <div className="flex h-[72px] flex-1 flex-col items-center justify-center rounded-[12px] bg-groov-primary text-center">
      
      <div className="text-[24px] leading-[24px]  ">
        {value}
      </div>

      <div className="mt-[6px] text-[15px] leading-[14px]  ">
        {label}
      </div>

    </div>
  )
}

function ProfileSectionHeader({
  icon,
  title,
  action,
}: {
  icon: React.ReactNode
  title: string
  action?: string
}) {
  return (
    <div className="flex items-center justify-between rounded-[10px] px-[10px] py-[8px]">
      <div className="flex items-center gap-[10px]">
        <div className="flex h-[32px] w-[32px] items-center justify-center rounded-[10px] bg-groov-accent">
          {icon}
        </div>

        <div className="text-[16px] font-semibold leading-[16px]  ">
          {title}
        </div>
      </div>

      {action ? (
        <button
          type="button"
          className="text-[12px] leading-[12px] text-groov-secondary"
        >
          {action}
        </button>
      ) : null}
    </div>
  )
}

function FavoriteArtistCard({
  name,
  genre,
  image,
}: {
  name: string
  genre: string
  image: string
}) {
  return (
    <div className="rounded-[12px] bg-groov-accent px-[8px] pb-[10px] pt-[8px]">
      <div className="relative mx-auto h-[60px] w-[60px] overflow-hidden rounded-[12px]">
        <Image src={image} alt={name} fill className="object-cover" />
      </div>

      <div className="mt-[10px] text-center text-[14px] font-medium leading-[16px] text-groov-textDark">
        {name}
      </div>

      <div className="mt-[4px] text-center text-[12px]  leading-[14px] text-groov-textDark/80">
        {genre}
      </div>
    </div>
  )
}

export function ProfileScreen() {
  const displayNameFromStore = useAuthStore((s) => s.displayName)
const router = useRouter()
  const displayName = displayNameFromStore?.trim() || 'Користувач'
  const username = useMemo(() => toUsername(displayName), [displayName])
  const initials = useMemo(() => getInitials(displayName), [displayName])

  const bio =
    'Музичний ентузіаст | Люблю відкривати нові звуки | Створюю плейлісти для кожного настрою'

  return (
    <div className="mx-auto min-h-dvh w-full max-w-[402px] bg-groov-bg">
      <div className="relative">
        <div className="relative h-[206px] w-full overflow-hidden">
          <Image
            src="/profile-cover.jpg"
            alt="Profile cover"
            fill
            className="object-cover"
            priority
          />

          <div className="absolute inset-0 bg-groov-bg/10" />

          <button
  type="button"
  onClick={() => router.push('/settings')}
  className="absolute right-[16px] top-[max(env(safe-area-inset-top),20px)] rounded-full"
  aria-label="Налаштування"
>
<div className="flex h-[44px] w-[44px] items-center justify-center rounded-full bg-groov-surface">
  <ProfileSettingsIcon className="h-[28px] w-[28px] text-groov-accent" />
</div></button>
        </div>

        <div className="absolute left-1/2 top-[131px] h-[150px] w-[150px] -translate-x-1/2 overflow-hidden rounded-full border-[4px] border-groov-surface bg-groov-accent shadow-[0_8px_24px_rgba(0,0,0,0.28)]">
          <Image
            src="/profile-avatar1.png"
            alt={displayName}
            fill
            className="object-cover"
          />
          <div className="hidden h-full w-full items-center justify-center bg-gradient-to-br from-groov-secondary to-groov-primary text-[30px] font-bold  ">
            {initials}
          </div>
        </div>
      </div>

<div className="px-[20px] pt-[90px]">
            <div className="text-center">
          <div className="text-[24px] font-semibold leading-[22px]  ">
            {displayName}
          </div>

          <div className="mt-[8px] text-[16px] leading-[16px] text-groov-muted">
            {username}
          </div>

          <p className="mx-auto mt-[16px] max-w-[370px] text-center text-[16px] font-normal leading-[16px] tracking-[0px]  ">
            {bio}
          </p>
        </div>

        <div className="mt-[22px] flex gap-[8px]">
          <ProfileStatCard value="4" label="Відкриттів" />
          <ProfileStatCard value="2" label="Підписників" />
          <ProfileStatCard value="64" label="Годин" />
        </div>

        <button
          type="button"
          className="mt-[12px] flex h-[40px] w-full items-center justify-center rounded-[10px] bg-groov-surface text-[16px] font-medium leading-[14px]  "
        >
          Поділитися профілем
        </button>

        <div className="mt-[16px] rounded-[14px] bg-groov-surface p-[8px]">
          <ProfileSectionHeader
            icon={<ProfileCompassIcon />}
            title="Відкриття місяця"
            action="Дивитися всі"
          />

          <div className="mt-[8px] flex gap-[10px] rounded-[12px] bg-groov-primary p-[8px]">
            <div className="relative h-[88px] w-[88px] shrink-0 overflow-hidden rounded-[10px]">
              <Image
                src={profileDiscoveryOfMonth.image}
                alt={profileDiscoveryOfMonth.title}
                fill
                className="object-cover"
              />
            </div>

            <div className="min-w-0 flex-1">
              <div className="text-[16px] font-medium leading-[20px]  ">
                {profileDiscoveryOfMonth.title}
              </div>

              <div className="mt-[4px] text-[14px] font-medium leading-[14px]  ">
                {profileDiscoveryOfMonth.artist}
              </div>

              <div className="mt-[10px] flex items-center gap-[8px] text-[14px] leading-[14px]  /90">
                <ProfileCalendarIcon />
                <span>{profileDiscoveryOfMonth.discoveredAt}</span>
              </div>
            </div>
          </div>
        </div>

        <div className="mt-[14px] rounded-[14px] bg-groov-surface p-[8px]">
          <ProfileSectionHeader
            icon={<ProfileHeartIcon />}
            title="Улюблені артисти"
          />

          <div className="mt-[8px] grid grid-cols-3 gap-[8px]">
            {profileFavoriteArtists.map((artist) => (
              <FavoriteArtistCard
                key={artist.id}
                name={artist.name}
                genre={artist.genre}
                image={artist.image}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}