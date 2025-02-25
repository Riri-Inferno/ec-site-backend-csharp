# EcSiteBackend プロジェクト構成

## ディレクトリ構成

```
C:.
│  EcSiteBackend.sln
│
├─.idea
│  └─.idea.EcSiteBackend
│      └─.idea
│              .gitignore
│              encodings.xml
│              indexLayout.xml
│              projectSettingsUpdater.xml
│              vcs.xml
│              workspace.xml
│
├─docs
├─src
│  ├─EcSiteBackend.Application
│  ├─EcSiteBackend.Domain
│  ├─EcSiteBackend.Infrastructure
│  └─EcSiteBackend.Presentation
│      ├─EcSiteBackend.WebAPI
│
└─tests
    ├─EcSiteBackend.IntegrationTests
```

## プロジェクト概要

EcSiteBackendはECサイト向けのバックエンドシステムで、以下の4つの主要レイヤーで構成されています。

### 1. Application レイヤー

ビジネスロジックを実装するレイヤーです。サービスクラスやユースケースを定義します。

### 2. Domain レイヤー

ドメインモデルを管理するレイヤーです。エンティティ、値オブジェクト、リポジトリインターフェースなどが含まれます。

### 3. Infrastructure レイヤー

データベースや外部APIとの接続など、インフラストラクチャ関連の処理を担当します。

### 4. Presentation レイヤー

Web APIを提供するレイヤーで、クライアントからのリクエストを受け付け、Applicationレイヤーに処理を委譲します。

## 依存関係

```
[Presentation] → [Application] → [Domain]
[Infrastructure] → [Domain]
```

- `Presentation` は `Application` に依存
- `Application` は `Domain` に依存
- `Infrastructure` は `Domain` に依存

## 使用技術・フレームワーク

- **.NET 8.0**: プロジェクトの主要フレームワーク
- **ASP.NET Core Web API**: API構築
- **Entity Framework Core**: ORM
- **xUnit**: テストフレームワーク
- **Coverlet**: コードカバレッジツール

## ビルド & 実行方法

```sh
# ソリューションのビルド
dotnet build EcSiteBackend.sln

# APIの実行
cd src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI
dotnet run
```

## テストの実行

```sh
# すべてのテストを実行
cd tests/EcSiteBackend.IntegrationTests
dotnet test
```
