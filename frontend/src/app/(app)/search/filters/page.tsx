'use client'

import { useRouter } from 'next/navigation'
import { AppShell } from '@/shared/ui/layout/AppShell'
import { BackButton } from '@/shared/ui/buttons/BackButton'
import { advancedFilters } from '@/features/search/mock/advanced-filters.mock'
import { AdvancedFiltersGrid } from '@/features/search/ui/AdvancedFiltersGrid'

const filterSlugMap: Record<string, string> = {
  'Нові релізи': 'new',
  'Класика': 'classic',
  'РОК': 'rock',
  'Хіп-хоп': 'hip-hop',
  'Метал': 'metal',
  'Поп': 'pop',
  'Джаз': 'jazz',
  'R&B та соул': 'rnb',
  'Український поп': 'ua-pop',
  'Український рок': 'ua-rock',
  'Арабська музика': 'arabic',
  'Африканська': 'african',
  'Блюз': 'blues',
  'Боллівуд': 'bollywood',
  "Діти та сім'я": 'kids',
  'Концентрація': 'focus',
  'Тренування': 'workout',
  'Сум': 'sad',
  'Сон': 'sleep',
  'Романтика': 'romance',
  'В дорозі': 'travel',
  'Вечірка': 'party',
  'Гарний настрій': 'happy',
  'Латинська': 'latin',
  'Релакс': 'relax',
  'Подкасти': 'podcasts',
  'Музика': 'music',
  'Ігри': 'gaming',
  'Їжа': 'food',
  'K-Pop': 'kpop',
  'Фолк': 'folk',
  'Саундтреки': 'soundtracks',
}

function toSlug(value: string) {
  return filterSlugMap[value] ?? value.toLowerCase().replace(/\s+/g, '-')
}

export default function SearchAdvancedFiltersPage() {
  const router = useRouter()

  const handleSelect = (value: string) => {
    router.push(`/search/filters/${toSlug(value)}`)
  }

  return (
    <AppShell 
    
      mobileMaxWidth={402}
      withDefaultPadding={false}
    >
      <div className="bg-groov-surface px-4 pb-[16px] pt-[16px]">
        <div className="flex items-center gap-3 text-groov-accent">
          <BackButton
            fallbackHref="/search"
            className="flex h-[26px] w-[26px] items-center justify-center text-groov-accent"
          />

          <h1 className="line-clamp-1 text-[20px] font-semibold tracking-[-0.01em] text-groov-accent">
            Розширені фільтри
          </h1>
        </div>
      </div>

      <div className="px-4 pb-[24px] pt-[18px]">
        <AdvancedFiltersGrid items={advancedFilters} onSelect={handleSelect} />
      </div>
    </AppShell>
  )
}