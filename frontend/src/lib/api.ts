import { tokenStore } from '@/lib/tokenStore'

const API_BASE = process.env.NEXT_PUBLIC_API_BASE ?? 'http://localhost:5000'

type FetchOptions = RequestInit & { skipAuth?: boolean }

export async function apiFetch<T>(path: string, options: FetchOptions = {}): Promise<T> {
  const headers = new Headers(options.headers)

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
      if (token) retryHeaders.set('Authorization', `Bearer ${token}`)

      const retryRes = await fetch(`${API_BASE}${path}`, {
        ...options,
        headers: retryHeaders,
        credentials: 'include',
      })

      if (!retryRes.ok) throw await toError(retryRes)
      return retryRes.json()
    }
  }

  if (!res.ok) throw await toError(res)
  return res.json()
}

async function tryRefresh() {
  try {
    const res = await fetch(`${API_BASE}/auth/refresh`, {
      method: 'POST',
      credentials: 'include',
      headers: { 'Content-Type': 'application/json' },
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
  let message = `Request failed (${res.status})`
  try {
    const body = await res.json()
    if ((body as any)?.message) message = (body as any).message
  } catch {}
  return new Error(message)
}
