import type { LibraryPageData } from './types'

export const libraryData: LibraryPageData = {
  tabs: [
    { id: 'saved', label: 'Збережена музика' },
    { id: 'liked', label: 'Вподобані треки' },
    { id: 'artists', label: 'Артисти' },
    { id: 'playlists', label: 'Мої плейлісти' },
  ],
  stats: [
    { id: 'tracks', value: '44', label: 'Треків' },
    { id: 'playlists', value: '4', label: 'Мої плейлісти' },
    { id: 'artists', value: '11', label: 'Артистів' },
    { id: 'hours', value: '64 год', label: 'Прослухано' },
  ],
  saved: {
    sectionTitle: 'Збережена музика',
    savedCollections: [
      {
        id: 'playlist',
        title: 'Плейлісти',
        subtitle: '12 збережено',
        icon: 'playlist',
      },
      {
        id: 'album',
        title: 'Альбоми',
        subtitle: '8 збережено',
        icon: 'album',
      },
      {
        id: 'podcast',
        title: 'Подкасти',
        subtitle: '1 збережено',
        icon: 'podcast',
      },
      {
        id: 'download',
        title: 'Завантажено',
        subtitle: '11 треків',
        icon: 'download',
      },
    ],
    recentPlaylists: [
      {
        id: 'chill-out',
        title: 'Чіл-аут вечір',
        author: 'GROOV',
        tracksCount: 22,
        cover:
          'https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?auto=format&fit=crop&w=800&q=80',
      },
      {
        id: 'ukrainian-wave',
        title: 'Українська хвиля',
        author: 'GROOV',
        tracksCount: 77,
        cover:
          'https://images.unsplash.com/photo-1500382017468-9049fed747ef?auto=format&fit=crop&w=800&q=80',
      },
      {
        id: 'training',
        title: 'Тренування',
        author: 'GROOV',
        tracksCount: 22,
        cover:
          'https://images.unsplash.com/photo-1517836357463-d25dfeac3438?auto=format&fit=crop&w=800&q=80',
      },
      {
        id: 'more',
        title: 'Більше',
        author: '',
        tracksCount: 0,
        isMoreCard: true,
      },
    ],
  },
  liked: {
    sectionTitle: 'Вподобані треки',
    tracks: [
      {
        id: 'motion',
        title: 'MOTION',
        artist: 'Nemzzz',
        duration: '02:22',
        cover: 'https://i.scdn.co/image/ab67616d00001e02e3b0d4e4c8ad7e6d7f4f5c28',
        isLiked: true,
      },
      {
        id: 'sorry',
        title: 'SORRY',
        artist: 'Nemzzz',
        duration: '01:40',
        cover: 'https://i.scdn.co/image/ab67616d00001e02e3b0d4e4c8ad7e6d7f4f5c28',
        isLiked: true,
      },
      {
        id: 'sprinter',
        title: 'Sprinter',
        artist: 'Dave i Central Cee',
        duration: '03:50',
        cover: 'https://i.scdn.co/image/ab67616d00001e028d7b3b6b5df4d2f6f7c84d8b',
        isLiked: true,
      },
      {
        id: 'did-it-first',
        title: 'Did It First',
        artist: 'Ice Spice i Central Cee',
        duration: '01:59',
        cover: 'https://i.scdn.co/image/ab67616d00001e02820b8f2d5f2c858447f0c4e5',
        isLiked: true,
      },
      {
        id: 'which-one',
        title: 'Which One',
        artist: 'Central Cee i Drake',
        duration: '02:50',
        cover: 'https://i.scdn.co/image/ab67616d00001e029d3f0b0fd3c1bd5f0fdb59bd',
        isLiked: true,
      },
      {
        id: 'darkness',
        title: 'InThisDarkness',
        artist: '2YOUNG',
        duration: '02:50',
        cover: 'https://i.scdn.co/image/ab67616d00001e02dc5f0ec16a3a836b6b5480a8',
        isLiked: true,
      },
      {
        id: 'mm3',
        title: 'MM3',
        artist: 'SoFaygo',
        duration: '02:55',
        cover: 'https://i.scdn.co/image/ab67616d00001e02ef4e4dc6d8cb12c325a4e497',
        isLiked: true,
      },
    ],
  },
  artists: {
    title: 'Слідкування за артистами',
    items: [
      {
        id: 'sh0tzi',
        name: 'sh0tzi',
        followers: '470 підписників',
        image: 'https://images.unsplash.com/photo-1516280440614-37939bbacd81?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'playboi-carti',
        name: 'Playboi Carti',
        followers: '71,6млн. підписників',
        image: 'https://images.unsplash.com/photo-1501386761578-eac5c94b800a?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'ken-carson',
        name: 'Ken Carson',
        followers: '8млн. підписників',
        image: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'kairo-keyz',
        name: 'Kairo Keyz',
        followers: '1,15млн. підписників',
        image: 'https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'drake',
        name: 'Drake',
        followers: '126млн. підписників',
        image: 'https://images.unsplash.com/photo-1504593811423-6dd665756598?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'sexyy-red',
        name: 'Sexyy Red',
        followers: '21,2млн. підписників',
        image: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'metro-boomin',
        name: 'Metro Boomin',
        followers: '352 млн. підписників',
        image: 'https://images.unsplash.com/photo-1507591064344-4c6ce005b128?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'weeknd',
        name: 'The Weeknd',
        followers: '266 млн. підписників',
        image: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
      {
        id: 'joji',
        name: 'Joji',
        followers: '24,2 тис. підписників',
        image: 'https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?auto=format&fit=crop&w=600&q=80',
        isFollowing: true,
      },
    ],
  },
  playlists: {
    title: 'Мої плейлісти',
    items: [
      {
        id: 'walk',
        title: 'Walk',
        author: '@andrii_koval',
        tracksCount: 23,
        cover:
          'https://images.unsplash.com/photo-1517841905240-472988babdf9?auto=format&fit=crop&w=800&q=80',
      },
      {
        id: 'mix-1',
        title: 'Мій мікс #1',
        author: '@andrii_koval',
        tracksCount: 16,
        accentColor: '#A68AD0',
      },
      {
        id: 'real-life',
        title: 'REal life',
        author: '@andrii_koval',
        tracksCount: 11,
        cover:
          'https://images.unsplash.com/photo-1493246507139-91e8fad9978e?auto=format&fit=crop&w=800&q=80',
      },
      {
        id: 'mix-2',
        title: 'Мій мікс #2',
        author: '@andrii_koval',
        tracksCount: 118,
        cover:
          'https://images.unsplash.com/photo-1571266028243-d220c9a45820?auto=format&fit=crop&w=800&q=80',
      },
    ],
  },
}