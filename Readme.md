# Sistema
<p>Sistema desenvolvido, para calcular uma divida com juros compostos e simples!</p>
##Iniciando
<p>Os próximos tópicos o guiarão para que você tenha uma cópia deste projeto em sua máquina local funcionando, pronto para desenvolver e executar testes.<p>

## Pré-requisitos
+ 1 .NET Core SDK 3.1
+ PostgreSQL 10.12

## Instalação
<p>Passo a passo para executar a aplicação</p>

<p>Clone o repositório em seu computador:</p>

```git@gitlab.com:Scalfi/paschoalotto.backend.git```

<p>Entre na pasta do repositorio e rode o codigo</p>

```dotnet restore```


#### Banco de dados

<p>Para configurar o banco de dados, selecione o appsettings e na linha DatabaseConection configure as credencias de conexão com seu banco de dados postgres<p>

```"DatabaseConection": "User ID =postgres;Password=senha;Server=localhost;Port=5432;Database=paschoalotto;Integrated Security=true;Pooling=true;"```

<p>Para criar as tabelas no banco de dados você deve ir na  pasta schemadb e copiar o SQL e executar no postgres.</p>

<p>  Com programa restaurado e banco de dados configurado, rode o comando abaixo para iniciar o sistema<p>

```dotnet run```