export type LoginBody = {
  identifier: string
  password: string
}

export type RegisterBody = {
  email?: string
  phoneNumber?: string
  password?: string
  displayName?: string
  birthDateUtc?: string
  gender?: string
}

export type AuthResponse = {
  accessToken: string
  expiresAt?: string
}

export type PasswordResetRequestBody = {
  email: string
}

export type PasswordResetVerifyBody = {
  email: string
  code: string
}

export type PasswordResetConfirmBody = {
  email: string
  code: string
  newPassword: string
}

export type OtpRequestBody = {
  phoneNumber: string
}

export type OtpVerifyBody = {
  phoneNumber: string
  code: string
}