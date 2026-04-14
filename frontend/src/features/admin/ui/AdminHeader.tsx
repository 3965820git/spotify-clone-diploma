import { BackButton } from '@/shared/ui/buttons/BackButton'
import { ProfileSettingsIcon } from '@/shared/ui/icons/profile/ProfileSettingsIcon'

export function AdminHeader() {
  return (
    <div className="flex items-center justify-between">
      <div className="flex min-w-0 items-center gap-[12px]">
        <div className="flex h-[24px] w-[24px] shrink-0 items-center justify-center">
          <BackButton className="flex h-[24px] w-[24px] items-center justify-center p-0" />
        </div>

        <h1 className="truncate text-[22px] font-medium leading-[26px] text-groov-accent">
          Адмін-панель
        </h1>
      </div>

      <button
        type="button"
        className="flex h-[24px] w-[24px] shrink-0 items-center justify-center text-groov-accent"
        aria-label="Налаштування адмін-панелі"
      >
        <ProfileSettingsIcon className="h-[24px] w-[24px] text-groov-accent" />
      </button>
    </div>
  )
}