# StarWarsSpaceShipManager🚀
### Contexto
Trata-se de um projeto de um bootcamp da **DIO** - **Digital Innovation One**. O objetivo é criar um **gerenciador de espaçonaves do star wars com SQL Server + .NET**.
Para isso, eu modelei um banco de dados, e fiz uma pequena aplicação WPF para fazer o uso desse DB. (O aquivo para criar o DB estará na pastar scriptsDB)
### Como funciona?
1. Primeiro a tela inicial do app é mostrada;
2. Depois, é preciso que o usuário clique em **"Iniciar Aplicação"**;
3. Para evitar dados duplicados, o app irá apagar todos os dados previamente existentes no banco (caso haja dados)
4. Logo após, ele irá fazer uma séries de requisições à SWAPI (https://swapi.dev/)
5. Após os dados serem fornecidos pela API, a aplicação irá salvar os dados no db
<img src="Images/Captura de Tela (45).png">
