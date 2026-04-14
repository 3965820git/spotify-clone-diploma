'use client'

import type { AdminTabId } from '@/features/admin/model/admin-tabs'
import { adminTracksMock } from '@/features/admin/api/admin.mock'
import { AdminUploadSection } from './AdminUploadSection'
import { AdminTracksSection } from './AdminTracksSection'
import { AdminUsersSection } from './AdminUsersSection'
import { AdminAnalyticsSection } from './AdminAnalyticsSection'

type Props = {
  activeTab: AdminTabId
  onTabChange: (tab: AdminTabId) => void
}

export function AdminContent({ activeTab, onTabChange }: Props) {
  if (activeTab === 'tracks') {
    return (
      <AdminTracksSection
        onUploadClick={() => onTabChange('upload')}
      />
    )
  }

  if (activeTab === 'users') {
    return <AdminUsersSection />
  }

  if (activeTab === 'analytics') {
    return <AdminAnalyticsSection />
  }

  return <AdminUploadSection />
}