export type FilterType = 'tracks' | 'artists' | 'albums'

export type SearchItemType = 'track' | 'artist' | 'album'

export type SearchItem = {
  id: string
  title: string
  subtitle: string
  type: SearchItemType
  image: string
}