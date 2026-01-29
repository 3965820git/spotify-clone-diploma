'use client'

import * as React from 'react'
import { useMutation } from '@tanstack/react-query'
import { zodResolver } from '@hookform/resolvers/zod'

import { useForm } from 'react-hook-form'

import { login, registerUser } from '@/features/auth/api'

import { loginSchema, registerSchema, type LoginForm, type RegisterForm } from '@/features/auth/validation'



import { Button } from '@/components/ui/button'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs'
import { Alert, AlertDescription } from '@/components/ui/alert'


export default function AuthCard() {
  const [error, setError] = React.useState<string | null>(null)

 const loginForm = useForm<LoginForm>({
  resolver: zodResolver(loginSchema),
  defaultValues: { email: '', password: '' },
})

const registerForm = useForm<RegisterForm>({
  resolver: zodResolver(registerSchema),
  defaultValues: { email: '', password: '', confirmPassword: '' },
})


  const loginMutation = useMutation({
    mutationFn: login,
    onSuccess: () => {
      setError(null)
    },
    onError: (e: Error) => setError(e.message),
  })

  const registerMutation = useMutation({
  mutationFn: (dto: { email: string; password: string }) => registerUser(dto),

    onSuccess: () => {
      setError(null)
    },
    onError: (e: Error) => setError(e.message),
  })

  return (
    <Card className="w-full max-w-md border-neutral-800 bg-neutral-950">
      <CardHeader>
        <CardTitle>Вхід</CardTitle>
        <CardDescription>Увійдіть або створіть акаунт (бек підʼєднаємо пізніше)</CardDescription>
      </CardHeader>

      <CardContent className="space-y-4">
        {error && (
          <Alert>
            <AlertDescription>{error}</AlertDescription>
          </Alert>
        )}

        <Tabs defaultValue="login" className="w-full">
          <TabsList className="w-full">
            <TabsTrigger className="flex-1" value="login">
              Login
            </TabsTrigger>
            <TabsTrigger className="flex-1" value="register">
              Register
            </TabsTrigger>
          </TabsList>

          <TabsContent value="login" className="mt-4">
            <form
              className="space-y-3"
              onSubmit={loginForm.handleSubmit((values) => loginMutation.mutate(values))}
            >
              <Field
                label="Email"
                type="email"
                error={loginForm.formState.errors.email?.message}
                {...loginForm.register('email')}
              />
              <Field
                label="Password"
                type="password"
                error={loginForm.formState.errors.password?.message}
                {...loginForm.register('password')}
              />

              <Button className="w-full" type="submit" disabled={loginMutation.isPending}>
                {loginMutation.isPending ? 'Signing in…' : 'Sign in'}
              </Button>
            </form>
          </TabsContent>

          <TabsContent value="register" className="mt-4">
            <form
              className="space-y-3"
              onSubmit={registerForm.handleSubmit((values) =>
                registerMutation.mutate({ email: values.email, password: values.password })
              )}
            >
              <Field
                label="Email"
                type="email"
                error={registerForm.formState.errors.email?.message}
                {...registerForm.register('email')}
              />
              <Field
                label="Password"
                type="password"
                error={registerForm.formState.errors.password?.message}
                {...registerForm.register('password')}
              />
              <Field
                label="Confirm password"
                type="password"
                error={registerForm.formState.errors.confirmPassword?.message}
                {...registerForm.register('confirmPassword')}
              />

              <Button className="w-full" type="submit" disabled={registerMutation.isPending}>
                {registerMutation.isPending ? 'Creating…' : 'Create account'}
              </Button>
            </form>
          </TabsContent>
        </Tabs>
      </CardContent>
    </Card>
  )
}

const Field = React.forwardRef<
  HTMLInputElement,
  React.ComponentProps<typeof Input> & { label: string; error?: string }
>(({ label, error, id, ...props }, ref) => {
  const inputId = id ?? label.toLowerCase().replace(/\s+/g, '-')
  return (
    <div className="space-y-1">
      <Label htmlFor={inputId}>{label}</Label>
      <Input ref={ref} id={inputId} {...props} />
      {error ? <p className="text-sm text-red-500">{error}</p> : null}
    </div>
  )
})
Field.displayName = 'Field'
