# Education Platform by Subscription

<p>Plataforma de educação baseada em assinaturas, integrada ao serviço de pagamento Asaas.</p> 

[![build and deploy to app service](https://github.com/backjoao88/edu-subscription/actions/workflows/build.yml/badge.svg)](https://github.com/backjoao88/edu-subscription/actions/workflows/build.yml)

## Regras de negócio

- [x] O sistema deve ser capaz de cadastrar, listar e atualizar planos de assinaturas (apenas administradores);
- [x] O sistema deve ser capaz de cadastrar, listar e atualizar cursos e aulas (apenas administradores);
- [x] O sistema deve ser capaz de cadastrar, listar e atualizar usuários/membros (todos usuários);
- [x] O sistema deve ser capaz de cadastrar registros de pagamentos locais (apenas por meio do evento de assinatura criada);
- [x] O sistema não deve permitir o acesso de membros à cursos não autorizados (por meio da assinatura atual);
- [x] O sistema deve ser capaz de permitir ao usuário escolher uma assinatura dentre as disponíveis;
- [x] O sistema deve ser capaz de receber o evento de pagamentos confirmados e processá-lo internamente;

## Checklist

- [x] Validar os dados de entrada das requisições dos casos de uso;
- [ ] Realizar testes unitários em todos os casos de uso;
- [x] Integrar a geração da cobrança com o Asaas (uma cobrança ou várias);
- [x] Usar pelo menos um evento de domínio;
- [x] Utilizar o Outbox Pattern;
- [x] Integrar um framework de logs;
- [x] Estudar sobre Webhooks e aplicá-los no projeto;
