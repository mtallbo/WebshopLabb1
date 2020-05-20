// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const ADDTOCART_URL = 'https://localhost:44318/Cart/Add?productId='
const REMOVEFROMCART_URL = 'https://localhost:44318/Cart/Remove?productId='

function AddToCart(productid) {
    fetch(ADDTOCART_URL + productid)
        .then(function(res) {
            location.reload()
        })
};

function RemoveFromCart(productid) {
    fetch(REMOVEFROMCART_URL + productid)
        .then(function (res) {
            location.reload()
    })
};
