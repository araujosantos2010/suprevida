using BioStore.Domain.StoreContext.Commands.CategoriaCommands.Inputs;
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
    public class CategoriaHandler : Notifiable, ICommandHandler<CriarCategoriaCommand>
    {
        private readonly ICategoriaRepository _repository;
        public CategoriaHandler(ICategoriaRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CriarCategoriaCommand command)
        {
            var categoria = default(Categoria);
            if (command.CategoriaId == default(Guid))
            {
                if(command.CategoriaPai != null)
                {
                    var categoriaPai  = new Categoria(command.CategoriaPai.CategoriaId, command.Status, command.Destaque, command.Nome, command.Descricao);
                    categoria = new Categoria(command.Status, command.Destaque, command.Nome, command.Descricao, categoriaPai);
                }
                else
                {
                    categoria = new Categoria(command.Status, command.Destaque, command.Nome, command.Descricao);
                }
                    
                // Verificar se o CPF já existe na base
                if (_repository.CheckCategoriaPorNome(command.Nome))
                {
                    AddNotification("Nome", "Esta categoria já está cadastrada.");
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
                    GravarImagem(command.arquivo, $"{command.Logo}/{categoria.CategoriaId}.png");
                }
                

                _repository.Salvar(categoria);

                return new CommandResult(true, "Categoria cadastrada com sucesso!", new
                {
                    categoria.CategoriaId
                });

            }
            else
            {

                if (command.CategoriaPai != null)
                {
                    var categoriaPai = new Categoria(command.CategoriaPai.CategoriaId, command.Status, command.Destaque, command.Nome, command.Descricao);
                    
                    categoria = new Categoria(command.CategoriaId, command.Status, command.Destaque, command.Nome, command.Descricao, categoriaPai);
                }
                else
                {
                    categoria = new Categoria(command.CategoriaId, command.Status, command.Destaque, command.Nome, command.Descricao);
                }

                
                if (_repository.CheckCategoriaPorNomeEId(command.Nome, command.CategoriaId))
                {
                    AddNotification("Nome", "Já existe uma categoria cadastrada com esse nome.");
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
               
                _repository.Atualizar(categoria);

                return new CommandResult(true, "Categoria atualizada com sucesso!", new
                {
                    categoria.CategoriaId
                });
            }
        }

        public ICommandResult Handle(ECategoriaStatus status, Guid id)
        {
            _repository.HabilitaOuDesabilitar(status, id);

            return new CommandResult(true, $"Categoria {(status == ECategoriaStatus.Ativo ? "ativada" : "desativada") }  com sucesso!", new
            {
                CategoriaId = id
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
