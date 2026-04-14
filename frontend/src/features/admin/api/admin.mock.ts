import type {
  AdminAnalyticsProgressItem,
  AdminAnalyticsSummary,
  AdminAnalyticsTipItem,
  AdminGenreOption,
  AdminStats,
  AdminTrackItem,
} from './admin.types'

export const adminStatsMock: AdminStats = {
  followers: 2,
  tracks: 0,
  listens: 0,
}

export const adminGenresMock: AdminGenreOption[] = [
  { id: '1', name: 'Pop' },
  { id: '2', name: 'Rock' },
  { id: '3', name: 'Jazz' },
  { id: '4', name: 'Classical' },
]

export const adminTracksMock: AdminTrackItem[] = []

export const adminAnalyticsSummaryMock: AdminAnalyticsSummary = {
  publishedTracks: 0,
  followers: 2,
  listens: 0,
  likes: 0,
}

export const adminAnalyticsProgressMock: AdminAnalyticsProgressItem[] = [
  {
    id: 'first-track',
    title: 'Завантаження першого треку',
    description: 'Завантажте перший трек, щоб розпочати',
    value: 0,
  },
  {
    id: 'first-followers',
    title: 'Перші 10 підписників',
    description: '2 з 10 підписників',
    value: 20,
  },
  {
    id: 'first-listens',
    title: 'Перші 100 прослуховувань',
    description: 'Публікуйте треки, щоб отримати прослуховування',
    value: 0,
  },
]

export const adminAnalyticsTipsMock: AdminAnalyticsTipItem[] = [
  {
    id: 'upload-track',
    title: 'Завантажте перший трек',
    description: 'Це найважливіший крок для артиста',
    icon: 'track',
  },
  {
    id: 'share-profile',
    title: 'Поділіться профілем',
    description: 'Запросіть друзів підписатися на вас',
    icon: 'share',
  },
]