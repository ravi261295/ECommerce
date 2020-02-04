

$(document).ready(function () {

    var localStorageEmail;
    var localStorageAddress;
    var bearer = "";
    var totalAmount = 0;
    var estDeliveryTime = 0;

    $('#addModelButton').click(function(event){
        event.preventDefault();
        $.ajax({
            url: 'http://localhost:55206/api/PurchaseProduct',
            type: 'POST',
            headers: {
                "Authorization": 'bearer ' + localStorage.getItem("bearer")
            },
            data: {
                modelNumber: $('#modelNumber').val(),
                description: $('#description').val(),
                price: $('#price').val(),
                availableQuantity: $('#availableQuantity').val(),
                requiredQuantity: $('#requiredQuantity').val(),
                deliveryTime: $('#deliveryTime').val(),
                email: localStorage.getItem("UserEmail")
            },
            success: function (data, textStatus, jqXHR) {
                //console.log(jqXHR.responseJSON);
                //console.log(textStatus);
                $('#myTable tbody').append("<tr><td>" + jqXHR.responseJSON.modelNumber + "</td><td>" + jqXHR.responseJSON.requiredQuantity + "</td><td>"
                    + jqXHR.responseJSON.price + "</td><td>" + jqXHR.responseJSON.description + "</td></tr>");
            },
            error: function (jqXHR) {
                $('.requiredQuantity').removeClass("hidden");
                $('#requiredQuantityWrong').html(jqXHR.responseJSON.message);
                //console.log("Error : ", jqXHR);
            }
        });
    });

    var LoginModel = Backbone.Model.extend({
        //url: 'http://localhost:55206/api/Login'
    });

    var loginModel = new LoginModel({
        email: $('#loginEmail').val(),
        password: $('#loginPassword').val()
    });

    var LoginView = Backbone.View.extend({
        el: '#divView',

        template: _.template($('#loginTemplate').html()),

        model: loginModel,

        events: {
            'click #loginSubmit': 'loginClicked',
            'click #btnReset': 'resetLoginForm',
            'click #loginToRegister': 'redirectToRegister'
        },

        initialize: function () {
            console.log("On Login View");
            this.render();
        },

        render: function () {
            this.$el.html(this.template);
            return this;
        },

        loginClicked: function (event) {

            event.preventDefault();

            formData = {
                email: $('#loginEmail').val(),
                password: $('#loginPassword').val()
            };

            $.ajax({
                type: "POST",
                url: "http://localhost:55206/token",
                data: {
                    grant_type: 'password',
                    username: $('#loginEmail').val(),
                    password: $('#loginPassword').val()
                },
                contentType: "application/x-www-form-urlencoded",
                dataType: "json",
                success: function (data) {
                    bearer = JSON.parse(JSON.stringify(data));
                    //console.log("Bearer token : ", bearer);
                    bearer = bearer.access_token;
                    localStorage.setItem("bearer", bearer);
                }
            });

            $.ajax({
                url: 'http://localhost:55206/api/Login',
                
                type: 'POST',

                data: formData,

                success: function (data, textStatus, jqXHR) {
                    //console.log(textStatus);
                    //console.dir(jqXHR);
                    //console.log(jqXHR.responseJSON.email);
                    localStorage.setItem("UserEmail", jqXHR.responseJSON.email);
                    //localStorageEmail = localStorage.getItem("UserEmail");
                    //console.log(localStorageEmail);
                    //console.log("LocalStorage Email : ", localStorage.getItem("UserEmail"));
                    window.location.href = 'http://localhost:55206/Backbonejs/html/index.html#dashboard';    
                },

                error: function (jqXHR) {
                    $('.hidden').removeClass("hidden");
                    $('.alert-danger').html(jqXHR.responseJSON.message);
                    //console.log("Error : ", jqXHR);    
                }
            });
        },

        redirectToRegister: function () {
            window.location.replace('http://localhost:55206/Backbonejs/Html/index.html#register');
        },

        resetLoginForm: function () {
            $('#loginEmail').val('');
            $('#loginPassword').val('');
        }
    });

    var RegisterModel = Backbone.Model.extend({
        url: 'http://localhost:55206/api/Register'
    });

    var registerModel = new RegisterModel();

    var RegisterView = Backbone.View.extend({

        el: '#divView',

        model: registerModel,

        template: _.template($('#registerTemplate').html()),

        events: {
            'click #registerSubmit': 'registerClicked',
            'click #btnResetRegister': 'resetRegisterForm',
            'click #registerToLogin': 'redirectToLogin'
        },

        initialize: function () {
            this.render();
        },

        render: function () {
            this.$el.html(this.template);
            return this;
        },

        registerClicked: function (event) {

            $.ajax({
                url: 'http://localhost:55206/api/Register',
                type: 'POST',
                data: {
                    fullname: $('#registerFullName').val(),
                    email: $('#registerEmail').val(),
                    password: $('#registerPassword').val()
                },
                success: function (data, textStatus, jqXHR) {
                    console.log("Registration successful : ", jqXHR);
                    $('.registrationAlert').removeClass("hidden");
                    $('#registrationWrong').html(jqXHR.responseJSON);
                },
                error: function (jqXHR) {
                    $('.registrationAlert').removeClass("hidden");
                    $('#registrationWrong').html(jqXHR.responseJSON.message);
                    console.log("Register Error : ", jqXHR);
                }
            });
        },

        redirectToLogin: function () {
            window.location.replace('http://localhost:55206/Backbonejs/html/index.html#');
        },

        resetRegisterForm: function () {
            $('#registerFullName').val('');
            $('#registerEmail').val('');
            $('#registerPassword').val('');
        }
    });

    DashboardView = Backbone.View.extend({
        el: '#divView',

        template: _.template($('#dashboardTemplate').html()),

        initialize: function () {
            this.address();
            this.RenderTableData();
            this.render();
        },

        events: {
            'click #logout': 'logout',
            'click #searchButton': 'search',
            'click #placeOrderButton': 'placeOrder'
        },

        render: function () {
            //console.log("Total Amount in render dashboard : ", localStorage.getItem("TotalAmount"));
            //console.log("Est Time in render dashboard : ", localStorage.getItem("EstDeliveryTime"));
            if (window.localStorage.getItem("bearer") != null) {
                this.$el.html(this.template({ "email": localStorage.getItem("UserEmail"), "address": localStorage.getItem("address"), "sumTotal": localStorage.getItem("TotalAmount"), "estDeliveryTime": localStorage.getItem("EstDeliveryTime") }));
                return this;
            }
            else {
                window.location.replace('http://localhost:55206/Backbonejs/html/index.html#');
            }
        },

        search: function () {
            $.ajax({
                url: 'http://localhost:55206/api/GetModel',
                type: 'POST',
                headers: {
                    "Authorization": 'bearer ' + localStorage.getItem("bearer")
                },
                data: {
                    modelNumber: $('#searchModelNumber').val()
                },
                success: function (data, textStatus, jqXHR) {
                    //console.log(jqXHR.responseJSON);
                    //console.log(textStatus);
                    $('#modelNumber').val(jqXHR.responseJSON.modelNumber);
                    $('#description').val(jqXHR.responseJSON.description);
                    $('#price').val(jqXHR.responseJSON.price);
                    $('#availableQuantity').val(jqXHR.responseJSON.availableQuantity);
                    $('#requiredQuantity').val(jqXHR.responseJSON.requiredQuantity);
                    $('#deliveryTime').val(jqXHR.responseJSON.deliveryTime);
                },
                error: function (jqXHR) {
                    $('.modelAlert').removeClass("hidden");
                    $('#modelNumberWrong').html(jqXHR.responseJSON.message);
                    console.log("Error : ", jqXHR);
                }
            });
        },

        address: function () {
            $.ajax({
                url: 'http://localhost:55206/api/UserAddress',
                type: 'POST',
                headers: {
                    "Authorization": 'bearer ' + localStorage.getItem("bearer")
                },
                data: {
                    email: localStorage.getItem("UserEmail"),
                    address: ""
                },
                success: function (data, textStatus, jqXHR) {
                    console.log(jqXHR);
                    localStorage.setItem("address", jqXHR.responseJSON.address);
                    //localStorageAddress = localStorage.getItem("address");
                    //console.log("Address in local storage : ", localStorageAddress);
                },
                error: function (jqXHR) {
                    console.log("Error : ", jqXHR);
                }
            });
        },

        RenderTableData: function () {
            $.ajax({
                url: 'http://localhost:55206/api/UserTableList',
                type: 'POST',
                headers: {
                    "Authorization": 'bearer ' + localStorage.getItem("bearer")
                },
                data: {
                    email: localStorage.getItem("UserEmail"),
                    modelNumber: "",
                    requiredQuantity: 0,
                    price: 0,
                    description: "",
                    deliveryTime: 0
                },
                success: function (data, textStatus, jqXHR) {
                    console.log("Table : ", jqXHR);
                    if (data) {
                        var len = data.length;
                        var txt = "";
                        if (len > 0) {
                            for (var i = 0; i < len; i++) {
                                if (data[i].modelNumber && data[i].requiredQuantity && data[i].price && data[i].description) {
                                    txt += "<tr><td>" + data[i].modelNumber + "</td><td>" + data[i].requiredQuantity + "</td><td>"
                                        + data[i].price + "</td><td>" + data[i].description + "</td></tr>";
                                }

                                if (estDeliveryTime < data[i].deliveryTime) {
                                    estDeliveryTime = data[i].deliveryTime;
                                    //console.log("EstDeliveryTime : ", estDeliveryTime);
                                    localStorage.setItem("EstDeliveryTime", estDeliveryTime);
                                }

                                totalAmount += data[i].price * data[i].requiredQuantity;
                                localStorage.setItem("TotalAmount", totalAmount);
                                //console.log("Total Amount : ", totalAmount);
                            }

                            if (txt != "") {
                                $("#myTable tbody").append(txt);
                            }
                        }
                    }
                },
                error: function (jqXHR) {
                    $("#myTable tbody").append(`<h2>No item is purchased </h2>`);
                    //console.log("Error : ", jqXHR);
                }
            });
        },

        placeOrder: function () {
            $.ajax({
                url: 'http://localhost:55206/api/PlaceOrder',
                type: 'POST',
                headers: {
                    "Authorization": 'bearer ' + localStorage.getItem("bearer")
                },
                data: {
                    email : localStorage.getItem("UserEmail")
                },
                success: function (data, textStatus, jqXHR) {
                    $('.placeOrderTitle').html("Order Status");
                    $('#modalBody').html(jqXHR.responseJSON);
                    //console.log("Place order Successfully : ", jqXHR);
                },
                error: function (jqXHR) {
                    $('.placeOrderTitle').html("Error");
                    $('#modalBody').html("Login Again");
                    //console.log("Place order Error : ", jqXHR);
                }
            });
        },

        logout: function () {
            localStorage.removeItem('UserEmail');
            localStorage.removeItem('address');
            localStorage.removeItem('bearer');
            localStorage.removeItem('EstDeliveryTime');
            localStorage.removeItem('TotalAmount');
            window.location.href = 'http://localhost:55206/Backbonejs/html/index.html#';
        }
    });

    var Routing = Backbone.Router.extend({
        routes: {
            "": 'login',
            "register": 'register',
            "dashboard": 'dashboard'
        },

        login: function () {
            var loginView = new LoginView();
            loginView.render()
        },

        register: function () {
            var registerView = new RegisterView();
            registerView.render();
        },

        dashboard: function () {
            var dashboardView = new DashboardView();
            dashboardView.render();
        }
    });

    var routing = new Routing();

    Backbone.history.start();
});