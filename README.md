# Exemplos de Uso do Azure Service Bus com .NET

Bem-vindo ao repositório **Service Bus Exemplos**, desenvolvido especificamente para uma LIVE no canal D.E.P.L.O.Y. Este repositório serve como uma demonstração prática do uso do Azure Service Bus com .NET 8.

## Descrição

O Azure Service Bus é um serviço de mensagens totalmente gerenciado fornecido pela Microsoft Azure. Ele oferece uma plataforma robusta e escalável para o envio, recebimento e processamento de mensagens em nuvem.

Este repositório foi criado para demonstrar exemplos práticos de como utilizar o Azure Service Bus em aplicativos desenvolvidos com o framework .NET. Aqui você encontrará exemplos de código, guias de início rápido e outros recursos úteis para começar a trabalhar com o Azure Service Bus em seus projetos .NET.

## Conteúdo

O repositório inclui os seguintes recursos:

- Envio e Leitura de mensagens em filas;
- (Adicione outros recursos conforme necessário)

## Pré-requisitos

Para executar os exemplos neste repositório, você precisará ter:

- Uma assinatura do Microsoft Azure com acesso ao Azure Service Bus.
- O SDK do Azure para .NET instalado em seu ambiente de desenvolvimento.
- Conhecimento básico de desenvolvimento .NET (C#) e conceitos de mensageria.

## Estrutura do Projeto

A solução está estruturada em dois projetos:

### Sender

Projeto WebAPI responsável pelo envio das mensagens. Cada ação do controller é responsável por executar um exemplo.

### Receiver

Projeto Worker Service responsável pelo processamento das mensagens. Cada worker é responsável por processar um exemplo.

## Como Usar (VS Code)

1. Clone este repositório em sua máquina local.
2. Restaure a Solução.
```sh
dotnet restore
```
3. Configure os SECRESTS (em ambos os projetos)
```sh
dotnet user-secrets --project Sender set ServiceBus:Namespace <NAMESPACE_SERVICE_BUS>
```
```sh
dotnet user-secrets --project Receiver set ServiceBus:Namespace <NAMESPACE_SERVICE_BUS>
```
4. Realize o Login no Azure CLI
```sh
az login
```
5. Inicie os 2 projetos (Um em cada janela do Terminal)
```sh
dotnet run --project Sender
dotnet run --project Receiver
```

## Contribuições

Contribuições são bem-vindas! Se você tiver ideias para novos exemplos, melhorias nos exemplos existentes ou correções de problemas, sinta-se à vontade para abrir uma issue ou enviar um pull request.
