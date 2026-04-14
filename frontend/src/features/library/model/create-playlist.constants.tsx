import type { ReactNode } from 'react'
import type { VisibilityOption } from './create-playlist.types'
import { PublicIcon } from '@/shared/ui/icons/PublicIcon'
import { SharedAccessIcon } from '@/shared/ui/icons/SharedAccessIcon'
import { PrivateIcon } from '@/shared/ui/icons/PrivateIcon'

export type VisibilityOptionWithIcon = VisibilityOption & {
  icon: ReactNode
}

export const visibilityOptions: VisibilityOptionWithIcon[] = [
  {
    id: 'public',
    title: 'Публічний',
    description: 'Ваш плейліст буде видимий для всіх користувачів GROOV.',
   icon: <PublicIcon className="h-[40px] w-[40px]" />
  },
  {
    id: 'friends',
    title: 'Спільний доступ',
    description: 'Тільки обрані вами друзі зможуть бачити цей плейліст.',
    icon: <SharedAccessIcon className="h-[40px] w-[40px]" />
    
  },
  {
    id: 'private',
    title: 'Приватний',
    description: 'Тільки ви зможете бачити цей плейліст у своїй бібліотеці.',
  
    icon: <PrivateIcon className="h-[40px] w-[40px]" />
    
  },
]