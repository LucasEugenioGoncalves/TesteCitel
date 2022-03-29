
/*functions onclick*/
function CadastrarProduto() {

    event.preventDefault();

    $.blockUI({
        message: '<h5>Aguarde!....</h5>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });

    let idProduto = $("#productId").val();
    const name = $("#input-produto-nome").val();
    const price = $("#input-produto-preco").val();
    const category = $("#categories").val();
    
    const model = { 'Name': name, 'Price': price, 'CategoryId': category, 'Id': idProduto};

    if (idProduto == "") {
        $.ajax({
            type: 'POST',
            url: "/Product/Create",
            data: JSON.stringify(model),
            contentType: "application/json",
            dataType: 'html',
            success: function (data) {
                $('#tableproducts').empty();
                $('#tableproducts').html(data);
                $.unblockUI({});

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Produto cadastrado com sucesso',
                    showConfirmButton: false,
                    timer: 2500
                })
                LimparCampos();
            },
            error: function (data) {
                $.unblockUI({});
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Produto não pode ser cadastrado!',
                    showConfirmButton: false,
                    timer: 2500
                })
            }
        });
    }
    else {
        $.ajax({
            type: 'PUT',
            url: "/Product/Alter",
            data: JSON.stringify(model),
            contentType: "application/json",
            dataType: 'html',
            success: function (data) {
                $('#tableproducts').empty();
                $('#tableproducts').html(data);
                $.unblockUI({});

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Produto alterado com sucesso',
                    showConfirmButton: false,
                    timer: 2500
                })
                LimparCampos();
            },
            error: function (data) {
                $.unblockUI({});
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Produto não pode ser alterado!',
                    showConfirmButton: false,
                    timer: 2500
                })
            }
        });
    }
   
  
}

function ExcluirProduto(idproduto) {

    Swal.fire({
        title: "Tem certeza?",
        text: "Você não poderá recuperar o produto após a exclusão.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, exclua agora!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'DELETE',
                url: '/Product/Remove',
                data: { id: idproduto },
                dataType: 'html',
                success: function (data) {
                    $('#tableproducts').empty();
                    $('#tableproducts').html(data);
                    Swal.fire(
                        'Excluído!',
                        'Produto excluído com sucesso.',
                        'success'
                    )
                    LimparCampos();
                },
                error: function (data) {
                    $.unblockUI({});
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Produto não pode ser excluído!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                }
            });

        }
    })

}

function CadastrarCategoria() {

    event.preventDefault();

    $.blockUI({
        message: '<h5>Aguarde!....</h5>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });

    let idCategory = $("#categoryId").val();
    const name = $("#input-categoria-nome").val();

    const model = { 'Name': name, 'Id': idCategory };

    if (idCategory == "") {
        $.ajax({
            type: 'POST',
            url: "/Category/Create",
            data: JSON.stringify(model),
            contentType: "application/json",
            dataType: 'html',
            success: function (data) {
                $('#tablecategories').empty();
                $('#tablecategories').html(data);
                $.unblockUI({});

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Categoria cadastrado com sucesso',
                    showConfirmButton: false,
                    timer: 2500
                })
                LimparCampos();
            },
            error: function (data) {
                $.unblockUI({});
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Categoria não pode ser cadastrada!',
                    showConfirmButton: false,
                    timer: 2500
                })
            }
        });
    }
    else {
        $.ajax({
            type: 'PUT',
            url: "/Category/Alter",
            data: JSON.stringify(model),
            contentType: "application/json",
            dataType: 'html',
            success: function (data) {
                $('#tablecategories').empty();
                $('#tablecategories').html(data);
                $.unblockUI({});

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Categoria alterado com sucesso',
                    showConfirmButton: false,
                    timer: 2500
                })
                LimparCampos();
            },
            error: function (data) {
                $.unblockUI({});
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Categoria não pode ser alterada!',
                    showConfirmButton: false,
                    timer: 2500
                })
            }
        });
    }


}

function ExcluirCategoria(idproduto) {

    Swal.fire({
        title: "Tem certeza?",
        text: "Você não poderá recuperar o produto após a exclusão.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim, exclua agora!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'DELETE',
                url: '/Category/Remove',
                data: { id: idproduto },
                dataType: 'html',
                success: function (data) {
                    $('#tablecategories').empty();
                    $('#tablecategories').html(data);
                    Swal.fire(
                        'Excluído!',
                        'Categoria excluída com sucesso.',
                        'success'
                    )
                    LimparCampos();
                },
                error: function (data) {
                    $.unblockUI({});
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Categoria não pode ser excluida!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                }
            });
        }
    })

}

function LimparCampos() {

    $("#input-produto-nome").val("");
    $("#input-produto-preco").val("");
    $("#input-categoria-nome").val("");
    $("#productId").val("");
    $("#categoryId").val("");
}

/*----VALIDAR LOGIN -----*/

function ValidarLogin() {

    event.preventDefault();

    $.blockUI({
        message: '<h5>Aguarde!....</h5>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });

    var Usuario = $("#useradmin").val();
    var Senha = $("#pwdadmin").val();

    var AdminInputModel = $.extend({

        'Usuario': Usuario,
        'Senha': Senha
    }, {
        "CSRF-TOKEN-MOONGLADE-FORM": $('input[name="CSRF-TOKEN-MOONGLADE-FORM"]').val()
    });
    


    $.ajax({
        url: "/Admin/ValidarLogin",
        headers: {
            "CSRF-TOKEN-MOONGLADE-FORM": $('input[name="CSRF-TOKEN-MOONGLADE-FORM"]').val()
        },
        type: 'POST',
        data: AdminInputModel, 
        datatype: 'html',
        success: function (data) {

            $.unblockUI({});
            $.blockUI({ message: '<h5> Carregando Perfil...</h5>' });

            window.location.href = "/Admin/Index";
        },
        error: function (data) {

            $.unblockUI({});

            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Usuário ou senha inválidos.',
                showConfirmButton: false,
                timer: 2500
            })
            
        }
    });
}

function isEmpty(obj) {

    
    if (obj == null) return true;

    if (obj.length > 0) return false;
    if (obj.length === 0) return true;

    
    for (var key in obj) {
        if (hasOwnProperty.call(obj, key)) return false;
    }

    return true;
}
