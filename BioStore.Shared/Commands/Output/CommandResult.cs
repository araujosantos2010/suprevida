using BioStore.Shared.Enums;

namespace BioStore.Shared.Commands.Output
{
    public sealed class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message, object data, EtipoMensagem tipoMensagem = EtipoMensagem.Error, bool exibirMensagem = true)
        {
            TipoMensagem = tipoMensagem;
            Success = success;
            Message = message;
            Data = data;
            ExibirMensagem = exibirMensagem;
        }

        public EtipoMensagem TipoMensagem { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public bool ExibirMensagem { get; set; }
    }
}
