import { z } from 'zod'

export const loginSchema = z.object({
  email: z.string().email('Некоректний email'),
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

export type LoginForm = z.infer<typeof loginSchema>
export type RegisterForm = z.infer<typeof registerSchema>
