export class ApiError extends Error {
  status?: number
  code?: string
  detail?: string

  constructor(message: string, options?: { status?: number; code?: string; detail?: string }) {
    super(message)
    this.name = 'ApiError'
    this.status = options?.status
    this.code = options?.code
    this.detail = options?.detail
  }
}

export function mapApiError(payload: any, status: number) {
  const firstError = Array.isArray(payload?.errors) ? payload.errors[0] : null
  const code =
    firstError?.code ||
    payload?.code ||
    payload?.errors?.request?.[0] ||
    payload?.errors?.email?.[0] ||
    undefined

  const detail =
    firstError?.description ||
    payload?.detail ||
    payload?.title ||
    undefined

  const messageByCode: Record<string, string> = {
    'Auth.InvalidEmail': 'Електронна пошта або ім’я користувача не пов’язано з акаунтом Groov',
'Auth.UserNotFound': 'Електронна пошта або ім’я користувача не пов’язано з акаунтом Groov',
    'Auth.InvalidPassword': 'Невірний пароль',
    'Auth.EmailAlreadyExists': 'Користувач з таким email вже існує',
    'Auth.InvalidCredentials': 'Невірні дані для входу',
    'Auth.InvalidRefreshToken': 'Сесію завершено. Увійди знову',
    'Auth.EmailNotVerified': 'Підтверди email перед входом',
    'Auth.PhoneNotVerified': 'Підтверди номер телефону',
   
  }

  const fallbackByStatus: Record<number, string> = {
    400: 'Некоректні дані',
    401: 'Потрібно увійти знову',
    403: 'Немає доступу',
    404: 'Нічого не знайдено',
    409: 'Конфлікт даних',
    500: 'Помилка сервера. Спробуй ще раз',
  }

  const message =
    (code && messageByCode[code]) ||
    detail ||
    fallbackByStatus[status] ||
    'Сталася невідома помилка. Спробуй ще раз.'

  return new ApiError(message, { status, code, detail })
}