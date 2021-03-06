using BioStore.Domain.StoreContext.Commands.GradeCommands.Inputs;
using BioStore.Domain.StoreContext.CustomerCommands.Inputs;
using BioStore.Domain.StoreContext.Entities;
using BioStore.Domain.StoreContext.Enums;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Shared.Commands;
using BioStore.Shared.Commands.Output;
using BioStore.Shared.Enums;
using FluentValidator;
using System;
using System.IO;

namespace BioStore.Domain.StoreContext.Handlers
{
    public class GradeHandler : Notifiable, ICommandHandler<CriarGradeCommand>
    {
        private readonly IGradeRepository _repository;
        public GradeHandler(IGradeRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CriarGradeCommand command)
        {
            
            if (command.GradeId == default(Guid))
            {
                var Grade = new Grade(command.Nome);
                // Verificar se o CPF já existe na base
                if (_repository.CheckGradePorNome(command.Nome))
                {
                    AddNotification("Nome", "Esta Grade já está cadastrada.");
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications, EtipoMensagem.Info);
                }

                if (Invalid)
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications);
               
                

                _repository.Salvar(Grade);

                return new CommandResult(true, "Grade cadastrada com sucesso!", new
                {
                    Grade.GradeId,
                    Grade.Nome 
                });

            }
            else
            {
                var Grade = new Grade(command.GradeId, command.Nome);
                if (_repository.CheckGradePorNomeEId(command.Nome, command.GradeId))
                {
                    AddNotification("Nome", "Já existe uma Grade cadastrada com esse nome.");
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications, EtipoMensagem.Warning);
                }

                if (Invalid)
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications);

               
                _repository.Atualizar(Grade);

                return new CommandResult(true, "Grade atualizada com sucesso!", new
                {
                    Grade.GradeId
                });
            }
        }

        public ICommandResult Handle(CriarVariacaoCommand command)
        {

            if (command.VariacaoId == default(Guid))
            {
                var variacao = new Variacao(command.Nome, new Grade(command.Grade.GradeId));
                // Verificar se o CPF já existe na base
                if (_repository.CheckVariacaoPorNomeGrade(command.Nome, command.Grade.GradeId))
                {
                    AddNotification("Nome", "Variação já cadastrada paa grade.");
                    return new CommandResult(
                        false,
                        "Variação já cadastrada paa grade",
                        Notifications, EtipoMensagem.Info);
                }

                if (Invalid)
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications);



                _repository.SalvarVariacao(variacao);

                return new CommandResult(true, "Grade cadastrada com sucesso!", new
                {
                    variacao.VariacaoId,
                    variacao.Nome
                }, exibirMensagem: false);

            }
            else
            {
                var Grade = new Grade(command.VariacaoId, command.Nome);
                if (_repository.CheckGradePorNomeEId(command.Nome, command.VariacaoId))
                {
                    AddNotification("Nome", "Já existe uma Grade cadastrada com esse nome.");
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications, EtipoMensagem.Warning);
                }

                if (Invalid)
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications);


                _repository.Atualizar(Grade);

                return new CommandResult(true, "Grade atualizada com sucesso!", new
                {
                    Grade.GradeId
                });
            }
        }
    }
}
