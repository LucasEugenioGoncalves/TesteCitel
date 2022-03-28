



function CadastrarCliente() {

    event.preventDefault();

    $('#modalCadastro').modal('hide');

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

    var nomeCliente = $("#nomeCliente").val();
    var username = $("#usuario").val();
    var password = $("#senha").val();
    var confpassword = $("#confpassword").val();


    var cliente = {
        'nomeCliente': nomeCliente,
        'Usuario': username,
        'Senha': password,
        'ConfirmarSenha': confpassword,
   
    };

    var dataJson = JSON.stringify(cliente);

    console.log(dataJson);

    $.ajax({
        type: 'POST',
        url: '/User/CadastrarCliente',
        data: JSON.stringify(cliente),
        contentType: "application/json",
        dataType: 'html',
        success: function (data) {
           
            $.unblockUI({});

            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cliente cadastrado com sucesso',
                showConfirmButton: false,
                timer: 2500
            })

            LimparCamposCadCliente();

        },
        error: function (data) {
            $.unblockUI({});

            console.log('erro --->',data);

            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: data.responseText,
                showConfirmButton: false,
                timer: 3800
            })
        }
    });
}

function validarLogin()
{
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

    var username = $("#username").val();
    var password = $("#password").val();
 
    var cliente = {
        'Usuario': username,
        'Senha': password
    };

    $.ajax({
        type: 'POST',
        url: '/User/ValidarLogin',
        data: JSON.stringify(cliente),
        contentType: "application/json",
        dataType: 'html',
        success: function (data) {

            $.unblockUI({});

            window.location.href = "/User/MinhaConta";
        
        },
        error: function (data) {
            $.unblockUI({});

            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Usuário ou senha inválidos.',
                showConfirmButton: false,
                timer: 3800
            })
        }
    });
}

function LimparCamposCadCliente()
{
    $("#nomeCliente").val("");
    $("#usuario").val("");
    $("#senha").val("");
    $("#confpassword").val("");
}