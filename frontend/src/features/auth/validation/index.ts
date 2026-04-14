import { z } from 'zod'

export const loginSchema = z.object({
  identifier: z.string().min(1, 'Введи email або username'),
  password: z.string().min(6, 'Мінімум 6 символів'),
})

export const registerSchema = z
  .object({
    email: z.string().email('Некоректний email'),
    password: z.string().min(6, 'Мінімум 6 символів'),
    confirmPassword: z.string().min(6, 'Мінімум 6 символів'),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ['confirmPassword'],
    message: 'Паролі не співпадають',
  })

export const passwordResetRequestSchema = z.object({
  email: z.string().email('Некоректний email'),
})

export const passwordResetCodeSchema = z.object({
  email: z.string().email('Некоректний email'),
  code: z.string().length(6, 'Код має містити 6 символів'),
})

export const passwordResetConfirmSchema = z
  .object({
    email: z.string().email('Некоректний email'),
    code: z.string().length(6, 'Код має містити 6 символів'),
    newPassword: z.string().min(6, 'Мінімум 6 символів'),
    confirmPassword: z.string().min(6, 'Мінімум 6 символів'),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    path: ['confirmPassword'],
    message: 'Паролі не співпадають',
  })

export const otpRequestSchema = z.object({
  phoneNumber: z.string().min(8, 'Введи номер телефону'),
})

export const otpVerifySchema = z.object({
  phoneNumber: z.string().min(8, 'Введи номер телефону'),
  code: z.string().length(6, 'Код має містити 6 символів'),
})

export type LoginForm = z.infer<typeof loginSchema>
export type RegisterForm = z.infer<typeof registerSchema>
export type PasswordResetRequestForm = z.infer<typeof passwordResetRequestSchema>
export type PasswordResetCodeForm = z.infer<typeof passwordResetCodeSchema>
export type PasswordResetConfirmForm = z.infer<typeof passwordResetConfirmSchema>
export type OtpRequestForm = z.infer<typeof otpRequestSchema>
export type OtpVerifyForm = z.infer<typeof otpVerifySchema>