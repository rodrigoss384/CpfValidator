# Microsserviço de Validação de CPF com Azure Functions

Este é um projeto de um microsserviço serverless construído com .NET 8 e Azure Functions. A sua finalidade é fornecer um endpoint HTTP simples e eficiente para validar números de CPF (Cadastro de Pessoas Físicas) do Brasil.

## ✨ Features

-   **Endpoint HTTP:** Exposto via HTTP Trigger, fácil de integrar com qualquer aplicação.
-   **Validação Robusta:** Implementa o algoritmo padrão de cálculo dos dígitos verificadores para validar a estrutura do CPF.
-   **Arquitetura Serverless:** Projetado para ser econômico, escalável e de baixa manutenção, rodando na plataforma Azure Functions.
-   **.NET 8 Isolado:** Utiliza o modelo de processo isolado do .NET, recomendado para performance e flexibilidade.

## 🛠️ Tecnologias Utilizadas

-   **[C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)**
-   **[.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)**
-   **[Azure Functions Core Tools](https://learn.microsoft.com/pt-br/azure/azure-functions/functions-run-local)**

## 🚀 Como Executar Localmente

Siga os passos abaixo para testar a aplicação na sua máquina.

### Pré-requisitos

-   [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download)
-   [Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools#installing)
-   [Visual Studio Code](https://code.visualstudio.com/) ou outra IDE de sua preferência

### Passos

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/rodrigoss384/CpfValidator.git](https://github.com/rodrigoss384/CpfValidator.git)
    cd CpfValidator
    ```

2.  **Restaure as dependências e compile o projeto:**
    ```bash
    dotnet restore
    dotnet build
    ```

3.  **Inicie o host do Azure Functions:**
    ```bash
    func start
    ```

4.  **Teste o endpoint:**
    O terminal irá exibir a URL local. Use um navegador ou uma ferramenta como Postman para testar.

    -   **URL Base:** `http://localhost:7071/api/validate/{cpf}`
    -   **Exemplo (CPF Válido):** `http://localhost:7071/api/validate/11144477735`
    -   **Exemplo (CPF Inválido):** `http://localhost:7071/api/validate/12345678900`

    A resposta será um JSON indicando a validade do CPF:
    ```json
    {
      "cpf": "11144477735",
      "isValid": true
    }
    ```

## ☁️ Status da Publicação no Azure

**O código da aplicação está 100% funcional e opera corretamente em ambiente local.**

No entanto, o processo de deploy na nuvem Azure está atualmente bloqueado por um problema na plataforma. Ao tentar criar os recursos necessários (como a Conta de Armazenamento) via Azure CLI, a plataforma retorna o erro `(SubscriptionNotFound)`, mesmo quando todas as verificações confirmam que as permissões e configurações estão corretas.

### Diagnósticos Realizados

-   **Permissão:** O comando `az role assignment list` confirmou que a conta possui a permissão de **`Owner`** (Proprietário) sobre a assinatura.
-   **Status da Assinatura:** O Portal do Azure confirma que a assinatura está **`Ativa`**.
-   **Autenticação:** Foram realizados múltiplos ciclos de `az logout` e `az login` (incluindo login via `--tenant`) para garantir que não havia problemas de cache de credenciais.

A conclusão é que se trata de uma inconsistência no backend do Azure, que impede o reconhecimento da assinatura. Um ticket de suporte foi recomendado como o próximo passo para resolver o problema de provisionamento.
