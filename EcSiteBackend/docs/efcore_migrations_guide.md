# EF Core マイグレーションガイド

このドキュメントは、Entity Framework Core (EF Core) を使用してデータベースマイグレーションを行うための手順を簡潔にまとめたものです。

---

## 1. 環境設定

EF Core のマイグレーションには .NET SDK と Docker が必要です。

### データベースコンテナの起動

PostgreSQL データベースを Docker Compose で起動します。

- `docker-compose.yml` があるディレクトリで実行:
  ```bash
  docker-compose up -d
  ```

---

## 2. エンティティの定義

データベースのテーブルに対応するモデルクラスを作成し、`DbContext` に登録します。

### エンティティクラスの作成

- **場所:** `EcSiteBackend.Domain` プロジェクト
- **目的:** データベースのテーブル構造を定義します。

### DbContext に DbSet を追加

- **場所:** `EcSiteBackend.Infrastructure` プロジェクト内の `ApplicationDbContext.cs`
- **目的:** EF Core がどのエンティティをデータベースとマッピングするかを認識できるようにします。

---

## 3. データベース接続設定

アプリケーションがデータベースに接続するための情報（接続文字列）を設定します。

### 接続文字列の追加

- **場所:** `EcSiteBackend.Presentation/EcSiteBackend.WebAPI` プロジェクトの**ユーザーシークレット**
- **目的:** アプリケーションと EF Core ツールがデータベースにアクセスできるようにします。
- **コマンド (ユーザーシークレット):**
  ```bash
  cd ~/ec-site-backend-csharp/EcSiteBackend/src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=9999;Username=postgres;Password=supersecretpooppassword;Database=appdb"
  ```

---

## 4. デザインタイムファクトリの実装

`dotnet ef` コマンドのようなデザインタイムツールが `DbContext` を生成できるように、専用のファクトリを設定します。

### `IDesignTimeDbContextFactory` の実装

- **場所:** `EcSiteBackend.Presentation/EcSiteBackend.WebAPI/ApplicationDbContextFactory.cs`
- **目的:** アプリケーションが実行されていない状態でも、EF Core ツールが `DbContext` を使ってデータベーススキーマを読み取れるようにします。
- **実装のポイント:**
  - `EcSiteBackend.WebAPI` プロジェクトの正しい**ルートパス**を特定し、`appsettings.json` やユーザーシークレットから接続文字列を読み込むロジックを記述します。
  - `Program` クラスの**正しい名前空間** (`EcSiteBackend.Presentation.EcSiteBackend.WebAPI`) を `using` ディレクティブで追加し、`AddUserSecrets<Program>()` で指定します。

---

## 5. マイグレーションの実行

エンティティの変更をデータベーススキーマに反映させます。

### 環境変数の設定

- **目的:** デザインタイムツールが正しい環境設定（例: `Development`）で動作するようにします。
- **コマンド:**
  ```bash
  export ASPNETCORE_ENVIRONMENT=Development
  ```
  (Windows の場合は `set ASPNETCORE_ENVIRONMENT=Development`)

### WebAPI プロジェクトのビルド

- **目的:** ファクトリの変更を適用します。
- **コマンド:**
  ```bash
  cd ~/ec-site-backend-csharp/EcSiteBackend/src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI
  dotnet build
  ```

### マイグレーションファイルの生成

- **場所:** `EcSiteBackend.Infrastructure` プロジェクトディレクトリで実行
- **目的:** エンティティと現在のデータベーススキーマの差分から、データベース変更スクリプトを生成します。
- **コマンド:**
  ```bash
  cd ~/ec-site-backend-csharp/EcSiteBackend/src/EcSiteBackend.Infrastructure
  dotnet ef migrations add InitialCreate --project ../EcSiteBackend.Infrastructure --startup-project ../EcSiteBackend.Presentation/EcSiteBackend.WebAPI --output-dir DbContext/Migrations
  ```
  - `InitialCreate`: マイグレーションに任意の名前を付けます。

### データベースへのマイグレーション適用

- **場所:** `EcSiteBackend.Infrastructure` プロジェクトディレクトリで実行
- **目的:** 生成されたマイグレーションスクリプトをデータベースに実際に適用し、テーブル構造などを更新します。
- **コマンド:**
  ```bash
  cd ~/ec-site-backend-csharp/EcSiteBackend/src/EcSiteBackend.Infrastructure
  dotnet ef database update --project ../EcSiteBackend.Infrastructure --startup-project ../EcSiteBackend.Presentation/EcSiteBackend.WebAPI
  ```

---

## 6. データベースの確認 (オプション)

マイグレーションが正しく適用されたことを確認します。

- **目的:** 作成されたテーブルなどがデータベースに存在するか直接確認します。
- **コマンド:**
  ```bash
  docker exec -it ecsite-db psql -U postgres -d appdb
  # パスワード入力が求められることがある
  \dt
  # psql を終了するには \q
  ```
