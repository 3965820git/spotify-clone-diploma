import { AppShell } from '@/shared/ui/layout/AppShell'
import { BackButton } from '@/shared/ui/buttons/BackButton'
import { SearchIcon } from '@/shared/ui/icons/settings/SearchIcon'
import { settingsItems, themeOptions, dangerItems } from '@/features/settings/model/settings.mock'
import { SettingsListItem } from '@/features/settings/ui/SettingsListItem'
import { ThemeOptionCard } from '@/features/settings/ui/ThemeOptionCard'
import { ChevronDownIcon } from '@/shared/ui/icons/ChevronDownIcon'

export default function SettingsPage() {
    
  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      contentClassName="pb-[24px]"
    >
      <div className="px-[16px] pt-[6px]">
        <div className="flex items-center gap-[12px]">
          <BackButton className="flex h-[24px] w-[24px] items-center justify-center p-0  " />

          <h1 className="text-[22px] font-medium leading-[26px]  ">
            Налаштування
          </h1>
        </div>

        <div className="mt-[14px]">
          <label className="relative block">
            <span className="pointer-events-none absolute left-[14px] top-1/2 -translate-y-1/2  ">
              <SearchIcon className="h-[20px] w-[20px]" />
            </span>

            <input
              type="text"
              placeholder="Пошук"
              className="h-[40px] w-full rounded-[10px] border-0 bg-groov-surface pl-[42px] pr-[14px] text-[14px] leading-[14px]   outline-none placeholder:text-[#C7D1E0]"
            />
          </label>
        </div>

        <div className="mt-[16px] rounded-[22px] bg-groov-surface px-[2px] py-[2px]">
          <div className="space-y-[10px]">
            {settingsItems.map((item) => (
              <SettingsListItem key={item.id} item={item} />
            ))}
          </div>
        </div>

        <section className="mt-[18px] rounded-[22px]  px-[12px] pb-[14px] pt-[12px]">
          <h2 className="text-[18px] font-medium leading-[22px]  ">
            Спеціальна персоналізація
          </h2>

          <div className="mt-[14px] flex gap-[10px]">
            {themeOptions.map((option) => (
              <ThemeOptionCard key={option.id} option={option} />
            ))}
          </div>

          <div className="mt-[16px]">
            <p className="text-[18px] font-medium leading-[22px]  ">
              Мова інтерфейсу
            </p>

            <button
              type="button"
              className="mt-[10px] flex h-[54px] w-full items-center justify-between rounded-[16px] bg-groov-accent  px-[16px] text-groov-textDark"
            >
              <span className="text-[16px] font-medium leading-[19px]">
                Українська
              </span>

<ChevronDownIcon className="h-[24px] w-[24px]" />            </button>
          </div>
        </section>

        <div className="mt-[14px] h-px w-full bg-groov-accent" />

        <section className="pt-[14px]">
          <h2 className="text-[22px] font-semibold leading-[26px]  ">
            Небезпечна зона
          </h2>

          <div className="mt-[14px] rounded-[22px]  bg-groov-surface px-[2px] py-[2px]">
            <div className="space-y-[10px]">
              {dangerItems.map((item) => (
                <SettingsListItem key={item.id} item={item} />
              ))}
            </div>
          </div>
        </section>
      </div>
    </AppShell>
  )
}