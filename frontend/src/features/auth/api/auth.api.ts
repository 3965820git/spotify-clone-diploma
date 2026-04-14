const BASE_URL = "http://localhost:5000/api/v1/auth"

async function getErrorMessage(response: Response, fallback: string) {
  const text = await response.text()
  return text || fallback
}

export type LoginRequest = {
  identifier: string
  password: string
}

export type LoginResponse = {
  accessToken: string
  expiresAt: string
}

export type RegisterRequest = {
  email: string
  password: string
  displayName: string
  birthDate: string
  gender: string
}

export type RegisterResponse = {
  userId: string
  email: string
  displayName: string
  birthDateUtc: string | null
  gender: string
  accessToken: string
  expiresAt: string
}

export type PasswordResetRequest = {
  email: string
}

export type PasswordResetVerifyRequest = {
  email: string
  code: string
}

export type PasswordResetConfirmRequest = {
  email: string
  code: string
  newPassword: string
}

export type OtpRequest = {
  phoneNumber: string
}

export type OtpRequestResponse = {
  expiresInSeconds: number
}

export type OtpVerifyRequest = {
  phoneNumber: string
  code: string
}

export type OtpVerifyResponse = {
  accessToken: string
  expiresAt: string
}

export async function login(data: LoginRequest): Promise<LoginResponse> {
  const response = await fetch(`${BASE_URL}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(await getErrorMessage(response, "Login failed"))
  }

  return response.json()
}

export async function register(data: RegisterRequest): Promise<RegisterResponse> {
  const response = await fetch(`${BASE_URL}/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(await getErrorMessage(response, "Register failed"))
  }

  return response.json()
}

export async function requestPasswordReset(
  data: PasswordResetRequest
): Promise<void> {
  const response = await fetch(`${BASE_URL}/password-reset/request`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(
      await getErrorMessage(response, "Password reset request failed")
    )
  }
}

export async function verifyPasswordResetCode(
  data: PasswordResetVerifyRequest
): Promise<void> {
  const response = await fetch(`${BASE_URL}/password-reset/verify`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(
      await getErrorMessage(response, "Password reset code verification failed")
    )
  }
}

export async function confirmPasswordReset(
  data: PasswordResetConfirmRequest
): Promise<void> {
  const response = await fetch(`${BASE_URL}/password-reset/confirm`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(
      await getErrorMessage(response, "Password reset confirm failed")
    )
  }
}

export async function requestOtp(
  data: OtpRequest
): Promise<OtpRequestResponse> {
  const response = await fetch(`${BASE_URL}/login-otp/request`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(await getErrorMessage(response, "OTP request failed"))
  }

  return response.json()
}

export async function verifyOtp(
  data: OtpVerifyRequest
): Promise<OtpVerifyResponse> {
  const response = await fetch(`${BASE_URL}/login-otp/verify`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    throw new Error(await getErrorMessage(response, "OTP verification failed"))
  }

  return response.json()
}