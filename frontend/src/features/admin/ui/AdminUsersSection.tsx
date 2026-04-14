'use client'

import { AdminUsersIcon } from '@/shared/ui/icons/admin/AdminUsersIcon'
import { UserAddIcon } from '@/shared/ui/icons/admin/UserAddIcon'
import { SearchIcon } from '@/shared/ui/icons/settings/SearchIcon'
import { adminUsersMock } from '@/features/admin/api/admin.users.mock'

export function AdminUsersSection() {
  return (
    <div className="mt-[18px]">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-[12px]">
          <div className="flex h-[34px] w-[34px] items-center justify-center rounded-[10px] bg-groov-surface   ">
            <AdminUsersIcon className="h-[20px] w-[20px]" />
          </div>

          <h2 className="text-[18px] font-medium   ">
            Мої підписники
          </h2>
        </div>
      </div>

      <div className="mt-[14px] flex items-center gap-[10px] rounded-[12px] bg-groov-surface px-[14px] py-[12px]">
        <SearchIcon className="h-[18px] w-[18px] " />

        <input
          type="text"
          placeholder="Пошук підписників..."
          className="w-full bg-transparent text-[14px]  placeholder:text-groov-accent outline-none"
        />
      </div>

      <div className="mt-[14px] space-y-[12px]">
        {adminUsersMock.map((user) => (
          <div key={user.id} className="flex items-center justify-between">
            <div className="flex items-center gap-[12px]">
              <img
                src={user.avatar}
                alt={user.name}
className="h-[52px] w-[52px] rounded-[14px] object-cover"              />

              <div className="min-w-0">
                <p className="truncate text-[16px] font-medium   ">
                  {user.name}
                </p>

                <p className="text-[12px]   ">
                  Підписався {user.subscribedAt}
                </p>

                {!user.mutual && (
                  <span className="mt-[4px] inline-block rounded-[8px] bg-groov-primary px-[8px] py-[2px] text-[11px]   ">
                    Не взаємно
                  </span>
                )}
              </div>
            </div>

            <button
              type="button"
              className="flex h-[34px] w-[34px] shrink-0 items-center justify-center rounded-[12px] bg-groov-accent text-groov-textDark"
            >
              <UserAddIcon className="h-[24px] w-[24px]" />
            </button>
          </div>
        ))}
      </div>
    </div>
  )
}