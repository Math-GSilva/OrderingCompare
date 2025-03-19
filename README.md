# README - Projeto Ordering Compare
Integrantes: Matheus G. da Silva, Larissa Hoffmann, Lukas Thiago Rodrigues, Mateus Akira.

## Descrição
O projeto Ordering Compare é uma API para geração, leitura e ordenação de números usando diferentes algoritmos de ordenação. Ele implementa o padrão Strategy para permitir a seleção dinâmica do algoritmo de ordenação desejado.

## Funcionalidades
### **Geração e Leitura de Arquivos**
- **Gerar Números** (`POST /api/numbersgenerator/gerar`): Gera um arquivo contendo uma lista de números aleatórios.
- **Ler Números** (`GET /api/numbersgenerator/ler`): Lê os números armazenados no arquivo gerado.

### **Ordenação de Números**
- **Ordenar Números** (`POST /api/numbersgenerator/ordenar?algoritmo=<nome_do_algoritmo>`): Ordena os números usando um dos algoritmos disponíveis:
  - `bubble`
  - `bubble-optimized`
  - `insertion`
  - `selection`
  - `counting`
  - `heap`
  - `merge`
  - `quick`
  - `radix`
  - `shell`
  - `tim`

O método de ordenação utiliza o arquivo previamente gerado pelo endpoint de geração de números.

## Padrão de Desenvolvimento
O projeto utiliza o **padrão Strategy** para desacoplar a lógica de ordenação, permitindo que novos algoritmos possam ser adicionados facilmente sem modificar a estrutura existente.

## Requisitos
O ELK Stack está configurado em um contêiner Docker. Para visualizar logs e métricas, é necessário garantir que o ELK esteja devidamente configurado e em execução.

## Execução do Projeto
1. Clone o repositório:
```sh
git clone <URL_DO_REPOSITORIO>
cd OrderingCompare
```
2. Configure as dependências necessárias (como .NET, se aplicável).
3. Execute a API:
```sh
dotnet run
```
Agora você pode interagir com os endpoints e testar os diferentes algoritmos de ordenação.

