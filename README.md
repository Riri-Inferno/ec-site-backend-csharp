# バックエンドリポジトリ README

## 概要

このバックエンドサービスは、**C#** と **GraphQL** を使用して構築されたウェブアプリケーションのサーバーサイドです。GraphQL により、クライアントは必要なデータを効率的に取得でき、柔軟なクエリが可能となっています。

## 主な機能

- **GraphQL API**: データ取得および操作のためのエンドポイントを提供します。
- **ユーザー認証と認可**: JWT（JSON Web Token）を使用したセキュアな認証機能を実装しています。
- **データベース操作**: Entity Framework Core を用いてデータベースとのやり取りを簡素化します。
- **スケーラビリティ**: クリーンなアーキテクチャにより、将来的な機能追加や拡張が容易です。

## 技術スタック

- **言語**: C#
- **フレームワーク**: ASP.NET Core
- **API**: GraphQL（Hot Chocolate ライブラリ）
- **データベース**: PostgreSQL
- **ORM**: Entity Framework Core
- **認証**: JWT（JSON Web Token）
- **コンテナ**: Docker

## 必要条件

- **.NET 8 SDK** 以上
- **Docker** および **Docker Compose**
- **PostgreSQL**

> **⚠️ 重要な注意事項**:
>
> - Linux または macOS を使用している場合は、**Snap 版の Docker をインストールしないでください**。権限エラーが発生する可能性があります。
> - 通常版の Docker を公式サイトからインストールしてください。

## 環境構築手順

### 1. リポジトリのクローン

```bash
git clone https://github.com/Riri-Inferno/ec-site-backend-csharp.git
cd ec-site-backend-csharp
```



### 2. 必要なファイルの準備

#### `.env` ファイルの作成

プロジェクトルートに **`.env`** ファイルを作成し、以下の内容を記述してください。

**重要**:

  * `JWTSettings__Secret` は、**64文字以上の安全な文字列**を設定してください。
  * `POSTGRES_PASSWORD` と `ConnectionStrings__DefaultConnection` のパスワードは一致させてください。
  * `ConnectionStrings__DefaultConnection` の `Host` は、Docker Compose ネットワーク内のデータベースサービス名 (`db`) を指定してください。

```env
# JWT シークレットキー（64文字以上の安全な文字列を設定）
JWTSettings__Secret="your-64-character-safe-jwt-secret-key-here-xxxxxxxxxxxxxxxxxxxxxxxx"

# JWT トークンの有効期限（分単位）
JWTSettings__ExpirationInMinutes=60

# JWT Issuer (発行者)
JWTSettings__Issuer="EcSiteBackend"

# JWT Audience (対象利用者)
JWTSettings__Audience="EcSiteBackendClient"

# PostgreSQL データベースのパスワード
POSTGRES_PASSWORD="your-database-password"

# データベース接続文字列 (Docker コンテナ用)
# Host は docker-compose.yml で定義された 'db' サービス名と一致させる
ConnectionStrings__DefaultConnection="Host=db;Port=5432;Database=appdb;Username=postgres;Password=${POSTGRES_PASSWORD}"
```

#### `secrets.json` ファイルの作成

開発環境（ローカル実行時）用の設定ファイルです。プロジェクトの適切な場所にこのファイルを作成し、以下の内容を記述してください。

**重要**:

  * `JwtSettings.Secret` は、**64文字以上の安全な文字列**を設定してください。
  * `ConnectionStrings.DefaultConnection` の `Password` は `POSTGRES_PASSWORD` と一致させてください。
  * `ConnectionStrings.DefaultConnection` の `Host` は、ローカル開発環境でPostgreSQLが動作しているホスト (`localhost` など) を指定してください。

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=9999;Username=postgres;Password=your-database-password;Database=appdb"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "JwtSettings": {
    "Secret": "your-64-character-safe-jwt-secret-key-here-xxxxxxxxxxxxxxxxxxxxxxxx",
    "Issuer": "EcSiteBackend",
    "Audience": "EcSiteBackendClient",
    "ExpirationInMinutes": 60
  },
  "AllowedHosts": "*"
}
```

### 3. Docker 環境の起動（開発時はこちらを推奨、マイグレーションは別途行ってください）

Docker Compose を使用してコンテナを起動します。`api` サービスは常に最新のコードでビルドされるように、キャッシュを無効にしてビルドします。

```bash
# 既存のコンテナを停止・削除
docker compose down

# 'api' サービスをキャッシュなしでビルド（変更が頻繁なため推奨）
docker compose build --no-cache api

# 全てのサービスを起動（'api' は既にビルド済み、'db' は既存があれば利用）
docker compose up -d
```



### 4. パッケージの復元

```bash
dotnet restore
```

### 5. データベースのマイグレーション

```bash
cd ~/ec-site-backend-csharp/EcSiteBackend/src/EcSiteBackend.Infrastructure
dotnet ef database update --project ../EcSiteBackend.Infrastructure --startup-project ../EcSiteBackend.Presentation/EcSiteBackend.WebAPI
```

### 6. アプリケーションの起動

```bash
cd ~/ec-site-backend-csharp/EcSiteBackend/src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI
dotnet run
```

アプリケーションはデフォルトで `http://localhost:5000` で実行されます。

## GraphQL の使用方法

### GraphQL エンドポイント
Dockerで起動した場合デフォルトで以下URLに公開されます

  - **エンドポイント URL**: `http://localhost:8753/graphql`

### GraphQL Playground のアクセス

ブラウザでエンドポイントにアクセスすると、Banana Cake Pop（Hot Chocolate の GraphQL IDE）が表示され、クエリの実行やスキーマの確認が可能です。

### サンプルクエリ

いくつかの基本的な操作を行うためのサンプルクエリです。Banana Cake Pop でこれらのクエリをコピー＆ペーストして試すことができます。

#### ユーザー登録 (signUp)

新しいユーザーアカウントを作成します。

```graphql
mutation {
  signUp(
    input: {
      email: "your.email@hoge.com" # 適切なメールアドレスに変更してください
      firstName: "Mieken"
      lastName: "Nyan"
      password: "YourStrongPassword" # 強力なパスワードを設定してください
      phoneNumber: "111-2222-4444"
    }
  ) {
    expiresAt
    token
    user {
      createdAt
      displayName
      email
      emailConfirmed
      firstName
      fullName
      id
      isActive
      lastLoginAt
      lastName
      phoneNumber
      updatedAt
    }
    errors {
      code
      field
      message
    }
    success
  }
}
```

#### ユーザーログイン (signIn)

既存のユーザーアカウントでログインし、JWTトークンを取得します。

```graphql
mutation {
  signIn(
    input: {
      email: "your.email@hoge.com" # 登録済みのメールアドレスに変更してください
      password: "YourStrongPassword" # 登録したパスワードを入力してください
    }
  ) {
    expiresAt
    success
    token
    errors {
      code
      field
      message
    }
    user {
      createdAt
      displayName
      email
      emailConfirmed
      firstName
      fullName
      id
      isActive
      lastLoginAt
      lastName
      phoneNumber
      updatedAt
    }
  }
}
```

## 認証

クライアントは認証が必要なクエリやミューテーションを実行する際、リクエストヘッダーに JWT を含める必要があります。

- **ヘッダー例**:
  ```
  Authorization: Bearer your-jwt-token-here
  ```

## テストの実行

単体テストを実行するには、テストプロジェクトをビルドして IDE の GUI で実行するか、以下のコマンドを使用します：

```bash
dotnet test
```

## ディレクトリ構成

本プロジェクトはクリーンアーキテクチャの原則に従って構成されています：

```
EcSiteBackend/
├── src/
│   ├── EcSiteBackend.Domain/           # ビジネスエンティティとルール
│   │   ├── Entities/                   # User, Product, Order等
│   │   ├── Enums/                      # ビジネス列挙型
│   │   └── Constants/                  # ドメイン定数
│   │
│   ├── EcSiteBackend.Application/      # ビジネスロジック
│   │   ├── UseCases/                   # ユースケース実装
│   │   ├── DTOs/                       # データ転送オブジェクト
│   │   └── Common/                     # 共通インターフェース
│   │
│   ├── EcSiteBackend.Infrastructure/   # 外部システム連携
│   │   ├── DbContext/                  # EF Core コンテキスト
│   │   ├── Persistence/                # リポジトリ実装
│   │   └── Services/                   # 外部サービス実装
│   │
│   └── EcSiteBackend.Presentation/     # プレゼンテーション層
│       └── EcSiteBackend.WebAPI/       # Web API プロジェクト
│           ├── GraphQL/                # GraphQL定義
│           └── Program.cs              # エントリーポイント
│
└── tests/                              # テストプロジェクト
```

詳細は `docs/architecture.md` を参照してください。

## 開発に関する情報

### 使用したライブラリ

- **Hot Chocolate**: GraphQL 実装のためのライブラリ
- **Entity Framework Core**: データベース接続と ORM
- **System.IdentityModel.Tokens.Jwt**: JWT の生成と検証

## トラブルシューティング

### Docker 関連のエラー

主にリナックス向け

- **Permission denied エラー**: Snap 版 Docker を使用している場合は、アンインストールして通常版をインストールしてください
- **docker-compose コマンドが見つからない**: 最新の Docker では `docker compose`（ハイフンなし）を使用してください

### データベース接続エラー

- PostgreSQL コンテナが起動していることを確認: `docker ps`
- ポート 9999 が使用されていないことを確認
- DBever 等の管理ツールを使用する際は 接続設定の DB 欄に appdb が入力されているか確認

## ライセンス

このプロジェクトは [MIT ライセンス](LICENSE) の下で提供されています。

## 連絡先

- **作者**: T.U

## 参考資料

- [ASP.NET Core の公式ドキュメント](https://docs.microsoft.com/ja-jp/aspnet/core/)
- [Hot Chocolate GraphQL のドキュメント](https://chillicream.com/docs/hotchocolate/)
- [Entity Framework Core のドキュメント](https://docs.microsoft.com/ja-jp/ef/core/)

---
