````markdown
---
name: New Resolver
about: GraphQL Query/Mutationの追加
title: "[Resolver] "
labels: enhancement, graphql
assignees: ""
---

## 概要

追加する Query/Mutation の目的と概要を記載する。

## リゾルバ定義

### 種別

- [ ] Query
- [ ] Mutation
- [ ] Subscription

### スキーマ

```graphql
# Query例
query {
  xxxQuery(id: ID!): XxxPayload!
}

# Mutation例
mutation {
  xxxMutation(input: XxxInput!): XxxPayload!
}
```
````

### 入力型

```graphql
input XxxInput {
  # 必須フィールド
  field1: String!
  field2: Int!

  # オプションフィールド
  field3: String
}
```

### 出力型

```graphql
type XxxPayload {
  success: Boolean!
  errors: [Error!]
  # その他の戻り値
}
```

## 実装詳細

### バリデーション要件

- [ ] 入力値の検証内容を記載
- [ ] ビジネスルールの検証内容を記載

### 認証・認可

- [ ] 認証必須
- [ ] 必要な権限：
- [ ] 認証不要

### エラーハンドリング

想定されるエラーケースと対応するエラーコードを記載：

- `XXX_NOT_FOUND`:
- `VALIDATION_ERROR`:
- その他：

### パフォーマンス考慮事項

- N+1 問題の対策：
- キャッシュ戦略：
- ページネーション：

## GraphQL クエリサンプル

### 正常系

```graphql
# Query例
query {
  xxxQuery(id: "123") {
    success
    data {
      field1
      field2
    }
  }
}

# Mutation例
mutation {
  xxxMutation(input: { field1: "value1", field2: 123 }) {
    success
    errors {
      code
      message
    }
  }
}
```

### 異常系

```graphql
# エラーケースの例を記載
```

## 受け入れ条件

- [ ] リゾルバが正常に実行できる
- [ ] エラーハンドリングが適切に行われる
- [ ] 認証・認可が正しく動作する
- [ ] ログが適切に出力される（センシティブ情報はマスク）
- [ ] GraphQL Playground で動作確認済み
- [ ] ドキュメントが更新されている

## 備考

その他、実装に関する注意事項や参考情報を記載。

```

```
