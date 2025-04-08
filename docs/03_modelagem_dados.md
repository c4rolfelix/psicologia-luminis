#  Modelagem de Dados – Psicologia Luminis

---

##  Visão Geral

Abaixo estão listadas as principais entidades do sistema, seus campos e os relacionamentos entre elas. Essa modelagem serve como base para o desenvolvimento do banco de dados.

---

## Usuário

| Campo         | Tipo         | Descrição                        |
|---------------|--------------|----------------------------------|
| Id            | int          | Identificador único              |
| Nome          | string       | Nome completo                    |
| Email         | string       | E-mail para login                |
| Senha         | string (hash)| Senha de acesso                  |
| Tipo          | enum         | "Psicólogo" ou "Paciente"        |
| DataCadastro  | datetime     | Registro de criação              |

---

##  Psicólogo

| Campo            | Tipo     | Descrição                           |
|------------------|----------|-------------------------------------|
| Id               | int      | FK de Usuário                       |
| CRP              | string   | Número do registro profissional     |
| Especialidade    | string   | Abordagem terapêutica               |
| Curriculo        | string   | Mini bio, experiência, formação     |
| ContatoWhatsapp  | string   | Link ou número de contato           |
| PlanoAtivo       | bool     | Indica se o pagamento está em dia   |

---

##  Paciente

| Campo         | Tipo   | Descrição                         |
|---------------|--------|-----------------------------------|
| Id            | int    | FK de Usuário                     |
| CPF (opcional)| string | Pode ser adicionado futuramente   |

---

##  Plano (assinatura dos psicólogos)

| Campo         | Tipo     | Descrição                          |
|---------------|----------|------------------------------------|
| Id            | int      | Identificador                     |
| PsicologoId   | int      | FK para Psicólogo                  |
| Valor         | decimal  | Valor pago                         |
| DataInicio    | datetime | Início da vigência                 |
| DataFim       | datetime | Fim da vigência                    |
| Status        | string   | Ativo, Expirado, Cancelado         |

---

##  Agenda

| Campo         | Tipo     | Descrição                            |
|---------------|----------|--------------------------------------|
| Id            | int      | Identificador                       |
| PsicologoId   | int      | FK para Psicólogo                   |
| DiaSemana     | string   | Segunda, Terça, etc.                |
| HoraInicio    | time     | Ex: 08:00                           |
| HoraFim       | time     | Ex: 12:00                           |

---

##  ContatoRecebido

| Campo           | Tipo     | Descrição                          |
|-----------------|----------|------------------------------------|
| Id              | int      | Identificador                      |
| PsicologoId     | int      | FK para Psicólogo                  |
| NomePaciente    | string   | Nome informado no formulário       |
| Idade           | int      | Idade                              |
| Genero          | string   | Gênero (opcional)                  |
| Motivo          | string   | Motivo do contato                  |
| MelhorHorario   | string   | Horário ideal de atendimento       |
| DataContato     | datetime | Data em que foi enviado            |

---

##  Relacionamentos

- Um **usuário** pode ser um **psicólogo** ou **paciente**.
- Um **psicólogo** tem **vários contatos recebidos**.
- Um **psicólogo** tem **uma ou mais agendas**.

---
##  Diagrama de fluxos



##  Modelo Entidade-Relacionamento

