# Dockerfile (этот файл останется только у тебя)
FROM node:18-alpine

WORKDIR /app

# Копируем зависимости
COPY package*.json ./
RUN npm install

# Копируем код
COPY . .

# --- ГЛАВНЫЙ ФОКУС ---
# Мы объявляем, что ждем переменную при сборке
ARG NEXT_PUBLIC_API_BASE
# И сразу записываем её в переменную окружения
ENV NEXT_PUBLIC_API_BASE=$NEXT_PUBLIC_API_BASE
# ---------------------

RUN npm run build

EXPOSE 3000
CMD ["npm", "start"]
