var util = {};

util.MascaraValor = function (valor) {
    valor = valor.toString().replace(/\D/g, "");
    valor = valor.toString().replace(/(\d)(\d{8})$/, "$1.$2");
    valor = valor.toString().replace(/(\d)(\d{5})$/, "$1.$2");
    valor = valor.toString().replace(/(\d)(\d{2})$/, "$1,$2");
    return valor;
}

util.Mensagem = function (tipo, mensagem) {
    $.toast().reset('all');
    $("body").removeAttr('class');
    $("body").addClass("top-center-fullwidth");
    switch (tipo) {
        
        case "info":
            $.toast({
                text: '<div class="text-center"><p>' + mensagem + '</p></div>',
                position: 'top-center',
                loaderBg: '#1E301E',
                bgColor: '#4169e1',    
                hideAfter: 8000,
                stack: 6,
                showHideTransition: 'fade'
            });
            break;
        case "warning":
            $.toast({
                text: '<div class="text-center"><p>' + mensagem + '</p></div>',
                position: 'top-center',
                loaderBg: '#1E301E',
                bgColor: '#ffa500',    
                hideAfter: 8000,
                stack: 6,
                showHideTransition: 'fade'
            });
            break;
        case "success":                 
            $.toast({
                text: '<div class="text-center"><p>' + mensagem + '</p></div>',
                position: 'top-center',
                loaderBg: '#1E301E',
                bgColor: '#32cd32',    
                hideAfter: 8000,
                stack: 6,
                showHideTransition: 'fade'
            });
            break;
        case "danger":
            $.toast({
                heading: 'Ops, Algo Errado!',
                text: '<div class="text-center"><p>' + mensagem + '</p></div>',
                position: 'top-center',
                loaderBg: '#1E301E',
                bgColor: '#ff0000',    
                hideAfter: 8000,
                stack: 6,
                showHideTransition: 'fade'
            });
            break;
        default:
            $.toast({
                text: '<div class="text-center"><p>' + mensagem + '</p></div>',
                position: 'top-center',
                loaderBg: '#1E301E',
                class: 'jq-has-icon jq-toast-dark',
                hideAfter: 8000,
                stack: 6,
                showHideTransition: 'fade'
            });
            break
    }


    return false;
}