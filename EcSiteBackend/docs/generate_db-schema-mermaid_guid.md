# DB ER 図生成ガイド (Mermaid 形式)

PostgreSQL データベースから Mermaid 形式の ER 図を生成する手順を説明します。

## 1. 必要なもののインストール

### Node.js と npm のインストール

- **Windows**: [Node.js 公式サイト](https://nodejs.org/)から LTS 版をダウンロードしてインストール
- **Linux**: 以下のコマンドを実行
  ```bash
  curl -fsSL https://deb.nodesource.com/setup_lts.x | sudo -E bash -
  sudo apt-get install -y nodejs
  ```

インストール確認:

```bash
node -v
npm -v
```

## 2. pg-mermaid のインストール

```bash
# Windows (管理者権限のコマンドプロンプト/PowerShell)
npm install -g pg-mermaid

# Linux
sudo npm install -g pg-mermaid
```

## 3. ER 図の生成

### 前提条件

PostgreSQL コンテナが起動していること:

```bash
docker-compose up -d
```

### 実行コマンド

```bash
pg-mermaid --host localhost --port 9999 --dbname appdb --username postgres --password supersecretpooppassword
```

### 出力について

- 実行すると `database.md` ファイルが**現在のディレクトリ**に生成されます
- 成功時のメッセージ例:
  ```
  Diagram was generated successfully at '/path/to/your/project/database.md'
  ```

## 4. ER 図の表示 (VS Code)

1. **拡張機能のインストール**

   - VS Code で拡張機能タブを開く（`Ctrl+Shift+X`）
   - 「Mermaid」で検索し、Mermaid 拡張機能をインストール

2. **プレビュー表示**
   - `database.md` を開く
   - 右上のプレビューアイコンをクリック、または `Ctrl+Shift+V`

これで ER 図が視覚的に表示されます。

---
