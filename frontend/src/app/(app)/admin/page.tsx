'use client'

import { useState } from 'react'
import { AppShell } from '@/shared/ui/layout/AppShell'
import { AdminHeader } from '@/features/admin/ui/AdminHeader'
import { AdminStatsSection } from '@/features/admin/ui/AdminStatsSection'
import { AdminTabs } from '@/features/admin/ui/AdminTabs'
import { AdminContent } from '@/features/admin/ui/AdminContent'
import type { AdminTabId } from '@/features/admin/model/admin-tabs'

export default function AdminPage() {
  const [activeTab, setActiveTab] = useState<AdminTabId>('upload')

  return (
    <AppShell
      mobileMaxWidth={402}
      withDefaultPadding={false}
      contentClassName="pb-[24px]"
    >
      <div className="px-[16px] pt-[6px]">
        <AdminHeader />
        <AdminStatsSection />
        <AdminTabs activeTab={activeTab} onChange={setActiveTab} />
        <AdminContent activeTab={activeTab} onTabChange={setActiveTab} />
      </div>
    </AppShell>
  )
}