
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

    var rte = document.querySelector("#profileImage").src;

    var c = document.createElement('canvas');
    var img = document.getElementById('profileImage');
    c.height = img.naturalHeight;
    c.width = img.naturalWidth;
    var ctx = c.getContext('2d');

    ctx.drawImage(img, 0, 0, c.width, c.height);
    var base64String = c.toDataURL();


    var idProduto = $("#idproduto").val();
    var nome = $("#input-produto-nome").val();
    var descricao = $("#input-produto-descricao").val();
    var preco = $("#input-produto-preco").val();
    var estoque = $("#input-produto-estoque").val();

    var url = "/Admin/Cadastrarproduto";

    if (idProduto != "") {

        url = "/Admin/EditarProduto";
    }
    else {

        idProduto = 0;
    }

    var ProdutoInputModel = {
        'idProduto': parseInt(idProduto),
        'nomeProduto': nome,
        'descricaoProduto': descricao,
        'precoProduto': preco,
        'estoqueProduto': estoque,
        'fotoProduto': base64String
    };

    var dataJson = JSON.stringify(ProdutoInputModel);

    console.log(dataJson);

    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(ProdutoInputModel),
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
                title: 'Internal Server Error!',
                showConfirmButton: false,
                timer: 2500
            })
        }
    });
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
                url: '/Admin/ExcluirProduto',
                data: { idProduto: idproduto },
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
                        title: 'Internal Server Error!',
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
    $("#input-produto-descricao").val("");
    $("#input-produto-preco").val("");
    $("#input-produto-estoque").val("");
    $("#idproduto").val("");
    $("#imageUpload").val("");
    document.getElementById('divbtnalterarfoto').style.display = 'none';


}

function RemoveItem(nomeproduto) {

    var cs = [];
    var prod = localStorage.getItem('carrinho');

    var contact = JSON.parse(prod);

    contact.splice(nomeproduto, 1);

    localStorage.setItem("carrinho", JSON.stringify(contact));

    var itens = localStorage.getItem('carrinho');

    if (itens != null) {

        var contact = JSON.parse(itens);

        for (i = 0; i < contact.length; i++) {

            var lista = {
                produto: contact[i].produto,
                qtd: contact[i].qtd,
                valor: contact[i].valor,
                posicao: i,
                id: contact[i].id,
                foto: contact[i].foto
            };

            cs.push(lista);
        }
    }


    localStorage.setItem("carrinho", JSON.stringify(cs));

    window.location.reload();
}

function AddCarrinho(produto, qtd, valor, posicao, id, foto) {

    var cs = [];

    var itens = localStorage.getItem('carrinho');

    if (itens != null) {

        var contact = JSON.parse(itens);

        for (i = 0; i < contact.length; i++) {

            var lista = {
                produto: contact[i].produto,
                qtd: contact[i].qtd,
                valor: contact[i].valor,
                posicao: contact[i].posicao,
                id: contact[i].id,
                foto: contact[i].foto
            };

            cs.push(lista);
        }
    }

    if (cs.length == 1) {

        posicao = 1;
    }
    else {
        posicao = cs.length;
    }

    var lst = {
        produto: produto,
        qtd: qtd,
        valor: valor,
        posicao: posicao,
        id: id,
        foto: foto
    };

    cs.push(lst);

    localStorage.setItem("carrinho", JSON.stringify(cs));

    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Produto adicionado ao carrinho!',
        showConfirmButton: false,
        timer: 2000
    })

}


function finalizarCompra() {

    var itens = localStorage.getItem('carrinho');

    if (itens != null) {

        var ct = JSON.parse(itens);

        if (ct.length != 0) {
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

            var lstidproduto = [];

            var contact = JSON.parse(itens);

            for (i = 0; i < contact.length; i++) {

                lstidproduto.push(parseInt(contact[i].id));
            }


            var dt = {

                'lstidproduto': lstidproduto
            };


            $.ajax({
                type: 'POST',
                url: "/Admin/FinalizarPedido",
                data: dt,
                dataType: 'json',
                success: function (data) {

                    $.unblockUI({});

                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Pedido finalizado com sucesso',
                        showConfirmButton: false,
                        timer: 2500
                    })

                    localStorage.clear();

                    window.location.reload();


                },
                error: function (data) {
                    $.unblockUI({});

                    var dt = JSON.stringify(data.responseJSON[0]._msg);
                 
                    console.log('result erro estoque', dt);

                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Produtos sem estoque: \n'+dt,
                        showConfirmButton: false,
                        timer: 4000
                    })
                   
                  
                }
            });

        }
        else {

            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'O Carrinho está vazio!',
                showConfirmButton: false,
                timer: 2500
            })
        } 
    }
    else {

        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'O Carrinho está vazio!',
            showConfirmButton: false,
            timer: 2500
        })
    } 
          


    
   

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
