import { filterDetailsMap } from './filter-details.mock'

export type PlaylistTrack = {
  id: string
  title: string
  artist: string
  image: string
}

export type PlaylistDetails = {
  id: string
  title: string
  description: string
  author: string
  year: string
  tracksCount: number
  cover: string
  tracks: PlaylistTrack[]
}

const defaultCover = '/Weeknd.png'

function createTrack(
  id: string,
  title: string,
  artist: string,
  image = defaultCover
): PlaylistTrack {
  return { id, title, artist, image }
}

function getGenreFromPlaylistId(playlistId: string) {
  const parts = playlistId.split('-')
  parts.pop()
  return parts.join('-')
}

function buildTracksByGenre(
  genre: string,
  cover: string
): PlaylistTrack[] {
  switch (genre) {
    case 'classic':
      return [
        createTrack('t1', 'Prelude in C Major, BWV 846', 'Йоганн Себастьян Бах', cover),
        createTrack('t2', 'Metamorphosis Two', 'Філіп Гласс', cover),
        createTrack('t3', "Vladimir's Blues", 'Макс Ріхтер', cover),
        createTrack('t4', 'Piano Sonata No. 16 in C Major, K. 545', 'Вольфганг Амадей Моцарт', cover),
        createTrack('t5', 'Gnossienne No. 1', 'Ерік Саті', cover),
        createTrack('t6', 'Goldberg Variations, BWV 988: Aria', 'Йоганн Себастьян Бах', cover),
        createTrack('t7', 'Spiegel im Spiegel', 'Арво Пярт', cover),
        createTrack('t8', 'Moonlight Sonata', 'Людвіг ван Бетховен', cover),
        createTrack('t9', 'The Four Seasons, "Winter"', 'Антоніо Вівальді', cover),
        createTrack('t10', 'Rêverie', 'Клод Дебюссі', cover),
      ]

    case 'rock':
    case 'metal':
    case 'ua-rock':
      return [
        createTrack('t1', 'Bohemian Rhapsody', 'Queen', cover),
        createTrack('t2', 'Smells Like Teen Spirit', 'Nirvana', cover),
        createTrack('t3', 'Numb', 'Linkin Park', cover),
        createTrack('t4', 'Back In Black', 'AC/DC', cover),
        createTrack('t5', 'Nothing Else Matters', 'Metallica', cover),
        createTrack('t6', 'Bring Me To Life', 'Evanescence', cover),
        createTrack('t7', 'Kryptonite', '3 Doors Down', cover),
        createTrack('t8', 'Highway to Hell', 'AC/DC', cover),
        createTrack('t9', 'Paranoid', 'Black Sabbath', cover),
        createTrack('t10', 'The Pretender', 'Foo Fighters', cover),
      ]

    case 'jazz':
      return [
        createTrack('t1', 'Take Five', 'Dave Brubeck', cover),
        createTrack('t2', 'So What', 'Miles Davis', cover),
        createTrack('t3', 'Autumn Leaves', 'Chet Baker', cover),
        createTrack('t4', 'Blue in Green', 'Bill Evans', cover),
        createTrack('t5', 'My Funny Valentine', 'Chet Baker', cover),
        createTrack('t6', 'Round Midnight', 'Thelonious Monk', cover),
        createTrack('t7', 'Fly Me to the Moon', 'Frank Sinatra', cover),
        createTrack('t8', 'A Night in Tunisia', 'Dizzy Gillespie', cover),
        createTrack('t9', 'Take the A Train', 'Duke Ellington', cover),
        createTrack('t10', 'Summertime', 'Ella Fitzgerald & Louis Armstrong', cover),
      ]

    case 'pop':
    case 'kpop':
    case 'ua-pop':
      return [
        createTrack('t1', 'Blinding Lights', 'The Weeknd', cover),
        createTrack('t2', 'Levitating', 'Dua Lipa', cover),
        createTrack('t3', 'As It Was', 'Harry Styles', cover),
        createTrack('t4', 'Don’t Start Now', 'Dua Lipa', cover),
        createTrack('t5', 'Style', 'Taylor Swift', cover),
        createTrack('t6', 'Shivers', 'Ed Sheeran', cover),
        createTrack('t7', 'Bad Romance', 'Lady Gaga', cover),
        createTrack('t8', 'Firework', 'Katy Perry', cover),
        createTrack('t9', 'Watermelon Sugar', 'Harry Styles', cover),
        createTrack('t10', 'Save Your Tears', 'The Weeknd', cover),
      ]

    case 'hip-hop':
    case 'rap':
      return [
        createTrack('t1', 'SICKO MODE', 'Travis Scott', cover),
        createTrack('t2', 'HUMBLE.', 'Kendrick Lamar', cover),
        createTrack('t3', 'God’s Plan', 'Drake', cover),
        createTrack('t4', 'Praise The Lord', 'A$AP Rocky', cover),
        createTrack('t5', 'Fe!n', 'Travis Scott', cover),
        createTrack('t6', 'Lucid Dreams', 'Juice WRLD', cover),
        createTrack('t7', 'Lose Yourself', 'Eminem', cover),
        createTrack('t8', 'Goosebumps', 'Travis Scott', cover),
        createTrack('t9', 'All The Stars', 'Kendrick Lamar & SZA', cover),
        createTrack('t10', 'Star Walkin’', 'Lil Nas X', cover),
      ]

    case 'sleep':
    case 'relax':
    case 'focus':
      return [
        createTrack('t1', 'Clair de Lune', 'Клод Дебюссі', cover),
        createTrack('t2', 'Gymnopédie No. 1', 'Ерік Саті', cover),
        createTrack('t3', 'Nocturne in E-flat Major', 'Фредерік Шопен', cover),
        createTrack('t4', 'Pavane', 'Габріель Форе', cover),
        createTrack('t5', 'Arabesque No. 1', 'Клод Дебюссі', cover),
        createTrack('t6', 'The Swan', 'Каміль Сен-Санс', cover),
        createTrack('t7', 'Canon in D', 'Йоганн Пахельбель', cover),
        createTrack('t8', 'Meditation', 'Жюль Массне', cover),
        createTrack('t9', 'Arioso', 'Йоганн Себастьян Бах', cover),
        createTrack('t10', 'Moonlight Sonata', 'Людвіг ван Бетховен', cover),
      ]

    case 'rnb':
      return [
        createTrack('t1', 'Earned It', 'The Weeknd', cover),
        createTrack('t2', 'Snooze', 'SZA', cover),
        createTrack('t3', 'We Belong Together', 'Mariah Carey', cover),
        createTrack('t4', 'Adorn', 'Miguel', cover),
        createTrack('t5', 'Thinkin Bout You', 'Frank Ocean', cover),
        createTrack('t6', 'CUFF IT', 'Beyoncé', cover),
        createTrack('t7', 'Best Part', 'Daniel Caesar & H.E.R.', cover),
        createTrack('t8', 'Location', 'Khalid', cover),
        createTrack('t9', 'Die For You', 'The Weeknd', cover),
        createTrack('t10', 'Good Days', 'SZA', cover),
      ]

    default:
      return [
        createTrack('t1', 'Timeless', 'The Weeknd і Playboi Carti', cover),
        createTrack('t2', 'Starboy', 'The Weeknd', cover),
        createTrack('t3', 'After Hours', 'The Weeknd', cover),
        createTrack('t4', 'Blinding Lights', 'The Weeknd', cover),
        createTrack('t5', 'Die For You', 'The Weeknd', cover),
        createTrack('t6', 'Save Your Tears', 'The Weeknd', cover),
        createTrack('t7', 'Less Than Zero', 'The Weeknd', cover),
        createTrack('t8', 'Reminder', 'The Weeknd', cover),
        createTrack('t9', 'I Was Never There', 'The Weeknd', cover),
        createTrack('t10', 'Call Out My Name', 'The Weeknd', cover),
      ]
  }
}

function findPlaylistCard(playlistId: string) {
  for (const genre of Object.values(filterDetailsMap)) {
    for (const section of genre.sections) {
      const foundItem = section.items.find((item) => item.id === playlistId)
      if (foundItem) {
        return foundItem
      }
    }
  }

  return null
}

export function getPlaylistDetails(playlistId: string): PlaylistDetails | null {
  const item = findPlaylistCard(playlistId)

  if (!item) {
    return null
  }

  const genre = getGenreFromPlaylistId(item.id)
  const tracks = buildTracksByGenre(genre, item.image)

  return {
    id: item.id,
    title: item.title,
    description:
      item.subtitle ||
      'Добірка, автоматично сформована на основі жанру та настрою.',
    author: 'GROOV',
    year: '2025',
    tracksCount: tracks.length,
    cover: item.image || defaultCover,
    tracks,
  }
}