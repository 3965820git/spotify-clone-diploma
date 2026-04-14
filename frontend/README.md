# Spotify Clone — Auth Starter

Це готовий стартовий шаблон сторінки авторизації (Login/Register) під дипломний проект.

## Що всередині
- Next.js (App Router) + TypeScript
- Tailwind CSS
- ESLint + Prettier (без крапок з комою `;`)
- react-hook-form + yup (валідація)
- @tanstack/react-query (мутації/кеш)
- API-клієнт під JWT в headers + refresh token в HTTPOnly cookies

## Запуск
1) Встанови залежності:
```bash
npm i
```

2) Створи `.env.local` (можеш взяти з `.env.example`):
```env
NEXT_PUBLIC_API_BASE=http://localhost:5000
```

3) Запусти:
```bash
npm run dev
```

Відкрий:
- http://localhost:3000/auth

## Підключення бекенду
Очікувані ендпоінти:
- POST `/auth/login` -> `{ accessToken }` + refresh cookie (HttpOnly)
- POST `/auth/register` -> `{ accessToken }` + refresh cookie (HttpOnly)
- POST `/auth/refresh` -> `{ accessToken }` (cookie надсилається автоматично через `credentials: include`)
