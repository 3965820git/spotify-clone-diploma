export const tokenStore = {
  get(key?: 'accessToken' | 'expiresAt') {
    if (typeof window === 'undefined') return null

    if (!key) {
      return localStorage.getItem('accessToken')
    }

    return localStorage.getItem(key)
  },

  set(
    keyOrValue: 'accessToken' | 'expiresAt' | string | null | undefined,
    maybeValue?: string | null
  ) {
    if (typeof window === 'undefined') return

    if (maybeValue === undefined) {
      const value = keyOrValue

      if (value == null) {
        localStorage.removeItem('accessToken')
        return
      }

      localStorage.setItem('accessToken', value)
      return
    }

    const key = keyOrValue as 'accessToken' | 'expiresAt'
    const value = maybeValue

    if (value == null) {
      localStorage.removeItem(key)
      return
    }

    localStorage.setItem(key, value)
  },

  clear() {
    if (typeof window === 'undefined') return
    localStorage.removeItem('accessToken')
    localStorage.removeItem('expiresAt')
  },
}