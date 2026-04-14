export type Item = {
  id: string
  title: string
  subtitle?: string
  image?: string
  contextType?: 'album' | 'playlist'
  contextExternalId?: string | null
}

const logo = '/Weeknd.png'

export const recently: Item[] = [
  {
    id: 'ec94d33b-5031-4ba3-b406-6236c7397ab7',
    title: 'Intro',
    subtitle: 'Azahriah',
    image: logo,
    contextType: 'album',
    contextExternalId: '37dd539a-504a-44f4-962d-ed584d04b739',
  },
  {
    id: 'a4ecae60-d0fe-45f5-902f-9049ad3b5109',
    title: 'Pannonia',
    subtitle: 'Azahriah',
    image: logo,
    contextType: 'album',
    contextExternalId: 'b1ab29d4-8404-4132-a496-e614d5cfadbf',
  },
  {
    id: 'd3e5c288-1319-41af-adbd-879e53a61772',
    title: 'Very Long Track Title for Testing Purposes That Might Break Your CSS Layout If Not Handled Correctly',
    subtitle: 'YoungFly, Kesha, Manuel',
    image: logo,
    contextType: 'album',
    contextExternalId: '28493895-7e1a-4622-a64d-a3fa7f480c48',
  },
  {
    id: 'fbbeb23c-09ce-4e1a-b5dc-60d99aca3ac3',
    title: 'Deep Focus Session',
    subtitle: 'DESH',
    image: logo,
    contextType: 'album',
    contextExternalId: '041ef758-5d4c-4fb8-b05c-19790aceb540',
  },
  {
    id: '5d09be9a-599c-45dc-81a6-fa3aee58e8ef',
    title: 'Ret',
    subtitle: 'Azahriah, DESH',
    image: logo,
    contextType: 'album',
    contextExternalId: '296bded1-907d-4157-b12b-edfe000085b0',
  },
  {
    id: '217b1416-04ca-4fb2-9348-c7df190e3c98',
    title: 'Track Number 2',
    subtitle: 'YoungFly',
    image: logo,
    contextType: 'album',
    contextExternalId: '28493895-7e1a-4622-a64d-a3fa7f480c48',
  },
  {
    id: 'f1a5ef1d-00cc-4068-8583-e6614e879c9f',
    title: 'Track Number 3',
    subtitle: 'Manuel',
    image: logo,
    contextType: 'album',
    contextExternalId: '37dd539a-504a-44f4-962d-ed584d04b739',
  },
  {
    id: '573ad79e-663c-4ebb-9a89-297f17998b90',
    title: 'Track Number 4',
    subtitle: 'Kesha',
    image: logo,
    contextType: 'album',
    contextExternalId: '041ef758-5d4c-4fb8-b05c-19790aceb540',
  },
  {
    id: '4648bb01-d31e-48ab-ad7c-32870f2b8224',
    title: 'Track Number 5',
    subtitle: 'Azahriah',
    image: logo,
    contextType: 'album',
    contextExternalId: 'b1ab29d4-8404-4132-a496-e614d5cfadbf',
  },
]

export const forYou: Item[] = [
  {
    id: 'playlist-1',
    title: 'Плейлісти для вас',
    subtitle: 'Найпопулярніші треки',
    image: logo,
  },
  {
    id: 'playlist-2',
    title: 'Чіл-аут вечір',
    subtitle: 'Ретельно підібрані треки',
    image: logo,
  },
  {
    id: 'playlist-3',
    title: 'Українська хвиля',
    subtitle: 'Найкраще з української сцени',
    image: logo,
  },
  {
    id: 'playlist-4',
    title: 'Робочий настрій',
    subtitle: 'Музика для концентрації та продуктивності',
    image: logo,
  },
]

export const albums: Item[] = [
  {
    id: '28493895-7e1a-4622-a64d-a3fa7f480c48',
    title: 'TIP TIP',
    subtitle: 'YoungFly • Kesha • Manuel',
    image: logo,
  },
  {
    id: '37dd539a-504a-44f4-962d-ed584d04b739',
    title: 'skatulya',
    subtitle: 'Azahriah',
    image: logo,
  },
  {
    id: '041ef758-5d4c-4fb8-b05c-19790aceb540',
    title: 'tripq',
    subtitle: 'DESH',
    image: logo,
  },
  {
    id: '296bded1-907d-4157-b12b-edfe000085b0',
    title: 'ret',
    subtitle: 'single',
    image: logo,
  },
  {
    id: 'b1ab29d4-8404-4132-a496-e614d5cfadbf',
    title: 'a lo tuloldalan',
    subtitle: 'extended play',
    image: logo,
  },
]