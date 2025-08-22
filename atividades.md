Obrigatório:

- [x] Receber um envelope JSON, via chamada de API, contendo uma solicitação de simulação de empréstimo
- [x] Consultar um conjunto de informações parametrizadas em uma tabela de banco de dados SQL Server
- [ ] Validar os dados de entrada da API com base nos parâmetros de produtos retornados no banco de dados
- [x] Filtrar qual produto se adequa aos parâmetros de entrada
- [x] Realizar os cálculos para os sistemas de amortização SAC e PRICE de acordo com os dados validados
- [ ] Retornar um envelope JSON contendo o nome do produto validado, e o resultado da simulação utilizando dois sistemas de amortização (SAC e PRICE), gravando este mesmo envelope JSON no EventHub. A gravação no EventHub visa simular uma possibilidade de integração com a área de relacionamento com o cliente da empresa, que receberia em poucos segundos este evento de simulação, e estaria apto à execução de estratégia negocial com base na interação do cliente
- [x] Persistir em banco local a simulação realizada
- [x] Criar um endpoint para retornar todas as simulações realizadas
- [ ] Criar um endpoint para retornar dados de telemetria com volumes e tempos de resposta para cada serviço
- [x] Disponibilizar o código fonte, com todas as evidências no formato zip
- [] Incluir no projeto todos os arquivos para execução via container (dockerfile / Docker compose)

Opcional:

- [x] Pattern de tratamento de exceção
- [ ] Rest nivel III
- [x] README bem escrito
- [ ] Cache
- [ ] Testes unitários
- [ ] Subir um SSO no docker
