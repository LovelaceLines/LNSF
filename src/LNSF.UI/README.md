# Como usar

 Para executar um projeto React com Vite (que é um bundler rápido para projetos JavaScript), siga os passos abaixo:

1. Certifique-se de ter o Node.js instalado: Antes de começar, verifique se você tem o Node.js instalado no seu sistema. Você pode fazer isso executando o seguinte comando no seu terminal:

    ```
    node -v
    ```
   Se o Node.js não estiver instalado, você pode baixá-lo em https://nodejs.org/.



Passo 1: Clone o Repositório do GitHub

   Abra o terminal e navegue para o diretório onde você deseja que o projeto seja clonado. Em seguida, execute o comando git clone seguido pela URL do repositório do GitHub:

   ```
   git clone https://github.com/LovelaceLines/LNSF-Frontend.git
   ```
Passo 2: Navegue para o Diretório do Projeto

Use o comando cd para entrar no diretório do projeto que você acabou de clonar:

```
cd ./LNSF
```

Passo 3: Instale as Dependências

Dentro do diretório do projeto, execute o comando npm install para instalar as dependências listadas no arquivo package.json:

```
npm install
```
Passo 4: Inicie o Projeto com Vite

Agora que você tem as dependências instaladas, pode usar o Vite para iniciar o projeto. O Vite é um servidor de desenvolvimento rápido para projetos JavaScript modernos. Execute o seguinte comando:

```
npm run dev
```
Isso iniciará o servidor de desenvolvimento do Vite e abrirá seu projeto React em seu navegador padrão. Você poderá fazer alterações no código, e o Vite atualizará automaticamente a página para refletir essas alterações.















# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react/README.md) uses [Babel](https://babeljs.io/) for Fast Refresh
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh

## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type aware lint rules:

- Configure the top-level `parserOptions` property like this:

```js
   parserOptions: {
    ecmaVersion: 'latest',
    sourceType: 'module',
    project: ['./tsconfig.json', './tsconfig.node.json'],
    tsconfigRootDir: __dirname,
   },
```

- Replace `plugin:@typescript-eslint/recommended` to `plugin:@typescript-eslint/recommended-type-checked` or `plugin:@typescript-eslint/strict-type-checked`
- Optionally add `plugin:@typescript-eslint/stylistic-type-checked`
- Install [eslint-plugin-react](https://github.com/jsx-eslint/eslint-plugin-react) and add `plugin:react/recommended` & `plugin:react/jsx-runtime` to the `extends` list
