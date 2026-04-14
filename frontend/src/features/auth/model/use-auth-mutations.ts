'use client'

import { useMutation } from '@tanstack/react-query'
import {
  login,
  register,
  requestPasswordReset,
  verifyPasswordResetCode,
  confirmPasswordReset,
  requestOtp,
  verifyOtp,
  type LoginRequest,
  type LoginResponse,
  type RegisterRequest,
  type RegisterResponse,
  type PasswordResetRequest,
  type PasswordResetVerifyRequest,
  type PasswordResetConfirmRequest,
  type OtpRequest,
  type OtpRequestResponse,
  type OtpVerifyRequest,
  type OtpVerifyResponse,
} from '../api/auth.api'
import { useAuth } from './auth-context'

export function useLoginMutation() {
  const { setAccessToken } = useAuth()

  return useMutation<LoginResponse, Error, LoginRequest>({
    mutationFn: login,
    onSuccess: (data) => setAccessToken(data.accessToken, data.expiresAt ?? null),
  })
}

export function useRegisterMutation() {
  const { setAccessToken } = useAuth()

  return useMutation<RegisterResponse, Error, RegisterRequest>({
    mutationFn: register,
    onSuccess: (data) => setAccessToken(data.accessToken, data.expiresAt ?? null),
  })
}

export function usePasswordResetRequestMutation() {
  return useMutation<void, Error, PasswordResetRequest>({
    mutationFn: requestPasswordReset,
  })
}

export function usePasswordResetVerifyMutation() {
  return useMutation<void, Error, PasswordResetVerifyRequest>({
    mutationFn: verifyPasswordResetCode,
  })
}

export function usePasswordResetConfirmMutation() {
  return useMutation<void, Error, PasswordResetConfirmRequest>({
    mutationFn: confirmPasswordReset,
  })
}

export function useOtpRequestMutation() {
  return useMutation<OtpRequestResponse, Error, OtpRequest>({
    mutationFn: requestOtp,
  })
}

export function useOtpVerifyMutation() {
  const { setAccessToken } = useAuth()

  return useMutation<OtpVerifyResponse, Error, OtpVerifyRequest>({
    mutationFn: verifyOtp,
    onSuccess: (data) => setAccessToken(data.accessToken, data.expiresAt ?? null),
  })
}