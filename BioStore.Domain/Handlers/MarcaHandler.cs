using BioStore.Domain.StoreContext.Commands.MarcaCommands.Inputs;
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
    public class MarcaHandler : Notifiable, ICommandHandler<CriarMarcaCommand>
    {
        private readonly IMarcaRepository _repository;
        public MarcaHandler(IMarcaRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CriarMarcaCommand command)
        {
            
            if (command.MarcaId == default(Guid))
            {
                var marca = new Marca(command.Nome, command.Destaque, command.Logo, command.Descricao, command.Status);
                // Verificar se o CPF já existe na base
                if (_repository.CheckMarcaPorNome(command.Nome))
                {
                    AddNotification("Nome", "Esta marca já está cadastrada.");
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
                if(!string.IsNullOrEmpty(command.Logo))
                {
                    GravarImagem(command.arquivo, $"{command.Logo}/{marca.MarcaId}.png");
                }
                

                _repository.Salvar(marca);

                return new CommandResult(true, "Marca cadastrada com sucesso!", new
                {
                    marca.MarcaId
                });

            }
            else
            {
                var marca = new Marca(command.MarcaId, command.Nome, command.Destaque, command.Logo, command.Descricao, command.Status);
                if (_repository.CheckMarcaPorNomeEId(command.Nome, command.MarcaId))
                {
                    AddNotification("Nome", "Já existe uma marca cadastrada com esse nome.");
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

                if (!string.IsNullOrEmpty(command.arquivo))
                {
                    GravarImagem(command.arquivo, $"{command.Logo}/{marca.MarcaId}.png");
                }

                _repository.Atualizar(marca);

                return new CommandResult(true, "Marca atualizada com sucesso!", new
                {
                    marca.MarcaId
                });
            }
        }

        public ICommandResult Handle(EMarcaStatus status, Guid id)
        {
            _repository.HabilitaOuDesabilitar(status, id);

            return new CommandResult(true, $"Marca {(status == EMarcaStatus.Ativo ? "ativada" : "desativada") }  com sucesso!", new
            {
                MarcaId = id
            });
            
        }

        private void GravarImagem(string imagem, string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                if (File.Exists(nome))
                {
                    File.Delete(nome);
                }
                File.WriteAllBytes(nome, Convert.FromBase64String(imagem));
            }
        }
    }
}
