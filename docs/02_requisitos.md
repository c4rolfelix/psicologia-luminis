# 📋 Requisitos do Sistema – Psicologia Luminis

---

## ✅ Requisitos Funcionais (RF)
Funcionalidades que o sistema deverá oferecer ao usuário.

### RF001 – Cadastro de psicólogos
O sistema deve permitir que psicólogos se cadastrem, preenchendo informações como nome, CRP, especialidades, formas de contato e currículo.

### RF002 – Cadastro de pacientes
O sistema deve permitir que pacientes se cadastrem com nome, e-mail e senha para acessar a plataforma.

### RF003 – Login de usuários
O sistema deve permitir que psicólogos e pacientes façam login com suas credenciais.

### RF004 – Pagamento da mensalidade do psicólogo
O sistema deve permitir que psicólogos efetuem o pagamento de uma taxa mensal para manterem seus perfis ativos e visíveis.

### RF005 – Listagem de psicólogos
O sistema deve exibir uma lista com os psicólogos cadastrados para os pacientes, com filtros por especialidade, abordagem ou localização.

### RF006 – Redirecionamento para contato
O sistema deve permitir que o paciente entre em contato com o psicólogo via WhatsApp, e-mail ou outro meio configurado no perfil do profissional.

### RF007 – Painel do psicólogo
O sistema deve oferecer um painel para que o psicólogo possa editar seus dados, renovar o pagamento e visualizar seu status de visibilidade.

### RF008 – Painel do paciente (simples)
O sistema deve permitir que o paciente veja os profissionais já contatados e edite suas informações básicas.

### RF009 – Agenda de disponibilidade do psicólogo
O sistema deve permitir que psicólogos cadastrem dias e horários em que estão disponíveis para atendimento.

### RF010 – Visualização da agenda pelos pacientes
O sistema deve permitir que pacientes vejam a agenda do psicólogo e escolham o melhor horário para entrar em contato ou agendar externamente.

### RF011 – Envio e armazenamento de informações básicas ao psicólogo
Antes de ser redirecionado para o contato externo, o paciente deve preencher um pequeno formulário com os seguintes dados:

- Nome completo
- Idade
- Gênero (opcional)
- Motivo do interesse pelo atendimento
- Melhor horário para contato

Essas informações devem ser:
- Armazenadas no banco de dados associadas ao psicólogo selecionado.
- Visualizadas pelo psicólogo no seu painel, em uma área chamada “Contatos recebidos”.

---

## 🚧 Requisitos Não Funcionais (RNF)
Características de qualidade, segurança e desempenho que o sistema precisa ter.

### RNF001 – Interface responsiva
O sistema deve ser acessível em computadores, tablets e celulares.

### RNF002 – Acessibilidade
O sistema deve seguir boas práticas de acessibilidade (ex: contraste adequado, legibilidade e uso de labels em formulários).

### RNF003 – Código versionado
O código-fonte deve ser mantido em repositório Git, com histórico de alterações.

### RNF004 – Segurança
O sistema deve proteger os dados dos usuários com autenticação, senhas criptografadas e comunicação segura.
