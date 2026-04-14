const API_BASE = process.env.NEXT_PUBLIC_API_BASE ?? 'http://localhost:5000'

export async function http<T>(
  path: string,
  init: RequestInit & { json?: unknown } = {}
): Promise<T> {
  const headers = new Headers(init.headers)

  if (init.json !== undefined) {
    headers.set('Content-Type', 'application/json')
  }

  const res = await fetch(`${API_BASE}${path}`, {
    ...init,
    headers,
    credentials: 'include',
    body: init.json !== undefined ? JSON.stringify(init.json) : init.body,
  })

  if (!res.ok) throw await toError(res)

  // якщо бек повертає 204
  if (res.status === 204) return undefined as T

  return (await res.json()) as T
}

async function toError(res: Response) {
  let message = `Request failed (${res.status})`
  try {
    const body = await res.json()
    message =
      body?.detail ||
      body?.title ||
      body?.message ||
      (typeof body === 'string' ? body : JSON.stringify(body))
  } catch {}
  return new Error(message)
}