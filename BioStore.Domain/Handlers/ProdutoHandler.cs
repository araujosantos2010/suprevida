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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BioStore.Domain.StoreContext.Handlers
{
    public class ProdutoHandler : Notifiable, ICommandHandler<CriarProdutoCommand>
    {
        private readonly IProdutoRepository _repository;
        public ProdutoHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CriarProdutoCommand command)
        {
            var marca = new Marca(command.MarcaId);
            var categorias = new List<Categoria>();
            var disponibilidade = new Disponibilidade(command.DisponibilideId);
            var fimdisponibilidade = new Disponibilidade(command.FimDisponibilideId);
            command.CategoriaId.ForEach(c => categorias.Add(new Categoria(c)));

            if (command.ProdutoId == default(Guid))
            {
                var produto = new Produto();
                
                if (_repository.CheckProdutoPorNome(command.Nome))
                {
                    AddNotification("Nome", "Este produto já está cadastrado.");
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
                var imagens = new List<string>();
                if(command.Arquivos.Any())
                {
                    for (int i = 0; i < command.Arquivos.Count; i++)
                    {
                        GravarImagem(command.Arquivos[i], $"{command.diretorio}/{produto.ProdutoId}-{i}.jpg");

                        imagens.Add($"{command.diretorio}/{produto.ProdutoId}-{i}.jpg");
                    }                    
                }

               

                var produtoSalvar = new Produto(
                    command.Nome,
                    imagens,
                    command.ProdutoPossuiVariacao,
                    command.ProdutoEmDestaque,
                    command.ProdutoNovo,
                    command.Sku,
                    command.PrecoDeCusto,
                    command.PrecoDeVenda,
                    command.PrecoPromocional,
                    marca,
                    categorias,
                    command.UrlProdutoYouTube,
                    command.Descricao,
                    command.Peso,
                    command.Altura,
                    command.Largura,
                    command.Profundidade,
                    command.GerenciarEstoqueDesseProduto,
                    command.QuantidadeDisponivel,               
                    command.TagTitle,
                    command.MetaTagDescription,
                    command.MetaTagKeywords);
                 
                _repository.Salvar(produtoSalvar);

                return new CommandResult(true, "Produto cadastrado com sucesso!", new
                {
                    produto.ProdutoId
                });

            }
            else
            {
               
                if (_repository.CheckProdutoPorNomeEId(command.Nome, command.ProdutoId))
                {
                    AddNotification("Nome", "Já existe um produto cadastrado com esse nome.");
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

                var imagens = new List<string>();
                if (command.Arquivos.Any())
                {
                    for (int i = 0; i < command.Arquivos.Count; i++)
                    {
                        GravarImagem(command.Arquivos[i], $"{command.diretorio}/{command.ProdutoId}-{i}.jpg");

                        imagens.Add($"{command.diretorio}/{command.ProdutoId}-{i}.jpg");
                    }
                }

                var produto = new Produto(
                   command.ProdutoId,
                   command.Nome,
                   imagens,
                   command.ProdutoPossuiVariacao,
                   command.ProdutoEmDestaque,
                   command.ProdutoNovo,
                   command.Sku,
                   command.PrecoDeCusto,
                   command.PrecoDeVenda,
                   command.PrecoPromocional,
                   marca,
                   categorias,
                   command.UrlProdutoYouTube,
                   command.Descricao,
                   command.Peso,
                   command.Altura,
                   command.Largura,
                   command.Profundidade,
                   command.GerenciarEstoqueDesseProduto,
                   command.QuantidadeDisponivel,                 
                   command.TagTitle,
                   command.MetaTagDescription,
                   command.MetaTagKeywords);

                _repository.Atualizar(produto);

                return new CommandResult(true, "Produto atualizada com sucesso!", new
                {
                    marca.MarcaId
                });
            }
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
