import { create } from 'zustand'
import { persist } from 'zustand/middleware'
import * as authApi from '@/shared/api/auth'

type RegisterDto = {
  email: string
  password: string
  displayName: string
  birthDate: string
  gender: string
}

type LoginDto = {
  identifier: string
  password: string
}

type AuthState = {
  accessToken: string | null
  displayName: string | null

  isHydrated: boolean
  setHydrated: (v: boolean) => void

  setAccessToken: (t: string | null) => void
  setDisplayName: (n: string | null) => void

  login: (dto: LoginDto) => Promise<void>
  register: (dto: RegisterDto) => Promise<void>
  tryRefresh: () => Promise<boolean>
  logout: () => Promise<void>
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set, get) => ({
      accessToken: null,
      displayName: null,

      isHydrated: false,
      setHydrated: (v) => set({ isHydrated: v }),

      setAccessToken: (t) => set({ accessToken: t }),
      setDisplayName: (n) => set({ displayName: n }),

      login: async (dto) => {
        const { accessToken } = await authApi.login(dto)
        set({ accessToken })
      },

      register: async (dto) => {
        const { accessToken } = await authApi.register(dto)
        set({ accessToken, displayName: dto.displayName })
      },

      tryRefresh: async () => {
        try {
          const { accessToken } = await authApi.refresh()
          set({ accessToken })
          return true
        } catch {
          return false
        }
      },

      logout: async () => {
        const token = get().accessToken

        if (token) {
          try {
            await authApi.logout(token)
          } catch {}
        }

        set({ accessToken: null, displayName: null })
      },
    }),
    {
      name: 'auth',
      partialize: (s) => ({
        accessToken: s.accessToken,
        displayName: s.displayName,
      }),
      onRehydrateStorage: () => (state) => {
        state?.setHydrated(true)
      },
    }
  )
)