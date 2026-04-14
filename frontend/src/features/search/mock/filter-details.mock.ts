export type FilterSectionItem = {
  id: string
  title: string
  subtitle: string
  image: string
}

export type FilterSection = {
  id: string
  title: string
  items: FilterSectionItem[]
}

export type FilterDetails = {
  title: string
  sections: FilterSection[]
}

const defaultCover = '/Weeknd.png'

function createItem(
  id: string,
  title: string,
  subtitle: string,
  image = defaultCover
): FilterSectionItem {
  return { id, title, subtitle, image }
}

function createDefaultGenreSections(
  prefix: string,
  genreTitle: string
): FilterSection[] {
  return [
    {
      id: `${prefix}-songs`,
      title: `${genreTitle} пісні`,
      items: [
        createItem(
          `${prefix}-1`,
          `${genreTitle} Vibes`,
          `Найкращі пісні жанру ${genreTitle.toLowerCase()}.`
        ),
        createItem(
          `${prefix}-2`,
          `Best of ${genreTitle}`,
          `Добірка популярних треків у стилі ${genreTitle.toLowerCase()}.`
        ),
        createItem(
          `${prefix}-3`,
          `${genreTitle} Essentials`,
          `Основні треки та звучання жанру ${genreTitle.toLowerCase()}.`
        ),
      ],
    },
    {
      id: `${prefix}-for-you-small`,
      title: `${genreTitle} пісні для вас`,
      items: [
        createItem(
          `${prefix}-4`,
          `${genreTitle} Моменти`,
          `Персональна добірка треків у жанрі ${genreTitle.toLowerCase()}.`
        ),
        createItem(
          `${prefix}-5`,
          `${genreTitle} Потік`,
          `Музика для занурення в атмосферу ${genreTitle.toLowerCase()}.`
        ),
        createItem(
          `${prefix}-6`,
          `${genreTitle} Колекція`,
          `Підібрані композиції для щоденного прослуховування.`
        ),
      ],
    },
    {
      id: `${prefix}-for-you-wide`,
      title: `${genreTitle} для вас`,
      items: [
        createItem(
          `${prefix}-7`,
          `${genreTitle} Настрій`,
          `Добірка, що найкраще передає емоції жанру ${genreTitle.toLowerCase()}.`
        ),
        createItem(
          `${prefix}-8`,
          `${genreTitle} Простір`,
          `Звучання, яке допомагає зосередитись на відчуттях і ритмі.`
        ),
        createItem(
          `${prefix}-9`,
          `${genreTitle} Хвиля`,
          `Музичний потік для глибшого занурення в жанр.`
        ),
      ],
    },
    {
      id: `${prefix}-relax-wide`,
      title: `${genreTitle} для розслаблення`,
      items: [
        createItem(
          `${prefix}-10`,
          `${genreTitle} Relax`,
          `М’які композиції та спокійна атмосфера для відпочинку.`
        ),
        createItem(
          `${prefix}-11`,
          `${genreTitle} Вечір`,
          `Спокійний настрій і плавне звучання для вечірнього часу.`
        ),
        createItem(
          `${prefix}-12`,
          `${genreTitle} Ніч`,
          `Тиха добірка для повільного та комфортного прослуховування.`
        ),
      ],
    },
    {
      id: `${prefix}-albums`,
      title: 'Альбоми',
      items: [
        createItem(
          `${prefix}-13`,
          `${genreTitle} Album One`,
          `Збірка популярних композицій жанру.`
        ),
        createItem(
          `${prefix}-14`,
          `${genreTitle} Album Two`,
          `Альбом із виразним настроєм і характером.`
        ),
        createItem(
          `${prefix}-15`,
          `${genreTitle} Album Three`,
          `Один із найцікавіших релізів у цьому жанрі.`
        ),
      ],
    },
  ]
}

export const filterDetailsMap: Record<string, FilterDetails> = {
  classic: {
    title: 'Класика',
    sections: [
      {
        id: 'classic-songs',
        title: 'Класичні пісні',
        items: [
          createItem(
            'classic-1',
            "Amélie - Comptine d’un autre été...",
            'André Vanzzo'
          ),
          createItem('classic-2', 'Perfect Symphony', 'Ed Sheeran'),
          createItem('classic-3', 'Clair de Lune', 'Tony Ann'),
        ],
      },
      {
        id: 'classic-for-you-small',
        title: 'Класичні пісні для вас',
        items: [
          createItem('classic-4', 'La Rive Gauche', 'Virginio Aiello'),
          createItem('classic-5', 'Idea 7', 'Gibran Alcocer'),
          createItem('classic-6', 'Color Noise', 'Akane'),
        ],
      },
      {
        id: 'classic-for-you-wide',
        title: 'Класика для вас',
        items: [
          createItem(
            'classic-7',
            'Архітектура Думок',
            'Математична точність Баха та структура бароко для вашої продуктивності.'
          ),
          createItem(
            'classic-8',
            'Метод Моцарта',
            'Світла класика, що активує когнітивні процеси.'
          ),
          createItem(
            'classic-9',
            'Оксамитова Ніч',
            'Найніжніші ноктюрни Шопена та колискові для вечірнього спокою.'
          ),
        ],
      },
      {
        id: 'classic-relax-wide',
        title: 'Класика для розслаблення',
        items: [
          createItem(
            'classic-10',
            'Сон у літню ніч',
            'Легкі оркестрові текстури та мрійливі мелодії для вечірнього спокою.'
          ),
          createItem(
            'classic-11',
            'Акварелі Дебюссі',
            'Музичний імпресіонізм, що розчиняє тривоги, ...'
          ),
          createItem(
            'classic-12',
            'Ефірний Спокій',
            'Адажіо та камерні твори, що створюють затишок.'
          ),
        ],
      },
      {
        id: 'classic-albums',
        title: 'Альбоми',
        items: [
          createItem('classic-13', 'In A Time Lapse', 'Ludovico Einaudi'),
          createItem('classic-14', 'Immaterial', 'Albion · Adrían Berenguer'),
          createItem('classic-15', 'Victory', 'Various Artists'),
        ],
      },
    ],
  },

  pop: {
    title: 'Поп',
    sections: createDefaultGenreSections('pop', 'Поп'),
  },

  rock: {
    title: 'Рок',
    sections: createDefaultGenreSections('rock', 'Рок'),
  },

  jazz: {
    title: 'Джаз',
    sections: createDefaultGenreSections('jazz', 'Джаз'),
  },

  'hip-hop': {
    title: 'Хіп-хоп',
    sections: createDefaultGenreSections('hip-hop', 'Хіп-хоп'),
  },

  rap: {
    title: 'Реп',
    sections: createDefaultGenreSections('rap', 'Реп'),
  },

  electronic: {
    title: 'Електронна',
    sections: createDefaultGenreSections('electronic', 'Електронна музика'),
  },

  indie: {
    title: 'Інді',
    sections: createDefaultGenreSections('indie', 'Інді'),
  },

  'lo-fi': {
    title: 'Lo-fi',
    sections: createDefaultGenreSections('lo-fi', 'Lo-fi'),
  },

  romance: {
    title: 'Романтика',
    sections: createDefaultGenreSections('romance', 'Романтика'),
  },

  sleep: {
    title: 'Для сну',
    sections: createDefaultGenreSections('sleep', 'Музика для сну'),
  },

  workout: {
    title: 'Для тренувань',
    sections: createDefaultGenreSections('workout', 'Тренування'),
  },

  study: {
    title: 'Для навчання',
    sections: createDefaultGenreSections('study', 'Навчання'),
  },

  relax: {
    title: 'Для розслаблення',
    sections: createDefaultGenreSections('relax', 'Розслаблення'),
  },

  new: {
    title: 'Нові релізи',
    sections: createDefaultGenreSections('new', 'Нові релізи'),
  },

  metal: {
    title: 'Метал',
    sections: createDefaultGenreSections('metal', 'Метал'),
  },

  rnb: {
    title: 'R&B та соул',
    sections: createDefaultGenreSections('rnb', 'R&B та соул'),
  },

  'ua-pop': {
    title: 'Український поп',
    sections: createDefaultGenreSections('ua-pop', 'Український поп'),
  },

  'ua-rock': {
    title: 'Український рок',
    sections: createDefaultGenreSections('ua-rock', 'Український рок'),
  },

  arabic: {
    title: 'Арабська музика',
    sections: createDefaultGenreSections('arabic', 'Арабська музика'),
  },

  african: {
    title: 'Африканська',
    sections: createDefaultGenreSections('african', 'Африканська музика'),
  },

  blues: {
    title: 'Блюз',
    sections: createDefaultGenreSections('blues', 'Блюз'),
  },

  bollywood: {
    title: 'Боллівуд',
    sections: createDefaultGenreSections('bollywood', 'Боллівуд'),
  },

  kids: {
    title: "Діти та сім'я",
    sections: createDefaultGenreSections('kids', "Діти та сім'я"),
  },

  focus: {
    title: 'Концентрація',
    sections: createDefaultGenreSections('focus', 'Концентрація'),
  },

  sad: {
    title: 'Сум',
    sections: createDefaultGenreSections('sad', 'Сум'),
  },

  travel: {
    title: 'В дорозі',
    sections: createDefaultGenreSections('travel', 'В дорозі'),
  },

  party: {
    title: 'Вечірка',
    sections: createDefaultGenreSections('party', 'Вечірка'),
  },

  happy: {
    title: 'Гарний настрій',
    sections: createDefaultGenreSections('happy', 'Гарний настрій'),
  },

  latin: {
    title: 'Латинська',
    sections: createDefaultGenreSections('latin', 'Латинська'),
  },

  podcasts: {
    title: 'Подкасти',
    sections: createDefaultGenreSections('podcasts', 'Подкасти'),
  },

  music: {
    title: 'Музика',
    sections: createDefaultGenreSections('music', 'Музика'),
  },

  gaming: {
    title: 'Ігри',
    sections: createDefaultGenreSections('gaming', 'Ігри'),
  },

  food: {
    title: 'Їжа',
    sections: createDefaultGenreSections('food', 'Їжа'),
  },

  kpop: {
    title: 'K-Pop',
    sections: createDefaultGenreSections('kpop', 'K-Pop'),
  },

  folk: {
    title: 'Фолк',
    sections: createDefaultGenreSections('folk', 'Фолк'),
  },

  soundtracks: {
    title: 'Саундтреки',
    sections: createDefaultGenreSections('soundtracks', 'Саундтреки'),
  },
}