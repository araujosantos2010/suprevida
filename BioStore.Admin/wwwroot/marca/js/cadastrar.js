var Marca = {};
$(function () {
    $('.dropify').dropify({
        messages: {
            'default': 'Arraste e solte um arquivo aqui ou clique',
            'replace': 'Arraste e solte ou clique para substituir',
            'remove': 'Remover',
            'error': 'Ooops, algo de errado aconteceu.'
        },
        error: {
            'fileSize': 'O tamanho do arquivo é muito grande {{ value }}.',            
            'fileExtension': 'Tipo de aruivo inválido  insira um arquivo (jpg, png, jpeg ou gif).',
            'maxWidth': 'A largura máxima deve ser de {{ value }} px.'
        }
    });

    $("#btnUpload").click(function () {
        Marca.uploadFiles();
    });
});

Marca.uploadFiles = function () {
    // Get the files
    var file1 = $("#input-file-now")[0].files[0];
    var formData = new FormData();
    formData.append("files", file1);
    // You can abort the upload by calling jqxhr.abort();
    var jqxhr = $.ajax({
        url: "/Administracao/upload/processar",
        type: "POST",
        contentType: false,
        data: formData,
        dataType: "json",
        cache: false,
        processData: false,
        async: false
    })
        .done(function (data, textStatus, jqXhr) {
            if (data) {
                util.Mensagem("success", "Arquivo enviado com sucesso!");
                $(".dropify-clear").click();
            }
            else {
                util.Mensagem("danger", "Já existe cum arquivo com o mesmo nome");
            }

        })
        .fail(function (jqXhr, textStatus, errorThrown) {

            if (errorThrown === "abort") {
                util.Mensagem("danger", "O upload foi abortado");
            } else {
                util.Mensagem("danger", "Erro ao enviar arquivo");
            }
        })
        .always(function (data, textStatus, jqXhr) { });
};