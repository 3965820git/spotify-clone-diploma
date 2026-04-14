const DEVICE_ID_KEY = 'spotify_clone_device_id'

export function getOrCreateDeviceId(): string {
  if (typeof window === 'undefined') {
    return ''
  }

  const existingDeviceId = window.localStorage.getItem(DEVICE_ID_KEY)

  if (existingDeviceId) {
    return existingDeviceId
  }

  const newDeviceId = crypto.randomUUID()
  window.localStorage.setItem(DEVICE_ID_KEY, newDeviceId)

  return newDeviceId
}