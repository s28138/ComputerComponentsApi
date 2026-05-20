# ComputerComponentsApi

Projekt wykonany na ćwiczenia z APBD.

Aplikacja jest prostym REST API napisanym w ASP.NET Core Web API. Służy do zarządzania komputerami oraz ich komponentami.

Projekt korzysta z Entity Framework Core w podejściu Code First, czyli baza danych jest tworzona na podstawie klas przygotowanych w kodzie.

## Użyte technologie

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Swagger
- GitHub

## Co znajduje się w projekcie

W projekcie są przygotowane encje odpowiadające tabelom z diagramu:

- PC
- Component
- ComponentType
- ComponentManufacturer
- PCComponent

Tabela `PCComponent` służy do połączenia komputerów z komponentami i przechowuje też ilość danego komponentu w komputerze.

## Struktura projektu

```text
ComputerComponentsApi
├── Controllers
├── DTOs
├── Data
├── Migrations
├── Models
├── Services
├── Program.cs
└── appsettings.json
```

Krótki opis folderów:

- `Controllers` – kontrolery API,
- `DTOs` – klasy do przesyłania danych,
- `Data` – kontekst bazy danych `AppDbContext`,
- `Migrations` – migracje Entity Framework Core,
- `Models` – klasy encji,
- `Services` – logika aplikacji.

## Baza danych

Baza danych została przygotowana przy pomocy Entity Framework Core Code First.

Relacje, klucze główne, klucze obce i ograniczenia pól są skonfigurowane w pliku:

```text
Data/AppDbContext.cs
```

W projekcie dodane są też dane startowe przez `HasData()`, żeby po utworzeniu bazy były już przykładowe rekordy.

## Endpointy

### Pobranie wszystkich komputerów

```http
GET /api/pcs
```

Endpoint zwraca listę komputerów.

### Pobranie komponentów danego komputera

```http
GET /api/pcs/{id}/components
```

Przykład:

```http
GET /api/pcs/1/components
```

Jeżeli komputer nie istnieje, zwracany jest status:

```http
404 Not Found
```

### Dodanie komputera

```http
POST /api/pcs
```

Przykładowe body:

```json
{
  "name": "Test PC",
  "weight": 8.5,
  "warranty": 24,
  "createdAt": "2026-05-20T20:00:00",
  "stock": 10
}
```

Po poprawnym dodaniu zwracany jest status:

```http
201 Created
```

### Edycja komputera

```http
PUT /api/pcs/{id}
```

Przykładowe body:

```json
{
  "name": "Test PC Updated",
  "weight": 9.2,
  "warranty": 36,
  "createdAt": "2026-05-20T20:00:00",
  "stock": 7
}
```

Jeżeli komputer istnieje, zwracany jest status:

```http
200 OK
```

Jeżeli komputer nie istnieje, zwracany jest status:

```http
404 Not Found
```

### Usunięcie komputera

```http
DELETE /api/pcs/{id}
```

Po poprawnym usunięciu zwracany jest status:

```http
204 No Content
```

Jeżeli komputer nie istnieje, zwracany jest status:

```http
404 Not Found
```

## Uruchomienie projektu

Po pobraniu projektu należy przywrócić paczki:

```bash
dotnet restore
```

Następnie trzeba utworzyć bazę danych z migracji:

```bash
dotnet ef database update
```

Uruchomienie projektu:

```bash
dotnet run
```

Swagger powinien być dostępny pod adresem:

```text
http://localhost:5223/swagger
```

## Testowanie

Endpointy zostały sprawdzone w Swaggerze.

Przetestowano:

- pobranie listy komputerów,
- pobranie komponentów komputera,
- dodanie nowego komputera,
- edycję komputera,
- usunięcie komputera,
- przypadki, gdy komputer o podanym id nie istnieje.
