import { notFound } from 'next/navigation'
import { AppShell } from '@/shared/ui/layout/AppShell'
import { BottomNav } from '@/shared/ui/BottomNav'
import { BackButton } from '@/shared/ui/buttons/BackButton'
import { FilterSection } from '@/features/search/ui/FilterSection'
import { filterDetailsMap } from '@/features/search/mock/filter-details.mock'

type Props = {
  params: Promise<{
    slug: string
  }>
}

function getSectionVariant(title: string): 'default' | 'wide' {
  const normalized = title.toLowerCase()

  if (
    normalized.includes('для вас') &&
    !normalized.includes('пісні для вас')
  ) {
    return 'wide'
  }

  if (normalized.includes('для розслаблення')) {
    return 'wide'
  }

  return 'default'
}

export default async function FilterDetailsPage({ params }: Props) {
  const { slug } = await params
  const filter = filterDetailsMap[slug]

  if (!filter) {
    notFound()
  }

  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
    >
      <div className="px-4 pb-[16px] pt-[16px]">
        <div className="flex items-center gap-3 text-groov-accent">
          <BackButton
            fallbackHref="/search/filters"
            className="flex h-[26px] w-[26px] items-center justify-center"
          />

          <h1 className="line-clamp-1 text-[20px] font-semibold tracking-[-0.01em]">
            {filter.title}
          </h1>
        </div>
      </div>

      <div className="px-4 pb-[110px] pt-[2px]">
        {filter.sections.length > 0 ? (
          filter.sections.map((section) => (
            <FilterSection
              key={section.id}
              title={section.title}
              items={section.items}
              variant={getSectionVariant(section.title)}
            />
          ))
        ) : (
          <div className="mt-6 rounded-[16px] bg-groov-surface px-4 py-5 text-[14px] text-groov-muted">
            Поки що немає контенту для цієї категорії.
          </div>
        )}
      </div>

      <BottomNav />
    </AppShell>
  )
}