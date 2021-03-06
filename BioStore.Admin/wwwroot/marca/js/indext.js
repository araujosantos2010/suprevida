$(function () {
    $(".lkHabilitar").click(function () {
        alert($(this).data("status"));
        alert($(this).data("id"));
        console.log(JSON.stringify({ MarcaId: "teste" }));

        $.post("/Marca/AtivarDesativar", { MarcaId: $(this).data("id") }, function (data) {
            
        });

        //$.ajax({
        //    url: "/Marca/AtivarDesativar",
        //    type: "POST",
        //    dataType: "json",
        //    contentType: "application/json; charset=utf-8",
        //    data: { MarcaId: "teste" },
        //    success: function (data) {
        //        console.log(data);
        //        $("#" + $(this).data("id")).removeClass("badge-success");
        //    }
        //});
        
    });
});