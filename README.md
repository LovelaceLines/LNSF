# LNSF - Lar Nossa Senhora de Fátima

O sistema LNSF foi desenvolvido com um propósito fundamental: **aprimorar a eficiência das operações realizadas pelos servidores dedicados ao Lar Nossa Senhora de Fátima**. Este sistema visa a melhoria do processo de **gestão dos pacientes** e quartos, além de **automatizar tarefas complexas**, anteriormente realizadas de forma manual, como a emissão de relatórios 

## Principáis tecnologias utilizadas

### .Net
O .NET é uma plataforma de desenvolvedor multiplataforma de código aberto gratuita para criar muitos tipos diferentes de aplicativos. O ASP.NET amplia a plataforma de desenvolvedor do .NET com ferramentas e bibliotecas específicas para a criação de aplicativos web. O C# é sua principal linguagem de programação, simples, moderna, com foco no objeto e de tipo seguro. 

Bibliotecas utilizadas no desenvolvimento:

| [ASP.Net](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi) | [Entity Framework](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) | [AutoMapper](https://www.nuget.org/packages/AutoMapper) | [Bogus](https://www.nuget.org/packages/Bogus) | [FluentValidation](https://www.nuget.org/packages/FluentValidation) | [Serilog](https://www.nuget.org/packages/Serilog) | [xunit](https://www.nuget.org/packages/xunit) | [FastReports](https://www.nuget.org/packages/FastReport.OpenSource)
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
| ![ASP.Net](./docs/icons/net-framework.png)| ![ENtity FrameWork](./docs/icons/entity-framework.png) | ![AutoMapper](./docs/icons/automapper.png) | ![Bogus](./docs/icons/bogus.png) | ![FluentValidation](./docs/icons/fluentvalidation.png) | ![Serilog](./docs/icons/serilog.png) | ![xunit](./docs/icons/xunit.png) | ![FastReports](./docs/icons/fastreports.png)

### React
O React é uma biblioteca JavaScript para criar interfaces de usuário (UI) reativas baseado no conceito de componentes, que são blocos de construção reutilizáveis que podem ser combinados para criar interfaces complexas. uma biblioteca popular para criar aplicativos web, incluindo sites, aplicativos móveis e aplicativos de desktop.

Bibliotecas utilizadas no desenvolvimento:

| [Axios](https://www.npmjs.com/package/axios) | [ReactRouter](https://www.npmjs.com/package/react-router-dom) | [MUI](https://www.npmjs.com/package/@mui/material) | [Unform](https://www.npmjs.com/package/@unform/core) | [ESLint](https://www.npmjs.com/package/eslint)
|:--:|:--:|:--:|:--:|:--:|
| ![Axios](./docs/icons/axios.png) | ![ReactRouter](./docs/icons/react-router.png) | ![MUI](./docs/icons/mui.png) | ![Unform](./docs/icons/unform.png) | ![ESLint](./docs/icons/eslint.png)

## Diagramas

Para modelar e representar a estrutura lógica dos dados, é utilizado o **Diagrama de Entidade-Relacionamento**, proporcionando uma visão clara e concisa das relações e da organização dos dados dentro do banco de dados.

![https://drive.google.com/file/d/1tS996mNY6VW1bymbzMfibeJtbmoVGWFq/view?usp=drive_web](./docs/images/Entity%20Relationship%20Diagram.png)

Por fim, o projeto esta sobre a Clean Architecture, visando testabilidade, flexíveis e separação de responsabilidades. Essa abordagem de arquitetura de software promove a separação clara e distinta das responsabilidades em diferentes camadas, permitindo que cada uma delas evolua de forma independente.

![https://drive.google.com/file/d/17xy_svjjbLNoT9HOEAuety9PCTEwO09v/view?usp=drive_web](./docs/images/Clean%20Architecture.png)