import axios from 'axios'

function getAccessTokenFromStorage() {
  if (typeof window === 'undefined') {
    return null
  }

  try {
    const raw = window.localStorage.getItem('auth')
    if (!raw) {
      return null
    }

    const parsed = JSON.parse(raw)
    return parsed?.state?.accessToken ?? null
  } catch {
    return null
  }
}

export const apiClient = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_BASE,
  withCredentials: true,
})

apiClient.interceptors.request.use((config) => {
  const accessToken = getAccessTokenFromStorage()

  if (accessToken) {
    config.headers.Authorization = `Bearer ${accessToken}`
  }

  return config
})