export const adminTabs = [
  { id: 'upload', label: 'Завантаження' },
  { id: 'tracks', label: 'Треки' },
  { id: 'users', label: 'Користувачі' },
  { id: 'analytics', label: 'Аналітика' },
] as const

export type AdminTabId = (typeof adminTabs)[number]['id']