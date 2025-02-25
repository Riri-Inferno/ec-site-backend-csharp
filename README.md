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
- **データベース**: SQL Server または PostgreSQL
- **ORM**: Entity Framework Core
- **認証**: JWT（JSON Web Token）

## 必要条件

- **.NET 6 SDK** 以上
- **SQL Server** または **PostgreSQL** のいずれか
- （オプション）**Docker**（コンテナ化された環境で実行する場合）

## 環境構築手順

### リポジトリのクローン

```bash
git clone https://github.com/あなたのユーザー名/リポジトリ名.git
cd リポジトリ名
```

### パッケージの復元

```bash
dotnet restore
```

### アプリケーション設定

`appsettings.json` ファイルをコピーして新しく `appsettings.Development.json` を作成し、データベース接続文字列などの開発環境用設定を追加します。

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=データベース名;User Id=ユーザー名;Password=パスワード;"
  },
  "JwtSettings": {
    "Secret": "あなたのJWTシークレットキー",
    "Issuer": "あなたのIssuer",
    "Audience": "あなたのAudience",
    "ExpiresInMinutes": 60
  }
}
```

### データベースのマイグレーションと更新

```bash
dotnet ef database update
```

### アプリケーションの起動

```bash
dotnet run
```

アプリケーションはデフォルトで `https://localhost:5004` または `http://localhost:5000` で実行されます。

## GraphQL の使用方法

### GraphQL エンドポイント

- **エンドポイント URL**: `https://localhost:5001/graphql`

### GraphQL プレイグラウンドのアクセス

ブラウザでエンドポイントにアクセスすると、GraphQL Playground（または Hot Chocolate 使用時は Banana Cake Pop）が表示され、クエリの実行やスキーマの確認が可能です。

### サンプルクエリ

```graphql
# ユーザー一覧を取得
query {
  users {
    id
    name
    email
  }
}

# ユーザーを追加
mutation {
  addUser(
    input: { name: "太郎", email: "taro@example.com", password: "password123" }
  ) {
    id
    name
    email
  }
}
```

## 認証

クライアントは認証が必要なクエリやミューテーションを実行する際、リクエストヘッダーに JWT を含める必要があります。

- **ヘッダー例**:

  ```
  Authorization: Bearer あなたのJWTトークン
  ```

## テストの実行

単体テストを実行するには、以下のコマンドを使用します。

```bash
dotnet test
```

## デプロイ方法

### リリースビルドの作成

```bash
dotnet publish -c Release
```

### Docker を使用したデプロイ（いつか）

1. **Docker イメージのビルド**

   ```bash
   docker build -t あなたのイメージ名 .
   ```

2. **コンテナの実行**

   ```bash
   docker run -d -p 80:80 あなたのイメージ名
   ```

## 開発に関する情報

### 使用したライブラリ

- **Hot Chocolate**: GraphQL 実装のためのライブラリ
- **Entity Framework Core**: データベース接続と ORM
- **System.IdentityModel.Tokens.Jwt**: JWT の生成と検証

### ディレクトリ構成

- `/Entities`: データベースエンティティ
- `/Repositories`: データ操作のためのリポジトリクラス
- `/GraphQL`: GraphQL スキーマ、クエリ、ミューテーション
- `/Services`: ビジネスロジックの実装
- `/Configurations`: アプリケーション設定

## ライセンス

このプロジェクトは [MIT ライセンス](LICENSE) の下で提供されています。

## 連絡先

- **作者**: T.U
- **メール**: [your.email@example.com](mailto:your.email@example.com)

## 参考資料

- [ASP.NET Core の公式ドキュメント](https://docs.microsoft.com/ja-jp/aspnet/core/)
- [Hot Chocolate GraphQL のドキュメント](https://chillicream.com/docs/hotchocolate/)
- [Entity Framework Core のドキュメント](https://docs.microsoft.com/ja-jp/ef/core/)

---
