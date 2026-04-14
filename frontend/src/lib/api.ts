import { tokenStore } from '@/lib/tokenStore'

const API_BASE = process.env.NEXT_PUBLIC_API_BASE ?? 'http://localhost:5000/api/v1'

type FetchOptions = RequestInit & {
  skipAuth?: boolean
}

export async function apiFetch<T>(
  path: string,
  options: FetchOptions = {}
): Promise<T> {
  const headers = new Headers(options.headers)

  if (!headers.has('Content-Type') && !(options.body instanceof FormData)) {
    headers.set('Content-Type', 'application/json')
  }

  if (!options.skipAuth) {
    const token = tokenStore.get()
    if (token) headers.set('Authorization', `Bearer ${token}`)
  }

  const res = await fetch(`${API_BASE}${path}`, {
    ...options,
    headers,
    credentials: 'include',
  })

  if (res.status === 401 && !options.skipAuth) {
    const refreshed = await tryRefresh()

    if (refreshed) {
      const retryHeaders = new Headers(options.headers)
      const token = tokenStore.get()
      if (!(options.body instanceof FormData) && !retryHeaders.has('Content-Type')) {
        retryHeaders.set('Content-Type', 'application/json')
      }
      if (token) retryHeaders.set('Authorization', `Bearer ${token}`)

      const retryRes = await fetch(`${API_BASE}${path}`, {
        ...options,
        headers: retryHeaders,
        credentials: 'include',
      })

      if (!retryRes.ok) throw await toError(retryRes)
      if (retryRes.status === 204) return undefined as T
      return retryRes.json()
    }
  }

  if (!res.ok) throw await toError(res)
  if (res.status === 204) return undefined as T

  const text = await res.text()
  return text ? JSON.parse(text) : (undefined as T)
}

async function tryRefresh() {
  try {
    const res = await fetch(`${API_BASE}/auth/refresh`, {
      method: 'POST',
      credentials: 'include',
    })

    if (!res.ok) return false

    const data = (await res.json()) as { accessToken: string }
    tokenStore.set(data.accessToken)
    return true
  } catch {
    return false
  }
}

async function toError(res: Response) {
  try {
    const body = await res.json()
    const message =
      body?.message ||
      body?.detail ||
      body?.title ||
      (Array.isArray(body?.errors) ? body.errors.join(', ') : null)

    return new Error(message || `Request failed (${res.status})`)
  } catch {
    return new Error(`Request failed (${res.status})`)
  }
}