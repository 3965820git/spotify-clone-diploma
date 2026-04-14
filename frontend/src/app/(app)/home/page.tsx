'use client'

import { useAuthStore } from '@/shared/stores/auth.store'
import { BottomNav } from '@/shared/ui/BottomNav'
import { AppShell } from '@/shared/ui/layout/AppShell'
import { SectionBlock } from '@/shared/ui/layout/SectionBlock'
import { HomeTopBar } from '@/features/home/ui/HomeTopBar'
import { HomeHeroCard } from '@/features/home/ui/HomeHeroCard'
import { SectionHeader } from '@/features/home/ui/SectionHeader'
import { HScroll } from '@/features/home/ui/HScroll'
import { GridCards } from '@/features/home/ui/GridCards'
import { recently, forYou, albums } from '@/features/home/mock'

export default function HomePage() {
  const displayName = useAuthStore((s) => s.displayName)

  return (
    <AppShell withBottomNavSpacing>
      <HomeTopBar />

      <div className="mt-[26px]">
        <div className="w-full max-w-[370px] text-[18px] font-medium leading-[100%] text-groov-accent md:text-[20px]">
          Добрий вечір{displayName ? `, ${displayName}` : ''}
        </div>

        <div className="mt-[12px] w-full max-w-[370px] text-[16px] font-normal leading-[120%] text-groov-accent md:max-w-[520px]">
          Слухайте музику, яка налаштовує на потрібний лад
        </div>
      </div>

      <HomeHeroCard />

      {/* DESKTOP */}
      <div className="hidden lg:grid lg:mt-8 lg:grid-cols-[1.3fr_1fr] lg:gap-6">
        <div className="space-y-7">
          <SectionBlock className="mt-0">
            <SectionHeader title="Нещодавно слухали" href="/recent" />
<HScroll items={recently} hrefPrefix="/play" />
          </SectionBlock>

          <SectionBlock>
            <SectionHeader title="Популярні альбоми" href="/albums" />
           <HScroll items={albums} hrefPrefix="/album" variant="album" />
          </SectionBlock>
        </div>

        <div className="space-y-7">
          <SectionBlock className="mt-0">
            <SectionHeader title="Плейлісти для вас" href="/playlists" />
            <GridCards items={forYou.slice(0, 4)} hrefPrefix="/playlist" />
          </SectionBlock>
        </div>
      </div>

      {/* MOBILE */}
      <div className="lg:hidden">
        <SectionBlock>
          <SectionHeader title="Нещодавно слухали" href="/recent" />
<HScroll items={recently} />
        </SectionBlock>

        <SectionBlock>
          <SectionHeader title="Плейлісти для вас" href="/playlists" />
          <GridCards items={forYou.slice(0, 4)} hrefPrefix="/playlist" />
        </SectionBlock>

        <SectionBlock>
          <SectionHeader title="Популярні альбоми" href="/albums" />
         <HScroll items={albums} hrefPrefix="/album" variant="album" />
        </SectionBlock>
      </div>

      <BottomNav />
    </AppShell>
  )
}