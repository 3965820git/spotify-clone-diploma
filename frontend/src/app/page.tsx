import Link from 'next/link'

export default function HomePage() {
  return (
    <main className="min-h-screen flex items-center justify-center p-6">
      <div className="max-w-md w-full space-y-4 rounded-xl border border-neutral-800 bg-neutral-950 p-6">
        <h1 className="text-2xl font-semibold">Spotify Clone</h1>
        <p className="text-neutral-300">
           Шаблон із готовою сторінкою авторизації без беку
        </p>
        <Link
          className="inline-flex items-center justify-center rounded-md bg-white px-4 py-2 text-sm font-medium text-black hover:opacity-90"
          href="/auth"
        >
          Перейти до /auth
        </Link>
      </div>
    </main>
  )
}
