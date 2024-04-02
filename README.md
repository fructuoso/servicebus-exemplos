Entendido! Vou ajustar esses pontos para garantir que cada item seja abordado de forma individual e corrigir a classificação dos tópicos. Vamos lá:

---

# Guia Prático do Azure Service Bus com .NET para Iniciantes

Bem-vindo(a) ao **Guia Prático do Azure Service Bus**, criado especialmente para uma LIVE no canal D.E.P.L.O.Y. Este material é seu ponto de partida para explorar como o Azure Service Bus pode ser utilizado em projetos .NET, com foco em quem está começando ou deseja expandir seus conhecimentos.

## O que é o Azure Service Bus?

O Azure Service Bus é um sistema de mensagens na nuvem da Microsoft que facilita a comunicação entre aplicativos e serviços de forma escalável e segura. Pense nele como uma ponte que permite que diferentes partes do seu software conversem entre si, trocando mensagens de maneira eficaz.

## Por que este guia é para você?

Aqui você encontra exemplos práticos para implementar várias funcionalidades do Azure Service Bus em aplicativos .NET. Desde conceitos básicos até dicas avançadas, tudo explicado de maneira simples.

## Conteúdo do Guia

### Exemplos de Código

- **Envio de Mensagens**: Como enviar dados de um ponto a outro do seu sistema.
- **Sem Confirmação de Execução**: O que acontece quando uma ação é realizada sem que haja confirmação.
- **Manuseio de TIMEOUT**: Como lidar com situações onde a mensagem demora mais do que o esperado para ser processada.
- **Garantias de Entrega**: Diferentes formas de assegurar que suas mensagens cheguem ao destino.
  - Execução no Máximo Uma Vez
  - Pelo Menos Uma Vez
  - Exatamente Uma Vez (Evitando Duplicidades)
- **Particionamento**: Aumente a eficiência do seu sistema distribuindo as mensagens por várias filas ou tópicos.
- **Envio em Batch**: Aprenda a enviar múltiplas mensagens de uma só vez para melhorar a performance.

## Primeiros Passos

### Pré-requisitos

Para seguir este guia, você precisa:

- Ter uma conta no Microsoft Azure com acesso ao Azure Service Bus.
- O SDK do Azure para .NET configurado no seu ambiente de desenvolvimento.
- Conhecimento básico em programação .NET (C#) e mensageria.

### Estrutura do Projeto

Nossa solução se divide em:

- **Sender**: Uma WebAPI para enviar mensagens.
- **Receiver**: Um Worker Service para receber e processar as mensagens.

## Filas, Tópicos e Subscrições

Para executar plenamente todos os exemplos presentes nesse repositório, se faz necessário criar as seguintes entidades no Service Bus:

### Filas

| Name | Max Size (GB) | Max Delivery Count | Default Message TTL | Lock Duration | Auto Delete On Idle | Requires Duplicate Detection | Duplicate Detection History Time Window | Dead Lettering On Message Expiration | Enable Partitioning | Requires Session |
|-----------------------|---------------|--------------------|---------------------|---------------|----------------------|----------------------------------|----------------------------------------|--------------------------------------|---------------------|------------------|
| fila-ate-uma-vez | 1 | 10 | 10m | 1m | No | No | | No | No | No |
| fila-basica | 1 | 10 | 10m | 1m | No | No | | No | No | No |
| fila-sem-confirmacao | 1 | 10 | 10m | 5s | No | No | | No | No | No |
| fila-sem-duplicidade | 1 | 10 | 10m | 1m | No | Yes | 10m | No | No | No |
| fila-timeout | 1 | 1 | 10m | 5s | No | No | | No | No | No |
| fila-particionada | 1 | 1 | 10m | 5s | No | No | | No | Yes | No |

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

## Contribuindo

Quer ajudar a enriquecer este guia? Suas ideias para novos exemplos, melhorias ou correções são muito bem-vindas! Abra uma issue ou envie um pull request para contribuir.