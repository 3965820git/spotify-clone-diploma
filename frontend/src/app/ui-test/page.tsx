import { Button } from "@/shared/ui/button";

export default function UiTestPage() {
  return (
    <div className="min-h-dvh bg-[rgb(var(--bg))] p-6 space-y-6 max-w-md">
      <div className="space-y-3">
        <Button variant="light">Увійти</Button>
        <Button variant="primary">Увійти</Button>
        <Button variant="primary" disabled>
          Увійти
        </Button>
      </div>

      <div className="space-y-3">
        <Button variant="primary">Зареєструватися безкоштовно</Button>
        <Button variant="light">Зареєструватися безкоштовно</Button>
        <Button variant="primary" disabled>
          Зареєструватися безкоштовно
        </Button>
      </div>

      <div className="space-y-3">
        <Button variant="outline">Увійти</Button>
        <Button variant="outline" disabled>
          Увійти
        </Button>
      </div>
    </div>
  );
}
