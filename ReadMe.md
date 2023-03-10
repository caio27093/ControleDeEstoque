Para rodas o app e ele fazer a conexão com a api corretamente seria necessário trocar o base da url apontando para a sua API já que não está hospedado em nenhum lugar e funcionará para um local host em modo debug por causa do if de aceitar fazer requisição sem certificados confiaveis.
E a base da url fica em Service>AppControleEstoque.Service.Api>Implementations>ApiClient
Na region de Variaveis
