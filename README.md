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
 
 
6. Em seguida ele irá ocultar o primeiro Form, e abrirá o Form de viagens;
7. O usuário deve escolher um piloto e uma viagem, clicar em **"Iniciar Viagem"** e em seguida o app irá verificar se o piloto tem ou não permissão para pilotar aquela naves (segundo os dados fornecidos pela API);
8. Caso, seja possível, a viagem será iniciada e registrada no DB;


<img src="Images/Captura de Tela (46).png">;


### Como usar? 
1. Primeiro é preciso clonar o projeto 
2. Depois, é preciso executar o arquivo que está na pasta ScriptsDB
3. Compile com o VS Code
4. **Divirta-se!**
