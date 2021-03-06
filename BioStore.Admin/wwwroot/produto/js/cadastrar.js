var Produto = {};
$(function () {   
   
    Produto.ControlarEstoque($("#GerenciarEstoqueDesseProduto").is(":checked"));
    Produto.PossuiVariacao($("#ProdutoPossuiVariacao").is(":checked"));
    
    $('.dropify').dropify({
        messages: {
            'default': 'Adicionar',
            'replace': 'Substituir',
            'remove': 'Remover',
            'error': 'Ooops, algo de errado aconteceu.'
        },
        error: {
            'fileSize': 'O tamanho do arquivo é muito grande {{ value }}.',            
            'fileExtension': 'Tipo de aruivo inválido  insira um arquivo (jpg, png, jpeg ou gif).',
            'maxWidth': 'A largura máxima deve ser de {{ value }} px.'
        }
    });


    $("#GerenciarEstoqueDesseProduto").change(function () {
        Produto.ControlarEstoque($(this).is(":checked"));
    });

    $("#ProdutoPossuiVariacao").change(function () {
        Produto.PossuiVariacao($(this).is(":checked"));
    });

    $('#MarcaId').select2({
        placeholder: "Selecione uma marca"
    });

    $('#GradeId').select2({
        placeholder: "Selecione uma grade"
    });

    $("#VariacaoId").change(function () {
        alert();
        $("#variacaoNome").val($(this).find(":selected").text());
    });

    $('#VariacaoId').select2({
        placeholder: "Selecione uma ou mais variações"
    });

    $('#CategoriaId').select2({
        placeholder: "Selecione uma categoria"
    });

    $("#btnGerarCodigo").click(function () {
        $.ajax({
            url: "/produto/GerarCodigo",
            type: "GET",
            contentType: false,          
            dataType: "json",
            cache: false,
            processData: false          
        })
            .done(function (data, textStatus, jqXhr) {
                $("#Sku").val(data);
        })            
            .always(function (data, textStatus, jqXhr) { });
    });
    
   
});


Produto.showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            //$('.modal-dialog').draggable({
            //    handle: ".modal-header"
            //});
        }
    })
}

Produto.jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

Produto.jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

Produto.PossuiVariacao = function (checked) {
    if (checked) {
        $(".sectionVariacao").fadeIn(600);
    }
    else {
        $(".sectionVariacao").fadeOut(600);
    }
    
};

Produto.ControlarEstoque = function (checked) {

    if (checked) {
        $(".estoque").fadeIn(600);
    }
    else {
        $(".estoque").fadeOut(600);
    }
   
};